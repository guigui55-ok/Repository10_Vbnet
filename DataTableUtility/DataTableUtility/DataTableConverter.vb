Public Class DataTableConverter

    ' Array（Object型配列）からDataSetを作成
    Public Shared Function FromArray(data As Object(,)) As DataSet
        Dim dataSet As New DataSet()
        Dim dataTable As New DataTable("ArrayTable")

        ' 列を定義
        For col As Integer = 0 To data.GetLength(1) - 1
            dataTable.Columns.Add($"Col{col + 1}", GetType(String))
        Next

        ' 行を追加
        For row As Integer = 0 To data.GetLength(0) - 1
            Dim newRow As DataRow = dataTable.NewRow()
            For col As Integer = 0 To data.GetLength(1) - 1
                newRow(col) = ConvertToString(data(row, col))
            Next
            dataTable.Rows.Add(newRow)
        Next

        dataSet.Tables.Add(dataTable)
        Return dataSet
    End Function

    ' List(Of Object)からDataSetを作成
    Public Shared Function FromList(data As List(Of Object)) As DataSet
        Dim dataSet As New DataSet()
        Dim dataTable As New DataTable("ListTable")

        ' 列を定義
        dataTable.Columns.Add("Value", GetType(String))

        ' 行を追加
        For Each item As Object In data
            Dim newRow As DataRow = dataTable.NewRow()
            newRow("Value") = ConvertToString(item)
            dataTable.Rows.Add(newRow)
        Next

        dataSet.Tables.Add(dataTable)
        Return dataSet
    End Function

    ' Dictionary(Of String, Object)からDataSetを作成
    Public Shared Function FromDictionary(data As Dictionary(Of String, Object)) As DataSet
        Dim dataSet As New DataSet()
        Dim dataTable As New DataTable("DictionaryTable")

        ' 列を定義
        dataTable.Columns.Add("Key", GetType(String))
        dataTable.Columns.Add("Value", GetType(String))

        ' 行を追加
        For Each kvp As KeyValuePair(Of String, Object) In data
            Dim newRow As DataRow = dataTable.NewRow()
            newRow("Key") = kvp.Key
            newRow("Value") = ConvertToString(kvp.Value)
            dataTable.Rows.Add(newRow)
        Next

        dataSet.Tables.Add(dataTable)
        Return dataSet
    End Function

    ' ObjectをStringに置き換える
    Private Shared Function ConvertToString(value As Object) As String
        If value Is Nothing Then
            Return String.Empty
        End If
        Return value.ToString()
    End Function

    ' DataTable の行と列を入れ替える
    Public Shared Function Transpose(dataTable As DataTable) As DataTable
        Dim transposedTable As New DataTable("Transposed")

        ' 新しいテーブルの列を元の行数分定義
        For i As Integer = 0 To dataTable.Rows.Count - 1
            transposedTable.Columns.Add($"Row{i + 1}", GetType(String))
        Next

        ' 新しいテーブルの行を元の列数分追加
        For j As Integer = 0 To dataTable.Columns.Count - 1
            Dim newRow As DataRow = transposedTable.NewRow()
            For i As Integer = 0 To dataTable.Rows.Count - 1
                newRow(i) = ConvertToString(dataTable.Rows(i)(j))
            Next
            transposedTable.Rows.Add(newRow)
        Next

        Return transposedTable
    End Function

End Class
