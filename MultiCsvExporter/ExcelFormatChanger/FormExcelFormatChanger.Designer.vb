<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormExcelFormatChanger
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
        Me.GroupBox_Conditions = New System.Windows.Forms.GroupBox()
        Me.DataGridView_Conditions = New System.Windows.Forms.DataGridView()
        Me.Button_Execute = New System.Windows.Forms.Button()
        Me.GroupBox_SrcFilePath = New System.Windows.Forms.GroupBox()
        Me.TextBox_SrcFilePath = New System.Windows.Forms.TextBox()
        Me.GroupBox_DestFilePath = New System.Windows.Forms.GroupBox()
        Me.TextBox_DestFilePath = New System.Windows.Forms.TextBox()
        Me.Button_ShowDetails = New System.Windows.Forms.Button()
        Me.GroupBox_DestCondition = New System.Windows.Forms.GroupBox()
        Me.DataGridView_DestCondition = New System.Windows.Forms.DataGridView()
        Me.Button_AddRowSrc = New System.Windows.Forms.Button()
        Me.Button_RemoveRowSrc = New System.Windows.Forms.Button()
        Me.Button_Explorer = New System.Windows.Forms.Button()
        Me.Button_SetTestData = New System.Windows.Forms.Button()
        Me.Button_Save = New System.Windows.Forms.Button()
        Me.GroupBox_Conditions.SuspendLayout()
        CType(Me.DataGridView_Conditions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_SrcFilePath.SuspendLayout()
        Me.GroupBox_DestFilePath.SuspendLayout()
        Me.GroupBox_DestCondition.SuspendLayout()
        CType(Me.DataGridView_DestCondition, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox_Conditions
        '
        Me.GroupBox_Conditions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Conditions.Controls.Add(Me.DataGridView_Conditions)
        Me.GroupBox_Conditions.Location = New System.Drawing.Point(16, 79)
        Me.GroupBox_Conditions.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox_Conditions.Name = "GroupBox_Conditions"
        Me.GroupBox_Conditions.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox_Conditions.Size = New System.Drawing.Size(735, 163)
        Me.GroupBox_Conditions.TabIndex = 0
        Me.GroupBox_Conditions.TabStop = False
        Me.GroupBox_Conditions.Text = "Conditions"
        '
        'DataGridView_Conditions
        '
        Me.DataGridView_Conditions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView_Conditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView_Conditions.Location = New System.Drawing.Point(6, 23)
        Me.DataGridView_Conditions.Name = "DataGridView_Conditions"
        Me.DataGridView_Conditions.RowTemplate.Height = 21
        Me.DataGridView_Conditions.Size = New System.Drawing.Size(723, 133)
        Me.DataGridView_Conditions.TabIndex = 0
        '
        'Button_Execute
        '
        Me.Button_Execute.Location = New System.Drawing.Point(657, 12)
        Me.Button_Execute.Name = "Button_Execute"
        Me.Button_Execute.Size = New System.Drawing.Size(94, 38)
        Me.Button_Execute.TabIndex = 1
        Me.Button_Execute.Text = "Execute"
        Me.Button_Execute.UseVisualStyleBackColor = True
        '
        'GroupBox_SrcFilePath
        '
        Me.GroupBox_SrcFilePath.Controls.Add(Me.TextBox_SrcFilePath)
        Me.GroupBox_SrcFilePath.Location = New System.Drawing.Point(16, 12)
        Me.GroupBox_SrcFilePath.Name = "GroupBox_SrcFilePath"
        Me.GroupBox_SrcFilePath.Size = New System.Drawing.Size(627, 60)
        Me.GroupBox_SrcFilePath.TabIndex = 2
        Me.GroupBox_SrcFilePath.TabStop = False
        Me.GroupBox_SrcFilePath.Text = "Source File Path"
        '
        'TextBox_SrcFilePath
        '
        Me.TextBox_SrcFilePath.Location = New System.Drawing.Point(6, 22)
        Me.TextBox_SrcFilePath.Name = "TextBox_SrcFilePath"
        Me.TextBox_SrcFilePath.Size = New System.Drawing.Size(615, 23)
        Me.TextBox_SrcFilePath.TabIndex = 0
        '
        'GroupBox_DestFilePath
        '
        Me.GroupBox_DestFilePath.Controls.Add(Me.TextBox_DestFilePath)
        Me.GroupBox_DestFilePath.Location = New System.Drawing.Point(772, 12)
        Me.GroupBox_DestFilePath.Name = "GroupBox_DestFilePath"
        Me.GroupBox_DestFilePath.Size = New System.Drawing.Size(627, 60)
        Me.GroupBox_DestFilePath.TabIndex = 3
        Me.GroupBox_DestFilePath.TabStop = False
        Me.GroupBox_DestFilePath.Text = "Destination File Path"
        '
        'TextBox_DestFilePath
        '
        Me.TextBox_DestFilePath.Location = New System.Drawing.Point(6, 22)
        Me.TextBox_DestFilePath.Name = "TextBox_DestFilePath"
        Me.TextBox_DestFilePath.Size = New System.Drawing.Size(615, 23)
        Me.TextBox_DestFilePath.TabIndex = 0
        '
        'Button_ShowDetails
        '
        Me.Button_ShowDetails.Location = New System.Drawing.Point(16, 249)
        Me.Button_ShowDetails.Name = "Button_ShowDetails"
        Me.Button_ShowDetails.Size = New System.Drawing.Size(94, 38)
        Me.Button_ShowDetails.TabIndex = 4
        Me.Button_ShowDetails.Text = "Detail"
        Me.Button_ShowDetails.UseVisualStyleBackColor = True
        '
        'GroupBox_DestCondition
        '
        Me.GroupBox_DestCondition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_DestCondition.Controls.Add(Me.DataGridView_DestCondition)
        Me.GroupBox_DestCondition.Location = New System.Drawing.Point(772, 79)
        Me.GroupBox_DestCondition.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox_DestCondition.Name = "GroupBox_DestCondition"
        Me.GroupBox_DestCondition.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox_DestCondition.Size = New System.Drawing.Size(735, 163)
        Me.GroupBox_DestCondition.TabIndex = 5
        Me.GroupBox_DestCondition.TabStop = False
        Me.GroupBox_DestCondition.Text = "Conditions"
        '
        'DataGridView_DestCondition
        '
        Me.DataGridView_DestCondition.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView_DestCondition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView_DestCondition.Location = New System.Drawing.Point(6, 23)
        Me.DataGridView_DestCondition.Name = "DataGridView_DestCondition"
        Me.DataGridView_DestCondition.RowTemplate.Height = 21
        Me.DataGridView_DestCondition.Size = New System.Drawing.Size(723, 133)
        Me.DataGridView_DestCondition.TabIndex = 0
        '
        'Button_AddRowSrc
        '
        Me.Button_AddRowSrc.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button_AddRowSrc.Location = New System.Drawing.Point(657, 56)
        Me.Button_AddRowSrc.Name = "Button_AddRowSrc"
        Me.Button_AddRowSrc.Size = New System.Drawing.Size(34, 28)
        Me.Button_AddRowSrc.TabIndex = 6
        Me.Button_AddRowSrc.Text = "＋"
        Me.Button_AddRowSrc.UseVisualStyleBackColor = True
        '
        'Button_RemoveRowSrc
        '
        Me.Button_RemoveRowSrc.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button_RemoveRowSrc.Location = New System.Drawing.Point(701, 56)
        Me.Button_RemoveRowSrc.Name = "Button_RemoveRowSrc"
        Me.Button_RemoveRowSrc.Size = New System.Drawing.Size(34, 28)
        Me.Button_RemoveRowSrc.TabIndex = 7
        Me.Button_RemoveRowSrc.Text = "ー"
        Me.Button_RemoveRowSrc.UseVisualStyleBackColor = True
        '
        'Button_Explorer
        '
        Me.Button_Explorer.Location = New System.Drawing.Point(1407, 19)
        Me.Button_Explorer.Name = "Button_Explorer"
        Me.Button_Explorer.Size = New System.Drawing.Size(94, 38)
        Me.Button_Explorer.TabIndex = 8
        Me.Button_Explorer.Text = "Explorer"
        Me.Button_Explorer.UseVisualStyleBackColor = True
        '
        'Button_SetTestData
        '
        Me.Button_SetTestData.Location = New System.Drawing.Point(138, 249)
        Me.Button_SetTestData.Name = "Button_SetTestData"
        Me.Button_SetTestData.Size = New System.Drawing.Size(94, 38)
        Me.Button_SetTestData.TabIndex = 9
        Me.Button_SetTestData.Text = "SetTestData"
        Me.Button_SetTestData.UseVisualStyleBackColor = True
        '
        'Button_Save
        '
        Me.Button_Save.Location = New System.Drawing.Point(260, 249)
        Me.Button_Save.Name = "Button_Save"
        Me.Button_Save.Size = New System.Drawing.Size(94, 38)
        Me.Button_Save.TabIndex = 10
        Me.Button_Save.Text = "Save"
        Me.Button_Save.UseVisualStyleBackColor = True
        '
        'FormExcelFormatChanger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1518, 288)
        Me.Controls.Add(Me.Button_Save)
        Me.Controls.Add(Me.Button_SetTestData)
        Me.Controls.Add(Me.Button_Explorer)
        Me.Controls.Add(Me.Button_RemoveRowSrc)
        Me.Controls.Add(Me.Button_AddRowSrc)
        Me.Controls.Add(Me.GroupBox_DestCondition)
        Me.Controls.Add(Me.Button_ShowDetails)
        Me.Controls.Add(Me.GroupBox_DestFilePath)
        Me.Controls.Add(Me.GroupBox_SrcFilePath)
        Me.Controls.Add(Me.Button_Execute)
        Me.Controls.Add(Me.GroupBox_Conditions)
        Me.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FormExcelFormatChanger"
        Me.Text = "ExcelFormatChanger"
        Me.GroupBox_Conditions.ResumeLayout(False)
        CType(Me.DataGridView_Conditions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_SrcFilePath.ResumeLayout(False)
        Me.GroupBox_SrcFilePath.PerformLayout()
        Me.GroupBox_DestFilePath.ResumeLayout(False)
        Me.GroupBox_DestFilePath.PerformLayout()
        Me.GroupBox_DestCondition.ResumeLayout(False)
        CType(Me.DataGridView_DestCondition, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox_Conditions As GroupBox
    Friend WithEvents DataGridView_Conditions As DataGridView
    Friend WithEvents Button_Execute As Button
    Friend WithEvents GroupBox_SrcFilePath As GroupBox
    Friend WithEvents TextBox_SrcFilePath As TextBox
    Friend WithEvents GroupBox_DestFilePath As GroupBox
    Friend WithEvents TextBox_DestFilePath As TextBox
    Friend WithEvents Button_ShowDetails As Button
    Friend WithEvents GroupBox_DestCondition As GroupBox
    Friend WithEvents DataGridView_DestCondition As DataGridView
    Friend WithEvents Button_AddRowSrc As Button
    Friend WithEvents Button_RemoveRowSrc As Button
    Friend WithEvents Button_Explorer As Button
    Friend WithEvents Button_SetTestData As Button
    Friend WithEvents Button_Save As Button
End Class
