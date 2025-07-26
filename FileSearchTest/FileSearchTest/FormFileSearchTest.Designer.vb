<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormFileSearchTest
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox_TargetDir = New System.Windows.Forms.TextBox()
        Me.Button_Start = New System.Windows.Forms.Button()
        Me.GroupBox_Log = New System.Windows.Forms.GroupBox()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.GroupBox_Settings = New System.Windows.Forms.GroupBox()
        Me.TextBox_Filter = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.GroupBox_UiThread = New System.Windows.Forms.GroupBox()
        Me.TextBox_Count = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ButtonUp = New System.Windows.Forms.Button()
        Me.ButtonDown = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox_Log.SuspendLayout()
        Me.GroupBox_Settings.SuspendLayout()
        Me.GroupBox_UiThread.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox_TargetDir)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(770, 64)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Target Dir"
        '
        'TextBox_TargetDir
        '
        Me.TextBox_TargetDir.Location = New System.Drawing.Point(6, 22)
        Me.TextBox_TargetDir.Name = "TextBox_TargetDir"
        Me.TextBox_TargetDir.Size = New System.Drawing.Size(758, 23)
        Me.TextBox_TargetDir.TabIndex = 0
        '
        'Button_Start
        '
        Me.Button_Start.Location = New System.Drawing.Point(670, 82)
        Me.Button_Start.Name = "Button_Start"
        Me.Button_Start.Size = New System.Drawing.Size(112, 36)
        Me.Button_Start.TabIndex = 1
        Me.Button_Start.Text = "Execute"
        Me.Button_Start.UseVisualStyleBackColor = True
        '
        'GroupBox_Log
        '
        Me.GroupBox_Log.Controls.Add(Me.RichTextBox1)
        Me.GroupBox_Log.Location = New System.Drawing.Point(12, 263)
        Me.GroupBox_Log.Name = "GroupBox_Log"
        Me.GroupBox_Log.Size = New System.Drawing.Size(764, 156)
        Me.GroupBox_Log.TabIndex = 2
        Me.GroupBox_Log.TabStop = False
        Me.GroupBox_Log.Text = "Log"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(6, 22)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(752, 128)
        Me.RichTextBox1.TabIndex = 0
        Me.RichTextBox1.Text = ""
        '
        'GroupBox_Settings
        '
        Me.GroupBox_Settings.Controls.Add(Me.TextBox_Filter)
        Me.GroupBox_Settings.Controls.Add(Me.Label1)
        Me.GroupBox_Settings.Location = New System.Drawing.Point(12, 82)
        Me.GroupBox_Settings.Name = "GroupBox_Settings"
        Me.GroupBox_Settings.Size = New System.Drawing.Size(630, 109)
        Me.GroupBox_Settings.TabIndex = 3
        Me.GroupBox_Settings.TabStop = False
        Me.GroupBox_Settings.Text = "Settings"
        '
        'TextBox_Filter
        '
        Me.TextBox_Filter.Location = New System.Drawing.Point(104, 27)
        Me.TextBox_Filter.Name = "TextBox_Filter"
        Me.TextBox_Filter.Size = New System.Drawing.Size(520, 23)
        Me.TextBox_Filter.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Search Pattern"
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Location = New System.Drawing.Point(670, 134)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(112, 36)
        Me.Button_Cancel.TabIndex = 4
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'GroupBox_UiThread
        '
        Me.GroupBox_UiThread.Controls.Add(Me.ButtonDown)
        Me.GroupBox_UiThread.Controls.Add(Me.ButtonUp)
        Me.GroupBox_UiThread.Controls.Add(Me.Label2)
        Me.GroupBox_UiThread.Controls.Add(Me.TextBox_Count)
        Me.GroupBox_UiThread.Location = New System.Drawing.Point(12, 197)
        Me.GroupBox_UiThread.Name = "GroupBox_UiThread"
        Me.GroupBox_UiThread.Size = New System.Drawing.Size(630, 60)
        Me.GroupBox_UiThread.TabIndex = 5
        Me.GroupBox_UiThread.TabStop = False
        Me.GroupBox_UiThread.Text = "UI Thread Proc"
        '
        'TextBox_Count
        '
        Me.TextBox_Count.Location = New System.Drawing.Point(83, 27)
        Me.TextBox_Count.Name = "TextBox_Count"
        Me.TextBox_Count.Size = New System.Drawing.Size(70, 23)
        Me.TextBox_Count.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Count:"
        '
        'ButtonUp
        '
        Me.ButtonUp.Location = New System.Drawing.Point(174, 27)
        Me.ButtonUp.Name = "ButtonUp"
        Me.ButtonUp.Size = New System.Drawing.Size(56, 23)
        Me.ButtonUp.TabIndex = 2
        Me.ButtonUp.Text = "Up"
        Me.ButtonUp.UseVisualStyleBackColor = True
        '
        'ButtonDown
        '
        Me.ButtonDown.Location = New System.Drawing.Point(247, 27)
        Me.ButtonDown.Name = "ButtonDown"
        Me.ButtonDown.Size = New System.Drawing.Size(56, 23)
        Me.ButtonDown.TabIndex = 3
        Me.ButtonDown.Text = "Down"
        Me.ButtonDown.UseVisualStyleBackColor = True
        '
        'FormFileSearchTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 431)
        Me.Controls.Add(Me.GroupBox_UiThread)
        Me.Controls.Add(Me.Button_Cancel)
        Me.Controls.Add(Me.GroupBox_Settings)
        Me.Controls.Add(Me.GroupBox_Log)
        Me.Controls.Add(Me.Button_Start)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FormFileSearchTest"
        Me.Text = "File Search Test"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox_Log.ResumeLayout(False)
        Me.GroupBox_Settings.ResumeLayout(False)
        Me.GroupBox_Settings.PerformLayout()
        Me.GroupBox_UiThread.ResumeLayout(False)
        Me.GroupBox_UiThread.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TextBox_TargetDir As TextBox
    Friend WithEvents Button_Start As Button
    Friend WithEvents GroupBox_Log As GroupBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents GroupBox_Settings As GroupBox
    Friend WithEvents TextBox_Filter As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button_Cancel As Button
    Friend WithEvents GroupBox_UiThread As GroupBox
    Friend WithEvents ButtonDown As Button
    Friend WithEvents ButtonUp As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox_Count As TextBox
End Class
