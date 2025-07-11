Imports System.Windows.Forms

Public Class CountdownTimer
    Private ReadOnly _timer As System.Timers.Timer
    Private _remainingTime As TimeSpan
    Private ReadOnly _label As Label

    ' カウント終了時に発生するイベント
    Public Event CountdownCompleted()

    Public Sub New(targetLabel As Label, countdownTime As TimeSpan)
        _label = targetLabel
        _remainingTime = countdownTime

        _timer = New System.Timers.Timer(1000)
        _timer.AutoReset = True
        AddHandler _timer.Elapsed, AddressOf TimerElapsed

        UpdateLabel()
    End Sub

    ' カウントダウンを開始
    Public Sub Start()
        _timer.Start()
    End Sub

    ' 一時停止
    Public Sub Pause()
        _timer.Stop()
    End Sub

    ' リセット（時間を変更し再表示）
    Public Sub Reset(newTime As TimeSpan)
        _timer.Stop()
        _remainingTime = newTime
        UpdateLabel()
    End Sub

    ' 時間だけを設定（次のStartで反映される）
    Public Sub SetTime(newTime As TimeSpan)
        _remainingTime = newTime
        UpdateLabel()
    End Sub

    ' ミリ秒からカウントダウン時間を設定
    Public Sub SetTimeFromMilliseconds(milliseconds As Integer)
        _remainingTime = TimeSpan.FromMilliseconds(milliseconds)
        UpdateLabel()
    End Sub
    Private Sub TimerElapsed(sender As Object, e As System.Timers.ElapsedEventArgs)
        If _label.InvokeRequired Then
            _label.Invoke(Sub() Tick())
        Else
            Tick()
        End If
    End Sub

    Private Sub Tick()
        If _remainingTime.TotalSeconds <= 0 Then
            _timer.Stop()
            _label.Text = "00:00:00"
            RaiseEvent CountdownCompleted()
        Else
            _remainingTime = _remainingTime.Subtract(TimeSpan.FromSeconds(1))
            UpdateLabel()
        End If
    End Sub

    Private Sub UpdateLabel()
        _label.Text = _remainingTime.ToString("hh\:mm\:ss")
    End Sub

    Public Sub SetLabelTimeZero()
        _label.Text = "00:00:00"
    End Sub

End Class


Class FormUsage_CountDownTImer
    Inherits Form
    Private WithEvents countdown As CountdownTimer
    Private Label1 As New Label

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim countdownTime As New TimeSpan(0, 1, 0) ' 1分
        countdown = New CountdownTimer(Label1, countdownTime)
        countdown.Start()
    End Sub

    Private Sub countdown_CountdownCompleted() Handles countdown.CountdownCompleted
        MessageBox.Show("カウントダウン終了！")
    End Sub
End Class