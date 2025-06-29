Imports Microsoft.Office.Interop.Excel

Public Class ExcelAnyUtil
    Private _excelApp As Application

    Public Sub New()
        _excelApp = New Application()
        _excelApp.Visible = False
    End Sub

    ''' <summary>
    ''' Excelファイルを開き、条件に一致するセルのアドレスを返す
    ''' </summary>
    Public Function FindCellAddress(filePath As String, filSrc As ChangeFormatData) As String
        Dim wb As Workbook = _excelApp.Workbooks.Open(filePath)
        Dim ws As Worksheet

        If Not String.IsNullOrEmpty(filSrc.FindSheetName) Then
            ws = wb.Sheets(filSrc.FindSheetName)
        Else
            ws = wb.Sheets(1)
        End If

        Dim targetRange As Range = ws.Range(filSrc.FindRangeString)
        Dim foundCell As Range = Nothing

        For Each cell As Range In targetRange
            If cell.Value IsNot Nothing Then
                Dim cellValue As String = cell.Value.ToString()
                Select Case filSrc.FindMode
                    Case ConstfilterMode.EQUALS
                        If cellValue = filSrc.FindValue Then
                            foundCell = cell
                            Exit For
                        End If
                    Case ConstfilterMode.CONTAINS
                        If cellValue.Contains(filSrc.FindValue) Then
                            foundCell = cell
                            Exit For
                        End If
                End Select
            End If
        Next

        Dim address As String = If(foundCell IsNot Nothing, foundCell.Address(False, False), "")
        wb.Close(False)
        Return address
    End Function

    ''' <summary>
    ''' A1形式のアドレスから EndXlDown などで移動した先のアドレスを取得
    ''' </summary>
    Public Function GetEndAddress(filePath As String, sheetName As String, startAddress As String, direction As XlDirection) As String
        Dim wb As Workbook = _excelApp.Workbooks.Open(filePath)
        Dim ws As Worksheet = wb.Sheets(sheetName)
        Dim startCell As Range = ws.Range(startAddress)
        Dim endCell As Range = startCell.End(direction)
        Dim result As String = endCell.Address(False, False)
        wb.Close(False)
        Return result
    End Function

    ''' <summary>
    ''' ソースブックから別のブックへ、指定レンジの書式をコピーする（ファイルパス指定）
    ''' </summary>
    Public Sub CopyFormatBetweenBooks(srcFilePath As String, srcSheetName As String, srcRangeAddress As String,
                                  destFilePath As String, destSheetName As String, destRangeAddress As String)
        Dim srcBook As Workbook = _excelApp.Workbooks.Open(srcFilePath)
        Dim destBook As Workbook = _excelApp.Workbooks.Open(destFilePath)

        Dim srcSheet As Worksheet = srcBook.Sheets(srcSheetName)
        Dim destSheet As Worksheet = destBook.Sheets(destSheetName)

        Dim srcRange As Range = srcSheet.Range(srcRangeAddress)
        Dim destRange As Range = destSheet.Range(destRangeAddress)

        srcRange.Copy()
        destRange.PasteSpecial(XlPasteType.xlPasteFormats)

        ' 保存して閉じる
        destBook.Save()
        srcBook.Close(False)
        destBook.Close(False)
    End Sub

    ''' <summary>
    ''' 使用例（外部ソースで実行）
    ''' </summary>
    Public Sub UsageSampleInExternalSource()
        Dim util As New ExcelAnyUtil()
        Dim conditionSrc = New ExcelAnyUtil.ChangeFormatData With {
            .FindSheetName = "",
            .FindRangeString = "A:A",
            .FindValue = "■TableA",
            .FindMode = ExcelAnyUtil.ConstfilterMode.CONTAINS
        }
        Dim conditionDest = New ExcelAnyUtil.ChangeFormatData With {
            .FindSheetName = "",
            .FindRangeString = "A:A",
            .FindValue = "■TableA",
            .FindMode = ExcelAnyUtil.ConstfilterMode.CONTAINS
        }
        'src
        Dim srcBook = "C:\testSrc.xlsx"
        Dim foundAddrSrc = util.FindCellAddress(srcBook, conditionSrc)
        Console.WriteLine("src 見つかったアドレス: " & foundAddrSrc)

        'dest
        Dim destBook = "C:\testDest.xlsx"
        Dim foundAddrDest = util.FindCellAddress(destBook, conditionDest)
        Console.WriteLine("dest 見つかったアドレス: " & foundAddrDest)

        util.CopyFormatBetweenBooks(
                srcBook,
                conditionSrc.FindSheetName,
                foundAddrSrc,
                destBook,
                conditionSrc.FindSheetName,
                foundAddrDest
            )
    End Sub

    ''' <summary>
    ''' Excelアプリケーションを終了
    ''' </summary>
    Public Sub Quit()
        _excelApp.Quit()
    End Sub

    ' ▼ 内部クラス・列挙体
    Public Class ChangeFormatData
        Public Property FindSheetName As String
        Public Property FindRangeString As String
        Public Property FindValue As String
        Public Property FindMode As ConstfilterMode
    End Class

    Public Enum ConstfilterMode
        EQUALS
        CONTAINS
    End Enum
End Class
