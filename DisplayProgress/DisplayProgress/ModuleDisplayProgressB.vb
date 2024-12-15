Imports System.Threading

Public Module ModuleDisplayProgressB

    Public Class ProgressDisplayB
        Private targetControl As Control
        Private cancellationTokenSource As CancellationTokenSource
        Private displayAction As Action(Of String)
        Private updateInterval As Integer = 500 ' 更新間隔（ミリ秒）

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

        ' Initialize メソッド修正案
        Public Sub Initialize(
    control As Control,
    Optional interval As Integer = 500,
    Optional displayAction As Action(Of String) = Nothing)
            ' 対象コントロールを設定
            Me.targetControl = control
            Me.updateInterval = interval
            ' デフォルト動作を設定
            Me.displayAction = If(displayAction, Sub(text) UpdateControlText(text))
        End Sub


        Public Sub Start()
            ' 二重開始防止
            If _isRunning Then Return

            _isRunning = True
            cancellationTokenSource = New CancellationTokenSource()

            ' 非同期処理で進捗表示
            Task.Run(Function() UpdateProgressAsync(cancellationTokenSource.Token))
        End Sub

        '予約語の Stop を避けるために角括弧を使用
        '予約語の Stop を避けるために角括弧を使用
        Public Sub [Stop]()
            If Not _isRunning Then Return

            _isRunning = False
            If cancellationTokenSource IsNot Nothing Then
                cancellationTokenSource.Cancel()
                cancellationTokenSource.Dispose()
                cancellationTokenSource = Nothing
            End If

            ' 初期状態に戻す
            Try
                displayAction.Invoke(initText)
            Catch ex As Exception
                Debug.WriteLine($"停止時のエラー: {ex.Message}")
            End Try
        End Sub

        Private Async Function UpdateProgressAsync(token As CancellationToken) As Task
            Dim displayText As String = "実行中"
            Dim dotCount As Integer = 0

            Try
                Do
                    If token.IsCancellationRequested Then Exit Do

                    ' "."を追加またはリセット
                    If dotCount < 5 Then
                        dotCount += 1
                        displayAction.Invoke($"{displayText}{New String("."c, dotCount)}")
                    Else
                        dotCount = 0
                        displayAction.Invoke(displayText)
                    End If

                    If token.IsCancellationRequested Then Exit Do
                    ' 次の更新まで待機（キャンセル可能）
                    Await Task.Delay(updateInterval, token)
                Loop
            Catch ex As OperationCanceledException
                ' キャンセルされた場合は無視
                displayAction.Invoke($"エラー: {ex.Message}")
            Catch ex As Exception
                ' その他の例外をログ出力
                displayAction.Invoke($"エラー: {ex.Message}")
            Finally
                ' 最後に初期状態に戻す
                displayAction.Invoke(initText)
            End Try
        End Function

        Private Sub UpdateControlText(newText As String)
            If targetControl Is Nothing Then Return ' 対象コントロールが設定されていない場合は処理を中断

            If targetControl.InvokeRequired Then
                targetControl.Invoke(New Action(Sub() targetControl.Text = newText))
            Else
                targetControl.Text = newText
            End If
        End Sub
    End Class

End Module
