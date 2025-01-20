Imports Excel = Microsoft.Office.Interop.Excel

Public Class ExcelManager
    Public Sub WriteToExcel(outputPath As String, csvDict As Dictionary(Of String, String))
        Dim excelApp As Excel.Application = Nothing
        Dim workbook As Excel.Workbook = Nothing

        Try
            ' Excelアプリケーションの作成
            excelApp = New Excel.Application()
            workbook = excelApp.Workbooks.Add()

            ' 各CSVデータをExcelシートに転記
            For Each kvp In csvDict
                Dim sheet As Excel.Worksheet = workbook.Sheets.Add()
                sheet.Name = kvp.Key
                Dim csvContent = IO.File.ReadAllLines(kvp.Value)

                ' CSVデータをシートに書き込み
                For i As Integer = 0 To csvContent.Length - 1
                    Dim row = csvContent(i).Split(","c)
                    For j As Integer = 0 To row.Length - 1
                        sheet.Cells(i + 1, j + 1).Value = row(j)
                    Next
                Next
            Next

            ' Excelファイル保存
            workbook.SaveAs(outputPath)
        Catch ex As Exception
            Throw New Exception("Excel書き込み中にエラーが発生しました: " & ex.Message)
        Finally
            ' リソース解放
            If workbook IsNot Nothing Then workbook.Close(False)
            If excelApp IsNot Nothing Then excelApp.Quit()
            ReleaseObject(workbook)
            ReleaseObject(excelApp)
        End Try
    End Sub

    Private Sub ReleaseObject(obj As Object)
        Try
            If obj IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
                obj = Nothing
            End If
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub
End Class
