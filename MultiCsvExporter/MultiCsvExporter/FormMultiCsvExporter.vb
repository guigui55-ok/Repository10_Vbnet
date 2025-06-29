Imports System.IO
Imports ExcelReaderPrj

Public Class FormMultiCsvExporter

    Public _logger As AppLogger
    Public _formLog As FormLog
    Public _formConditions As FormConditions
    Public _filterCondition As FilterCondition

    Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub

    Sub New(ByRef logger As AppLogger, ByRef filterCondition As FilterCondition)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        _logger = logger
        _formConditions = New FormConditions(logger, filterCondition)

    End Sub

    Private Sub FormMultiCsvExporter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetTestData()
    End Sub

    Private Sub SetTestData()
        '※追加機能　予定
        '出力形式はCsvかExcelか 済
        '貼り付け順番は、ファイル種別ごとか、フォルダごとか（未）
        '縦書きか、横書きか（保留）
        'DragAndDrop
        '設定保存機能（My.Setting）
        '引数を読み込んで、Formなしで処理をする（未）

        TextBox_OutputDirPath.Text = Application.StartupPath + "\" + "Output"
        TextBox_OutputFileName.Text = "Export.csv"
        TextBox_OutputFileName.Text = "Export.xlsx"

        Dim dirPath = Path.GetDirectoryName(Application.StartupPath)
        dirPath = Path.GetDirectoryName(dirPath)

        RichTextBox_DirPaths.AppendText(dirPath + "\" + "TestData\folder01" + vbCrLf)
        RichTextBox_DirPaths.AppendText(dirPath + "\" + "TestData\folder02" + vbCrLf)

        'RichTextBox_RegexPatterns.AppendText(".*TableA*.csv$" + vbCrLf)
        'RichTextBox_RegexPatterns.AppendText(".*TableB*.csv$" + vbCrLf)
        RichTextBox_RegexPatterns.AppendText("*TableA*.csv" + vbCrLf)
        RichTextBox_RegexPatterns.AppendText("*TableB*.csv" + vbCrLf)
    End Sub

    Private Sub LogoutEventExternal(sender As Object, e As EventArgs)
        Dim logmsg = DirectCast(sender, String)
        _logger.Info(logmsg)
    End Sub

    Private Sub ShowError(errMsg As String)
        'とりあえずログに出すだけ
        _logger.Info(errMsg)
    End Sub

#Region "MainProcess Relation"
    Private Sub Button_Execute_Click(sender As Object, e As EventArgs) Handles Button_Execute.Click
        _logger.Info("*")
        _logger.Info("*Button_Execute_Click")

        'フォルダリストをすべて読み込み
        'ファイル名を探す
        Dim _fileFinder = New FileFInder(_logger)
        _fileFinder.SetValues(RichTextBox_DirPaths.Text, RichTextBox_RegexPatterns.Text)
        If Not _fileFinder.IsExistsPathAll() Then
            ShowError("IsExists NotFound DirPath ,Exit")
            Exit Sub
        End If
        _fileFinder.SetFilePaths()

        Dim wDir = TextBox_OutputDirPath.Text
        Dim wFilename = TextBox_OutputFileName.Text

        ExecuteMain(_logger, _filterCondition, _fileFinder._filePathList.ToArray(), wDir, wFilename)
    End Sub

    ''' <summary>
    ''' 実行部
    ''' （外部からでも呼び出し可能）
    ''' </summary>
    ''' <param name="logger"></param>
    ''' <param name="readFilePaths"></param>
    ''' <returns></returns>
    Public Function ExecuteMain(
            logger As AppLogger,
            filterCondition As FilterCondition,
            readFilePaths() As String,
            wDirPath As String,
            wFilename As String) As Integer

        'CSVファイル読み込み
        'DataSet、DataTableにすべて読み込み
        Dim ds = ReadCsvToDataset(readFilePaths)

        '条件絞り込み（サブウィンドウで）DataGridView＋ボタンで行追加
        Dim filterDict = filterCondition.GetFilterConditions()
        Dim newDs = ExecuteFilter(ds, filterDict)

        '書き込み用DataTableに書き込み（各DataSet、DataTableを1つにまとめる）
        Dim writeDt = GatherDataTable(newDs)

        'エクセルorCSVに書き込み
        Dim wPath = Path.Combine(wDirPath, wFilename)
        _logger.Info($"writePath = {wPath}")
        WriteToFile(writeDt, wPath)

        _logger.Info("File Export Done.")
        Return 1
    End Function

    Private Sub WriteToFile(writeDt As DataTable, wPath As String)
        _logger.Info("*WriteToFile")
        Dim wFilename = IO.Path.GetFileName(wPath)
        If IO.File.Exists(wPath) Then
            _logger.Info("IsAlreadyExists write Path")
            wPath = Renamer.AddNumberIfExists(wPath)
            _logger.Info($"newFilename = {IO.Path.GetFileName(wPath)}")
        End If

        Dim dirInfo = Directory.CreateDirectory(IO.Path.GetDirectoryName(wPath))

        '既存ファイルがあるときは、ファイル名の後ろにカウントアップさせる
        'ファイル名TextBoxが空の時は仮の名前を

        Dim isCsv = True
        If wFilename.ToLower.EndsWith(".csv") Then
            'CSVファイルの場合
            WriteToCsv(writeDt, wPath)
        ElseIf wFilename.Contains(".xl") Then
            'エクセルの場合
            'エクセルファイルがないときは、新規ファイルを作成
            WriteToExcel(writeDt, wPath)
        Else
            _logger.Info($"Invalid FileName [{wFilename}]")
        End If
    End Sub

    Private Sub WriteToCsv(dt As DataTable, filePath As String)
        Dim converter = New DataTableManager()
        Dim wDictList As List(Of Dictionary(Of String, Object)) = converter.ConvertDataTableToDictionaryList(dt)
        Dim writer = New CsvStream()
        writer.WriteCsvDictList(filePath, wDictList, isWriteColumn:=False)
    End Sub

    Private Sub WriteToExcel(dt As DataTable, wFilePath As String)
        _logger.Info("*WriteToExcel")
        Dim writer = New ExcelReaderWriter()
        AddHandler writer.LogoutEvent, AddressOf LogoutEventExternal
        writer.WriteDataTableToExcel(dt, wFilePath, 1, 1, isWriteColumn:=False)
        RemoveHandler writer.LogoutEvent, AddressOf LogoutEventExternal
    End Sub

    Private Function GatherDataTable(ds As DataSet) As DataTable
        _logger.Info("*GatherDataTable")
        Dim dtMan = New DataTableManager()
        Dim newDt = New DataTable
        Dim nowRow = 10
        Dim nowCol = 0
        For Each dt As DataTable In ds.Tables
            dtMan.SetValueAt(newDt, nowRow, nowCol, "■" + dt.TableName)
            nowRow += 1
            dtMan.CopyDataTableToPosition(newDt, dt, nowRow, nowCol)
            nowRow = dtMan.GetLastRowIndex(newDt) + 2
        Next
        _logger.Info($"newDt Rows.Count = {newDt.Rows.Count}")
        Return newDt
    End Function

    Private Function ExecuteFilter(ds As DataSet, conditions As Dictionary(Of String, Object)) As DataSet
        _logger.Info("*ExecuteFilter")
        Dim newDs = New DataSet
        For Each dt In ds.Tables
            Dim filter As New MultiConditionFilter()
            Dim filteredTable As DataTable = filter.FilterExcludeMatchingDataTable(dt, conditions)
            newDs.Tables.Add(filteredTable)
        Next
        Return newDs
    End Function

    Private Function ReadCsvToDataset(filePaths() As String) As DataSet
        _logger.Info("*ReadCsvToDataset")
        Dim csvReader = New CsvStream()
        Dim ds = New DataSet()
        Dim _dsManager As DataTableManager
        _dsManager = New DataTableManager()
        Dim count = 0
        _logger.Info(" -ReadFile")
        For Each filePath In filePaths
            Dim dt As DataTable
            dt = csvReader.ReadCsvFile(filePath, addColumn:=True)
            _logger.Info($"[{count}] filename={IO.Path.GetFileName(filePath)} , dt.rows.count={dt.Rows.Count}")
            _dsManager.AddDataTableToDataSet(ds, dt)
            count += 1
        Next
        _logger.Info($"Tables.Count = {ds.Tables.Count}")
        Return ds
    End Function
#End Region

    Private Sub Button_Condition_Click(sender As Object, e As EventArgs) Handles Button_Condition.Click
        Me._formConditions.Show()
    End Sub
End Class
