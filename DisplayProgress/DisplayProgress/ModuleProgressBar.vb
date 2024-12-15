Public Module ModuleProgressBar
    Public Class DisplayProgressBar
        Private targetProgressBar As ProgressBar

        ' ProgressBar の初期化
        Public Sub Initialize(progressBar As ProgressBar, Optional max As Integer = 100)
            Me.targetProgressBar = progressBar
            If targetProgressBar IsNot Nothing Then
                targetProgressBar.Minimum = 0
                targetProgressBar.Maximum = max
                targetProgressBar.Value = 0
            End If
        End Sub

        ' ProgressBar の値を設定
        Public Sub SetProgress(value As Integer)
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
