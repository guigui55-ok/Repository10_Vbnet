Public Class FormExcelFormatChanger

    Public _logger As AppLogger
    Public _formLog As FormLog
    Public _mainProc As ExcelFormatChangerProc
    Private _dgvAddRemoveUiSrc As DataGridView_AddRemove

    Private _dgvSyncer As DataGridViewSyncer

    Private _dgvSaverSrc As DataGridViewSaver
    Private _dgvSaverDest As DataGridViewSaver

    Private _dgvSaver As DataGridViewSaver

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
        _dgvSyncer = New DataGridViewSyncer(DataGridView_Conditions, DataGridView_DestCondition)

        'saver src
        _dgvSaverSrc = New DataGridViewSaver
        Dim csvPathSrc = Application.StartupPath + "\" + "SettingSrc.csv"
        Dim textPathSrc = Application.StartupPath + "\" + "SettingExcelPathSrc.txt"
        _dgvSaverSrc.Init(DataGridViewSaver.EnumSaveMode.CSV, Ui.tableSrc, csvPathSrc, textPathSrc)

        'saver dest
        _dgvSaverDest = New DataGridViewSaver
        Dim csvPathDest = Application.StartupPath + "\" + "SettingDest.csv"
        Dim textPathDest = Application.StartupPath + "\" + "SettingExcelPathDest.txt"
        _dgvSaverDest.Init(DataGridViewSaver.EnumSaveMode.CSV, Ui.tableDest, csvPathDest, textPathDest)

    End Sub
    Private Sub FormExcelFormatChanger_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        '最初にRowを調整する
        SetNoNoDataGridViewBoth(DataGridView_Conditions, DataGridView_DestCondition, 0)
        _dgvAddRemoveUiSrc.EndEditAll()
        LoadCsv()
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
            dgv.Rows.Add(bufData)

            'dest
            dgv = DataGridView_DestCondition
            Dim destData = _dataPair.DestItem
            bufData = destData.GetDataArray(count)
            'dgv.Rows.Add(bufData) 'DataGridViewの１と２は、行追加・削除時に行数が同期されるのでaddは不要
            SetChangeFormatDataToDataGridView(dgv, bufData)

            count += 1
        Next
    End Sub

    Private Function GetAddDataToDataPairManager(_data As ChangeFormatDataPairManager.ChangeFormatData)
        'Formで実行ボタンを押したときに実行される
        'DataGridViewからGetする
        GetDataDataGridView_DataPair()
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
    End Function
    Public Function GetDataDataGridViewRow(rowIndex As Integer) As ChangeFormatDataPairManager.DataPair
        Dim aryA() = DataGridViewAnyUtil.GetRowValues(DataGridView_Conditions, rowIndex)
        Dim itemSrc = New ChangeFormatDataPairManager.ChangeFormatData()
        itemSrc.SetData(aryA)

        Dim aryB() = DataGridViewAnyUtil.GetRowValues(DataGridView_DestCondition, rowIndex)
        Dim itemDest = New ChangeFormatDataPairManager.ChangeFormatData()
        itemSrc.SetData(aryB)

        Dim _dataPair = New ChangeFormatDataPairManager.DataPair()
        _dataPair.SrcItem = itemSrc
        _dataPair.DestItem = itemDest
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

#Region ""

    Private Sub Button_ShowDetails_Click(sender As Object, e As EventArgs) Handles Button_ShowDetails.Click

    End Sub

    Private Sub Button_Execute_Click(sender As Object, e As EventArgs) Handles Button_Execute.Click
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
        _dgvSaverSrc.SaveMain(_mainProc._dataPairManager._srcFilePath)
        _dgvSaverDest.SaveMain(_mainProc._dataPairManager._destFilePath)

        _logger.Info("SaveCsv srcPath = " + _dgvSaverSrc._filePathCsv)
        _logger.Info("SaveCsv destPath = " + _dgvSaverDest._filePathCsv)
    End Sub

    Private Sub LoadCsv()
        _dgvSaverSrc.LoadMain()
        _dgvSaverDest.LoadMain()
        TextBox_SrcFilePath.Text = _dgvSaverSrc._filePathExcel
        TextBox_DestFilePath.Text = _dgvSaverDest._filePathExcel
        _logger.Info("LoadCsv Done.")
    End Sub
#End Region
End Class
