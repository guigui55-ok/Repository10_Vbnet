Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop

Public Class ExcelReaderWriter
    '参照追加 Microsoft.Office.Interop.Excel

    Public Event LogoutEvent As EventHandler

    Public Sub Logout(value As String)
        RaiseEvent LogoutEvent(value, EventArgs.Empty)
    End Sub

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

    Private Sub CheckAddress(row As Integer, col As Integer)
        If row <= 0 OrElse col <= 0 Then
            Throw New ArgumentException("Excelのセルは1以上の行・列を指定する必要があります。")
        End If
    End Sub

    Public Sub WriteDataTableToExcel(
            dt As System.Data.DataTable,
            excelFilePath As String,
            startRow As Integer,
            startCol As Integer,
            isWriteColumn As Boolean)
        Dim xlApp As New Application()
        Dim xlBook As Workbook = xlApp.Workbooks.Add()
        Dim xlSheet As Worksheet = CType(xlBook.Sheets(1), Worksheet)

        Try
            xlApp.DisplayAlerts = False


            ' ヘッダー出力
            If isWriteColumn Then
                For col = 0 To dt.Columns.Count - 1
                    CheckAddress(startRow, startCol + col)
                    xlSheet.Cells(startRow, startCol + col).Value = dt.Columns(col).ColumnName
                Next
            End If

            ' データ出力
            For row = 0 To dt.Rows.Count - 1
                For col = 0 To dt.Columns.Count - 1
                    CheckAddress(startRow, startCol + col)
                    xlSheet.Cells(startRow + 1 + row, startCol + col).Value = dt.Rows(row)(col)
                Next
            Next

            ' 保存
            xlBook.SaveAs(excelFilePath)
        Finally
            ' 終了処理
            xlBook.Close(SaveChanges:=False)
            xlApp.Quit()

            ReleaseComObject(xlSheet)
            ReleaseComObject(xlBook)
            ReleaseComObject(xlApp)

            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub


    Public Sub CreateNewExcelFile(filePath As String)
        Dim excelApp As Excel.Application = Nothing
        Dim workbook As Excel.Workbook = Nothing

        Try
            ' Excelアプリケーションの起動
            excelApp = New Excel.Application()
            excelApp.Visible = False ' 非表示で実行

            ' 新しいワークブックの作成
            workbook = excelApp.Workbooks.Add()

            ' シートに初期データを入力（任意）
            Dim sheet As Excel.Worksheet = DirectCast(workbook.Sheets(1), Excel.Worksheet)
            'sheet.Cells(1, 1).Value = "これは新しいExcelファイルです"

            ' 指定されたパスに保存（.xlsx）
            workbook.SaveAs(filePath)

        Catch ex As Exception
            Throw ex
        Finally
            ' 後処理（メモリ解放）
            If Not workbook Is Nothing Then workbook.Close(False)
            If Not excelApp Is Nothing Then excelApp.Quit()

            ' COMオブジェクトの解放
            ReleaseComObject(workbook)
            ReleaseComObject(excelApp)
        End Try
    End Sub

    Private Sub ReleaseComObject(ByVal obj As Object)
        Try
            If obj IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            End If
        Finally
            obj = Nothing
        End Try
    End Sub

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


    ''' <summary>
    ''' Excelファイルから指定シートの有効範囲をDataTableとして読み込む
    ''' </summary>
    ''' <param name="filePath">Excelファイルのフルパス</param>
    ''' <param name="sheetName">読み込むシート名</param>
    ''' <returns>読み込んだDataTable（TableNameはシート名）</returns>
    Public Shared Function ReadSheetToDataTable(filePath As String, sheetName As String) As Data.DataTable
        Dim excelApp As New Application()
        Dim workbook As Workbook = Nothing
        Dim worksheet As Worksheet = Nothing
        Dim dt As New Data.DataTable()

        Try
            workbook = excelApp.Workbooks.Open(filePath)
            worksheet = CType(workbook.Sheets(sheetName), Worksheet)
            Dim usedRange As Range = worksheet.UsedRange

            Dim rowCount As Integer = usedRange.Rows.Count
            Dim colCount As Integer = usedRange.Columns.Count

            ' カラムを追加（1行目の値をヘッダと仮定）
            For col As Integer = 1 To colCount
                Dim header As Object = CType(usedRange.Cells(1, col), Range).Value
                If header Is Nothing Then
                    header = "Column" & col.ToString()
                End If
                dt.Columns.Add(header.ToString())
            Next

            ' データ行を追加
            For row As Integer = 2 To rowCount
                Dim dr As Data.DataRow = dt.NewRow()
                For col As Integer = 1 To colCount
                    dr(col - 1) = CType(usedRange.Cells(row, col), Range).Value
                Next
                dt.Rows.Add(dr)
            Next

            ' DataTable名にシート名を設定
            dt.TableName = sheetName

        Catch ex As Exception
            Throw New Exception("Excel読み込み中にエラーが発生しました: " & ex.Message)
        Finally
            ' Excelリソースの解放
            If workbook IsNot Nothing Then
                workbook.Close(False)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook)
            End If
            excelApp.Quit()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp)
        End Try

        Return dt
    End Function

End Class

