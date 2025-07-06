Public Class DataGridViewSaver

    Public Enum EnumSaveMode
        CSV = 0
        EXCEL = 1
    End Enum

    Public FileMode As EnumSaveMode
    Public _dt As DataTable = Nothing
    Public _filePathCsv = ""
    Public _dgv As DataGridView

    Public _filePathExcel = ""

    Public _enc As System.Text.Encoding = System.Text.Encoding.UTF8

    Public Sub Init(_mode As EnumSaveMode, dgv As DataGridView, filePathCsv As String, FilePathText As String)
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
        LoadFile(_filePathCsv)
        DataGridViewAnyUtil.SetDataTableToDataGridView(_dgv, _dt)
        ReadPath(_writeTextFileName, _filePathExcel)
    End Sub

    Public Sub SaveMain(filePathExcel As String)
        _dt = DataGridViewAnyUtil.CreateDataTableFromDataGridView(_dgv)
        SaveFile(_filePathCsv, _dgv)
        WritePath(_writeTextFileName, filePathExcel)
        _filePathExcel = filePathExcel
    End Sub

    Public _writeTextFileNameSrcPath = "SettingExcelPathDest.txt"
    Public _writeTextFileNameDestPath = "SettingExcelPathDest.txt"

    Public _writeTextFileName = ""

    Public Sub WritePath(fileName As String, content As String)
        Dim wPath = IO.Path.GetDirectoryName(_filePathCsv) + "\" + fileName
        Using writer = New IO.StreamWriter(wPath, append:=False, _enc)
            writer.Write(content)
        End Using
    End Sub

    Public Sub ReadPath(fileName As String, ByRef content As String)
        Dim rPath = IO.Path.GetDirectoryName(_filePathCsv) + "\" + fileName
        Using reader = New IO.StreamReader(rPath, _enc)
            content = reader.ReadToEnd
        End Using
        content = content.Trim
        content = content.Replace(vbCrLf, "")
    End Sub

End Class
