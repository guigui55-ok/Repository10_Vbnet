Imports System.IO

Public Class FormMdbSimpleManager
    Dim _mdbPath As String
    Dim _csvManager As CSVHandler
    Dim _mdbManager As MdbManager
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DebugPrint("Click WriteRecoreds")
        _mdbManager.SetValues(GetMdbPathFromTextBox())

        Dim tableName As String = TextBox2.Text
        DebugPrint(String.Format("TableName = {0}", tableName))
        ' テーブル名、カラム指定なし（すべてのカラムを取得）、最初の5レコードを取得
        'Dim records = _mdbManager.GetRecords("T_TEST01", 0, 5)
        Dim records = _mdbManager.GetRecords(tableName)

        ' 出力
        For Each record As Dictionary(Of String, Object) In records
            For Each kvp In record
                DebugPrint($"{kvp.Key}: {kvp.Value}")
            Next
        Next
        Dim wPath As String = Directory.GetCurrentDirectory() + "\" + "output_Records.csv"
        _csvManager.WriteRecordsToCSV(wPath, records)
        '//
        DebugPrint("WriteToCsv Recoreds")
        DebugPrint(String.Format("RecordLength = {0}", records.Count))
        DebugPrint(String.Format("TableName = {0}", tableName))
        DebugPrint(String.Format("wPath = {0}", wPath))
        SetWriteFilePathToTextBox(wPath)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'WriteTables
        DebugPrint("Click WriteTables")
        _mdbPath = GetMdbPathFromTextBox()
        _mdbManager.SetValues(_mdbPath)
        Dim list As List(Of String)
        list = _mdbManager.GetAllTables()
        Dim wPath As String = Directory.GetCurrentDirectory() + "\" + "output_Tables.csv"
        _csvManager.WriteToCSV(wPath, list)
        '//
        Dim buf As String = String.Join(",", list)
        DebugPrint(String.Format("Tables = {0}", list))
        '//
        DebugPrint("WriteToCsv TableNames")
        DebugPrint(String.Format("wPath = {0}", wPath))
        SetWriteFilePathToTextBox(wPath)
    End Sub

    Private Sub FormMdbSimpleManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _mdbPath = "F:\PROGRAM\VBA\TestMDB\TestMDB01.mdb"
        TextBox1.Text = _mdbPath
        TextBox2.Text = "T_TEST01"
        _csvManager = New CSVHandler()
        _mdbManager = New MdbManager()
    End Sub

    Private Function GetMdbPathFromTextBox() As String
        Return TextBox1.Text
    End Function

    Private Sub SetWriteFilePathToTextBox(path As String)
        TextBox3.Text = path
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim filePath As String = TextBox3.Text
            Dim csvOpener = New OpenCsvByExcelClass()
            ' CsvToExcelOpener にパスを設定し、Excelで開く
            csvOpener.SetCsvFilePath(filePath)
            csvOpener.OpenCsvWithExcel()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
