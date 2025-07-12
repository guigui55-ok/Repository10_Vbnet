<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormImageAutoCutter
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
        Me.GroupBox_InputDirPath = New System.Windows.Forms.GroupBox()
        Me.TextBox_InputDirPath = New System.Windows.Forms.TextBox()
        Me.GroupBox_Settings = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox_RegexPattern = New System.Windows.Forms.TextBox()
        Me.Button_Execute = New System.Windows.Forms.Button()
        Me.GroupBox_FileList = New System.Windows.Forms.GroupBox()
        Me.RichTextBox_FileList = New System.Windows.Forms.RichTextBox()
        Me.Button_FindFile = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox_CutSize = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox_TempletePath = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox_Threshold = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox_OutputDirPath = New System.Windows.Forms.GroupBox()
        Me.TextBox_OutputDir = New System.Windows.Forms.TextBox()
        Me.CheckBox_AutoInputOutputDir = New System.Windows.Forms.CheckBox()
        Me.GroupBox_InputDirPath.SuspendLayout()
        Me.GroupBox_Settings.SuspendLayout()
        Me.GroupBox_FileList.SuspendLayout()
        Me.GroupBox_OutputDirPath.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_InputDirPath
        '
        Me.GroupBox_InputDirPath.Controls.Add(Me.TextBox_InputDirPath)
        Me.GroupBox_InputDirPath.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_InputDirPath.Name = "GroupBox_InputDirPath"
        Me.GroupBox_InputDirPath.Size = New System.Drawing.Size(732, 64)
        Me.GroupBox_InputDirPath.TabIndex = 0
        Me.GroupBox_InputDirPath.TabStop = False
        Me.GroupBox_InputDirPath.Text = "Input DIr Path"
        '
        'TextBox_InputDirPath
        '
        Me.TextBox_InputDirPath.Location = New System.Drawing.Point(6, 22)
        Me.TextBox_InputDirPath.Name = "TextBox_InputDirPath"
        Me.TextBox_InputDirPath.Size = New System.Drawing.Size(720, 23)
        Me.TextBox_InputDirPath.TabIndex = 0
        '
        'GroupBox_Settings
        '
        Me.GroupBox_Settings.Controls.Add(Me.CheckBox_AutoInputOutputDir)
        Me.GroupBox_Settings.Controls.Add(Me.Label6)
        Me.GroupBox_Settings.Controls.Add(Me.Label5)
        Me.GroupBox_Settings.Controls.Add(Me.TextBox_Threshold)
        Me.GroupBox_Settings.Controls.Add(Me.Label4)
        Me.GroupBox_Settings.Controls.Add(Me.TextBox_TempletePath)
        Me.GroupBox_Settings.Controls.Add(Me.Label3)
        Me.GroupBox_Settings.Controls.Add(Me.Label2)
        Me.GroupBox_Settings.Controls.Add(Me.TextBox_CutSize)
        Me.GroupBox_Settings.Controls.Add(Me.Label1)
        Me.GroupBox_Settings.Controls.Add(Me.TextBox_RegexPattern)
        Me.GroupBox_Settings.Location = New System.Drawing.Point(12, 82)
        Me.GroupBox_Settings.Name = "GroupBox_Settings"
        Me.GroupBox_Settings.Size = New System.Drawing.Size(602, 161)
        Me.GroupBox_Settings.TabIndex = 1
        Me.GroupBox_Settings.TabStop = False
        Me.GroupBox_Settings.Text = "Settings"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 15)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Find Pattern"
        '
        'TextBox_RegexPattern
        '
        Me.TextBox_RegexPattern.Location = New System.Drawing.Point(98, 22)
        Me.TextBox_RegexPattern.Name = "TextBox_RegexPattern"
        Me.TextBox_RegexPattern.Size = New System.Drawing.Size(498, 23)
        Me.TextBox_RegexPattern.TabIndex = 0
        '
        'Button_Execute
        '
        Me.Button_Execute.Location = New System.Drawing.Point(637, 323)
        Me.Button_Execute.Name = "Button_Execute"
        Me.Button_Execute.Size = New System.Drawing.Size(107, 38)
        Me.Button_Execute.TabIndex = 2
        Me.Button_Execute.Text = "Auto Cut Execute"
        Me.Button_Execute.UseVisualStyleBackColor = True
        '
        'GroupBox_FileList
        '
        Me.GroupBox_FileList.Controls.Add(Me.RichTextBox_FileList)
        Me.GroupBox_FileList.Location = New System.Drawing.Point(12, 310)
        Me.GroupBox_FileList.Name = "GroupBox_FileList"
        Me.GroupBox_FileList.Size = New System.Drawing.Size(602, 126)
        Me.GroupBox_FileList.TabIndex = 3
        Me.GroupBox_FileList.TabStop = False
        Me.GroupBox_FileList.Text = "File List"
        '
        'RichTextBox_FileList
        '
        Me.RichTextBox_FileList.Location = New System.Drawing.Point(9, 22)
        Me.RichTextBox_FileList.Name = "RichTextBox_FileList"
        Me.RichTextBox_FileList.Size = New System.Drawing.Size(587, 98)
        Me.RichTextBox_FileList.TabIndex = 0
        Me.RichTextBox_FileList.Text = ""
        '
        'Button_FindFile
        '
        Me.Button_FindFile.Location = New System.Drawing.Point(637, 95)
        Me.Button_FindFile.Name = "Button_FindFile"
        Me.Button_FindFile.Size = New System.Drawing.Size(107, 38)
        Me.Button_FindFile.TabIndex = 4
        Me.Button_FindFile.Text = "Find FIle"
        Me.Button_FindFile.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Cut Size"
        '
        'TextBox_CutSize
        '
        Me.TextBox_CutSize.Location = New System.Drawing.Point(98, 51)
        Me.TextBox_CutSize.Name = "TextBox_CutSize"
        Me.TextBox_CutSize.Size = New System.Drawing.Size(205, 23)
        Me.TextBox_CutSize.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(320, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 15)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "( ex:  500, 600 )"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 83)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 15)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Templete Path"
        '
        'TextBox_TempletePath
        '
        Me.TextBox_TempletePath.Location = New System.Drawing.Point(98, 80)
        Me.TextBox_TempletePath.Name = "TextBox_TempletePath"
        Me.TextBox_TempletePath.Size = New System.Drawing.Size(498, 23)
        Me.TextBox_TempletePath.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 15)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Threshold"
        '
        'TextBox_Threshold
        '
        Me.TextBox_Threshold.Location = New System.Drawing.Point(98, 109)
        Me.TextBox_Threshold.Name = "TextBox_Threshold"
        Me.TextBox_Threshold.Size = New System.Drawing.Size(205, 23)
        Me.TextBox_Threshold.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(320, 112)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 15)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "( 0 ~ 1.0 )"
        '
        'GroupBox_OutputDirPath
        '
        Me.GroupBox_OutputDirPath.Controls.Add(Me.TextBox_OutputDir)
        Me.GroupBox_OutputDirPath.Location = New System.Drawing.Point(12, 249)
        Me.GroupBox_OutputDirPath.Name = "GroupBox_OutputDirPath"
        Me.GroupBox_OutputDirPath.Size = New System.Drawing.Size(732, 55)
        Me.GroupBox_OutputDirPath.TabIndex = 5
        Me.GroupBox_OutputDirPath.TabStop = False
        Me.GroupBox_OutputDirPath.Text = "Output Dir Path"
        '
        'TextBox_OutputDir
        '
        Me.TextBox_OutputDir.Location = New System.Drawing.Point(6, 22)
        Me.TextBox_OutputDir.Name = "TextBox_OutputDir"
        Me.TextBox_OutputDir.Size = New System.Drawing.Size(720, 23)
        Me.TextBox_OutputDir.TabIndex = 0
        '
        'CheckBox_AutoInputOutputDir
        '
        Me.CheckBox_AutoInputOutputDir.AutoSize = True
        Me.CheckBox_AutoInputOutputDir.Checked = True
        Me.CheckBox_AutoInputOutputDir.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox_AutoInputOutputDir.Location = New System.Drawing.Point(9, 138)
        Me.CheckBox_AutoInputOutputDir.Name = "CheckBox_AutoInputOutputDir"
        Me.CheckBox_AutoInputOutputDir.Size = New System.Drawing.Size(170, 19)
        Me.CheckBox_AutoInputOutputDir.TabIndex = 11
        Me.CheckBox_AutoInputOutputDir.Text = "OutputDirPath を自動入力"
        Me.CheckBox_AutoInputOutputDir.UseVisualStyleBackColor = True
        '
        'FormImageAutoCutter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(756, 448)
        Me.Controls.Add(Me.GroupBox_OutputDirPath)
        Me.Controls.Add(Me.Button_FindFile)
        Me.Controls.Add(Me.GroupBox_FileList)
        Me.Controls.Add(Me.Button_Execute)
        Me.Controls.Add(Me.GroupBox_Settings)
        Me.Controls.Add(Me.GroupBox_InputDirPath)
        Me.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FormImageAutoCutter"
        Me.Text = "ImageAutoCutter"
        Me.GroupBox_InputDirPath.ResumeLayout(False)
        Me.GroupBox_InputDirPath.PerformLayout()
        Me.GroupBox_Settings.ResumeLayout(False)
        Me.GroupBox_Settings.PerformLayout()
        Me.GroupBox_FileList.ResumeLayout(False)
        Me.GroupBox_OutputDirPath.ResumeLayout(False)
        Me.GroupBox_OutputDirPath.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox_InputDirPath As GroupBox
    Friend WithEvents TextBox_InputDirPath As TextBox
    Friend WithEvents GroupBox_Settings As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox_RegexPattern As TextBox
    Friend WithEvents Button_Execute As Button
    Friend WithEvents GroupBox_FileList As GroupBox
    Friend WithEvents RichTextBox_FileList As RichTextBox
    Friend WithEvents Button_FindFile As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox_CutSize As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox_TempletePath As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox_Threshold As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents CheckBox_AutoInputOutputDir As CheckBox
    Friend WithEvents GroupBox_OutputDirPath As GroupBox
    Friend WithEvents TextBox_OutputDir As TextBox
End Class
