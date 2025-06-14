Imports System.Security.Principal
Module ModuleCheckAdmin
    ''' <summary>
    ''' 管理者として実行されているかを判定する
    ''' </summary>
    ''' <returns>True: 管理者, False: 一般ユーザー</returns>
    Public Function IsAdministrator() As Boolean
        Dim identity = WindowsIdentity.GetCurrent()
        Dim principal = New WindowsPrincipal(identity)
        Return principal.IsInRole(WindowsBuiltInRole.Administrator)
    End Function
End Module
