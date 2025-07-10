Public Class StatusControlManager

    Enum EnumAppStatus
        _NONE = 0
        _INIT
        _WAIT
        _EXECUTING
        _STOPPING
    End Enum

    Private ConstStatusTextArray = {
        "None",
        "Init",
        "Wait",
        "Executing",
        "Stopping"
    }
    Private ConstButtonTextArray = {
        "-",
        "Start",
        "Start",
        "Stop",
        "Stop",
        "Stop"
    }

    Public _logger As AppLogger
    Public Status As EnumAppStatus

    Public _button As Button
    Public _label As Label


    Sub New(logger As AppLogger, __button As Button, __label As Label)
        _logger = logger
        _button = __button
        _label = __label
    End Sub


    Public Sub DoInit()
        ChangeStatus(EnumAppStatus._INIT, "DoInit")
    End Sub
    Public Sub DoWait()
        ChangeStatus(EnumAppStatus._WAIT, "DoWait")
    End Sub

    Public Sub DoStart()
        ChangeStatus(EnumAppStatus._EXECUTING, "DoStart")
    End Sub

    Public Sub DoStop()
        ChangeStatus(EnumAppStatus._STOPPING, "DoStop")
        DoStopping()
    End Sub

    Public Sub ChangeStatus(_status As EnumAppStatus, callerFuncName As String)
        Status = _status
        _logger.Info($"{callerFuncName} [Status={Status}]")
        ChangeControlText()
    End Sub

    Private Sub ChangeControlText()
        If _button IsNot Nothing Then
            _button.Text = ConstButtonTextArray(Status)
        End If
        If _label IsNot Nothing Then
            _label.Text = ConstStatusTextArray(Status)
        End If
    End Sub

    Public Sub DoStopping()
        'ストップ時の処理
        'pass

        'ストップ処理終了
        DoWait()
    End Sub

End Class
