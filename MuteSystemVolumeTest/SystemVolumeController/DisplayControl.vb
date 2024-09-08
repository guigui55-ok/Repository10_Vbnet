Imports System.Runtime.InteropServices

Module DisplayControl

    ' Windows API 関数の定義
    <DllImport("user32.dll", SetLastError:=True)>
    Private Function SendMessage(hWnd As IntPtr, Msg As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    ' 定数
    Private Const HWND_BROADCAST As Integer = &HFFFF
    Private Const WM_SYSCOMMAND As Integer = &H112
    Private Const SC_MONITORPOWER As Integer = &HF170

    ' ディスプレイをOFFにする関数
    Public Sub TurnOffDisplay()
        SendMessage(New IntPtr(HWND_BROADCAST), WM_SYSCOMMAND, New IntPtr(SC_MONITORPOWER), New IntPtr(2))
    End Sub

    ' ディスプレイをONにする関数
    Public Sub TurnOnDisplay()
        SendMessage(New IntPtr(HWND_BROADCAST), WM_SYSCOMMAND, New IntPtr(SC_MONITORPOWER), New IntPtr(-1))
    End Sub

    ' ディスプレイをOFFにする非同期関数
    Public Async Sub TurnOffDisplayAsync()
        Await Task.Run(Sub()
                           SendMessage(New IntPtr(HWND_BROADCAST), WM_SYSCOMMAND, New IntPtr(SC_MONITORPOWER), New IntPtr(2))
                       End Sub)
    End Sub

    ' ディスプレイをONにする非同期関数
    Public Async Sub TurnOnDisplayAsync()
        Await Task.Run(Sub()
                           SendMessage(New IntPtr(HWND_BROADCAST), WM_SYSCOMMAND, New IntPtr(SC_MONITORPOWER), New IntPtr(-1))
                       End Sub)
    End Sub
End Module