Imports System.Drawing
Imports System.Timers
Imports System.Windows.Forms


'Newtonsoft.Json
'System.Drawing.Common
'をインストール済み

Module Module1

    Private notifier As DesknetNotifier
    Private emailCheckTimer As System.Timers.Timer ' System.Timers.Timer を明示的に指定
    Private CHECK_INTERVAL = 60000 ' 60000ms = 1分
    Private CHECK_INTERVAL_B = 5000

    Sub Main()
        ' NotifyIconを初期化
        Dim notifyIcon As New System.Windows.Forms.NotifyIcon() With { ' System.Windows.Forms.NotifyIcon を明示的に指定
            .Icon = System.Drawing.SystemIcons.Information, ' System.Drawing.SystemIcons を明示的に指定
            .Visible = True
        }

        ' DesknetNotifierを初期化
        notifier = New DesknetNotifier() With {
            .NotifyIcon1 = notifyIcon
        }

        ' Timerを設定（1分間隔）
        emailCheckTimer = New System.Timers.Timer(CHECK_INTERVAL_B)
        AddHandler emailCheckTimer.Elapsed, AddressOf CheckEmails
        emailCheckTimer.Start()

        ' コンソールアプリを終了させないように待機
        LogOutput("Desknet's新着メールチェック中... (終了するにはCtrl+Cを押してください)")
        Application.Run()
    End Sub

    ' タイマーで新着メールチェックを実行
    Private Sub CheckEmails(sender As Object, e As System.Timers.ElapsedEventArgs)
        LogOutput("Call CheckEmails")
        notifier.CheckEmails()
    End Sub

    Public Sub LogOutput(value)
        Dim buf = String.Format("{0}", value)
        buf = DateTime.Now.ToString() + "   " + buf
        Debug.WriteLine(buf)
        Console.WriteLine(buf)
    End Sub

End Module