<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormOpenExePath
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxProcessName = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ProcessName"
        '
        'TextBoxProcessName
        '
        Me.TextBoxProcessName.Location = New System.Drawing.Point(102, 20)
        Me.TextBoxProcessName.Name = "TextBoxProcessName"
        Me.TextBoxProcessName.Size = New System.Drawing.Size(169, 19)
        Me.TextBoxProcessName.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(300, 18)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(101, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Open Explorer"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'FormOpenExePath
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(426, 71)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBoxProcessName)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FormOpenExePath"
        Me.Text = "OpenExePath"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxProcessName As TextBox
    Friend WithEvents Button1 As Button
End Class
