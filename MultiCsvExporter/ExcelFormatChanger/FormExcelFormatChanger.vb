Public Class FormExcelFormatChanger

    Public _logger As AppLogger
    Public _formLog As FormLog
    Public _mainProc As ExcelFormatChangerProc
    Private _dgvAddRemoveUiSrc As DataGridView_AddRemove

    Private _dgvSyncer As DataGridViewSyncer

    Private _dgvSaverSrc As DataGridViewSaver
    Private _dgvSaverDest As DataGridViewSaver

    Private _dgvSaver As DataGridViewSaver

    Private _dragDropManager As DragDropManager_FormExcelFormatChanger

    Public Class Ui
        Public Shared tableSrc As DataGridView
        Public Shared tableDest As DataGridView
    End Class

    Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub

    Sub New(ByRef logger As AppLogger)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        'init
        _logger = logger
        Me.AllowDrop = True

        'drag drop
        _dragDropManager = New DragDropManager_FormExcelFormatChanger(_logger, Me)

        'dgv src
        Ui.tableSrc = DataGridView_Conditions
        SetupSearchColumns(Ui.tableSrc)
        Ui.tableSrc.EditMode = DataGridViewEditMode.EditOnEnter

        'dgv dest
        Ui.tableDest = DataGridView_DestCondition
        SetupSearchColumns(Ui.tableDest)
        Ui.tableDest.EditMode = DataGridViewEditMode.EditOnEnter

        'dgv event1
        _dgvAddRemoveUiSrc = New DataGridView_AddRemove(_logger, DataGridView_Conditions)
        _dgvAddRemoveUiSrc.SetButton(Button_AddRowSrc, Button_RemoveRowSrc)
        AddHandler _dgvAddRemoveUiSrc.AddedRowEvent, AddressOf ChangeRowAmountEvent

        'dgv event2
        DataGridView_Conditions.AllowUserToAddRows = False
        DataGridView_DestCondition.AllowUserToAddRows = False

        'saver src
        _dgvSaverSrc = New DataGridViewSaver
        Dim csvPathSrc = Application.StartupPath + "\" + "SettingSrc.csv"
        Dim textPathSrc = Application.StartupPath + "\" + "SettingExcelPathSrc.txt"
        _dgvSaverSrc.Init(_logger, DataGridViewSaver.EnumSaveMode.CSV, Ui.tableSrc, csvPathSrc, textPathSrc)

        'saver dest
        _dgvSaverDest = New DataGridViewSaver
        Dim csvPathDest = Application.StartupPath + "\" + "SettingDest.csv"
        Dim textPathDest = Application.StartupPath + "\" + "SettingExcelPathDest.txt"
        _dgvSaverDest.Init(_logger, DataGridViewSaver.EnumSaveMode.CSV, Ui.tableDest, csvPathDest, textPathDest)

    End Sub
    Private Sub FormExcelFormatChanger_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        '最初にRowを調整する
        SetNoNoDataGridViewBoth(DataGridView_Conditions, DataGridView_DestCondition, 0)
        _dgvAddRemoveUiSrc.EndEditAll()

        'Csvからデータ追加
        LoadCsv()

        '★以下のSyncerクラスは、上記データをセットした後でなければ、片方を上書きしてしまう為注意★
        _dgvSyncer = New DataGridViewSyncer(DataGridView_Conditions, DataGridView_DestCondition)
    End Sub

    Private Sub FormExcelFormatChanger_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetData(_mainProc._dataPairManager)
    End Sub



    Public Sub SetData(dataManager As ChangeFormatDataPairManager)
        'path
        TextBox_SrcFilePath.Text = _mainProc._dataPairManager._srcFilePath
        TextBox_DestFilePath.Text = _mainProc._dataPairManager._destFilePath

        'DataGridViewにセットする
        Dim count = 0
        For Each _dataPair In dataManager._dataPairList
            Dim bufData = Nothing
            Dim dgv = Nothing
            Dim addrow = Nothing

            'src
            dgv = DataGridView_Conditions
            Dim srcData = _dataPair.SrcItem
            bufData = srcData.GetDataArray(count)
            bufData = ConvertRowToDataGridViewTypes(dgv, bufData)
            dgv.Rows.Add(bufData)

            'dest
            dgv = DataGridView_DestCondition
            Dim destData = _dataPair.DestItem
            bufData = destData.GetDataArray(count)
            bufData = ConvertRowToDataGridViewTypes(dgv, bufData)
            'dgv.Rows.Add(bufData) 'DataGridViewの１と２は、行追加・削除時に行数が同期されるのでaddは不要
            SetChangeFormatDataToDataGridView(dgv, bufData)

            count += 1
        Next
    End Sub
    ''' <summary>
    ''' DataGridView のカラム型に基づいて bufData の型を変換した配列を返します
    ''' </summary>
    ''' <param name="dgv">対象の DataGridView</param>
    ''' <param name="bufData">元のデータ配列</param>
    ''' <returns>変換後のデータ配列</returns>
    Private Function ConvertRowToDataGridViewTypes(dgv As DataGridView, bufData() As Object) As Object()
        Dim convertedData(dgv.Columns.Count - 1) As Object

        For i As Integer = 0 To dgv.Columns.Count - 1
            Dim colType As Type = dgv.Columns(i).ValueType
            Debug.WriteLine("colType = " + colType.ToString)
            If i < bufData.Length Then
                Dim value = bufData(i)
                If value Is Nothing OrElse IsDBNull(value) Then
                    convertedData(i) = DBNull.Value
                Else
                    Try
                        convertedData(i) = Convert.ChangeType(value, colType)
                    Catch ex As Exception
                        convertedData(i) = value ' 変換できなければ元の値を保持
                    End Try
                End If
            Else
                convertedData(i) = DBNull.Value
            End If
        Next

        Return convertedData
    End Function

    Private Function GetAddDataToDataPairManager(_data As ChangeFormatDataPairManager.ChangeFormatData)
        'Formで実行ボタンを押したときに実行される
        'DataGridViewからGetする
        GetDataDataGridView_DataPair()
        _mainProc._dataPairManager._srcFilePath = TextBox_SrcFilePath.Text
        _mainProc._dataPairManager._destFilePath = TextBox_DestFilePath.Text
        Return Nothing
    End Function


    ''' <summary>
    ''' 指定インデックスの要素を削除した新しい配列を返します
    ''' </summary>
    ''' <typeparam name="T">任意の型</typeparam>
    ''' <param name="sourceArray">元の配列</param>
    ''' <param name="indexToRemove">削除したい要素のインデックス</param>
    ''' <returns>指定要素を削除した新しい配列</returns>
    Public Function RemoveAtIndex(Of T)(sourceArray As T(), indexToRemove As Integer) As T()
        If sourceArray Is Nothing Then Throw New ArgumentNullException(NameOf(sourceArray))
        If indexToRemove < 0 OrElse indexToRemove >= sourceArray.Length Then Throw New ArgumentOutOfRangeException(NameOf(indexToRemove))

        Dim newArray(sourceArray.Length - 2) As T ' 1つ要素が少ない配列を作成
        Dim newIndex As Integer = 0

        For i As Integer = 0 To sourceArray.Length - 1
            If i = indexToRemove Then Continue For
            newArray(newIndex) = sourceArray(i)
            newIndex += 1
        Next

        Return newArray
    End Function

#Region "DataGridView"

    Public Sub SetChangeFormatDataToDataGridView(dgv As DataGridView, dataAry() As Object)
        DataGridViewAnyUtil.SetRowValuesFromArray(dgv, dgv.Rows.Count - 1, dataAry, 0)
    End Sub

    Public Function GetDataDataGridView_DataPair()
        _mainProc._dataPairManager._dataPairList.Clear()
        For i = 0 To DataGridView_Conditions.Rows.Count - 1
            Dim _dataPair = GetDataDataGridViewRow(i)
            _mainProc._dataPairManager._dataPairList.Add(_dataPair)
        Next
        Return Nothing
    End Function


    Public Function GetDataDataGridViewRow(rowIndex As Integer) As ChangeFormatDataPairManager.DataPair

        'ary src
        Dim arySrc() = DataGridViewAnyUtil.GetRowValues(DataGridView_Conditions, rowIndex)
        arySrc = RemoveAtIndex(arySrc, 0) '最初の"No"カラムは除外
        '_logger.Info($"[{rowIndex}]arySrc = {String.Join(" ,", arySrc)}")

        Dim itemSrc = New ChangeFormatDataPairManager.ChangeFormatData()
        itemSrc.SetData(arySrc)

        'ary dest
        Dim aryDest() = DataGridViewAnyUtil.GetRowValues(DataGridView_DestCondition, rowIndex)
        aryDest = RemoveAtIndex(aryDest, 0) '最初の"No"カラムは除外
        '_logger.Info($"[{rowIndex}]aryDest = {String.Join(" ,", aryDest)}")

        Dim itemDest = New ChangeFormatDataPairManager.ChangeFormatData()
        itemDest.SetData(aryDest)

        Dim _dataPair = New ChangeFormatDataPairManager.DataPair()
        _dataPair.SrcItem = itemSrc
        _dataPair.DestItem = itemDest

        If Not _dataPair.SrcItem.DataIsValid() Then
            _dataPair.SrcItem = itemDest.GetDeepCopyObject()
        Else
            'pass
        End If

        _logger.Info($"[{rowIndex}]DestItem = {String.Join(" ,", itemDest.GetDataArray(rowIndex))}")
        _logger.Info($"[{rowIndex}]SrcItem = {String.Join(" ,", itemSrc.GetDataArray(rowIndex))}")

        Return _dataPair
    End Function

    ''' <summary>
    ''' 指定の DataGridView に検索用カラムを設定する
    ''' </summary>
    ''' <param name="dgv">対象の DataGridView</param>
    Public Sub SetupSearchColumns(dgv As DataGridView)
        dgv.Columns.Clear()

        dgv.Columns.Add(CreateTextColumn("No", "No", GetType(Integer)))
        ' 文字列列
        dgv.Columns.Add(CreateTextColumn("Sheet", "検索" & vbLf & "Sheet"))
        dgv.Columns.Add(CreateTextColumn("Range", "検索" & vbLf & "Range"))
        dgv.Columns.Add(CreateTextColumn("Value", "検索値"))
        dgv.Columns.Add(CreateTextColumn("Mode", "検索" & vbLf & "Mode", GetType(Integer)))

        ' 整数列（オフセット）
        dgv.Columns.Add(CreateTextColumn("OffsetRow", "Offset" & vbLf & "Row", GetType(Integer)))
        dgv.Columns.Add(CreateTextColumn("OffsetCol", "Offset" & vbLf & "Col", GetType(Integer)))

        ' チェックボックス列（全体行・列）
        dgv.Columns.Add(CreateCheckBoxColumn("EntireRow", "Entire" & vbLf & "Row"))
        dgv.Columns.Add(CreateCheckBoxColumn("EntireCol", "Entire" & vbLf & "Col"))

        ' 行列数列
        dgv.Columns.Add(CreateTextColumn("RowCount", "Row" & vbLf & "Count", GetType(Integer)))
        dgv.Columns.Add(CreateTextColumn("ColCount", "Col" & vbLf & "Count", GetType(Integer)))

        'fontSize
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font(dgv.Font.FontFamily, 8)

        '幅設定
        dgv.Columns("No").Width = 30
        dgv.Columns("Range").Width = 70
        Dim widthShortBase = 45
        dgv.Columns("OffsetRow").Width = widthShortBase
        dgv.Columns("OffsetCol").Width = widthShortBase
        dgv.Columns("EntireRow").Width = widthShortBase
        dgv.Columns("EntireCol").Width = widthShortBase
        dgv.Columns("RowCount").Width = widthShortBase
        dgv.Columns("ColCount").Width = widthShortBase

        'log
        _logger.Info($"dgv.Width = {dgv.Width}") '※これ＋40を親フォーム.widthにすると大体ちょうどよい
    End Sub

    Public Sub SetNoNoDataGridView(dgv As DataGridView, colIndex As Integer, Optional startNum As Integer = 1)
        Dim count = 0
        For Each row As DataGridViewRow In dgv.Rows
            row.Cells(colIndex).Value = (startNum + count).ToString
            count += 1
        Next
    End Sub

    Public Sub SetNoNoDataGridViewBoth(dgvA As DataGridView, dgvB As DataGridView, colIndex As Integer, Optional startNum As Integer = 1)
        SetNoNoDataGridView(dgvA, colIndex, startNum)
        SetNoNoDataGridView(dgvB, colIndex, startNum)
    End Sub

    Public Sub ChangeRowAmountEvent(sender As Object, e As EventArgs)
        SetNoNoDataGridViewBoth(DataGridView_Conditions, DataGridView_DestCondition, 0)
    End Sub

    ''' <summary>
    ''' テキスト列を作成する
    ''' </summary>
    Private Function CreateTextColumn(name As String, headerText As String, Optional valueType As Type = Nothing) As DataGridViewTextBoxColumn
        Dim col As New DataGridViewTextBoxColumn()
        col.Name = name
        col.HeaderText = headerText
        If valueType IsNot Nothing Then
            col.ValueType = valueType
        End If
        Return col
    End Function

    ''' <summary>
    ''' チェックボックス列を作成する
    ''' </summary>
    Private Function CreateCheckBoxColumn(name As String, headerText As String) As DataGridViewCheckBoxColumn
        Dim col As New DataGridViewCheckBoxColumn()
        col.Name = name
        col.HeaderText = headerText
        Return col
    End Function
#End Region

#Region "Events"

    Private Sub Button_ShowDetails_Click(sender As Object, e As EventArgs) Handles Button_ShowDetails.Click

    End Sub

    Private Sub Button_Execute_Click(sender As Object, e As EventArgs) Handles Button_Execute.Click
        GetAddDataToDataPairManager(Nothing)
        _mainProc.ExecuteChangeFormat()
    End Sub

    'Private Sub Button_AddRowSrc_Click(sender As Object, e As EventArgs) Handles Button_AddRowSrc.Click

    'End Sub

    'Private Sub Button_RemoveRowSrc_Click(sender As Object, e As EventArgs) Handles Button_RemoveRowSrc.Click

    'End Sub

    Private Sub Button_Explorer_Click(sender As Object, e As EventArgs) Handles Button_Explorer.Click
        If Not IO.File.Exists(TextBox_DestFilePath.Text) Then Exit Sub
        FileExplorerHelper.OpenInExplorer(TextBox_DestFilePath.Text)
    End Sub

    Private Sub Button_SetTestData_Click(sender As Object, e As EventArgs) Handles Button_SetTestData.Click
        SetTestData(_logger, _mainProc._dataPairManager)
        SetData(_mainProc._dataPairManager)
    End Sub

    Private Sub Button_Save_Click(sender As Object, e As EventArgs) Handles Button_Save.Click
        _logger.Info("SaveInfo (Button_Save_Click)")
        _mainProc._dataPairManager._srcFilePath = TextBox_SrcFilePath.Text
        _mainProc._dataPairManager._destFilePath = TextBox_DestFilePath.Text
        _dgvSaverSrc.SaveMain(_mainProc._dataPairManager._srcFilePath)
        _dgvSaverDest.SaveMain(_mainProc._dataPairManager._destFilePath)
    End Sub

    Private Sub LoadCsv()
        _dgvSaverSrc.LoadMain()
        _dgvSaverDest.LoadMain()
        TextBox_SrcFilePath.Text = _dgvSaverSrc._targetFilePath
        TextBox_DestFilePath.Text = _dgvSaverDest._targetFilePath
        _logger.Info("LoadCsv Done.")
    End Sub

    Private Sub FormExcelFormatChanger_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Button_Save_Click(Nothing, EventArgs.Empty)
    End Sub
#End Region
End Class
