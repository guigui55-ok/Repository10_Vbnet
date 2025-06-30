Public Class FormExcelFormatChanger

    Public _logger As AppLogger
    Public _formLog As FormLog
    Public _mainProc As ExcelFormatChangerProc
    Private _dgvAddRemoveUiSrc As DataGridView_AddRemove

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

        _logger = logger
        Ui.tableSrc = DataGridView_Conditions
        SetupSearchColumns(Ui.tableSrc)
        'Ui.tableSrc.Rows.Add()

        Ui.tableDest = DataGridView_DestCondition
        SetupSearchColumns(Ui.tableDest)
        'Ui.tableDest.Rows.Add()

        _dgvAddRemoveUiSrc = New DataGridView_AddRemove(_logger, DataGridView_Conditions)
        _dgvAddRemoveUiSrc.SetButton(Button_AddRowSrc, Button_RemoveRowSrc)
    End Sub
    Private Sub FormExcelFormatChanger_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        _dgvAddRemoveUiSrc.EndEditAll()
    End Sub

    Private Sub FormExcelFormatChanger_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetData(_mainProc._dataPairManager)
    End Sub

    Public Sub SetData(dataManager As ChangeFormatDataPairManager)
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
            dgv.Rows.Add(bufData)

            count += 1
        Next
    End Sub

    Private Function GetAddData(_data As ChangeFormatDataPairManager.ChangeFormatData)

    End Function


#Region "DataGridView"
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

    Private Sub Button_ShowDetails_Click(sender As Object, e As EventArgs) Handles Button_ShowDetails.Click

    End Sub

    Private Sub Button_Execute_Click(sender As Object, e As EventArgs) Handles Button_Execute.Click
        _mainProc.ExecuteChangeFormat()
    End Sub

    Private Sub Button_AddRowSrc_Click(sender As Object, e As EventArgs) Handles Button_AddRowSrc.Click

    End Sub

    Private Sub Button_RemoveRowSrc_Click(sender As Object, e As EventArgs) Handles Button_RemoveRowSrc.Click

    End Sub


#End Region
End Class
