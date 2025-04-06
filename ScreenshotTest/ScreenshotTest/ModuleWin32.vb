Imports System.Diagnostics
Imports System.Runtime.InteropServices

Module ModuleWin32

    <StructLayout(LayoutKind.Sequential)>
    Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    <DllImport("user32.dll")>
    Private Function GetWindowRect(ByVal hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
    End Function

    ''' <summary>
    ''' 指定したプロセスのウィンドウ位置とサイズを取得する
    ''' </summary>
    ''' <param name="processName">プロセス名（拡張子なし）</param>
    ''' <param name="left">ウィンドウの左座標（出力）</param>
    ''' <param name="top">ウィンドウの上座標（出力）</param>
    ''' <param name="width">ウィンドウの幅（出力）</param>
    ''' <param name="height">ウィンドウの高さ（出力）</param>
    ''' <returns>成功した場合 True, 失敗した場合 False</returns>
    Function GetWindowSizeAndPosition(ByVal processName As String, ByRef left As Integer, ByRef top As Integer, ByRef width As Integer, ByRef height As Integer) As Boolean
        Dim processes As Process() = Process.GetProcessesByName(processName)

        If processes.Length > 0 Then
            Dim hWnd As IntPtr = processes(0).MainWindowHandle ' メインウィンドウのハンドルを取得

            If hWnd <> IntPtr.Zero Then
                Dim rect As RECT
                If GetWindowRect(hWnd, rect) Then
                    left = rect.Left
                    top = rect.Top
                    width = rect.Right - rect.Left
                    height = rect.Bottom - rect.Top
                    Return True ' 成功
                End If
            End If
        End If

        ' 失敗
        left = 0
        top = 0
        width = 0
        height = 0
        Return False
    End Function
End Module
