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
        Me.GroupBox_Remain = New System.Windows.Forms.GroupBox()
        Me.Label_Remain = New System.Windows.Forms.Label()
        Me.GroupBox_Quality = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox_Quality = New System.Windows.Forms.TextBox()
        Me.Button_Explorer = New System.Windows.Forms.Button()
        Me.GroupBox_WaitTIme = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox_WaitTime = New System.Windows.Forms.TextBox()
        Me.GroupBox_Event = New System.Windows.Forms.GroupBox()
        Me.RadioButton_MouseDown = New System.Windows.Forms.RadioButton()
        Me.RadioButton_MouseUp = New System.Windows.Forms.RadioButton()
        Me.GroupBox_Output.SuspendLayout()
        Me.GroupBox_Status.SuspendLayout()
        Me.GroupBox_Timeout.SuspendLayout()
        Me.GroupBox_Remain.SuspendLayout()
        Me.GroupBox_Quality.SuspendLayout()
        Me.GroupBox_WaitTIme.SuspendLayout()
        Me.GroupBox_Event.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_Output
        '
        Me.GroupBox_Output.Controls.Add(Me.TextBox_OutputDir)
        Me.GroupBox_Output.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_Output.Name = "GroupBox_Output"
        Me.GroupBox_Output.Size = New System.Drawing.Size(518, 59)
        Me.GroupBox_Output.TabIndex = 0
        Me.GroupBox_Output.TabStop = False
        Me.GroupBox_Output.Text = "Output Dir"
        '
        'TextBox_OutputDir
        '
        Me.TextBox_OutputDir.Location = New System.Drawing.Point(6, 22)
        Me.TextBox_OutputDir.Name = "TextBox_OutputDir"
        Me.TextBox_OutputDir.Size = New System.Drawing.Size(506, 23)
        Me.TextBox_OutputDir.TabIndex = 0
        '
        'Button_Execute
        '
        Me.Button_Execute.Location = New System.Drawing.Point(542, 92)
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
        Me.GroupBox_Status.Size = New System.Drawing.Size(184, 52)
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
        Me.TextBox_Timeout.Size = New System.Drawing.Size(91, 23)
        Me.TextBox_Timeout.TabIndex = 1
        '
        'GroupBox_Timeout
        '
        Me.GroupBox_Timeout.Controls.Add(Me.Label1)
        Me.GroupBox_Timeout.Controls.Add(Me.TextBox_Timeout)
        Me.GroupBox_Timeout.Location = New System.Drawing.Point(211, 81)
        Me.GroupBox_Timeout.Name = "GroupBox_Timeout"
        Me.GroupBox_Timeout.Size = New System.Drawing.Size(184, 52)
        Me.GroupBox_Timeout.TabIndex = 3
        Me.GroupBox_Timeout.TabStop = False
        Me.GroupBox_Timeout.Text = "Timeout"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(115, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 15)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "[second]"
        '
        'GroupBox_Remain
        '
        Me.GroupBox_Remain.Controls.Add(Me.Label_Remain)
        Me.GroupBox_Remain.Location = New System.Drawing.Point(405, 81)
        Me.GroupBox_Remain.Name = "GroupBox_Remain"
        Me.GroupBox_Remain.Size = New System.Drawing.Size(125, 52)
        Me.GroupBox_Remain.TabIndex = 4
        Me.GroupBox_Remain.TabStop = False
        Me.GroupBox_Remain.Text = "Remain"
        '
        'Label_Remain
        '
        Me.Label_Remain.AutoSize = True
        Me.Label_Remain.Location = New System.Drawing.Point(18, 23)
        Me.Label_Remain.Name = "Label_Remain"
        Me.Label_Remain.Size = New System.Drawing.Size(59, 15)
        Me.Label_Remain.TabIndex = 3
        Me.Label_Remain.Text = "00:00:00"
        '
        'GroupBox_Quality
        '
        Me.GroupBox_Quality.Controls.Add(Me.Label2)
        Me.GroupBox_Quality.Controls.Add(Me.TextBox_Quality)
        Me.GroupBox_Quality.Location = New System.Drawing.Point(18, 139)
        Me.GroupBox_Quality.Name = "GroupBox_Quality"
        Me.GroupBox_Quality.Size = New System.Drawing.Size(184, 52)
        Me.GroupBox_Quality.TabIndex = 5
        Me.GroupBox_Quality.TabStop = False
        Me.GroupBox_Quality.Text = "Quality"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(115, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "[0~100]"
        '
        'TextBox_Quality
        '
        Me.TextBox_Quality.Location = New System.Drawing.Point(18, 20)
        Me.TextBox_Quality.Name = "TextBox_Quality"
        Me.TextBox_Quality.Size = New System.Drawing.Size(91, 23)
        Me.TextBox_Quality.TabIndex = 1
        '
        'Button_Explorer
        '
        Me.Button_Explorer.Location = New System.Drawing.Point(542, 25)
        Me.Button_Explorer.Name = "Button_Explorer"
        Me.Button_Explorer.Size = New System.Drawing.Size(101, 38)
        Me.Button_Explorer.TabIndex = 6
        Me.Button_Explorer.Text = "Explorer"
        Me.Button_Explorer.UseVisualStyleBackColor = True
        '
        'GroupBox_WaitTIme
        '
        Me.GroupBox_WaitTIme.Controls.Add(Me.Label3)
        Me.GroupBox_WaitTIme.Controls.Add(Me.TextBox_WaitTime)
        Me.GroupBox_WaitTIme.Location = New System.Drawing.Point(211, 139)
        Me.GroupBox_WaitTIme.Name = "GroupBox_WaitTIme"
        Me.GroupBox_WaitTIme.Size = New System.Drawing.Size(184, 52)
        Me.GroupBox_WaitTIme.TabIndex = 6
        Me.GroupBox_WaitTIme.TabStop = False
        Me.GroupBox_WaitTIme.Text = "Wait TIme"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(115, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "[ms]"
        '
        'TextBox_WaitTime
        '
        Me.TextBox_WaitTime.Location = New System.Drawing.Point(18, 20)
        Me.TextBox_WaitTime.Name = "TextBox_WaitTime"
        Me.TextBox_WaitTime.Size = New System.Drawing.Size(91, 23)
        Me.TextBox_WaitTime.TabIndex = 1
        '
        'GroupBox_Event
        '
        Me.GroupBox_Event.Controls.Add(Me.RadioButton_MouseUp)
        Me.GroupBox_Event.Controls.Add(Me.RadioButton_MouseDown)
        Me.GroupBox_Event.Location = New System.Drawing.Point(405, 139)
        Me.GroupBox_Event.Name = "GroupBox_Event"
        Me.GroupBox_Event.Size = New System.Drawing.Size(238, 52)
        Me.GroupBox_Event.TabIndex = 5
        Me.GroupBox_Event.TabStop = False
        Me.GroupBox_Event.Text = "Event"
        '
        'RadioButton_MouseDown
        '
        Me.RadioButton_MouseDown.AutoSize = True
        Me.RadioButton_MouseDown.Location = New System.Drawing.Point(16, 23)
        Me.RadioButton_MouseDown.Name = "RadioButton_MouseDown"
        Me.RadioButton_MouseDown.Size = New System.Drawing.Size(95, 19)
        Me.RadioButton_MouseDown.TabIndex = 0
        Me.RadioButton_MouseDown.TabStop = True
        Me.RadioButton_MouseDown.Text = "MouseDown"
        Me.RadioButton_MouseDown.UseVisualStyleBackColor = True
        '
        'RadioButton_MouseUp
        '
        Me.RadioButton_MouseUp.AutoSize = True
        Me.RadioButton_MouseUp.Location = New System.Drawing.Point(137, 23)
        Me.RadioButton_MouseUp.Name = "RadioButton_MouseUp"
        Me.RadioButton_MouseUp.Size = New System.Drawing.Size(78, 19)
        Me.RadioButton_MouseUp.TabIndex = 1
        Me.RadioButton_MouseUp.TabStop = True
        Me.RadioButton_MouseUp.Text = "MouseUp"
        Me.RadioButton_MouseUp.UseVisualStyleBackColor = True
        '
        'FormScreenshotSaver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 200)
        Me.Controls.Add(Me.GroupBox_Event)
        Me.Controls.Add(Me.GroupBox_WaitTIme)
        Me.Controls.Add(Me.Button_Explorer)
        Me.Controls.Add(Me.GroupBox_Quality)
        Me.Controls.Add(Me.GroupBox_Remain)
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
        Me.GroupBox_Remain.ResumeLayout(False)
        Me.GroupBox_Remain.PerformLayout()
        Me.GroupBox_Quality.ResumeLayout(False)
        Me.GroupBox_Quality.PerformLayout()
        Me.GroupBox_WaitTIme.ResumeLayout(False)
        Me.GroupBox_WaitTIme.PerformLayout()
        Me.GroupBox_Event.ResumeLayout(False)
        Me.GroupBox_Event.PerformLayout()
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
    Friend WithEvents GroupBox_Remain As GroupBox
    Friend WithEvents Label_Remain As Label
    Friend WithEvents GroupBox_Quality As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox_Quality As TextBox
    Friend WithEvents Button_Explorer As Button
    Friend WithEvents GroupBox_WaitTIme As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox_WaitTime As TextBox
    Friend WithEvents GroupBox_Event As GroupBox
    Friend WithEvents RadioButton_MouseUp As RadioButton
    Friend WithEvents RadioButton_MouseDown As RadioButton
End Class
