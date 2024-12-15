Imports System.Threading

Module ModuleDisplayProgress

    Public Class ProgressDisplay
        'Inherits ProgressDisplayA
        Inherits ProgressDisplayB

    End Class


    Public Class ProgressDisplayA
        Private targetControl As Control
        Private cancellationTokenSource As CancellationTokenSource

        Private _isRunning As Boolean = False
        ' isRunning を外部から参照するためのプロパティ
        Public ReadOnly Property IsRunning As Boolean
            Get
                Return _isRunning
            End Get
        End Property

        Public initText As String = "init"


        Public Sub New()

        End Sub

        Public Sub Initialize(control As Control)
            ' 対象コントロールを設定
            Me.targetControl = control
        End Sub

        Public Sub Start()
            ' 二重開始防止
            If isRunning Then Return

            _isRunning = True
            cancellationTokenSource = New CancellationTokenSource()

            ' 非同期処理で進捗表示
            Task.Run(Sub() UpdateProgress(cancellationTokenSource.Token))
        End Sub

        'プロジェクト内で独自のメソッドやプロパティに予約語と同じ名前を付けたい場合、
        'その名前を [] で囲むことで、VB.NET の予約語としてではなくユーザー定義の名前として扱えるようになります。
        Public Sub [Stop]()
            ' 停止
            If Not isRunning Then Return

            _isRunning = False
            cancellationTokenSource.Cancel()
            Me.targetControl.Text = initText
        End Sub

        Private Sub UpdateProgress(token As CancellationToken)
            Dim displayText As String = "実行中"
            Dim dotCount As Integer = 0

            Try
                Do
                    If token.IsCancellationRequested Then Exit Do

                    ' "."を追加またはリセット
                    If dotCount < 5 Then
                        dotCount += 1
                        UpdateControlText($"{displayText}{New String("."c, dotCount)}")
                    Else
                        dotCount = 0
                        UpdateControlText(displayText)
                    End If

                    ' 次の更新まで500ms待機
                    Task.Delay(500, token).Wait()
                Loop
            Catch ex As OperationCanceledException
                ' キャンセルされた場合の例外を無視
            Finally
                ' 最後に元の状態に戻す
                UpdateControlText(displayText)
            End Try
        End Sub

        Private Sub UpdateControlText(newText As String)
            If targetControl.InvokeRequired Then
                targetControl.Invoke(New Action(Sub() targetControl.Text = newText))
            Else
                targetControl.Text = newText
            End If
        End Sub
    End Class
End Module
