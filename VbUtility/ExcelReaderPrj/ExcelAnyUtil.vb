Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

Public Class ExcelAnyUtil
    'Private _excelApp As Application

    Public Sub New()
        SharedExcelApp.InitExcelApp()
    End Sub

    '''' <summary>
    '''' Excelファイルを開き、指定範囲で文字列検索を行い、一致したセルのアドレス（A1形式）を返す
    '''' </summary>
    'Public Function FindCellAddress(filePath As String,
    '                                sheetName As String,
    '                                rangeAddress As String,
    '                                findValue As String,
    '                                isPartialMatch As Boolean) As String
    '    Dim _excelApp = SharedExcelApp.ExcelApp
    '    Dim wb As Workbook = _excelApp.Workbooks.Open(filePath)
    '    Dim ws As Worksheet

    '    If Not String.IsNullOrEmpty(sheetName) Then
    '        ws = wb.Sheets(sheetName)
    '    Else
    '        ws = wb.Sheets(1)
    '    End If

    '    Dim targetRange As Range = ws.Range(rangeAddress)
    '    Dim foundCell As Range = Nothing

    '    For Each cell As Range In targetRange
    '        If cell.Value IsNot Nothing Then
    '            Dim cellText As String = cell.Value.ToString()
    '            If isPartialMatch Then
    '                If cellText.Contains(findValue) Then
    '                    foundCell = cell
    '                    Exit For
    '                End If
    '            Else
    '                If cellText = findValue Then
    '                    foundCell = cell
    '                    Exit For
    '                End If
    '            End If
    '        End If
    '    Next

    '    'Dim result As String = If(foundCell IsNot Nothing, foundCell.Address(False, False), "")
    '    'wb.Close(False)
    '    'Return result

    '    '// 補足：オブジェクト解放の観点でも改善するとより安全
    '    Dim result As String = ""
    '    Try
    '        If foundCell IsNot Nothing Then
    '            result = foundCell.Address(False, False)
    '        End If
    '    Finally
    '        If foundCell IsNot Nothing Then Marshal.ReleaseComObject(foundCell)
    '        If targetRange IsNot Nothing Then Marshal.ReleaseComObject(targetRange)
    '        If ws IsNot Nothing Then Marshal.ReleaseComObject(ws)
    '        If wb IsNot Nothing Then
    '            wb.Close(False)
    '            Marshal.ReleaseComObject(wb)
    '        End If
    '    End Try
    '    Return result

    'End Function


    Public Function GetWorkBook(filePath As String) As Workbook
        Dim excelApp As Application = SharedExcelApp.ExcelApp
        Dim wb = excelApp.Workbooks.Open(filePath)
        Return wb
    End Function

    Public Function GetWorkSheet(ByRef wb As Workbook, ByRef sheetName As String) As Worksheet
        Dim ws = If(Not String.IsNullOrEmpty(sheetName),
            CType(wb.Worksheets(sheetName), Worksheet),
            CType(wb.Worksheets(1), Worksheet))
        Return ws
    End Function

    Public Sub CloseWs(ByRef ws As Worksheet)
        Try
            ' 先にすべての Range などを呼び出し元で解放しておくことを推奨

            If ws IsNot Nothing Then
                Marshal.ReleaseComObject(ws)
                ws = Nothing
            End If

        Catch ex As Exception
            Debug.WriteLine(vbCrLf + "##########" + vbCrLf)
            Debug.WriteLine("Workbook Close 失敗: " & ex.Message)
            Debug.WriteLine(ex.GetType.ToString + ":" & ex.Message)
            Debug.WriteLine(ex.StackTrace)
            Debug.WriteLine(vbCrLf + "##########" + vbCrLf)
        End Try
    End Sub

    Public Sub CloseWb(ByRef wb As Workbook, ByRef ws As Worksheet)
        Try
            ' 先にすべての Range などを呼び出し元で解放しておくことを推奨

            If ws IsNot Nothing Then
                Marshal.ReleaseComObject(ws)
                ws = Nothing
            End If

            If wb IsNot Nothing Then
                wb.Close(False)
                Marshal.ReleaseComObject(wb)
                wb = Nothing
            End If
        Catch ex As Exception
            ' ログ出力やリカバリ処理があればここに
            Debug.WriteLine(vbCrLf + "##########" + vbCrLf)
            Debug.WriteLine("Workbook Close 失敗: " & ex.Message)
            Debug.WriteLine(ex.GetType.ToString + ":" & ex.Message)
            Debug.WriteLine(ex.StackTrace)
            Debug.WriteLine(vbCrLf + "##########" + vbCrLf)
        End Try
    End Sub

    ''' <summary>
    ''' Excelファイルを開き、指定範囲で文字列検索を行い、一致したセルのアドレス（A1形式）を返す
    ''' </summary>
    Public Function FindCellAddress(ByRef ws As Worksheet,
                                rangeAddress As String,
                                findValue As String,
                                isPartialMatch As Boolean) As String
        Dim targetRange As Range = Nothing
        Dim foundCell As Range = Nothing
        Dim result As String = ""

        Try
            'todo:
            'rangeAddress = Nothing などアドレスでない場合にエラー単にする

            ' 検索範囲取得
            targetRange = ws.Range(rangeAddress)

            ' セル内検索
            For Each cell As Range In targetRange
                If cell IsNot Nothing AndAlso cell.Value IsNot Nothing Then
                    Dim cellText As String = cell.Value.ToString()
                    If isPartialMatch Then
                        If cellText.Contains(findValue) Then
                            foundCell = cell
                            Exit For
                        End If
                    Else
                        If cellText = findValue Then
                            foundCell = cell
                            Exit For
                        End If
                    End If
                End If
                Marshal.ReleaseComObject(cell)
            Next

            ' 結果取得（Range → String に変換）
            If foundCell IsNot Nothing Then
                result = foundCell.Address(False, False)
            End If

        Catch ex As Exception
            ' 例外処理（ログ出力等を挿入しても良い）
            Throw

        Finally
            ' COMオブジェクトの明示的解放（逆順で）
            If foundCell IsNot Nothing Then Marshal.ReleaseComObject(foundCell)
            If targetRange IsNot Nothing Then Marshal.ReleaseComObject(targetRange)
        End Try

        Return result
    End Function

    ''' <summary>
    ''' A1形式のセルアドレスから、Endプロパティによる移動後のアドレスを取得
    ''' </summary>
    Public Function GetEndAddress(ByRef ws As Worksheet,
                                  startAddress As String,
                                  direction As XlDirection) As String

        Dim startCell As Range = ws.Range(startAddress)
        Dim endCell As Range = startCell.End(direction)
        Dim result As String = endCell.Address(False, False)
        Return result
    End Function

    Public Sub CopyFormatBetweenBooks(
                                  ByRef srcBook As Workbook,
                                  ByRef srcSheet As Worksheet,
                                  srcRangeAddress As String,
                                  ByRef destBook As Workbook,
                                  ByRef destSheet As Worksheet,
                                  destRangeAddress As String)

        Dim srcRange As Range = Nothing
        Dim destRange As Range = Nothing
        Try
            'TODO:
            'srcRangeAddress が無効なアドレスならエラーとする
            'destRangeAddress が無効なアドレスならエラーとする

            srcRange = srcSheet.Range(srcRangeAddress)
            destRange = destSheet.Range(destRangeAddress)

            srcRange.Copy()
            destRange.PasteSpecial(XlPasteType.xlPasteFormats)

            destBook.Save()

        Catch ex As Exception
            Debug.WriteLine(vbCrLf + "**********" + vbCrLf)
            Debug.WriteLine(ex.GetType.ToString + ":" + ex.Message)
            Debug.WriteLine(ex.StackTrace)
            Debug.WriteLine(vbCrLf + "**********" + vbCrLf)
            ' ※ 例外はスタックトレース保持のため Throw のみにする
            Throw

        Finally
            ' 使ったCOMオブジェクトをすべて解放
            If destRange IsNot Nothing Then Marshal.ReleaseComObject(destRange)
            If srcRange IsNot Nothing Then Marshal.ReleaseComObject(srcRange)
            If destSheet IsNot Nothing Then Marshal.ReleaseComObject(destSheet)
            If srcSheet IsNot Nothing Then Marshal.ReleaseComObject(srcSheet)

        End Try
    End Sub



    ''' <summary>
    ''' 使用例（外部ソースで実行）
    ''' </summary>
    Public Sub UsageSampleInExternalSource()
        Dim util As New ExcelAnyUtil()




        Dim conditionSrc = New ExcelAnyUtil.ChangeFormatData With {
            .FilePath = "C:\testSrc.xlsx",
            .FindSheetName = "",
            .FindRangeString = "A:A",
            .FindValue = "■TableA",
            .FindMode = ExcelAnyUtil.ConstfilterMode.CONTAINS
        }
        Dim conditionDest = New ExcelAnyUtil.ChangeFormatData With {
            .FilePath = "C:\testDest.xlsx",
            .FindSheetName = "",
            .FindRangeString = "A:A",
            .FindValue = "■TableA",
            .FindMode = ExcelAnyUtil.ConstfilterMode.CONTAINS
        }


        Dim srcWs = Nothing
        Dim srcWb = Nothing
        Dim destWb = Nothing
        Dim destWs = Nothing

        SharedExcelApp.InitExcelApp()

        srcWb = util.GetWorkBook(conditionSrc.FilePath)
        srcWs = util.GetWorkSheet(srcWb, conditionSrc.FindSheetName)

        destWb = util.GetWorkBook(conditionDest.FilePath)
        destWs = util.GetWorkSheet(destWb, conditionDest.FindSheetName)

        '' End(XlDown)で移動したアドレスを取得
        'Dim endAddr = util.GetEndAddress("C:\test.xlsx", "Sheet1", addr, XlDirection.xlDown)
        'Console.WriteLine("Endアドレス: " & endAddr)

        'src
        Dim foundAddrSrc = util.FindCellAddress(
            srcWs,
            conditionSrc.FindRangeString,
            conditionSrc.FindValue,
            True)
        Console.WriteLine("src 検索結果: " & foundAddrSrc)

        'dest
        Dim foundAddrDest = util.FindCellAddress(
            destWs,
            conditionDest.FindRangeString,
            conditionDest.FindValue,
            True)
        Console.WriteLine("dest 検索結果: " & foundAddrDest)

        util.CopyFormatBetweenBooks(
                srcWb,
                srcWs,
                foundAddrSrc,
                destWb,
                destWs,
                foundAddrDest
            )

        '※正しくは、TryCatchのFinallyで解放する（そうしなければ プロセス EXCEL.EXE が残り、実行するにつれてこのプロセスが増え続けてしまい、そうなると正しくbookやsheetが読み込めなくなる事が良くある）

        util.CloseWb(srcWb, srcWs)
        util.CloseWb(destWb, destWs)
        SharedExcelApp.Quit()
    End Sub

    ''' <summary>
    ''' Excelアプリケーションを終了
    ''' </summary>
    Public Sub Quit()
        SharedExcelApp.Quit()
    End Sub

    ' ▼ 内部クラス・列挙体
    Public Class ChangeFormatData
        Public Property FilePath As String
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
