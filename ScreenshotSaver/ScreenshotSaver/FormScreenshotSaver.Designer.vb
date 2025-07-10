<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormScreenshotSaver
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
        Me.GroupBox_Output = New System.Windows.Forms.GroupBox()
        Me.TextBox_OutputDir = New System.Windows.Forms.TextBox()
        Me.Button_Execute = New System.Windows.Forms.Button()
        Me.GroupBox_Status = New System.Windows.Forms.GroupBox()
        Me.Label_Status = New System.Windows.Forms.Label()
        Me.TextBox_Timeout = New System.Windows.Forms.TextBox()
        Me.GroupBox_Timeout = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox_Output.SuspendLayout()
        Me.GroupBox_Status.SuspendLayout()
        Me.GroupBox_Timeout.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_Output
        '
        Me.GroupBox_Output.Controls.Add(Me.TextBox_OutputDir)
        Me.GroupBox_Output.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_Output.Name = "GroupBox_Output"
        Me.GroupBox_Output.Size = New System.Drawing.Size(631, 59)
        Me.GroupBox_Output.TabIndex = 0
        Me.GroupBox_Output.TabStop = False
        Me.GroupBox_Output.Text = "Output Dir"
        '
        'TextBox_OutputDir
        '
        Me.TextBox_OutputDir.Location = New System.Drawing.Point(6, 22)
        Me.TextBox_OutputDir.Name = "TextBox_OutputDir"
        Me.TextBox_OutputDir.Size = New System.Drawing.Size(619, 23)
        Me.TextBox_OutputDir.TabIndex = 0
        '
        'Button_Execute
        '
        Me.Button_Execute.Location = New System.Drawing.Point(536, 92)
        Me.Button_Execute.Name = "Button_Execute"
        Me.Button_Execute.Size = New System.Drawing.Size(101, 38)
        Me.Button_Execute.TabIndex = 1
        Me.Button_Execute.Text = "Start"
        Me.Button_Execute.UseVisualStyleBackColor = True
        '
        'GroupBox_Status
        '
        Me.GroupBox_Status.Controls.Add(Me.Label_Status)
        Me.GroupBox_Status.Location = New System.Drawing.Point(18, 81)
        Me.GroupBox_Status.Name = "GroupBox_Status"
        Me.GroupBox_Status.Size = New System.Drawing.Size(262, 52)
        Me.GroupBox_Status.TabIndex = 2
        Me.GroupBox_Status.TabStop = False
        Me.GroupBox_Status.Text = "Status"
        '
        'Label_Status
        '
        Me.Label_Status.AutoSize = True
        Me.Label_Status.Location = New System.Drawing.Point(18, 23)
        Me.Label_Status.Name = "Label_Status"
        Me.Label_Status.Size = New System.Drawing.Size(45, 15)
        Me.Label_Status.TabIndex = 3
        Me.Label_Status.Text = "Label1"
        '
        'TextBox_Timeout
        '
        Me.TextBox_Timeout.Location = New System.Drawing.Point(18, 20)
        Me.TextBox_Timeout.Name = "TextBox_Timeout"
        Me.TextBox_Timeout.Size = New System.Drawing.Size(130, 23)
        Me.TextBox_Timeout.TabIndex = 1
        '
        'GroupBox_Timeout
        '
        Me.GroupBox_Timeout.Controls.Add(Me.Label1)
        Me.GroupBox_Timeout.Controls.Add(Me.TextBox_Timeout)
        Me.GroupBox_Timeout.Location = New System.Drawing.Point(295, 81)
        Me.GroupBox_Timeout.Name = "GroupBox_Timeout"
        Me.GroupBox_Timeout.Size = New System.Drawing.Size(200, 52)
        Me.GroupBox_Timeout.TabIndex = 3
        Me.GroupBox_Timeout.TabStop = False
        Me.GroupBox_Timeout.Text = "Timeout"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(154, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 15)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "[ms]"
        '
        'FormScreenshotSaver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 145)
        Me.Controls.Add(Me.GroupBox_Timeout)
        Me.Controls.Add(Me.GroupBox_Status)
        Me.Controls.Add(Me.Button_Execute)
        Me.Controls.Add(Me.GroupBox_Output)
        Me.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FormScreenshotSaver"
        Me.Text = "ScreenshotSaver"
        Me.GroupBox_Output.ResumeLayout(False)
        Me.GroupBox_Output.PerformLayout()
        Me.GroupBox_Status.ResumeLayout(False)
        Me.GroupBox_Status.PerformLayout()
        Me.GroupBox_Timeout.ResumeLayout(False)
        Me.GroupBox_Timeout.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox_Output As GroupBox
    Friend WithEvents TextBox_OutputDir As TextBox
    Friend WithEvents Button_Execute As Button
    Friend WithEvents GroupBox_Status As GroupBox
    Friend WithEvents Label_Status As Label
    Friend WithEvents TextBox_Timeout As TextBox
    Friend WithEvents GroupBox_Timeout As GroupBox
    Friend WithEvents Label1 As Label
End Class
