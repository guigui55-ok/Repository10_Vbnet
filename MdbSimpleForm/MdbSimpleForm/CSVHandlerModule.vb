Imports System.IO

Module CSVHandlerModule
    Public Class CSVHandler
        ' リストをCSVファイルに書き出すメソッド
        Public Sub WriteToCSV(filePath As String, data As List(Of String))
            Try
                Using writer As New StreamWriter(filePath)
                    For Each item As String In data
                        writer.WriteLine(item)
                    Next
                End Using
            Catch ex As Exception
                ' 例外処理（必要に応じてログを記録することも可能）
                Console.WriteLine("エラー: " & ex.Message)
            End Try
        End Sub
    End Class

End Module
