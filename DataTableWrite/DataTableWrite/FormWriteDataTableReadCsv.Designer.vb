<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormWriteDataTableReadCsv
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
        Me.Label_Paths = New System.Windows.Forms.Label()
        Me.RichTextBox_Paths = New System.Windows.Forms.RichTextBox()
        Me.Label_Conditions = New System.Windows.Forms.Label()
        Me.RichTextBox_Conditions = New System.Windows.Forms.RichTextBox()
        Me.Button_Write = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label_Paths
        '
        Me.Label_Paths.AutoSize = True
        Me.Label_Paths.Location = New System.Drawing.Point(12, 9)
        Me.Label_Paths.Name = "Label_Paths"
        Me.Label_Paths.Size = New System.Drawing.Size(44, 12)
        Me.Label_Paths.TabIndex = 0
        Me.Label_Paths.Text = "Paths : "
        '
        'RichTextBox_Paths
        '
        Me.RichTextBox_Paths.Location = New System.Drawing.Point(14, 24)
        Me.RichTextBox_Paths.Name = "RichTextBox_Paths"
        Me.RichTextBox_Paths.Size = New System.Drawing.Size(651, 118)
        Me.RichTextBox_Paths.TabIndex = 1
        Me.RichTextBox_Paths.Text = ""
        '
        'Label_Conditions
        '
        Me.Label_Conditions.AutoSize = True
        Me.Label_Conditions.Location = New System.Drawing.Point(12, 154)
        Me.Label_Conditions.Name = "Label_Conditions"
        Me.Label_Conditions.Size = New System.Drawing.Size(334, 12)
        Me.Label_Conditions.TabIndex = 2
        Me.Label_Conditions.Text = "Conditions(MatchRegex) [""column regex"" = ""data filter regex""] "
        '
        'RichTextBox_Conditions
        '
        Me.RichTextBox_Conditions.Location = New System.Drawing.Point(12, 169)
        Me.RichTextBox_Conditions.Name = "RichTextBox_Conditions"
        Me.RichTextBox_Conditions.Size = New System.Drawing.Size(651, 98)
        Me.RichTextBox_Conditions.TabIndex = 3
        Me.RichTextBox_Conditions.Text = ""
        '
        'Button_Write
        '
        Me.Button_Write.Location = New System.Drawing.Point(588, 273)
        Me.Button_Write.Name = "Button_Write"
        Me.Button_Write.Size = New System.Drawing.Size(75, 23)
        Me.Button_Write.TabIndex = 4
        Me.Button_Write.Text = "Write"
        Me.Button_Write.UseVisualStyleBackColor = True
        '
        'FormWriteDataTableReadCsv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(675, 314)
        Me.Controls.Add(Me.Button_Write)
        Me.Controls.Add(Me.RichTextBox_Conditions)
        Me.Controls.Add(Me.Label_Conditions)
        Me.Controls.Add(Me.RichTextBox_Paths)
        Me.Controls.Add(Me.Label_Paths)
        Me.Name = "FormWriteDataTableReadCsv"
        Me.Text = "WriteDataTableReadCsv"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label_Paths As Label
    Friend WithEvents RichTextBox_Paths As RichTextBox
    Friend WithEvents Label_Conditions As Label
    Friend WithEvents RichTextBox_Conditions As RichTextBox
    Friend WithEvents Button_Write As Button
End Class
