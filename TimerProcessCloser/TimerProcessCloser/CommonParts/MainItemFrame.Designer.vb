<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainItemFrame
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
        Me.GroupBoxItemFrame = New System.Windows.Forms.GroupBox()
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.ComboBoxNotification = New System.Windows.Forms.ComboBox()
        Me.LabelNotification = New System.Windows.Forms.Label()
        Me.ButtonStop = New System.Windows.Forms.Button()
        Me.ButtonStartOrPause = New System.Windows.Forms.Button()
        Me.TextBoxTimerTime = New System.Windows.Forms.TextBox()
        Me.LabelTimerTIme = New System.Windows.Forms.Label()
        Me.LabelRemainingTimeDisp = New System.Windows.Forms.Label()
        Me.LabelRemainingTime = New System.Windows.Forms.Label()
        Me.TextBoxProcessName = New System.Windows.Forms.TextBox()
        Me.LabelProcessName = New System.Windows.Forms.Label()
        Me.CheckBoxAutoRun = New System.Windows.Forms.CheckBox()
        Me.GroupBoxItemFrame.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBoxItemFrame
        '
        Me.GroupBoxItemFrame.Controls.Add(Me.CheckBoxAutoRun)
        Me.GroupBoxItemFrame.Controls.Add(Me.LabelStatus)
        Me.GroupBoxItemFrame.Controls.Add(Me.ComboBoxNotification)
        Me.GroupBoxItemFrame.Controls.Add(Me.LabelNotification)
        Me.GroupBoxItemFrame.Controls.Add(Me.ButtonStop)
        Me.GroupBoxItemFrame.Controls.Add(Me.ButtonStartOrPause)
        Me.GroupBoxItemFrame.Controls.Add(Me.TextBoxTimerTime)
        Me.GroupBoxItemFrame.Controls.Add(Me.LabelTimerTIme)
        Me.GroupBoxItemFrame.Controls.Add(Me.LabelRemainingTimeDisp)
        Me.GroupBoxItemFrame.Controls.Add(Me.LabelRemainingTime)
        Me.GroupBoxItemFrame.Controls.Add(Me.TextBoxProcessName)
        Me.GroupBoxItemFrame.Controls.Add(Me.LabelProcessName)
        Me.GroupBoxItemFrame.Location = New System.Drawing.Point(0, 0)
        Me.GroupBoxItemFrame.Name = "GroupBoxItemFrame"
        Me.GroupBoxItemFrame.Size = New System.Drawing.Size(543, 108)
        Me.GroupBoxItemFrame.TabIndex = 0
        Me.GroupBoxItemFrame.TabStop = False
        Me.GroupBoxItemFrame.Text = "Item1"
        '
        'LabelStatus
        '
        Me.LabelStatus.AutoSize = True
        Me.LabelStatus.Location = New System.Drawing.Point(6, 89)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(41, 15)
        Me.LabelStatus.TabIndex = 10
        Me.LabelStatus.Text = "Label1"
        '
        'ComboBoxNotification
        '
        Me.ComboBoxNotification.FormattingEnabled = True
        Me.ComboBoxNotification.Location = New System.Drawing.Point(376, 37)
        Me.ComboBoxNotification.Name = "ComboBoxNotification"
        Me.ComboBoxNotification.Size = New System.Drawing.Size(152, 23)
        Me.ComboBoxNotification.TabIndex = 9
        '
        'LabelNotification
        '
        Me.LabelNotification.AutoSize = True
        Me.LabelNotification.Location = New System.Drawing.Point(294, 40)
        Me.LabelNotification.Name = "LabelNotification"
        Me.LabelNotification.Size = New System.Drawing.Size(76, 15)
        Me.LabelNotification.TabIndex = 8
        Me.LabelNotification.Text = "Notification :"
        '
        'ButtonStop
        '
        Me.ButtonStop.Location = New System.Drawing.Point(456, 71)
        Me.ButtonStop.Name = "ButtonStop"
        Me.ButtonStop.Size = New System.Drawing.Size(72, 22)
        Me.ButtonStop.TabIndex = 7
        Me.ButtonStop.Text = "Stop"
        Me.ButtonStop.UseVisualStyleBackColor = True
        '
        'ButtonStartOrPause
        '
        Me.ButtonStartOrPause.Location = New System.Drawing.Point(376, 71)
        Me.ButtonStartOrPause.Name = "ButtonStartOrPause"
        Me.ButtonStartOrPause.Size = New System.Drawing.Size(72, 22)
        Me.ButtonStartOrPause.TabIndex = 6
        Me.ButtonStartOrPause.Text = "Start"
        Me.ButtonStartOrPause.UseVisualStyleBackColor = True
        '
        'TextBoxTimerTime
        '
        Me.TextBoxTimerTime.Location = New System.Drawing.Point(124, 40)
        Me.TextBoxTimerTime.Name = "TextBoxTimerTime"
        Me.TextBoxTimerTime.Size = New System.Drawing.Size(152, 23)
        Me.TextBoxTimerTime.TabIndex = 5
        Me.TextBoxTimerTime.Text = "1:00:00"
        '
        'LabelTimerTIme
        '
        Me.LabelTimerTIme.AutoSize = True
        Me.LabelTimerTIme.Location = New System.Drawing.Point(6, 43)
        Me.LabelTimerTIme.Name = "LabelTimerTIme"
        Me.LabelTimerTIme.Size = New System.Drawing.Size(102, 15)
        Me.LabelTimerTIme.TabIndex = 4
        Me.LabelTimerTIme.Text = "Time (hh:mm:ss) : "
        '
        'LabelRemainingTimeDisp
        '
        Me.LabelRemainingTimeDisp.AutoSize = True
        Me.LabelRemainingTimeDisp.Location = New System.Drawing.Point(397, 17)
        Me.LabelRemainingTimeDisp.Name = "LabelRemainingTimeDisp"
        Me.LabelRemainingTimeDisp.Size = New System.Drawing.Size(49, 15)
        Me.LabelRemainingTimeDisp.TabIndex = 3
        Me.LabelRemainingTimeDisp.Text = "00:000:0"
        '
        'LabelRemainingTime
        '
        Me.LabelRemainingTime.AutoSize = True
        Me.LabelRemainingTime.Location = New System.Drawing.Point(294, 17)
        Me.LabelRemainingTime.Name = "LabelRemainingTime"
        Me.LabelRemainingTime.Size = New System.Drawing.Size(97, 15)
        Me.LabelRemainingTime.TabIndex = 2
        Me.LabelRemainingTime.Text = "Remaining Time :"
        '
        'TextBoxProcessName
        '
        Me.TextBoxProcessName.Location = New System.Drawing.Point(124, 14)
        Me.TextBoxProcessName.Name = "TextBoxProcessName"
        Me.TextBoxProcessName.Size = New System.Drawing.Size(152, 23)
        Me.TextBoxProcessName.TabIndex = 1
        '
        'LabelProcessName
        '
        Me.LabelProcessName.AutoSize = True
        Me.LabelProcessName.Location = New System.Drawing.Point(6, 17)
        Me.LabelProcessName.Name = "LabelProcessName"
        Me.LabelProcessName.Size = New System.Drawing.Size(112, 15)
        Me.LabelProcessName.TabIndex = 0
        Me.LabelProcessName.Text = "LabelProcessName :"
        '
        'CheckBoxAutoRun
        '
        Me.CheckBoxAutoRun.AutoSize = True
        Me.CheckBoxAutoRun.Location = New System.Drawing.Point(6, 67)
        Me.CheckBoxAutoRun.Name = "CheckBoxAutoRun"
        Me.CheckBoxAutoRun.Size = New System.Drawing.Size(242, 19)
        Me.CheckBoxAutoRun.TabIndex = 11
        Me.CheckBoxAutoRun.Text = "Auto Run Timer at Startup (起動時に実行)"
        Me.CheckBoxAutoRun.UseVisualStyleBackColor = True
        '
        'MainItemFrame
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBoxItemFrame)
        Me.Name = "MainItemFrame"
        Me.Size = New System.Drawing.Size(549, 141)
        Me.GroupBoxItemFrame.ResumeLayout(False)
        Me.GroupBoxItemFrame.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBoxItemFrame As GroupBox
    Friend WithEvents ButtonStartOrPause As Button
    Friend WithEvents TextBoxTimerTime As TextBox
    Friend WithEvents LabelTimerTIme As Label
    Friend WithEvents LabelRemainingTimeDisp As Label
    Friend WithEvents LabelRemainingTime As Label
    Friend WithEvents TextBoxProcessName As TextBox
    Friend WithEvents LabelProcessName As Label
    Friend WithEvents ComboBoxNotification As ComboBox
    Friend WithEvents LabelNotification As Label
    Friend WithEvents ButtonStop As Button
    Friend WithEvents LabelStatus As Label
    Friend WithEvents CheckBoxAutoRun As CheckBox
End Class
