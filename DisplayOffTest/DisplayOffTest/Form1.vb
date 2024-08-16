
Imports System.Runtime.InteropServices

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim displayControl As New DisplayControl()
        displayControl.TurnOffDisplay()
    End Sub
End Class


Public Class DisplayControl

    ' Windows API の SendMessage 関数をインポート
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SendMessage(hWnd As IntPtr, Msg As UInteger, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    ' 画面をオフにするためのメッセージとパラメータ
    Private Const WM_SYSCOMMAND As UInteger = &H112
    Private Const SC_MONITORPOWER As UInteger = &HF170
    Private Const MONITOR_OFF As UInteger = 2

    Public Sub TurnOffDisplay()
        ' デスクトップのハンドルを取得し、ディスプレイをオフにする
        Dim handle As IntPtr = Process.GetCurrentProcess().MainWindowHandle
        SendMessage(handle, WM_SYSCOMMAND, CType(SC_MONITORPOWER, IntPtr), CType(MONITOR_OFF, IntPtr))
    End Sub

End Class