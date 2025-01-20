

Imports System.IO

Module Program

    Sub Main()
        MainB()
    End Sub

    Sub MainB()
        ' Loggerの初期化
        Dim logger As New AppLogger()

        ' ファイルパスをCurrentDirectoryに設定
        Dim currentDirectory As String = AppDomain.CurrentDomain.BaseDirectory
        Dim logFilePath As String = Path.Combine(currentDirectory, "__test_log.log")
        logger.LogFileTimeFormat = ""
        logger.SetFilePath(logFilePath)
        Debug.WriteLine(String.Format("logFilePath = {0}", logFilePath))

        ' ログレベルをINFOに設定
        logger.LoggerLogLevel = LogLevel.INFO

        ' ログをコンソールとファイルに出力するように設定
        logger.LogOutPutMode = OutputMode.CONSOLE Or OutputMode.FILE Or OutputMode.DEBUG_WINDOW

        'Dim logList = New List(Of String)(
        '    {"10001",
        '    "10002"})

        Dim valueList = New List(Of String)()

        For i = 1 To 10
            Dim buf = "100" + String.Format("{0:D2}", i)
            valueList.Add(buf)
        Next


        Dim bufValueList = valueList.ToArray().ToList()
        Dim interval = 2000
        interval = 600
        While True
            Dim buf = bufValueList(0)
            logger.PrintInfo(buf)
            Threading.Thread.Sleep(interval)

            bufValueList.RemoveAt(0)
            If bufValueList.Count <= 0 Then
                bufValueList = valueList.ToArray().ToList()
            End If
        End While

        Debug.WriteLine("ログテストが完了しました。")

    End Sub




    Sub MainA()
        ' Loggerの初期化
        Dim logger As New AppLogger()

        ' ファイルパスをCurrentDirectoryに設定
        Dim currentDirectory As String = AppDomain.CurrentDomain.BaseDirectory
        Dim logFilePath As String = Path.Combine(currentDirectory, "__test_log.log")
        logger.LogFileTimeFormat = ""
        logger.SetFilePath(logFilePath)
        Debug.WriteLine(String.Format("logFilePath = {0}", logFilePath))

        ' ログレベルをINFOに設定
        logger.LoggerLogLevel = LogLevel.INFO

        ' ログをコンソールとファイルに出力するように設定
        logger.LogOutPutMode = OutputMode.CONSOLE Or OutputMode.FILE Or OutputMode.DEBUG_WINDOW

        ' ログメッセージのテスト
        logger.PrintCritical("これはクリティカルログです。")
        logger.PrintError("これはエラーログです。")
        logger.PrintWarn("これは警告ログです。")
        logger.PrintInfo("これは情報ログです。")
        logger.PrintDebug("これはデバッグログです。")
        logger.PrintTrace("これはトレースログです。")

        Debug.WriteLine("ログテストが完了しました。")
        'Console.ReadKey()
    End Sub

End Module
