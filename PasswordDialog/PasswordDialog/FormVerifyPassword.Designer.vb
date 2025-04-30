<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormVerifyPassword
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label_Message = New System.Windows.Forms.Label()
        Me.Button_Ok = New System.Windows.Forms.Button()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.TextBox_Password = New System.Windows.Forms.TextBox()
        Me.Label_InputPassword = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label_Message)
        Me.Panel1.Controls.Add(Me.Button_Ok)
        Me.Panel1.Controls.Add(Me.Button_Cancel)
        Me.Panel1.Controls.Add(Me.TextBox_Password)
        Me.Panel1.Controls.Add(Me.Label_InputPassword)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(495, 121)
        Me.Panel1.TabIndex = 1
        '
        'Label_Message
        '
        Me.Label_Message.AutoSize = True
        Me.Label_Message.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label_Message.Location = New System.Drawing.Point(17, 79)
        Me.Label_Message.Name = "Label_Message"
        Me.Label_Message.Size = New System.Drawing.Size(79, 19)
        Me.Label_Message.TabIndex = 4
        Me.Label_Message.Text = "message"
        Me.Label_Message.Visible = False
        '
        'Button_Ok
        '
        Me.Button_Ok.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button_Ok.Location = New System.Drawing.Point(390, 39)
        Me.Button_Ok.Name = "Button_Ok"
        Me.Button_Ok.Size = New System.Drawing.Size(83, 26)
        Me.Button_Ok.TabIndex = 3
        Me.Button_Ok.Text = "OK"
        Me.Button_Ok.UseVisualStyleBackColor = True
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button_Cancel.Location = New System.Drawing.Point(281, 39)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(83, 26)
        Me.Button_Cancel.TabIndex = 2
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'TextBox_Password
        '
        Me.TextBox_Password.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox_Password.Location = New System.Drawing.Point(21, 39)
        Me.TextBox_Password.Name = "TextBox_Password"
        Me.TextBox_Password.Size = New System.Drawing.Size(212, 26)
        Me.TextBox_Password.TabIndex = 1
        '
        'Label_InputPassword
        '
        Me.Label_InputPassword.AutoSize = True
        Me.Label_InputPassword.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label_InputPassword.Location = New System.Drawing.Point(8, 8)
        Me.Label_InputPassword.Name = "Label_InputPassword"
        Me.Label_InputPassword.Size = New System.Drawing.Size(133, 19)
        Me.Label_InputPassword.TabIndex = 0
        Me.Label_InputPassword.Text = "Input Password"
        '
        'FormVerifyPassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(519, 145)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FormVerifyPassword"
        Me.Text = "Verify Password"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label_Message As Label
    Friend WithEvents Button_Ok As Button
    Friend WithEvents Button_Cancel As Button
    Friend WithEvents TextBox_Password As TextBox
    Friend WithEvents Label_InputPassword As Label
End Class
