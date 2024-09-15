Imports System.Diagnostics
Imports System.IO

Public Class OpenCsvByExcelClass

    ' CSVファイルのパスを格納するフィールド
    Private csvFilePath As String

    ' CSVファイルのパスを設定するメソッド
    Public Sub SetCsvFilePath(path As String)
        If System.IO.File.Exists(path) Then
            csvFilePath = path
        Else
            Throw New FileNotFoundException("指定されたCSVファイルが見つかりません。")
        End If
    End Sub

    ' ExcelでCSVファイルを開くメソッド
    Public Sub OpenCsvWithExcel()
        If Not String.IsNullOrEmpty(csvFilePath) Then
            ' ExcelでCSVファイルを開く
            Process.Start("excel.exe", csvFilePath)
        Else
            Throw New InvalidOperationException("CSVファイルのパスが設定されていません。")
        End If
    End Sub
End Class
