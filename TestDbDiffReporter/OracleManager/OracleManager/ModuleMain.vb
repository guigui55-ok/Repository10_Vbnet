Module ModuleMain
    Sub Main()
        'init
        Dim logger = New AppLogger()
        logger.LogOutPutMode = AppLogger.OutputMode.DEBUG_WINDOW
        'logger.LogOutPutMode = AppLogger.OutputMode.DEBUG_WINDOW + AppLogger.OutputMode.FILE

        Dim args = Environment.GetCommandLineArgs()
        logger.Info(vbCrLf + "*** OracleManager Main")
        logger.Info($"args = " + String.Join(", ", args))
        Dim mode As AppMode = GetAppModeFromArgs(Environment.GetCommandLineArgs())

        Select Case mode
            Case AppMode.Form
                Application.EnableVisualStyles()
                Application.SetCompatibleTextRenderingDefault(False)
                Dim formMain = New FormOracleManager()
                formMain._logger = logger
                formMain._formLog = New FormLog()
                formMain._formLog.parentForm = formMain
                AddHandler logger.AddLogEvent, AddressOf formMain.AddLogToControl
                AddHandler logger.AddLogEvent, AddressOf formMain._formLog.AddLogToControl
                Application.Run(formMain)
            Case AppMode.Console
                RunConsoleMode()
        End Select
    End Sub

    Private Sub RunConsoleMode()
        Console.WriteLine("コンソールモードで実行中... [未実装]")
        ' 処理をここに記述
    End Sub

    Public Enum AppMode
        Form
        Console
    End Enum

    ''' <summary>
    ''' 引数からAppModeを解析して返す
    ''' </summary>
    ''' <param name="args">コマンドライン引数</param>
    ''' <returns>AppMode (FormまたはConsole)</returns>
    Public Function GetAppModeFromArgs(args As String()) As AppMode
        For i As Integer = 0 To args.Length - 1
            If args(i).ToLower() = "--mode" AndAlso i + 1 < args.Length Then
                Select Case args(i + 1).ToLower()
                    Case "form"
                        Return AppMode.Form
                    Case "console"
                        Return AppMode.Console
                End Select
            End If
        Next
        ' デフォルトはフォームモード
        Return AppMode.Form
    End Function
End Module
