
Imports System.Drawing.Imaging

Public Class ScreenshotSaverProc

    Public Class ConstClickMode
        Public Const CLICK = 0
        Public Const MOUSE_UP = 1
    End Class


    Public _logger As AppLogger
    Public _appStatus As StatusControlManager
    Public _timerController As ExecutionTimerController
    Public WithEvents _clickWatcher As MouseClickWatcher

    Public _executer As ScreenshotSaveExecuter
    Dim _saveDirPath = ""
    Dim _quality As Integer = 0
    Dim _waitTime As Integer = 0

    Dim _asyncTimer As AsyncTimer

    Public _clickMode As Integer

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

        _asyncTimer = New AsyncTimer(0, isRepeating:=False)
        '_clickMode = ConstClickMode.CLICK
        _clickMode = ConstClickMode.MOUSE_UP
    End Sub

    Public Sub SetSaveDirPath(saveDirPath As String)
        _saveDirPath = saveDirPath
        _logger.Info($"_saveDirPath = {_saveDirPath}")
    End Sub

    Public Sub SetClickMode(isMouseDown As Boolean, isMouseUp As Boolean)
        If isMouseDown Then
            _clickMode = ConstClickMode.CLICK
        Else
            'default
            _clickMode = ConstClickMode.MOUSE_UP
        End If
        _logger.Info($"_clickMode = {_clickMode},  isMouseDown={isMouseDown},  isMouseUp={isMouseUp}")
    End Sub


    Public Sub SetQuality(value As String)
        If IsNumeric(value) Then _quality = CInt(value)
        If _quality < 0 Then _quality = 80
        If 100 < _quality Then _quality = 100
        _logger.Info($"_quality = {_quality}")
    End Sub

    Public Sub SetWaitTime(value As String)
        If IsNumeric(value) Then _waitTime = CInt(value)
        If _waitTime < 0 Then _waitTime = 0
        Dim ts = New TimeSpan(0, 0, CInt(_waitTime / 1000))
        If 5 < ts.Minutes Then _waitTime = New TimeSpan(0, 5, 0).TotalMilliseconds
        _logger.Info($"_waitTime = {_waitTime}")
    End Sub

    Private Sub watcher_MouseUp(button As MouseButtons) Handles _clickWatcher.MouseReleased
        If _clickMode = ConstClickMode.MOUSE_UP Then
            _logger.Info("MouseUp: " & button.ToString())
            ExecuteScreenshotWithTimer()
        End If

    End Sub

    Private Sub watcher_MouseClicked(button As MouseButtons) Handles _clickWatcher.MouseClicked
        If _clickMode = ConstClickMode.CLICK Then
            _logger.Info("Clicked: " & button.ToString())
            ExecuteScreenshotWithTimer()
        End If
    End Sub

    Private Sub ExecuteScreenshotWithTimer()
        If 0 < _waitTime Then
            _asyncTimer.StopTimer()
            _asyncTimer.SetInterval(_waitTime, isRepeating:=False)
            _asyncTimer.Start()
        Else
            '_waitTime <= 0
            _executer.SaveScreenshot(_saveDirPath, "", _quality)
        End If
    End Sub

    Private Sub OnTimerElapsed(sender As Object, e As EventArgs)
        '_executer.SaveScreenshot(_saveDirPath, "")
        _executer.SaveScreenshot(_saveDirPath, "", _quality)
    End Sub

    Private Sub watcher_MouseDoubleClicked(button As MouseButtons) Handles _clickWatcher.MouseDoubleClicked
        _logger.Info("Double Clicked: " & button.ToString())
    End Sub

End Class
