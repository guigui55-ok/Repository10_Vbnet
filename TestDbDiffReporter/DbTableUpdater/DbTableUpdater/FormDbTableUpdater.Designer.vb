<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDbTableUpdater
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
        Me.GroupBox_TargetData = New System.Windows.Forms.GroupBox()
        Me.DataGridView_TargetData = New System.Windows.Forms.DataGridView()
        Me.Button_Execute = New System.Windows.Forms.Button()
        Me.GroupBox_Log = New System.Windows.Forms.GroupBox()
        Me.RichTextBox_Log = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox_TableName = New System.Windows.Forms.TextBox()
        Me.Button_ReadTest = New System.Windows.Forms.Button()
        Me.GroupBox_TargetData.SuspendLayout()
        CType(Me.DataGridView_TargetData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_Log.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_TargetData
        '
        Me.GroupBox_TargetData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_TargetData.Controls.Add(Me.TextBox_TableName)
        Me.GroupBox_TargetData.Controls.Add(Me.Label1)
        Me.GroupBox_TargetData.Controls.Add(Me.DataGridView_TargetData)
        Me.GroupBox_TargetData.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox_TargetData.Name = "GroupBox_TargetData"
        Me.GroupBox_TargetData.Size = New System.Drawing.Size(909, 225)
        Me.GroupBox_TargetData.TabIndex = 0
        Me.GroupBox_TargetData.TabStop = False
        Me.GroupBox_TargetData.Text = "Target Data"
        '
        'DataGridView_TargetData
        '
        Me.DataGridView_TargetData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView_TargetData.Location = New System.Drawing.Point(6, 54)
        Me.DataGridView_TargetData.Name = "DataGridView_TargetData"
        Me.DataGridView_TargetData.RowTemplate.Height = 21
        Me.DataGridView_TargetData.Size = New System.Drawing.Size(897, 165)
        Me.DataGridView_TargetData.TabIndex = 0
        '
        'Button_Execute
        '
        Me.Button_Execute.Location = New System.Drawing.Point(795, 243)
        Me.Button_Execute.Name = "Button_Execute"
        Me.Button_Execute.Size = New System.Drawing.Size(120, 40)
        Me.Button_Execute.TabIndex = 1
        Me.Button_Execute.Text = "Execute"
        Me.Button_Execute.UseVisualStyleBackColor = True
        '
        'GroupBox_Log
        '
        Me.GroupBox_Log.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Log.Controls.Add(Me.RichTextBox_Log)
        Me.GroupBox_Log.Location = New System.Drawing.Point(18, 289)
        Me.GroupBox_Log.Name = "GroupBox_Log"
        Me.GroupBox_Log.Size = New System.Drawing.Size(897, 114)
        Me.GroupBox_Log.TabIndex = 2
        Me.GroupBox_Log.TabStop = False
        Me.GroupBox_Log.Text = "Log"
        '
        'RichTextBox_Log
        '
        Me.RichTextBox_Log.Location = New System.Drawing.Point(6, 22)
        Me.RichTextBox_Log.Name = "RichTextBox_Log"
        Me.RichTextBox_Log.Size = New System.Drawing.Size(885, 86)
        Me.RichTextBox_Log.TabIndex = 0
        Me.RichTextBox_Log.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "テーブル名："
        '
        'TextBox_TableName
        '
        Me.TextBox_TableName.Location = New System.Drawing.Point(91, 22)
        Me.TextBox_TableName.Name = "TextBox_TableName"
        Me.TextBox_TableName.Size = New System.Drawing.Size(215, 23)
        Me.TextBox_TableName.TabIndex = 2
        '
        'Button_ReadTest
        '
        Me.Button_ReadTest.Location = New System.Drawing.Point(508, 243)
        Me.Button_ReadTest.Name = "Button_ReadTest"
        Me.Button_ReadTest.Size = New System.Drawing.Size(120, 40)
        Me.Button_ReadTest.TabIndex = 3
        Me.Button_ReadTest.Text = "ReadTest"
        Me.Button_ReadTest.UseVisualStyleBackColor = True
        '
        'FormDbTableUpdater
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(933, 415)
        Me.Controls.Add(Me.Button_ReadTest)
        Me.Controls.Add(Me.GroupBox_Log)
        Me.Controls.Add(Me.Button_Execute)
        Me.Controls.Add(Me.GroupBox_TargetData)
        Me.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FormDbTableUpdater"
        Me.Text = "DbTableUpdater"
        Me.GroupBox_TargetData.ResumeLayout(False)
        Me.GroupBox_TargetData.PerformLayout()
        CType(Me.DataGridView_TargetData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Log.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox_TargetData As GroupBox
    Friend WithEvents DataGridView_TargetData As DataGridView
    Friend WithEvents Button_Execute As Button
    Friend WithEvents GroupBox_Log As GroupBox
    Friend WithEvents RichTextBox_Log As RichTextBox
    Friend WithEvents TextBox_TableName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button_ReadTest As Button
End Class
