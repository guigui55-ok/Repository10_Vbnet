Public Class FormScreenshotSaver

    Public _logger As AppLogger
    Public _mainProc As ScreenshotSaverProc
    Public _clickWatcher As MouseClickWatcher
    Public _appStatus As StatusControlManager
    Public _timerController As ExecutionTimerController

    Private Sub FormScreenshotSaver_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _appStatus._button = Button_Execute
        _appStatus._label = Label_Status
        _appStatus.DoInit()

        Dim picturesPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
        _logger.Info("ピクチャフォルダのパス: " & picturesPath)
        TextBox_OutputDir.Text = picturesPath
        _mainProc.SetSaveDirPath(picturesPath)

        TextBox_Timeout.Text = "60"
    End Sub
    Private Sub FormScreenshotSaver_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        _appStatus.DoWait()
    End Sub

    Private Function GetTimeout() As Integer
        Dim tb = TextBox_Timeout
        If IsNumeric(tb.Text) Then
            Return CInt(tb.Text)
        Else
            Return 0
        End If
    End Function

    Private Sub Button_Start_Click(sender As Object, e As EventArgs) Handles Button_Execute.Click

        If _appStatus.Status = StatusControlManager.EnumAppStatus._EXECUTING Then
            _timerController.StopExecution()
            _clickWatcher.Stop()
            _appStatus.DoStop()
        Else
            Dim timeoutVal = GetTimeout()
            _logger.Info($"GetTimeout = {timeoutVal}")
            If timeoutVal < 1 Then Exit Sub
            timeoutVal = timeoutVal * 1000
            _appStatus.DoStart()
            _clickWatcher.Start()
            _timerController.StartExecution(timeoutVal, AddressOf OnExecutionTimeout)
        End If
    End Sub

    Private Sub OnExecutionTimeout()
        If Me.InvokeRequired Then
            Me.Invoke(New Action(AddressOf OnExecutionTimeout))
        Else
            _clickWatcher.Stop()
            _appStatus.DoStop()
        End If
    End Sub



End Class
