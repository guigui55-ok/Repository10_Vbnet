Imports System.Timers

Public Class AsyncTimer
    Private _timer As Timer
    Private _interval As Double
    Private _isRepeating As Boolean

    ''' <summary>
    ''' イベント発生時に呼ばれる外部処理（コールバック）
    ''' </summary>
    Public Event TimerElapsed As EventHandler

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="interval">インターバル（ミリ秒）</param>
    ''' <param name="isRepeating">繰り返し動作するか</param>
    Public Sub New(interval As Double, isRepeating As Boolean)
        _interval = interval
        _isRepeating = isRepeating

        '_timer = New Timer(_interval) 'System.ArgumentException: パラメーター 'interval' の値 '0' が無効です。
        _timer = New Timer()
        AddHandler _timer.Elapsed, AddressOf OnElapsed
        _timer.AutoReset = _isRepeating
    End Sub

    ''' <summary>
    ''' タイマー開始
    ''' </summary>
    Public Sub Start()
        _timer.Start()
    End Sub

    ''' <summary>
    ''' タイマー停止
    ''' </summary>
    Public Sub StopTimer()
        _timer.Stop()
    End Sub

    ''' <summary>
    ''' タイマーの時間や繰り返し設定を変更
    ''' </summary>
    Public Sub SetInterval(interval As Double, Optional isRepeating As Boolean = True)
        _interval = interval
        _isRepeating = isRepeating
        _timer.Interval = interval
        _timer.AutoReset = isRepeating
    End Sub

    ''' <summary>
    ''' 内部タイマーイベント処理
    ''' </summary>
    Private Sub OnElapsed(sender As Object, e As ElapsedEventArgs)
        RaiseEvent TimerElapsed(Me, EventArgs.Empty)
    End Sub


    Private Class UsageSample
        Inherits Form
        Private WithEvents myTimer As AsyncTimer

        Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            myTimer = New AsyncTimer(3000, False) ' 3秒後に1回だけ実行
            AddHandler myTimer.TimerElapsed, AddressOf OnTimerElapsed
            myTimer.Start()
        End Sub

        Private Sub OnTimerElapsed(sender As Object, e As EventArgs)
            ' UIスレッドと別なので、UI更新はInvokeが必要
            If Me.InvokeRequired Then
                Me.Invoke(Sub() MessageBox.Show("タイマー完了！"))
            Else
                MessageBox.Show("タイマー完了！")
            End If
        End Sub
    End Class
End Class
