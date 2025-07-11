Public Class ScreenshotSaverBalloonTipNotifier
    Public _notifyIcon As NotifyIcon
    Public _balloonTime As Integer

    Sub New(notifyIconObj As NotifyIcon)
        _notifyIcon = notifyIconObj
    End Sub

    Sub Init(balloonTime As Integer)
        _notifyIcon.Icon = SystemIcons.Application ' アイコンを設定（カスタムアイコンも可能）
        _notifyIcon.Text = "ScreenshotSaver" ' アイコンにマウスオーバーした時に表示されるテキスト
        _notifyIcon.Visible = True

        ' バルーンチップ（ポップアップ通知）の設定
        _notifyIcon.BalloonTipTitle = "ScreenshotSaver End Timer"
        _notifyIcon.BalloonTipText = "タイマーが終了しました。"
        _notifyIcon.BalloonTipIcon = ToolTipIcon.Info ' アイコンタイプを設定（Info, Warning, Errorなど）

        'バルーンが表示される時間
        _balloonTime = balloonTime

        ' イベントハンドラの設定
        'AddHandler Me.NotifyIcon1.BalloonTipClicked, AddressOf Me.OnBalloonTipClicked
    End Sub

    '' バルーンチップがクリックされたときの処理
    'Private Sub OnBalloonTipClicked(sender As Object, e As EventArgs)
    '    Debug.WriteLine("### 通知がクリックされました。")
    'End Sub

    Sub ShowNotify()
        _notifyIcon.ShowBalloonTip(_balloonTime) 'ミリ秒
    End Sub

    Sub Dispose()
        _notifyIcon.Visible = False
        _notifyIcon.Dispose()
    End Sub
End Class
