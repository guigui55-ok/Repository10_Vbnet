Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Debug.WriteLine("Button1.Click")
        ' バルーンチップを表示
        Me.NotifyIcon1.ShowBalloonTip(3000) ' 3000ミリ秒（3秒間）表示
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.NotifyIcon1.Icon = SystemIcons.Application ' アイコンを設定（カスタムアイコンも可能）
        Me.NotifyIcon1.Text = "アプリケーション名" ' アイコンにマウスオーバーした時に表示されるテキスト
        Me.NotifyIcon1.Visible = True

        ' バルーンチップ（ポップアップ通知）の設定
        Me.NotifyIcon1.BalloonTipTitle = "通知タイトル"
        Me.NotifyIcon1.BalloonTipText = "これはポップアップ通知のメッセージです。"
        Me.NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info ' アイコンタイプを設定（Info, Warning, Errorなど）

        ' イベントハンドラの設定
        AddHandler Me.NotifyIcon1.BalloonTipClicked, AddressOf Me.OnBalloonTipClicked

    End Sub

    ' バルーンチップがクリックされたときの処理
    Private Sub OnBalloonTipClicked(sender As Object, e As EventArgs)
        Debug.WriteLine("### 通知がクリックされました。")
    End Sub

    ' フォームが閉じられる時にアイコンを非表示にする
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        Me.NotifyIcon1.Visible = False
        Me.NotifyIcon1.Dispose()
        MyBase.OnFormClosing(e)
    End Sub

End Class
