Imports Microsoft.Office.Interop.Excel

Module ModuleExcelReader

    Sub Main()

    End Sub

    Class ExcelReader
        '参照追加 Microsoft.Office.Interop.Excel
        Function ReadExcelFile(filePath As String) As Data.DataTable

            Dim dt As New Data.DataTable()

            ' Excelアプリケーションを作成
            Dim excelApp As New Microsoft.Office.Interop.Excel.Application()
            Dim excelWorkbook As Workbook = excelApp.Workbooks.Open(filePath)
            'Dim sheetName As String = "YourSheetName"
            'Dim excelWorksheet As Worksheet = CType(excelWorkbook.Sheets(sheetName), Worksheet)
            Dim excelWorksheet As Worksheet = CType(excelWorkbook.Sheets(1), Worksheet)

            ' 使用範囲の取得
            Dim usedRange As Range = excelWorksheet.UsedRange
            Dim colCount As Integer = usedRange.Columns.Count
            Dim rowCount As Integer = usedRange.Rows.Count

            ' DataTableの列を追加
            For i As Integer = 1 To colCount
                dt.Columns.Add("Column " & i)
            Next

            ' DataTableに行を追加
            For i As Integer = 1 To rowCount
                Dim row As DataRow = dt.NewRow()
                For j As Integer = 1 To colCount
                    Dim cellValue As Object = CType(usedRange.Cells(i, j), Range).Value2
                    If cellValue Is Nothing Then
                        row(j - 1) = DBNull.Value
                    Else
                        row(j - 1) = cellValue
                    End If
                Next
                dt.Rows.Add(row)
            Next

            ' リソースの解放
            excelWorkbook.Close(False)
            excelApp.Quit()
            ReleaseObject(excelWorksheet)
            ReleaseObject(excelWorkbook)
            ReleaseObject(excelApp)

            Return dt
        End Function

        Sub ReleaseObject(ByVal obj As Object)
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
                obj = Nothing
            Catch ex As Exception
                obj = Nothing
            Finally
                GC.Collect()
            End Try
        End Sub
    End Class

End Module
