Module CommonGeneralModule

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
End Module
