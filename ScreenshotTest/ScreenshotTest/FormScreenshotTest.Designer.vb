<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormScreenshotTest
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TextBox_ProcessName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(34, 26)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(121, 37)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "アプリの画面保存"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(181, 26)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(113, 37)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "画面全体保存"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(181, 75)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(113, 37)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "他のプロセス"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'TextBox_ProcessName
        '
        Me.TextBox_ProcessName.Location = New System.Drawing.Point(34, 93)
        Me.TextBox_ProcessName.Name = "TextBox_ProcessName"
        Me.TextBox_ProcessName.Size = New System.Drawing.Size(121, 19)
        Me.TextBox_ProcessName.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 12)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "プロセス名(exe不要)："
        '
        'FormScreenshotTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(337, 145)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox_ProcessName)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "FormScreenshotTest"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents TextBox_ProcessName As TextBox
    Friend WithEvents Label1 As Label
End Class
