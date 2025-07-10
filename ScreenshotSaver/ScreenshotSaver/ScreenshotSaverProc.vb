Public Class ScreenshotSaverProc

    Public _logger As AppLogger
    Public _appStatus As StatusControlManager
    Public _timerController As ExecutionTimerController
    Public WithEvents _clickWatcher As MouseClickWatcher

    Public _executer As ScreenshotSaveExecuter
    Dim _saveDirPath = ""

    'こちらからは、あまり使用しない想定
    Public _formMain As FormScreenshotSaver

    Sub New(logger As AppLogger, clickWatcher As MouseClickWatcher, appStatus As StatusControlManager, timerController As ExecutionTimerController)
        _logger = logger
        _clickWatcher = clickWatcher
        _appStatus = appStatus
        _timerController = timerController

        _executer = New ScreenshotSaveExecuter(_logger)
        'Dim savePath = Application.StartupPath + "\" + "image"
        'SetSaveDirPath(savePath)
    End Sub

    Public Sub SetSaveDirPath(saveDirPath As String)
        _saveDirPath = saveDirPath
        _logger.Info($"_saveDirPath = {_saveDirPath}")
    End Sub

    Private Sub watcher_MouseClicked(button As MouseButtons) Handles _clickWatcher.MouseClicked
        _logger.Info("Clicked: " & button.ToString())
        _executer.SaveScreenshot(_saveDirPath, "")
    End Sub

    Private Sub watcher_MouseDoubleClicked(button As MouseButtons) Handles _clickWatcher.MouseDoubleClicked
        _logger.Info("Double Clicked: " & button.ToString())
    End Sub

End Class
