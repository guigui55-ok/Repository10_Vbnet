Public Class FormScreenshotSaver

    Public _logger As AppLogger
    Public _mainProc As ScreenshotSaverProc
    Public _clickWatcher As MouseClickWatcher
    Public _appStatus As StatusControlManager
    Public _timerController As ExecutionTimerController
    Public _ballonNotiry As ScreenshotSaverBalloonTipNotifier

    Private _countDonw As CountdownTimer

    Private Sub FormScreenshotSaver_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'status
        _appStatus._button = Button_Execute
        _appStatus._label = Label_Status
        _appStatus.DoInit()

        'save dir path
        Dim picturesPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
        _logger.Info("ピクチャフォルダのパス: " & picturesPath)
        TextBox_OutputDir.Text = picturesPath
        _mainProc.SetSaveDirPath(picturesPath)

        'time counter
        Dim timeValue = New TimeSpan(0, 1, 0)
        TextBox_Timeout.Text = timeValue.TotalSeconds
        _countDonw = New CountdownTimer(Label_Remain, timeValue)

        'quality
        TextBox_Quality.Text = "75"

        'wait
        TextBox_WaitTime.Text = "20"

        'mode
        If _mainProc._clickMode = ScreenshotSaverProc.ConstClickMode.CLICK Then
            RadioButton_MouseDown.Checked = True
        Else
            RadioButton_MouseUp.Checked = True
        End If
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
            _countDonw.Pause()
        Else
            'set value
            Dim timeoutVal = GetTimeout()
            _logger.Info($"GetTimeout = {timeoutVal}")
            If timeoutVal < 1 Then Exit Sub
            timeoutVal = timeoutVal * 1000
            _countDonw.SetTimeFromMilliseconds(timeoutVal)
            _mainProc.SetSaveDirPath(TextBox_OutputDir.Text)
            _mainProc.SetQuality(TextBox_Quality.Text)
            _mainProc.SetClickMode(RadioButton_MouseDown.Checked, RadioButton_MouseUp.Checked)

            'start
            _countDonw.Start()
            _appStatus.DoStart()
            _clickWatcher.Start()
            _timerController.StartExecution(timeoutVal, AddressOf OnExecutionTimeout)
        End If
    End Sub

    Private Sub OnExecutionTimeout()
        If Me.InvokeRequired Then
            Me.Invoke(New Action(AddressOf OnExecutionTimeout))
        Else
            _countDonw.Pause()
            _clickWatcher.Stop()
            _appStatus.DoStop()
            _ballonNotiry.ShowNotify()
            _countDonw.SetLabelTimeZero()
        End If
    End Sub

    Private Sub Button_Explorer_Click(sender As Object, e As EventArgs) Handles Button_Explorer.Click
        Dim fpath = TextBox_OutputDir.Text
        If (Not IO.Directory.Exists(fpath)) And (Not IO.File.Exists(fpath)) Then
            _logger.Info($"file not exists [{fpath}]")
            _logger.Info("OpenExplorer Exit")
            Exit Sub
        End If
        ModulePathUtil.FileExplorerHelper.OpenInExplorer(fpath)
    End Sub
End Class
