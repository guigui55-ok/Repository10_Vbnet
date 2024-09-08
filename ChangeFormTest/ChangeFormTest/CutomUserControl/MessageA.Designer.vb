<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MessageA
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
        Me.LabelMessage = New System.Windows.Forms.Label()
        Me.MessageBase1 = New ChangeFormTest.MessageBase()
        Me.SuspendLayout()
        '
        'LabelMessage
        '
        Me.LabelMessage.AutoSize = True
        Me.LabelMessage.Font = New System.Drawing.Font("Yu Gothic UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.LabelMessage.Location = New System.Drawing.Point(31, 38)
        Me.LabelMessage.Name = "LabelMessage"
        Me.LabelMessage.Size = New System.Drawing.Size(78, 21)
        Me.LabelMessage.TabIndex = 1
        Me.LabelMessage.Text = "メッセージA"
        '
        'MessageBase1
        '
        Me.MessageBase1.Font = New System.Drawing.Font("Yu Gothic UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.MessageBase1.Location = New System.Drawing.Point(0, 0)
        Me.MessageBase1.Name = "MessageBase1"
        Me.MessageBase1.Size = New System.Drawing.Size(450, 120)
        Me.MessageBase1.TabIndex = 0
        '
        'MessageA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelMessage)
        Me.Controls.Add(Me.MessageBase1)
        Me.Name = "MessageA"
        Me.Size = New System.Drawing.Size(450, 120)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelMessage As Label
    Friend WithEvents MessageBase1 As MessageBase
End Class
