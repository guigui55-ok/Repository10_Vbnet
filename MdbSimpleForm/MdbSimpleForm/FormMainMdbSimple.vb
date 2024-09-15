'Imports MdbHandlerAdoModule
'Imports CSVHandlerModule
Imports System.IO

Public Class FormMainMdbSimple
    Dim _mdbPath As String
    Dim _mdbHandler As MDBHandler
    Dim _csvWriter As CSVHandler

    Private Sub FormMainMdbSimple_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _mdbPath = "F:\PROGRAM\VBA\TestMDB\TestMDB01.mdb"

        TextBox1.Text = _mdbPath
        _mdbHandler = New MDBHandler()
        _csvWriter = New CSVHandler()
    End Sub


    Private Sub ButtonConnect_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    End Sub

    Private Sub ButtonGetTableNames_Click(sender As Object, e As EventArgs) Handles ButtonGetTableNames.Click
        Dim list As List(Of String)
        _mdbHandler.SetValues(_mdbPath)
        list = _mdbHandler.GetAllTables()
        Dim wbuf As String = String.Join("," & vbCrLf, list)
        Dim filePath As String = Directory.GetCurrentDirectory + "\" + "OutputData.csv"
        _csvWriter.WriteToCSV(filePath, list)
        Console.WriteLine("WriteToCsv")
    End Sub
End Class
