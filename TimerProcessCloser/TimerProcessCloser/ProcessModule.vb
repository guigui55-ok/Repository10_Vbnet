Module ProcessModule

    Public Function KillProcess(logger As MainLogger, processName As String) As Boolean
        ' 終了させたいプロセス名（例: "notepad"）
        Dim isSuccess As Boolean

        ' 指定されたプロセス名を持つすべてのプロセスを取得
        Dim processes As Process() = Process.GetProcessesByName(processName)

        ' 各プロセスを終了させる
        For Each proc As Process In processes
            Try
                proc.Kill()
                proc.WaitForExit() ' プロセスが終了するまで待機
                logger.PrintInfo($"{processName} プロセスを終了しました。")
                isSuccess = True
            Catch ex As Exception
                logger.PrintInfo($"プロセスを終了できませんでした: {ex.Message}")
                isSuccess = False
            End Try
        Next
        Return isSuccess
    End Function
End Module
