<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MessageNone
    Inherits System.Windows.Forms.UserControl

    'UserControl はコンポーネント一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MessageBase1 = New ChangeFormTest.MessageBase()
        Me.LabelMessage = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'MessageBase1
        '
        Me.MessageBase1.Location = New System.Drawing.Point(0, 0)
        Me.MessageBase1.Name = "MessageBase1"
        Me.MessageBase1.Size = New System.Drawing.Size(450, 120)
        Me.MessageBase1.TabIndex = 0
        '
        'LabelMessage
        '
        Me.LabelMessage.AutoSize = True
        Me.LabelMessage.Font = New System.Drawing.Font("Yu Gothic UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.LabelMessage.Location = New System.Drawing.Point(32, 40)
        Me.LabelMessage.Name = "LabelMessage"
        Me.LabelMessage.Size = New System.Drawing.Size(217, 21)
        Me.LabelMessage.TabIndex = 3
        Me.LabelMessage.Text = "メッセージ 未対応のステータスです"
        '
        'MessageNone
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelMessage)
        Me.Controls.Add(Me.MessageBase1)
        Me.Name = "MessageNone"
        Me.Size = New System.Drawing.Size(450, 120)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MessageBase1 As MessageBase
    Friend WithEvents LabelMessage As Label
End Class
