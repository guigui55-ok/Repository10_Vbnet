

''' <summary>
''' 規定時間後に停止を通知するタイマークラス
''' </summary>
Public Class ExecutionTimerController
    Private _timer As Timer
    Private _durationMs As Integer
    Private _callbackOnTimeout As Action
    Private _logger As AppLogger

    Public Sub New(logger As AppLogger)
        _logger = logger
        _timer = New Timer()
        AddHandler _timer.Tick, AddressOf OnTimerTick
    End Sub

    Public Sub StartExecution(durationMs As Integer, callbackOnTimeout As Action)
        _logger.Info($"[ExecutionTimerController] StartExecution: {durationMs} ms")
        _durationMs = durationMs
        _callbackOnTimeout = callbackOnTimeout
        _timer.Interval = _durationMs
        _timer.Start()
    End Sub

    Public Sub StopExecution()
        _logger.Info("[ExecutionTimerController] StopExecution")
        _timer.Stop()
    End Sub

    Private Sub OnTimerTick(sender As Object, e As EventArgs)
        _timer.Stop()
        _logger.Info("[ExecutionTimerController] Timer Tick - Executing timeout.")
        _callbackOnTimeout?.Invoke()
    End Sub
End Class
