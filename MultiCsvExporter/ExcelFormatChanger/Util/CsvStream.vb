Imports System.IO
Imports System.Text

Public Class CsvStream
    ' リストをCSVファイルに書き出すメソッド

    ' デフォルトエンコーディングはShift-JISに設定
    'Private fileEncoding As Encoding = Encoding.GetEncoding("Shift-JIS")
    '241213
    'SJISだと警告が出ることがある
    '「開こうとしているファイル'TestDict.csv'の形式は、ファイル拡張子が示す形式と異なります。このファイルを開く前に、ファイルが破損していないこと、信頼できる発行元からのファイルであることを確認してください。」

    '250706
    '書き込み時DataTableに対応
    Private fileEncoding As Encoding = New UTF8Encoding(True)

    ' エンコーディングを設定するメソッド
    Public Sub SetEncoding(encodingName As String)
        fileEncoding = Encoding.GetEncoding(encodingName)
    End Sub
    Sub OutputConsole(value)
        Dim wVal = String.Format("{0}", value)
        Debug.WriteLine(wVal)
    End Sub

    ''' <summary>
    ''' List(Of String) をCSVに出力する
    ''' </summary>
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
            OutputConsole("エラー: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Diciionary(string, object)List をCSVに出力する
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="dictList"></param>
    Public Function WriteCsvDictList(filePath As String, dictList As List(Of Dictionary(Of String, Object)), isWriteColumn As Boolean) As Integer
        Try
            ' エンコーディングを指定してStreamWriterを作成
            Using writer As New StreamWriter(filePath, False, fileEncoding)

                If isWriteColumn Then
                    ' カラム名を1行目に書き出す（リストが空でない場合）
                    If dictList.Count > 0 Then
                        Dim columnNames As String() = dictList(0).Keys.ToArray()
                        writer.WriteLine(String.Join(",", columnNames))
                    End If
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
            Return 1
        Catch ex As Exception
            ' エラーハンドリング
            'OutputConsole("エラー: " & ex.Message)
            Throw ex
            Return -2
        End Try
    End Function

    Function ReadCsvFile(filePath As String, addColumn As Boolean) As Data.DataTable
        Dim filename = System.IO.Path.GetFileName(filePath)
        Dim dt As New Data.DataTable(filename)

        ' CSVファイルを読み込み
        Using sr As New StreamReader(filePath)
            Dim headers As String() = sr.ReadLine().Split(","c)
            For Each header As String In headers
                dt.Columns.Add(header)
            Next

            If addColumn Then
                Dim valueList = New List(Of String)
                For Each col As DataColumn In dt.Columns
                    valueList.Add(col.ColumnName)
                Next
                dt.Rows.Add(valueList.ToArray)
            End If

            While Not sr.EndOfStream
                Dim rows As String() = sr.ReadLine().Split(","c)
                dt.Rows.Add(rows)
            End While
        End Using

        Return dt
    End Function

    Public Function ConvertDataTableToListString(dataTable As DataTable) As List(Of List(Of String))
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

    Public Sub WriteCsv(filePath As String, dt As DataTable, isWriteColumn As Boolean)
        Dim dictList = ConvertDataTableToListOfDictionary(dt)
        Dim ret = WriteCsvDictList(filePath, dictList, isWriteColumn)
    End Sub

    ''' <summary>
    ''' DataTableの各行を Dictionary(Of String, Object) に変換し、それらを List にして返す関数
    ''' </summary>
    ''' <param name="dt">変換対象のDataTable</param>
    ''' <returns>List(Of Dictionary(Of String, Object))</returns>
    Public Function ConvertDataTableToListOfDictionary(dt As DataTable) As List(Of Dictionary(Of String, Object))
        Dim result As New List(Of Dictionary(Of String, Object))()

        For Each row As DataRow In dt.Rows
            Dim dict As New Dictionary(Of String, Object)()
            For Each col As DataColumn In dt.Columns
                dict(col.ColumnName) = row(col)
            Next
            result.Add(dict)
        Next

        Return result
    End Function

End Class
