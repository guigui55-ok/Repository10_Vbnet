Imports System.Threading

Module ModuleProgressBarForm
    Private progressBarHandlerA As DisplayProgressBar
    Private progressBarHandlerB As DisplayProgressBar
    Private cancellationTokenSource As CancellationTokenSource

    Public Class ProgressBarForm
        Dim _formMain As Form
        Dim _labelA As Label
        Dim _labelB As Label
        Dim _buttonStart As Button
        Dim _buttonStop As Button
        Dim _progressBarA As ProgressBar
        Dim _progressBarB As ProgressBar
        Public Sub New()
        End Sub

        Public Sub Initialize(
                formMain As Form,
                labelA As Label,
                labelB As Label,
                buttonStart As Button,
                buuttonStop As Button,
                progressBarA As ProgressBar,
                progressBarB As ProgressBar)
            _formMain = formMain
            _labelA = labelA
            _labelB = labelB
            _buttonStart = buttonStart
            _buttonStop = buuttonStop
            _progressBarA = progressBarA
            _progressBarB = progressBarB

            AddHandler _formMain.Load, AddressOf FormDisplayProgressBar_Load
            AddHandler _formMain.FormClosing, AddressOf FormDisplayProgressBar_FormClosing
            AddHandler _buttonStart.Click, AddressOf ButtonStart_Click
            AddHandler _buttonStop.Click, AddressOf ButtonStop_Click
        End Sub

        Private Sub FormDisplayProgressBar_Load(sender As Object, e As EventArgs)
            LogOutput("FormDisplayProgressBar_Load")
            ' DisplayProgressBar クラスのインスタンスを作成
            progressBarHandlerA = New DisplayProgressBar()
            progressBarHandlerB = New DisplayProgressBar()

            ' ProgressBar を初期化
            progressBarHandlerA.Initialize(_progressBarA, _labelA, "停止中A", "処理中A", max:=100)
            progressBarHandlerB.Initialize(_progressBarB, _labelB, "停止中B", "処理中B", max:=100)

            '//
            '_progressBarA.Style = ProgressBarStyle.Blocks
            '_progressBarB.Style = ProgressBarStyle.Blocks

            '//
            'https://dobon.net/vb/dotnet/control/pbmarquee.html
            'ProgressBarの特殊なスタイルとして、マーキースタイルのプログレスバー（indeterminate progress bar）があります。
            'マーキースタイルのプログレスバーでは、バーブロックが絶え間なく、連続して左から右に移動し続けます。
            ''ProgressBar1をマーキースタイルにする
            '_progressBarA.Style = ProgressBarStyle.Marquee
            ''ブロックの移動速度をデフォルトの倍にする
            '_progressBarA.MarqueeAnimationSpeed = 50

            '//
            '_progressBarA.Enabled = False
            '_progressBarA.TabStop = True
        End Sub

        Private Async Sub ButtonStart_Click(sender As Object, e As EventArgs)
            LogOutput("ButtonStart_Click")
            ' 二重実行を防ぐために、既存のタスクをキャンセル
            If cancellationTokenSource IsNot Nothing Then
                cancellationTokenSource.Cancel()
            End If

            ' 非同期処理用のキャンセルトークンを作成
            cancellationTokenSource = New CancellationTokenSource()

            Try
                ' 非同期で ProgressBar を更新
                Await UpdateProgressAsync(cancellationTokenSource.Token)
            Catch ex As OperationCanceledException
                ' キャンセルされた場合は無視
                LogOutput("ButtonStart_Click OperationCanceledException")
                progressBarHandlerA.SetProgress(0, "停止中")
            Catch ex As Exception
                MessageBox.Show($"エラーが発生しました: {ex.Message}")
            End Try
        End Sub

        Private Sub ButtonStop_Click(sender As Object, e As EventArgs)
            LogOutput("ButtonStop_Click")
            ' 非同期処理を停止
            If cancellationTokenSource IsNot Nothing Then
                cancellationTokenSource.Cancel()
                cancellationTokenSource = Nothing
            End If
        End Sub

        Private Async Function UpdateProgressAsync(token As CancellationToken) As Task
            LogOutput("UpdateProgressAsync")
            Dim progressValue As Integer = 0
            Dim stepValue As Integer = 10 ' 進捗の増加値

            Do
                If token.IsCancellationRequested Then Exit Do

                ' ProgressBar の進捗を設定
                progressBarHandlerA.SetProgress(progressValue, "処理中")

                ' 最大値に達したらリセット
                If progressValue >= 100 Then
                    LogOutput("UpdateProgressAsync progressValue >= 100")
                    progressValue = 0
                Else
                    progressValue += stepValue
                End If

                ' 一定間隔で更新
                Await Task.Delay(500, token)
            Loop
        End Function

        Private Sub FormDisplayProgressBar_FormClosing(sender As Object, e As FormClosingEventArgs)
            LogOutput("FormDisplayProgressBar_FormClosing")
            ' フォーム終了時に非同期処理を停止
            If cancellationTokenSource IsNot Nothing Then
                cancellationTokenSource.Cancel()
                cancellationTokenSource = Nothing
            End If
        End Sub

    End Class
End Module
