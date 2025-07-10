Module ScreenshotSaverMain

    '予定
    'カウントダウン（時間表示）
    'クリックしてからのウェイト
    '検討中：マウスアップで実行切替
    '検討中：対象のアプリが実行されてい無いときは動作しない


    Sub Main()

        Dim logger = New AppLogger
        Dim clickWacher = New MouseClickWatcher()
        Dim statusManager = New StatusControlManager(logger, Nothing, Nothing)
        Dim timerController = New ExecutionTimerController(logger)
        Dim mainProc = New ScreenshotSaverProc(logger, clickWacher, statusManager, timerController)
        Dim formMain = New FormScreenshotSaver()
        formMain._logger = logger
        formMain._clickWatcher = clickWacher
        formMain._appStatus = statusManager
        formMain._timerController = timerController
        formMain._mainProc = mainProc

        Application.Run(formMain)

    End Sub

End Module
