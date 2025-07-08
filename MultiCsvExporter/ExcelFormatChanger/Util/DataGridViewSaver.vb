Public Class DataGridViewSaver

    Public Enum EnumSaveMode
        CSV = 0
        EXCEL = 1
    End Enum

    Public _logger As Object

    Public FileMode As EnumSaveMode
    Public _dt As DataTable = Nothing
    Public _filePathCsv = ""
    Public _dgv As DataGridView

    Public _filePathExcel = "" '内容にエクセルファイルのパスが書いてあるテキストファイル
    Public _targetFilePath As String '処理対象のファイル


    Public _enc As System.Text.Encoding = System.Text.Encoding.UTF8

    Public Sub Init(logger As Object, _mode As EnumSaveMode, dgv As DataGridView, filePathCsv As String, FilePathText As String)
        _logger = logger
        FileMode = _mode
        _dgv = dgv
        _filePathCsv = filePathCsv
        _filePathExcel = FilePathText
    End Sub

    Public Sub SaveFile(filePath As String, dgv As DataGridView)
        Dim dt = DataGridViewAnyUtil.CreateDataTableFromDataGridView(dgv)
        Dim csvSt = New CsvStream()
        csvSt.WriteCsv(filePath, dt, True)
    End Sub

    Public Sub LoadFile(filepath As String)
        Dim csvSt = New CsvStream()
        _dt = csvSt.ReadCsvFile(filepath, addColumn:=True)
    End Sub

    Public Sub LoadMain()
        _logger.info($"readCsv = {_filePathCsv}")
        If IO.File.Exists(_filePathCsv) Then
            LoadFile(_filePathCsv)
            '1行目はカラムの行なので削除
            RemoveFirstRow(_dt)
            _logger.info($"_dtRowsCount = {_dt.Rows.Count}")
            _logger.info("CsvData=")
            _logger.info(DataTableAnyUtil.ConcatDataTableValues(_dt))
            DataGridViewAnyUtil.SetDataTableToDataGridView(_dgv, _dt, False)
        Else
            _logger.info("csv is not exists.")
        End If
        _logger.info($"readFile = {_filePathExcel}")
        If IO.File.Exists(_filePathExcel) Then
            ReadPath(_filePathExcel, _targetFilePath)
        Else
            _logger.info("file is not exists.")
        End If
    End Sub


    ''' <summary>
    ''' DataTable の最初の行（インデックス0）を削除する
    ''' </summary>
    ''' <param name="dt">対象の DataTable</param>
    Private Sub RemoveFirstRow(dt As DataTable)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            dt.Rows.RemoveAt(0)
        End If
    End Sub

    Public Sub SaveMain(writePathContent As String)
        _dt = DataGridViewAnyUtil.CreateDataTableFromDataGridView(_dgv)
        _logger.info($"SaveCsv = {_filePathCsv}")
        SaveFile(_filePathCsv, _dgv)

        _logger.info($"SaveTextContent = {writePathContent}")

        Dim wPath = IO.Path.GetDirectoryName(_filePathCsv) + "\" + _writeTextFileName
        wPath = _filePathExcel

        _logger.info($"SaveTextPath = {wPath}")
        WriteText(wPath, writePathContent)
    End Sub

    Public _writeTextFileNameSrcPath = "SettingExcelPathDest.txt"
    Public _writeTextFileNameDestPath = "SettingExcelPathDest.txt"

    Public _writeTextFileName = ""

    Public Sub WriteText(filePath As String, content As String)
        Using writer = New IO.StreamWriter(filePath, append:=False, _enc)
            writer.Write(content)
        End Using
    End Sub
    Public Sub WriteText(dirPath As String, fileName As String, content As String)
        Dim wPath = IO.Path.GetDirectoryName(_filePathCsv) + "\" + fileName
        Using writer = New IO.StreamWriter(wPath, append:=False, _enc)
            writer.Write(content)
        End Using
    End Sub

    Public Sub ReadPath(filePath As String, ByRef content As String)
        Dim rPath = filePath
        Using reader = New IO.StreamReader(rPath, _enc)
            content = reader.ReadToEnd
        End Using
        content = content.Trim
        content = content.Replace(vbCrLf, "")
    End Sub

End Class
