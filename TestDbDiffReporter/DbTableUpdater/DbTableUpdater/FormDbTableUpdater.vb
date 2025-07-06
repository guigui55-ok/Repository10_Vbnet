Imports VbDotNetOracleWrapper


'TODO 　1回登録した後、また、エクセルファイルをD＆Dするとエラーとなる

Public Class FormDbTableUpdater

    Public _logger As AppLogger
    Private _DragDropControl As DragAndDropControl
    Private _DragAndDropFIle As DragAndDropFile

    Private _oracleSetting As OracleSettingReader

    Private _updateTableName As String
    Private _updateDt As DataTable
    Private _oracleManager As OracleDbManager

    Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        'logger
        _logger = New AppLogger()
        AddHandler _logger.AddLogEvent, AddressOf AddLogToControl

        'drag drop
        _DragDropControl = New DragAndDropControl(_logger, GroupBox_TargetData)
        _DragDropControl.AddRecieveControl(DataGridView_TargetData)
        _DragAndDropFIle = New DragAndDropFile(_logger, _DragDropControl)
        _DragDropControl._dragAndDropForFile = _DragAndDropFIle
        AddHandler _DragDropControl.DragAndDropAfterEvent, AddressOf DragDropReadFile

        'oracle 
        _oracleSetting = New OracleSettingReader()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _logger.Info("FormDbTableUpdater Load")

    End Sub

    Public Sub SetOracleManager()
        'setting
        Dim oraSettingPath = "C:\Users\OK\source\repos\test_media_files\vbnet_oracle\oracle_setting.ini"
        _oracleSetting.ReadSettingData(oraSettingPath)
        _logger.Info("OracleSettingValue = " + _oracleSetting.GetSettingStr())

        'connect
        Dim _setting = _oracleSetting._data
        Dim connStr = ""
        connStr = OracleDbManager.CreateConnectionString(
             _setting.User, _setting.Password, _setting.Host, _setting.Port, _setting.Pdb)
        _logger.Info($"connStr = {connStr}" + vbCrLf)
        _oracleManager = New OracleDbManager(connStr, _logger)
    End Sub


    Private Sub ExecuteMain()
        _logger.Info("# ExecuteMain")

        SetOracleManager()

        'set value
        Dim tableName = TextBox_TableName.Text
        Dim updateDt = DataGridViewAnyUtil.CreateDataTableFromDataGridView(DataGridView_TargetData)
        If updateDt.Rows.Count < 1 Then
            _logger.Info("rowsCount < 1 Exit")
            Exit Sub
        End If

        'get pk
        Dim pkList = GetPrimaryKeyList(tableName, updateDt.Rows(0))
        Dim pkListStr = String.Join(", ", pkList)
        _logger.Info("pkList = " + pkListStr)


        SetOracleManager()

        Dim sqlList = New List(Of String)
        For Each row In updateDt.Rows
            Dim sql = ""
            sql = SqlAnyUtilOracle.CreateOracleMergeStatement(tableName, row, pkList)
            sqlList.Add(sql)
        Next

        Dim ds = _oracleManager.ExecuteQueryMulti(sqlList.ToArray)
        OutputDataSet(_logger, ds)

    End Sub

    Private Function IsExistsRecord(ros As DataRow, ByRef rowId As String)

    End Function

    Private Function GetPrimaryKeyList(tableName As String, row As DataRow) As List(Of String)

        Dim sql = SqlAnyUtilOracle.GeneratePrimaryKeySelectSql(_oracleSetting._data.User, tableName, row)
        Dim ds = _oracleManager.ExecuteQueryMulti({sql})

        Dim count = 0
        For Each dt In ds.Tables
            _logger.Info($"tablesCount = {count}")
            OutputDataTable(_logger, dt)
            count += 1
        Next
        Dim pkList = New List(Of String)
        For Each row In ds.Tables(0).Rows
            Dim buf = row("COLUMN_NAME").ToString
            pkList.Add(buf)
        Next
        Return pkList
    End Function

    Private Sub ExecuteSingle(row As DataRow)

    End Sub


    Sub OutputDataSet(logger As AppLogger, ds As DataSet)
        Dim count = 0
        For Each dt In ds.Tables
            logger.Info($"dataTableCount = {count}")
            OutputDataTable(logger, dt)
            count += 1
        Next
    End Sub

    Sub OutputDataTable(logger As AppLogger, dt As DataTable)
        logger.Info($"DataTable.Name = [{dt.TableName}]")
        Dim _retlist = New List(Of String)
        Dim count = 0
        For Each row As DataRow In dt.Rows
            Dim _list = New List(Of String)
            For Each col As DataColumn In dt.Columns
                Dim buf = row(col)
                If buf Is Nothing Or IsDBNull(buf) Then
                    buf = ""
                End If
                buf = $"{col.ColumnName} = {buf}"
                _list.Add(buf)
            Next
            Dim bufLog = String.Join(", ", _list)
            bufLog = $"[{count}]" + bufLog
            _retlist.Add(bufLog)
            logger.Info(bufLog)
            count += 1
        Next
        Dim log = String.Join(vbCrLf, _retlist)
    End Sub

#Region "ReadFile"
    ''' <summary>
    ''' ファイル読み込み（メイン関数）
    ''' </summary>
    ''' <param name="filePath"></param>
    Private Sub GetUpdateDataFromFileMain(filePath As String)
        Dim dt As DataTable
        If IO.Path.GetExtension(filePath) = ".xlsx" Then
            dt = ReadExcel(filePath)
        ElseIf IO.Path.GetExtension(filePath) = ".csv" Then
            dt = ReadCsv(filePath)
        Else
            _logger.Info($"filePath Extension Is Invalid [FileName={IO.Path.GetFileName(filePath)}]")
            Exit Sub
        End If

        'テーブル名をdtから取得
        Dim tableName = GetTableNameFromReadDataTable(dt)
        _logger.Info($"tableName = {tableName}")
        OutputDataTable(_logger, dt)

        'DB更新対象のデータを取得（1行目はカラム名）
        Dim dtB = GetUpdateDataFromReadDataTable(dt)

        '1行目のカラム名をDataTable.columnsに登録して、1行目を削除
        DataTableAnyUtil.UseFirstRowAsHeaderAndRemove(dtB)
        _logger.Info("UseFirstRowAsHeaderAndRemove")
        _logger.Info($"rowsCount = {dtB.Rows.Count}")
        _logger.Info($"colsCount = {dtB.Columns.Count}")

        _updateTableName = tableName
        _updateDt = dtB

        TextBox_TableName.Text = _updateTableName
        DataGridViewAnyUtil.SetDataTableToDataGridView(DataGridView_TargetData, _updateDt)

        ' ① データ読み込み（dtB = Oracleなどから取得したデータ）
        Dim defDt As DataTable = ChangeDataGridViewColumnType(tableName)

        ' ② DataTableの中身（型・値）をすべて変換
        Dim dtC As DataTable = DataTableAnyUtil.ConvertDataTableColumnTypes(defDt, dtB)

        ' ③ DataGridViewにセット（ここでDataTypeも反映される）
        DataGridView_TargetData.DataSource = dtC

        ' ④ DateTimeの表示形式など必要なら設定
        DataGridViewAnyUtil.SetDateTimeFormat(DataGridView_TargetData)

        ' ⑤ 表示調整
        DataGridViewAnyUtil.AutoResizeColumnsWithMaxWidth(DataGridView_TargetData)

        _logger.Info("GetUpdateDataFromFileMain Done.")
    End Sub

    Private Function ChangeDataGridViewColumnType(tableName As String) As DataTable

        SetOracleManager()

        Dim sql = SqlAnyUtilOracle.GenerateColumnInfoSql(tableName)
        Dim ds = _oracleManager.ExecuteQueryMulti({sql})
        OutputDataSet(_logger, ds)

        DataGridViewAnyUtil.SetDataGridViewColumnTypesFromSchema(DataGridView_TargetData, ds.Tables(0))
        Return ds.Tables(0)
    End Function


    Private Function GetTableNameFromReadDataTable(dt As DataTable) As String
        '読み込んだデータの「アドレス：B1」にテーブル名がある想定
        'A1には「テーブル名：」がある
        Dim tableName = DataTableAnyUtil.GetCellValue(dt, 0, 1)
        If tableName IsNot Nothing Then
            Return tableName.ToString
        Else
            Return ""
        End If
    End Function

    Private Function GetUpdateDataFromReadDataTable(dt As DataTable) As DataTable
        '読み込んだデータの「アドレス：A2」から対象のデータがある想定
        '上記の開始アドレスから、右方向と下方向に連続したデータを
        Dim startRow = 1
        Dim StartCol = 0

        'right end
        Dim rightEndPoint = DataTableAnyUtil.FindLastFilledCellInDirection(
            dt, startRow, StartCol, DataTableAnyUtil.ConstDirection.RIGHT)
        If rightEndPoint = Point.Empty Then
            _logger.Info("FindLastFilledCellInDirection Failed(rightEndPoint)")
            Return New DataTable()
        End If
        _logger.Info($"rightEndPoint = {rightEndPoint}")

        'down end
        Dim downEndPoint = DataTableAnyUtil.FindLastFilledCellInDirection(
            dt, startRow, StartCol, DataTableAnyUtil.ConstDirection.DOWN)
        If downEndPoint = Point.Empty Then
            _logger.Info("FindLastFilledCellInDirection Failed(downEndPoint)")
            Return New DataTable()
        End If
        _logger.Info($"downEndPoint = {downEndPoint}")

        Dim newDt = DataTableAnyUtil.ExtractDataTableRange(dt, startRow, StartCol, downEndPoint.Y, rightEndPoint.X)
        _logger.Info($"rowsCount = {newDt.Rows.Count}")
        _logger.Info($"colsCount = {newDt.Columns.Count}")
        OutputDataTable(_logger, newDt)

        Return newDt
    End Function

    Private Function ReadExcel(excelPath As String) As DataTable
        'Dim util As ExcelReaderPrj.ExcelAnyUtil()
        Dim excelrw = New ExcelReaderPrj.ExcelReaderWriter()
        Dim dt As DataTable
        dt = excelrw.ReadExcelFile(excelPath)
        Return dt
    End Function

    Private Function ReadCsv(csvPath As String) As DataTable
        Dim csvSt = New CsvStream()
        Dim dt As DataTable
        dt = csvSt.ReadCsvFile(csvPath, False)
        Return dt
    End Function
#End Region

#Region "DragDrop"

    Private Sub DragDropReadFile(sender As Object, e As EventArgs)
        Try
            _logger.AddLog(Me, " > _SampleDragDropEvent")

            Dim _dragAndDropFile As DragAndDropFile = _DragDropControl._dragAndDropForFile
            If (_dragAndDropFile.FileList Is Nothing) Then
                _logger.PrintInfo("Files == null")
                Return
            End If
            If (_dragAndDropFile.FileList.Count < 1) Then
                _logger.PrintInfo("Files.Length < 1")
                Return
            End If
            Dim targetPath As String = _dragAndDropFile.FileList(0)
            _logger.PrintInfo(String.Format("targetPath={0}", targetPath))
            If (IO.Directory.Exists(targetPath)) Then
                _logger.PrintInfo("Path Is Directory")
                Return
            End If
            GetUpdateDataFromFileMain(targetPath)
        Catch ex As Exception
            _logger.AddException(ex, Me, "DragDropReadFile")
        End Try
    End Sub

#End Region

    Public Sub AddLogToControl(sender As Object, e As EventArgs)
        Dim logMsg = DirectCast(sender, String)
        If RichTextBox_Log.Text.Length > 0 Then logMsg = vbCrLf + logMsg
        RichTextBox_Log.AppendText(logMsg)
        RichTextBox_Log.ScrollToCaret()
    End Sub

    Private Sub Button_Execute_Click(sender As Object, e As EventArgs) Handles Button_Execute.Click
        ExecuteMain()
    End Sub

    Private Sub Button_ReadTest_Click(sender As Object, e As EventArgs) Handles Button_ReadTest.Click
        Dim readPath = "C:\Users\OK\source\repos\Repository10_VBnet\TestDbDiffReporter\DbTableUpdater\DbTableUpdater\TestData\TestDataA1.xlsx"
        readPath = "C:\Users\OK\source\repos\Repository10_VBnet\TestDbDiffReporter\DbTableUpdater\DbTableUpdater\TestData\TestDataB1.xlsx"
        GetUpdateDataFromFileMain(readPath)
    End Sub
End Class
