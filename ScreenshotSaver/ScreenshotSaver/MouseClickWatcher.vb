Imports System.Runtime.InteropServices
Imports System.Diagnostics

Public Class MouseClickWatcher
    ' イベント定義
    Public Event MouseClicked(button As MouseButtons)
    Public Event MouseDoubleClicked(button As MouseButtons)

    ' 定数
    Private Const WH_MOUSE_LL As Integer = 14
    Private Const WM_LBUTTONDOWN As Integer = &H201
    Private Const WM_RBUTTONDOWN As Integer = &H204
    Private Const WM_LBUTTONDBLCLK As Integer = &H203
    Private Const WM_RBUTTONDBLCLK As Integer = &H206

    ' デリゲートとハンドル保持
    Private hookProc As LowLevelMouseProc
    Private hookId As IntPtr = IntPtr.Zero

    ' Win32 API宣言
    Private Delegate Function LowLevelMouseProc(nCode As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SetWindowsHookEx(idHook As Integer, lpfn As LowLevelMouseProc,
                                             hMod As IntPtr, dwThreadId As UInteger) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function UnhookWindowsHookEx(hhk As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function CallNextHookEx(hhk As IntPtr, nCode As Integer,
                                           wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function GetModuleHandle(lpModuleName As String) As IntPtr
    End Function

    ' 構造体定義
    <StructLayout(LayoutKind.Sequential)>
    Private Structure MSLLHOOKSTRUCT
        Public pt As Point
        Public mouseData As UInteger
        Public flags As UInteger
        Public time As UInteger
        Public dwExtraInfo As IntPtr
    End Structure

    ' 初期化
    Public Sub Start()
        If hookId = IntPtr.Zero Then
            hookProc = AddressOf HookCallback
            Dim hModule As IntPtr = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName)
            hookId = SetWindowsHookEx(WH_MOUSE_LL, hookProc, hModule, 0)
        End If
    End Sub

    ' 終了
    Public Sub [Stop]()
        If hookId <> IntPtr.Zero Then
            UnhookWindowsHookEx(hookId)
            hookId = IntPtr.Zero
        End If
    End Sub

    ' マウスイベント処理
    Private Function HookCallback(nCode As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
        If nCode >= 0 Then
            Dim button As MouseButtons = MouseButtons.None
            Select Case wParam.ToInt32()
                Case WM_LBUTTONDOWN
                    button = MouseButtons.Left
                    RaiseEvent MouseClicked(button)
                Case WM_RBUTTONDOWN
                    button = MouseButtons.Right
                    RaiseEvent MouseClicked(button)
                Case WM_LBUTTONDBLCLK
                    button = MouseButtons.Left
                    RaiseEvent MouseDoubleClicked(button)
                Case WM_RBUTTONDBLCLK
                    button = MouseButtons.Right
                    RaiseEvent MouseDoubleClicked(button)
            End Select
        End If
        Return CallNextHookEx(hookId, nCode, wParam, lParam)
    End Function


    Private Class _UsageSample
        Inherits Form
        Private WithEvents watcher As New MouseClickWatcher()

        Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            watcher.Start()
        End Sub

        Private Sub watcher_MouseClicked(button As MouseButtons) Handles watcher.MouseClicked
            Console.WriteLine("Clicked: " & button.ToString())
        End Sub

        Private Sub watcher_MouseDoubleClicked(button As MouseButtons) Handles watcher.MouseDoubleClicked
            Console.WriteLine("Double Clicked: " & button.ToString())
        End Sub

        Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
            watcher.Stop()
        End Sub
    End Class

End Class
