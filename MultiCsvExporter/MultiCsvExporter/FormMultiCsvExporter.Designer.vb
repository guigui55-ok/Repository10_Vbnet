<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMultiCsvExporter
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
        Me.TextBox_OutputFileName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox_OutputDirPath = New System.Windows.Forms.TextBox()
        Me.GroupBox_Input = New System.Windows.Forms.GroupBox()
        Me.RichTextBox_DirPaths = New System.Windows.Forms.RichTextBox()
        Me.RichTextBox_RegexPatterns = New System.Windows.Forms.RichTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button_Execute = New System.Windows.Forms.Button()
        Me.Button_Condition = New System.Windows.Forms.Button()
        Me.GroupBox_Output.SuspendLayout()
        Me.GroupBox_Input.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_Output
        '
        Me.GroupBox_Output.Controls.Add(Me.TextBox_OutputFileName)
        Me.GroupBox_Output.Controls.Add(Me.Label2)
        Me.GroupBox_Output.Controls.Add(Me.Label1)
        Me.GroupBox_Output.Controls.Add(Me.TextBox_OutputDirPath)
        Me.GroupBox_Output.Location = New System.Drawing.Point(13, 13)
        Me.GroupBox_Output.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox_Output.Name = "GroupBox_Output"
        Me.GroupBox_Output.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox_Output.Size = New System.Drawing.Size(668, 104)
        Me.GroupBox_Output.TabIndex = 0
        Me.GroupBox_Output.TabStop = False
        Me.GroupBox_Output.Text = "Output"
        '
        'TextBox_OutputFileName
        '
        Me.TextBox_OutputFileName.Location = New System.Drawing.Point(80, 59)
        Me.TextBox_OutputFileName.Name = "TextBox_OutputFileName"
        Me.TextBox_OutputFileName.Size = New System.Drawing.Size(570, 24)
        Me.TextBox_OutputFileName.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "FileName"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "DirPath"
        '
        'TextBox_OutputDirPath
        '
        Me.TextBox_OutputDirPath.Location = New System.Drawing.Point(80, 24)
        Me.TextBox_OutputDirPath.Name = "TextBox_OutputDirPath"
        Me.TextBox_OutputDirPath.Size = New System.Drawing.Size(570, 24)
        Me.TextBox_OutputDirPath.TabIndex = 0
        '
        'GroupBox_Input
        '
        Me.GroupBox_Input.Controls.Add(Me.RichTextBox_DirPaths)
        Me.GroupBox_Input.Controls.Add(Me.RichTextBox_RegexPatterns)
        Me.GroupBox_Input.Controls.Add(Me.Label4)
        Me.GroupBox_Input.Controls.Add(Me.Label3)
        Me.GroupBox_Input.Location = New System.Drawing.Point(13, 124)
        Me.GroupBox_Input.Name = "GroupBox_Input"
        Me.GroupBox_Input.Size = New System.Drawing.Size(797, 342)
        Me.GroupBox_Input.TabIndex = 1
        Me.GroupBox_Input.TabStop = False
        Me.GroupBox_Input.Text = "Input Conditions"
        '
        'RichTextBox_DirPaths
        '
        Me.RichTextBox_DirPaths.Location = New System.Drawing.Point(10, 46)
        Me.RichTextBox_DirPaths.Name = "RichTextBox_DirPaths"
        Me.RichTextBox_DirPaths.Size = New System.Drawing.Size(770, 87)
        Me.RichTextBox_DirPaths.TabIndex = 8
        Me.RichTextBox_DirPaths.Text = ""
        '
        'RichTextBox_RegexPatterns
        '
        Me.RichTextBox_RegexPatterns.Location = New System.Drawing.Point(10, 156)
        Me.RichTextBox_RegexPatterns.Name = "RichTextBox_RegexPatterns"
        Me.RichTextBox_RegexPatterns.Size = New System.Drawing.Size(770, 180)
        Me.RichTextBox_RegexPatterns.TabIndex = 7
        Me.RichTextBox_RegexPatterns.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 136)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(105, 17)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Regex Patterns"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 17)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "DirPaths"
        '
        'Button_Execute
        '
        Me.Button_Execute.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Execute.Location = New System.Drawing.Point(696, 29)
        Me.Button_Execute.Name = "Button_Execute"
        Me.Button_Execute.Size = New System.Drawing.Size(114, 32)
        Me.Button_Execute.TabIndex = 2
        Me.Button_Execute.Text = "Execute"
        Me.Button_Execute.UseVisualStyleBackColor = True
        '
        'Button_Condition
        '
        Me.Button_Condition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Condition.Location = New System.Drawing.Point(696, 85)
        Me.Button_Condition.Name = "Button_Condition"
        Me.Button_Condition.Size = New System.Drawing.Size(114, 32)
        Me.Button_Condition.TabIndex = 3
        Me.Button_Condition.Text = "SetCondition"
        Me.Button_Condition.UseVisualStyleBackColor = True
        '
        'FormMultiCsvExporter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(824, 478)
        Me.Controls.Add(Me.Button_Condition)
        Me.Controls.Add(Me.Button_Execute)
        Me.Controls.Add(Me.GroupBox_Input)
        Me.Controls.Add(Me.GroupBox_Output)
        Me.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormMultiCsvExporter"
        Me.Text = "FormMultiCsvExporter"
        Me.GroupBox_Output.ResumeLayout(False)
        Me.GroupBox_Output.PerformLayout()
        Me.GroupBox_Input.ResumeLayout(False)
        Me.GroupBox_Input.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox_Output As GroupBox
    Friend WithEvents TextBox_OutputFileName As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox_OutputDirPath As TextBox
    Friend WithEvents GroupBox_Input As GroupBox
    Friend WithEvents RichTextBox_RegexPatterns As RichTextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Button_Execute As Button
    Friend WithEvents RichTextBox_DirPaths As RichTextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Button_Condition As Button
End Class
