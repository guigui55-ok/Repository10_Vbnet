Imports System.Management
'参照設定：System.Management
Public Class WmiProcessPathFinder
    ''' <summary>
    ''' WMI を使用して指定したプロセス名から実行ファイルのパスを取得する
    ''' </summary>
    ''' <param name="processName">プロセス名（拡張子なし）</param>
    ''' <returns>実行ファイルのパスのリスト</returns>
    Public Shared Function GetProcessPaths(processName As String) As List(Of String)
        Dim pathList As New List(Of String)()

        Try
            Dim query As String = "SELECT ProcessId, ExecutablePath FROM Win32_Process WHERE Name = '" & processName & ".exe'"
            Dim searcher As New ManagementObjectSearcher(query)

            For Each obj As ManagementObject In searcher.Get()
                Dim processId As Integer = CInt(obj("ProcessId"))
                Dim executablePath As String = obj("ExecutablePath")?.ToString()

                If Not String.IsNullOrEmpty(executablePath) Then
                    pathList.Add(executablePath)
                    OutPut($"プロセスID: {processId}, 実行ファイルパス: {executablePath}")
                End If
            Next

        Catch ex As Exception
            OutPut($"エラー: {ex.Message}")
        End Try

        Return pathList
    End Function
End Class
