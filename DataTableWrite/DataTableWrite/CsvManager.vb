Public Class CsvManager
    ' エンコードを設定するプロパティ
    'Public Property FileEncoding As System.Text.Encoding = System.Text.Encoding.UTF8
    Public Property FileEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift-JIS")


    Public Function ReadCsv(filePath As String) As DataTable
        Dim dt As New DataTable()

        Using sr As New IO.StreamReader(filePath, FileEncoding)
            ' ヘッダー行の読み取りとDataTableの列定義
            Dim headers = sr.ReadLine().Split(","c)
            For Each header In headers
                dt.Columns.Add(header.Trim(), GetType(String))
            Next

            ' データ行の読み取りとDataTableへの追加
            While Not sr.EndOfStream
                Dim rows = sr.ReadLine().Split(","c)
                Dim newRow = ConvertToDataRow(rows, dt)
                If newRow IsNot Nothing Then
                    dt.Rows.Add(newRow)
                End If
            End While
        End Using

        Return dt
    End Function

    ' String() を DataRow に変換する汎用関数
    Private Function ConvertToDataRow(values As String(), dt As DataTable) As DataRow
        Dim newRow = dt.NewRow()

        ' DataTable の列数と一致するように値を設定
        For i As Integer = 0 To Math.Min(values.Length, dt.Columns.Count) - 1
            newRow(i) = values(i)
        Next

        ' 必要に応じて空の値を設定
        For i As Integer = values.Length To dt.Columns.Count - 1
            newRow(i) = DBNull.Value
        Next

        Return newRow
    End Function

    Public Sub WriteCsv(filePath As String, dataTable As DataTable)
        Using sw As New IO.StreamWriter(filePath, False, FileEncoding)
            'sw.WriteLine(String.Join(",", dataTable.Columns.Cast(Of DataColumn).Select(Function(c) c.ColumnName)))
            For Each row As DataRow In dataTable.Rows
                sw.WriteLine(String.Join(",", row.ItemArray))
            Next
        End Using
    End Sub

    Public Sub TransformDataTable(dataTable As DataTable, ByRef direction As String)
        If direction = "R" Then
            Dim transposedTable As New DataTable()
            For i As Integer = 0 To dataTable.Rows.Count
                transposedTable.Columns.Add("Col" & i.ToString())
            Next
            For Each column As DataColumn In dataTable.Columns
                Dim newRow = transposedTable.NewRow()
                For i As Integer = 0 To dataTable.Rows.Count - 1
                    newRow(i) = dataTable.Rows(i)(column)
                Next
                transposedTable.Rows.Add(newRow)
            Next
            dataTable = transposedTable
        End If
    End Sub
End Class
