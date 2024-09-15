Imports System.IO
Imports System.Text

Module CSVHandlerModule
    Public Class CSVHandler
        ' デフォルトエンコーディングはShift-JISに設定
        Private fileEncoding As Encoding = Encoding.GetEncoding("Shift-JIS")

        ' エンコーディングを設定するメソッド
        Public Sub SetEncoding(encodingName As String)
            fileEncoding = Encoding.GetEncoding(encodingName)
        End Sub

        ' リストをCSVファイルに書き出すメソッド
        Public Sub WriteToCSV(filePath As String, data As List(Of String))
            Try
                ' エンコーディングを指定してStreamWriterを作成
                Using writer As New StreamWriter(filePath, False, fileEncoding)
                    For Each item As String In data
                        writer.WriteLine(item)
                    Next
                End Using
            Catch ex As Exception
                ' 例外処理（必要に応じてログを記録することも可能）
                Console.WriteLine("エラー: " & ex.Message)
            End Try
        End Sub

        ' リストをCSVファイルに書き出すメソッド
        Public Sub WriteRecordsToCSV(filePath As String, records As List(Of Dictionary(Of String, Object)))
            Try
                ' エンコーディングを指定してStreamWriterを作成
                Using writer As New StreamWriter(filePath, False, fileEncoding)

                    ' カラム名を1行目に書き出す（リストが空でない場合）
                    If records.Count > 0 Then
                        Dim columnNames As String() = records(0).Keys.ToArray()
                        writer.WriteLine(String.Join(",", columnNames))
                    End If

                    ' レコードをCSVファイルに書き出す
                    For Each record As Dictionary(Of String, Object) In records
                        Dim values As New List(Of String)()
                        For Each value In record.Values
                            ' 値がnullの場合の対応
                            If value Is Nothing Then
                                values.Add("")
                            Else
                                values.Add(value.ToString())
                            End If
                        Next
                        writer.WriteLine(String.Join(",", values))
                    Next
                End Using
                Console.WriteLine("CSV書き出しが完了しました。")
            Catch ex As Exception
                ' エラーハンドリング
                Console.WriteLine("エラー: " & ex.Message)
            End Try
        End Sub
    End Class

End Module
