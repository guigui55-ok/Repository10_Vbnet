Public Module ModuleProgressBar
    Public Class DisplayProgressBar
        Private targetProgressBar As ProgressBar
        Public targetLabel As Label
        Private _initText As String
        Private _changeText As String

        Public Function getProgressBar() As ProgressBar
            Return targetProgressBar
        End Function

        ' ProgressBar の初期化
        Public Sub Initialize(progressBar As ProgressBar, label As Label, initText As String, changeText As String, Optional max As Integer = 100)
            Me.targetProgressBar = progressBar
            If targetProgressBar IsNot Nothing Then
                targetProgressBar.Minimum = 0
                targetProgressBar.Maximum = max
                targetProgressBar.Value = 0
                targetLabel = label
                _initText = initText
                _changeText = changeText
            End If
        End Sub

        ' ProgressBar の値を設定
        Public Sub SetProgress(value As Integer, Optional text As String = "")
            If targetProgressBar Is Nothing Then Throw New InvalidOperationException("ProgressBar が初期化されていません。")

            ' 安全に ProgressBar の値を更新
            UpdateProgressBarSafely(Sub()
                                        If value < targetProgressBar.Minimum Then
                                            targetProgressBar.Value = targetProgressBar.Minimum
                                        ElseIf value > targetProgressBar.Maximum Then
                                            targetProgressBar.Value = targetProgressBar.Maximum
                                        Else
                                            targetProgressBar.Value = value
                                        End If
                                    End Sub)
            If text <> "" Then
                targetLabel.Text = text
            End If
        End Sub

        ' ProgressBar を安全に更新
        Private Sub UpdateProgressBarSafely(updateAction As Action)
            If targetProgressBar.InvokeRequired Then
                targetProgressBar.Invoke(updateAction)
            Else
                updateAction()
            End If
        End Sub
    End Class


End Module
