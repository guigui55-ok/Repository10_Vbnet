Imports System.Threading.Tasks

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

    Public ReadOnly PROCESS_KEY_OUTPUT = "Output"
    Public ReadOnly PROCESS_KEY_ERROR = "Error"
    Public ReadOnly PROCESS_KEY_EXIT_CODE = "ExitCode"
    Public ReadOnly PROCESS_EXIT_CODE_OK = "1"
    Public ReadOnly PROCESS_EXIT_CODE_ERROR = "-1"


    Public Function IsSuccessToRunProcess(resultDict As Dictionary(Of String, String)) As Boolean
        If resultDict(PROCESS_KEY_EXIT_CODE) = PROCESS_EXIT_CODE_OK Then
            Return True
        Else
            Return False
        End If
    End Function


    ''' <summary>
    ''' プロセスを実行して結果を取得する
    ''' 戻り値はDictionaryで、このモジュール内の定数、Key{PROCESS_KEY_OUTPUT, PROCESS_KEY_ERROR, PROCESS_KEY_EXIT_CODE}で取得する。
    ''' 実行の成否は戻り値DictonaryのExitCodeの値がPROCESS_EXIT_CODE_OK = "1"であるかを判定する。IsSuccessToRunProcessを使用するとよい。
    ''' 
    ''' </summary>
    ''' <param name="logger"></param>
    ''' <param name="filePath"></param>
    ''' <param name="arguments"></param>
    ''' <param name="runAsAdmin"></param>
    ''' <returns></returns>
    Public Function RunProcess(logger As MainLogger, filePath As String, Optional arguments As String = "", Optional runAsAdmin As Boolean = False) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)
        Dim processOutput As String = String.Empty
        Dim processError As String = String.Empty

        Try
            Dim startInfo As New ProcessStartInfo(filePath)
            startInfo.Arguments = arguments
            startInfo.RedirectStandardOutput = True
            startInfo.RedirectStandardError = True
            startInfo.UseShellExecute = False
            startInfo.CreateNoWindow = True

            If runAsAdmin Then
                startInfo.Verb = "runas" ' 管理者として実行
            End If

            Using proc As Process = Process.Start(startInfo)
                processOutput = proc.StandardOutput.ReadToEnd()
                processError = proc.StandardError.ReadToEnd()
                proc.WaitForExit()
            End Using

            result(PROCESS_KEY_OUTPUT) = processOutput
            result(PROCESS_KEY_ERROR) = processError
            result(PROCESS_KEY_EXIT_CODE) = "0" ' 正常終了
            logger.PrintInfo("プロセスの実行が完了しました。")

        Catch ex As Exception
            result(PROCESS_KEY_OUTPUT) = processOutput
            result(PROCESS_KEY_ERROR) = ex.Message
            result(PROCESS_KEY_EXIT_CODE) = "-1" ' エラー終了
            logger.PrintInfo($"プロセスの実行中にエラーが発生しました: {ex.Message}")
        End Try

        Return result
    End Function


    Public Async Function RunProcessAsync(
        logger As MainLogger,
        filePath As String,
        Optional arguments As String = "",
        Optional runAsAdmin As Boolean = False
    ) As Task(Of Dictionary(Of String, String))
        '/
        Dim result As New Dictionary(Of String, String)
        Dim processOutput As String = String.Empty
        Dim processError As String = String.Empty

        Try
            Dim startInfo As New ProcessStartInfo(filePath)
            startInfo.Arguments = arguments
            startInfo.RedirectStandardOutput = True
            startInfo.RedirectStandardError = True
            startInfo.UseShellExecute = False
            startInfo.CreateNoWindow = True

            If runAsAdmin Then
                startInfo.Verb = "runas" ' 管理者として実行
            End If

            Using proc As Process = Process.Start(startInfo)
                processOutput = Await proc.StandardOutput.ReadToEndAsync()
                processError = Await proc.StandardError.ReadToEndAsync()
                'Await proc.WaitForExitAsync() 'エラー	BC30456	'WaitForExitAsync' は 'Process' のメンバーではありません。
                ' WaitForExit を非同期で実行
                Await Task.Run(Sub() proc.WaitForExit())
            End Using

            result(PROCESS_KEY_OUTPUT) = processOutput
            result(PROCESS_KEY_ERROR) = processError
            result(PROCESS_KEY_EXIT_CODE) = "0" ' 正常終了
            logger.PrintInfo("プロセスの実行が完了しました。")

        Catch ex As Exception
            result(PROCESS_KEY_OUTPUT) = processOutput
            result(PROCESS_KEY_ERROR) = ex.Message
            result(PROCESS_KEY_EXIT_CODE) = "-1" ' エラー終了
            logger.PrintInfo($"プロセスの実行中にエラーが発生しました: {ex.Message}")
        End Try

        Return result
    End Function



    Function SplitCommand(command As String) As (String, String)
        Dim parts As String() = Nothing

        If command.StartsWith("""") Then
            ' "で囲まれている場合、最初の " " を探して分割
            parts = command.Split(New String() {""" "}, 2, StringSplitOptions.None)
            parts(0) = parts(0).Trim(""""c) ' パスから " を削除
        Else
            ' 最初のスペースで分割
            parts = command.Split(New Char() {" "c}, 2, StringSplitOptions.None)
        End If

        If parts.Length = 1 Then
            Return (parts(0), String.Empty)
        Else
            Return (parts(0), parts(1))
        End If
    End Function

End Module
