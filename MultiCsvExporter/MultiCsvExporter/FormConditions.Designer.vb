<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormConditions
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupBox_Condition = New System.Windows.Forms.GroupBox()
        Me.DataGridView_Condition = New System.Windows.Forms.DataGridView()
        Me.RadioButton_MatchAll = New System.Windows.Forms.RadioButton()
        Me.GroupBox_ConditionAll = New System.Windows.Forms.GroupBox()
        Me.RadioButton_MatchAny = New System.Windows.Forms.RadioButton()
        Me.Button_AddRow = New System.Windows.Forms.Button()
        Me.Button_RemoveRow = New System.Windows.Forms.Button()
        Me.GroupBox_Condition.SuspendLayout()
        CType(Me.DataGridView_Condition, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_ConditionAll.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_Condition
        '
        Me.GroupBox_Condition.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Condition.Controls.Add(Me.DataGridView_Condition)
        Me.GroupBox_Condition.Location = New System.Drawing.Point(14, 64)
        Me.GroupBox_Condition.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox_Condition.Name = "GroupBox_Condition"
        Me.GroupBox_Condition.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox_Condition.Size = New System.Drawing.Size(503, 208)
        Me.GroupBox_Condition.TabIndex = 0
        Me.GroupBox_Condition.TabStop = False
        Me.GroupBox_Condition.Text = "Conditions"
        '
        'DataGridView_Condition
        '
        Me.DataGridView_Condition.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView_Condition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView_Condition.Location = New System.Drawing.Point(6, 23)
        Me.DataGridView_Condition.Name = "DataGridView_Condition"
        Me.DataGridView_Condition.RowTemplate.Height = 21
        Me.DataGridView_Condition.Size = New System.Drawing.Size(491, 178)
        Me.DataGridView_Condition.TabIndex = 0
        '
        'RadioButton_MatchAll
        '
        Me.RadioButton_MatchAll.AutoSize = True
        Me.RadioButton_MatchAll.Location = New System.Drawing.Point(6, 20)
        Me.RadioButton_MatchAll.Name = "RadioButton_MatchAll"
        Me.RadioButton_MatchAll.Size = New System.Drawing.Size(152, 19)
        Me.RadioButton_MatchAll.TabIndex = 1
        Me.RadioButton_MatchAll.TabStop = True
        Me.RadioButton_MatchAll.Text = "以下の条件をすべて満たす"
        Me.RadioButton_MatchAll.UseVisualStyleBackColor = True
        '
        'GroupBox_ConditionAll
        '
        Me.GroupBox_ConditionAll.Controls.Add(Me.RadioButton_MatchAny)
        Me.GroupBox_ConditionAll.Controls.Add(Me.RadioButton_MatchAll)
        Me.GroupBox_ConditionAll.Location = New System.Drawing.Point(14, 12)
        Me.GroupBox_ConditionAll.Name = "GroupBox_ConditionAll"
        Me.GroupBox_ConditionAll.Size = New System.Drawing.Size(393, 45)
        Me.GroupBox_ConditionAll.TabIndex = 2
        Me.GroupBox_ConditionAll.TabStop = False
        Me.GroupBox_ConditionAll.Text = "条件"
        '
        'RadioButton_MatchAny
        '
        Me.RadioButton_MatchAny.AutoSize = True
        Me.RadioButton_MatchAny.Location = New System.Drawing.Point(164, 20)
        Me.RadioButton_MatchAny.Name = "RadioButton_MatchAny"
        Me.RadioButton_MatchAny.Size = New System.Drawing.Size(214, 19)
        Me.RadioButton_MatchAny.TabIndex = 2
        Me.RadioButton_MatchAny.TabStop = True
        Me.RadioButton_MatchAny.Text = "以下の条件のいずれか1つ以上を満たす"
        Me.RadioButton_MatchAny.UseVisualStyleBackColor = True
        '
        'Button_AddRow
        '
        Me.Button_AddRow.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button_AddRow.Location = New System.Drawing.Point(463, 26)
        Me.Button_AddRow.Name = "Button_AddRow"
        Me.Button_AddRow.Size = New System.Drawing.Size(35, 25)
        Me.Button_AddRow.TabIndex = 3
        Me.Button_AddRow.Text = "＋"
        Me.Button_AddRow.UseVisualStyleBackColor = True
        '
        'Button_RemoveRow
        '
        Me.Button_RemoveRow.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button_RemoveRow.Location = New System.Drawing.Point(417, 26)
        Me.Button_RemoveRow.Name = "Button_RemoveRow"
        Me.Button_RemoveRow.Size = New System.Drawing.Size(35, 25)
        Me.Button_RemoveRow.TabIndex = 4
        Me.Button_RemoveRow.Text = "ー"
        Me.Button_RemoveRow.UseVisualStyleBackColor = True
        '
        'FormConditions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 285)
        Me.Controls.Add(Me.Button_RemoveRow)
        Me.Controls.Add(Me.Button_AddRow)
        Me.Controls.Add(Me.GroupBox_ConditionAll)
        Me.Controls.Add(Me.GroupBox_Condition)
        Me.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MinimumSize = New System.Drawing.Size(545, 200)
        Me.Name = "FormConditions"
        Me.Text = "FormCondition"
        Me.GroupBox_Condition.ResumeLayout(False)
        CType(Me.DataGridView_Condition, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_ConditionAll.ResumeLayout(False)
        Me.GroupBox_ConditionAll.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox_Condition As GroupBox
    Friend WithEvents DataGridView_Condition As DataGridView
    Friend WithEvents RadioButton_MatchAll As RadioButton
    Friend WithEvents GroupBox_ConditionAll As GroupBox
    Friend WithEvents RadioButton_MatchAny As RadioButton
    Friend WithEvents Button_AddRow As Button
    Friend WithEvents Button_RemoveRow As Button
End Class
