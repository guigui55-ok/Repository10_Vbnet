Module ModuleProgressStep

    Public Class ProgressStep
        Private targetPanel As Panel
        Private steps As Integer
        Private currentStep As Integer = 0
        Private stepLabels As List(Of Label)

        ' コンストラクタ
        Public Sub New()
            stepLabels = New List(Of Label)()
        End Sub

        ' 初期化
        Public Sub Initialize(panel As Panel, steps As Integer)
            If steps <= 0 Then Throw New ArgumentException("ステップ数は1以上である必要があります。")

            Me.targetPanel = panel
            Me.steps = steps

            ' パネルをクリア
            targetPanel.Controls.Clear()
            stepLabels.Clear()

            ' ステップのラベルを作成
            For i As Integer = 0 To steps - 1
                Dim lbl As New Label()
                lbl.AutoSize = True
                lbl.Font = New Font("Arial", 14, FontStyle.Bold)
                lbl.Text = "〇"
                lbl.Margin = New Padding(10, 0, 10, 0) ' スペースを調整
                stepLabels.Add(lbl)
                targetPanel.Controls.Add(lbl)

                ' スペース用のラベル（矢印「---」部分）
                If i < steps - 1 Then
                    Dim spacer As New Label()
                    spacer.AutoSize = True
                    spacer.Font = New Font("Arial", 14)
                    spacer.Text = "---"
                    targetPanel.Controls.Add(spacer)
                End If
            Next
            UpdateDisplay()
        End Sub

        ' 現在のステップを設定
        Public Sub SetCurrentStep(stepIndex As Integer)
            If stepIndex < 0 OrElse stepIndex >= steps Then
                Throw New ArgumentException("ステップ番号が無効です。")
            End If
            currentStep = stepIndex
            UpdateDisplay()
        End Sub

        ' 進捗表示を更新
        Private Sub UpdateDisplay()
            For i As Integer = 0 To stepLabels.Count - 1
                If i < currentStep Then
                    stepLabels(i).Text = "●" ' 完了
                    stepLabels(i).ForeColor = Color.Green
                ElseIf i = currentStep Then
                    stepLabels(i).Text = "■" ' 実行中
                    stepLabels(i).ForeColor = Color.Blue
                Else
                    stepLabels(i).Text = "〇" ' 未処理
                    stepLabels(i).ForeColor = Color.Gray
                End If
            Next
        End Sub
    End Class

End Module
