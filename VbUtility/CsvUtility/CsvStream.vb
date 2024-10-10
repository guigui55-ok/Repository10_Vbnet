Imports System.IO
Imports System.Text

Public Class CsvStream
    ' リストをCSVファイルに書き出すメソッド

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
    Public Sub WriteCsvDictList(filePath As String, dictList As List(Of Dictionary(Of String, Object)))
        Try
            ' エンコーディングを指定してStreamWriterを作成
            Using writer As New StreamWriter(filePath, False, fileEncoding)

                ' カラム名を1行目に書き出す（リストが空でない場合）
                If dictList.Count > 0 Then
                    Dim columnNames As String() = dictList(0).Keys.ToArray()
                    writer.WriteLine(String.Join(",", columnNames))
                End If

                ' レコードをCSVファイルに書き出す
                For Each record As Dictionary(Of String, Object) In dictList
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
        Catch ex As Exception
            ' エラーハンドリング
            Console.WriteLine("エラー: " & ex.Message)
        End Try
    End Sub

    Function ReadCsvFile(filePath As String) As Data.DataTable
        Dim dt As New Data.DataTable()

        ' CSVファイルを読み込み
        Using sr As New StreamReader(filePath)
            Dim headers As String() = sr.ReadLine().Split(","c)
            For Each header As String In headers
                dt.Columns.Add(header)
            Next

            While Not sr.EndOfStream
                Dim rows As String() = sr.ReadLine().Split(","c)
                dt.Rows.Add(rows)
            End While
        End Using

        Return dt
    End Function

    Public Function ConvertDataTableToListString(dataTable As DataTable)
        Dim rowList As List(Of List(Of String)) = New List(Of List(Of String))()
        Dim colList As List(Of String)

        colList = New List(Of String)()
        For Each col In dataTable.Columns
            colList.Add(col.ToString())
        Next
        rowList.Add(colList)

        For Each row As DataRow In dataTable.Rows
            colList = New List(Of String)()
            For Each cell In row.ItemArray
                colList.Add(cell.ToString())
            Next
            rowList.Add(colList)
        Next
        Return rowList
    End Function

    'Public Sub WriteCSV(filePath As String, data As List(Of String))
    '    Try
    '        Using writer As New StreamWriter(filePath)
    '            For Each item As String In data
    '                writer.WriteLine(item)
    '            Next
    '        End Using
    '    Catch ex As Exception
    '        ' 例外処理（必要に応じてログを記録することも可能）
    '        Console.WriteLine("エラー: " & ex.Message)
    '    End Try
    'End Sub
End Class
