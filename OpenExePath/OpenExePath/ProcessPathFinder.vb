Imports System.Diagnostics

Public Class ProcessPathFinder
    ''' <summary>
    ''' 指定したプロセス名から実行ファイルのパスを取得する
    ''' </summary>
    ''' <param name="processName">プロセス名（拡張子なし）</param>
    ''' <returns>実行ファイルのパスのリスト</returns>
    Public Shared Function GetProcessPaths(processName As String) As List(Of String)
        Dim pathList As New List(Of String)()

        Try
            Dim processes As Process() = Process.GetProcessesByName(processName)

            If processes.Length > 0 Then
                For Each proc As Process In processes
                    Try
                        Dim path As String = proc.MainModule.FileName
                        pathList.Add(path)
                        OutPut($"プロセス名: {proc.ProcessName}, 実行ファイルパス: {path}")
                    Catch ex As Exception
                        ' アクセスできないプロセスの処理
                        OutPut($"エラー（{proc.ProcessName}）: {ex.Message}")
                    End Try
                Next
            Else
                OutPut("指定したプロセスは実行されていません。")
            End If

        Catch ex As Exception
            OutPut($"エラー: {ex.Message}")
        End Try

        Return pathList
    End Function
End Class
