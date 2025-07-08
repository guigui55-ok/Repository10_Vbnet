Public Class DataTableAnyUtil

    Public Shared Function ConcatDataTableValues(dt As DataTable) As String
        Dim _list = New List(Of String)
        For Each row In dt.Rows
            Dim buf = ConcatDataRowValues(row)
            _list.Add(buf)
        Next
        Dim ret = String.Join(vbCrLf, _list)
        Return ret
    End Function

    Public Shared Function ConcatDataRowValues(row As DataRow) As String
        Dim _list = New List(Of String)
        For Each col As DataColumn In row.Table.Columns
            Dim buf = row(col)
            If buf Is Nothing Or IsDBNull(buf) Then
                buf = ""
            End If
            _list.Add(buf)
        Next
        Dim ret = String.Join(", ", _list)
        Return ret
    End Function
End Class
