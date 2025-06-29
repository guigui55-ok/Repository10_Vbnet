Imports Microsoft.Office.Interop.Excel

Public Class SharedExcelApp

    Public Shared ExcelApp As Application

    Public Shared Sub InitExcelApp()
        ExcelApp = New Application()
        ExcelApp.Visible = False
    End Sub

    ''' <summary>
    ''' Excelアプリケーションを終了
    ''' </summary>
    Public Shared Sub Quit()
        Try
            ExcelApp?.Quit()
        Catch ex As System.Runtime.InteropServices.COMException
            'ハンドルされていない例外: System.Runtime.InteropServices.COMException: RPC サーバーを利用できません。 (HRESULT からの例外:0x800706BA)
            If ex.Message.Contains("RPC サーバーを利用できません。") Then
                'pass
            Else
                Throw
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
