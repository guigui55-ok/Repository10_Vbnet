<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLog
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
        Me.GroupBox_Log = New System.Windows.Forms.GroupBox()
        Me.RichTextBox_Log = New System.Windows.Forms.RichTextBox()
        Me.GroupBox_Log.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_Log
        '
        Me.GroupBox_Log.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Log.Controls.Add(Me.RichTextBox_Log)
        Me.GroupBox_Log.Location = New System.Drawing.Point(16, 17)
        Me.GroupBox_Log.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox_Log.Name = "GroupBox_Log"
        Me.GroupBox_Log.Padding = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.GroupBox_Log.Size = New System.Drawing.Size(796, 182)
        Me.GroupBox_Log.TabIndex = 0
        Me.GroupBox_Log.TabStop = False
        Me.GroupBox_Log.Text = "Log"
        '
        'RichTextBox_Log
        '
        Me.RichTextBox_Log.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox_Log.Location = New System.Drawing.Point(6, 25)
        Me.RichTextBox_Log.Name = "RichTextBox_Log"
        Me.RichTextBox_Log.Size = New System.Drawing.Size(784, 149)
        Me.RichTextBox_Log.TabIndex = 0
        Me.RichTextBox_Log.Text = ""
        '
        'FormLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(824, 213)
        Me.Controls.Add(Me.GroupBox_Log)
        Me.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Name = "FormLog"
        Me.Text = "FormLog"
        Me.GroupBox_Log.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox_Log As GroupBox
    Friend WithEvents RichTextBox_Log As RichTextBox
End Class
