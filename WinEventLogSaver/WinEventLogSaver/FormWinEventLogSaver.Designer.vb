<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormWinEventLogSaver
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
        Me.GroupBox_Condition = New System.Windows.Forms.GroupBox()
        Me.Button_Save = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBox_EventMachineName = New System.Windows.Forms.TextBox()
        Me.TextBox_EventUserName = New System.Windows.Forms.TextBox()
        Me.TextBox_EventCategory = New System.Windows.Forms.TextBox()
        Me.TextBox_EventId = New System.Windows.Forms.TextBox()
        Me.TextBox_EventMessage = New System.Windows.Forms.TextBox()
        Me.TextBox_EventKind = New System.Windows.Forms.TextBox()
        Me.TextBox_AppName = New System.Windows.Forms.TextBox()
        Me.TextBox_EndDateTime = New System.Windows.Forms.TextBox()
        Me.TextBox_StartDateTime = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox_Output = New System.Windows.Forms.GroupBox()
        Me.Button_Explorer = New System.Windows.Forms.Button()
        Me.Button_Copy = New System.Windows.Forms.Button()
        Me.TextBox_OutputPath = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GroupBox_AppLog = New System.Windows.Forms.GroupBox()
        Me.RichTextBox_AppLog = New System.Windows.Forms.RichTextBox()
        Me.TextBox_LogName = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TextBox_MacCount = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.GroupBox_Condition.SuspendLayout()
        Me.GroupBox_Output.SuspendLayout()
        Me.GroupBox_AppLog.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox_Condition
        '
        Me.GroupBox_Condition.Controls.Add(Me.Label17)
        Me.GroupBox_Condition.Controls.Add(Me.Label16)
        Me.GroupBox_Condition.Controls.Add(Me.TextBox_MacCount)
        Me.GroupBox_Condition.Controls.Add(Me.Label15)
        Me.GroupBox_Condition.Controls.Add(Me.TextBox_LogName)
        Me.GroupBox_Condition.Controls.Add(Me.Label14)
        Me.GroupBox_Condition.Controls.Add(Me.Button_Save)
        Me.GroupBox_Condition.Controls.Add(Me.Label12)
        Me.GroupBox_Condition.Controls.Add(Me.TextBox_EventMachineName)
        Me.GroupBox_Condition.Controls.Add(Me.TextBox_EventUserName)
        Me.GroupBox_Condition.Controls.Add(Me.TextBox_EventCategory)
        Me.GroupBox_Condition.Controls.Add(Me.TextBox_EventId)
        Me.GroupBox_Condition.Controls.Add(Me.TextBox_EventMessage)
        Me.GroupBox_Condition.Controls.Add(Me.TextBox_EventKind)
        Me.GroupBox_Condition.Controls.Add(Me.TextBox_AppName)
        Me.GroupBox_Condition.Controls.Add(Me.TextBox_EndDateTime)
        Me.GroupBox_Condition.Controls.Add(Me.TextBox_StartDateTime)
        Me.GroupBox_Condition.Controls.Add(Me.Label11)
        Me.GroupBox_Condition.Controls.Add(Me.Label10)
        Me.GroupBox_Condition.Controls.Add(Me.Label9)
        Me.GroupBox_Condition.Controls.Add(Me.Label8)
        Me.GroupBox_Condition.Controls.Add(Me.Label7)
        Me.GroupBox_Condition.Controls.Add(Me.Label6)
        Me.GroupBox_Condition.Controls.Add(Me.Label5)
        Me.GroupBox_Condition.Controls.Add(Me.Label4)
        Me.GroupBox_Condition.Controls.Add(Me.Label3)
        Me.GroupBox_Condition.Controls.Add(Me.Label2)
        Me.GroupBox_Condition.Controls.Add(Me.Label1)
        Me.GroupBox_Condition.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox_Condition.Location = New System.Drawing.Point(12, 13)
        Me.GroupBox_Condition.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox_Condition.Name = "GroupBox_Condition"
        Me.GroupBox_Condition.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox_Condition.Size = New System.Drawing.Size(655, 340)
        Me.GroupBox_Condition.TabIndex = 0
        Me.GroupBox_Condition.TabStop = False
        Me.GroupBox_Condition.Text = "検索条件"
        '
        'Button_Save
        '
        Me.Button_Save.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button_Save.Location = New System.Drawing.Point(551, 297)
        Me.Button_Save.Name = "Button_Save"
        Me.Button_Save.Size = New System.Drawing.Size(98, 24)
        Me.Button_Save.TabIndex = 21
        Me.Button_Save.Text = "Save"
        Me.Button_Save.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(308, 82)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(147, 17)
        Me.Label12.TabIndex = 20
        Me.Label12.Text = "アプリ名 または、サービス名"
        '
        'TextBox_EventMachineName
        '
        Me.TextBox_EventMachineName.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox_EventMachineName.Location = New System.Drawing.Point(110, 267)
        Me.TextBox_EventMachineName.Name = "TextBox_EventMachineName"
        Me.TextBox_EventMachineName.Size = New System.Drawing.Size(186, 24)
        Me.TextBox_EventMachineName.TabIndex = 20
        '
        'TextBox_EventUserName
        '
        Me.TextBox_EventUserName.Location = New System.Drawing.Point(110, 240)
        Me.TextBox_EventUserName.Name = "TextBox_EventUserName"
        Me.TextBox_EventUserName.Size = New System.Drawing.Size(186, 24)
        Me.TextBox_EventUserName.TabIndex = 19
        '
        'TextBox_EventCategory
        '
        Me.TextBox_EventCategory.Location = New System.Drawing.Point(110, 213)
        Me.TextBox_EventCategory.Name = "TextBox_EventCategory"
        Me.TextBox_EventCategory.Size = New System.Drawing.Size(186, 24)
        Me.TextBox_EventCategory.TabIndex = 18
        '
        'TextBox_EventId
        '
        Me.TextBox_EventId.Location = New System.Drawing.Point(110, 186)
        Me.TextBox_EventId.Name = "TextBox_EventId"
        Me.TextBox_EventId.Size = New System.Drawing.Size(186, 24)
        Me.TextBox_EventId.TabIndex = 17
        '
        'TextBox_EventMessage
        '
        Me.TextBox_EventMessage.Location = New System.Drawing.Point(110, 159)
        Me.TextBox_EventMessage.Name = "TextBox_EventMessage"
        Me.TextBox_EventMessage.Size = New System.Drawing.Size(186, 24)
        Me.TextBox_EventMessage.TabIndex = 16
        '
        'TextBox_EventKind
        '
        Me.TextBox_EventKind.Location = New System.Drawing.Point(110, 132)
        Me.TextBox_EventKind.Name = "TextBox_EventKind"
        Me.TextBox_EventKind.Size = New System.Drawing.Size(186, 24)
        Me.TextBox_EventKind.TabIndex = 15
        '
        'TextBox_AppName
        '
        Me.TextBox_AppName.Location = New System.Drawing.Point(110, 78)
        Me.TextBox_AppName.Name = "TextBox_AppName"
        Me.TextBox_AppName.Size = New System.Drawing.Size(186, 24)
        Me.TextBox_AppName.TabIndex = 13
        '
        'TextBox_EndDateTime
        '
        Me.TextBox_EndDateTime.Location = New System.Drawing.Point(110, 51)
        Me.TextBox_EndDateTime.Name = "TextBox_EndDateTime"
        Me.TextBox_EndDateTime.Size = New System.Drawing.Size(186, 24)
        Me.TextBox_EndDateTime.TabIndex = 12
        '
        'TextBox_StartDateTime
        '
        Me.TextBox_StartDateTime.Location = New System.Drawing.Point(110, 24)
        Me.TextBox_StartDateTime.Name = "TextBox_StartDateTime"
        Me.TextBox_StartDateTime.Size = New System.Drawing.Size(186, 24)
        Me.TextBox_StartDateTime.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(308, 27)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(317, 17)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "yyyy/M/d H:m:s または yyyyMMdd HHmmss で入力"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(308, 166)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(246, 17)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "イベントビューアーの全般タブに表示される内容"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(18, 270)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 17)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "マシン名："
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(19, 243)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 17)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "ユーザー名："
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(18, 216)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 17)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "ログのカテゴリ："
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(18, 189)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 17)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "イベントID："
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 162)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 17)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "メッセージ："
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 135)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 17)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "ログレベル："
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 17)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "アプリ名："
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "終了日時："
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "開始日時："
        '
        'GroupBox_Output
        '
        Me.GroupBox_Output.Controls.Add(Me.Button_Explorer)
        Me.GroupBox_Output.Controls.Add(Me.Button_Copy)
        Me.GroupBox_Output.Controls.Add(Me.TextBox_OutputPath)
        Me.GroupBox_Output.Controls.Add(Me.Label13)
        Me.GroupBox_Output.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox_Output.Location = New System.Drawing.Point(12, 360)
        Me.GroupBox_Output.Name = "GroupBox_Output"
        Me.GroupBox_Output.Size = New System.Drawing.Size(655, 78)
        Me.GroupBox_Output.TabIndex = 1
        Me.GroupBox_Output.TabStop = False
        Me.GroupBox_Output.Text = "出力"
        '
        'Button_Explorer
        '
        Me.Button_Explorer.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button_Explorer.Location = New System.Drawing.Point(551, 48)
        Me.Button_Explorer.Name = "Button_Explorer"
        Me.Button_Explorer.Size = New System.Drawing.Size(98, 24)
        Me.Button_Explorer.TabIndex = 24
        Me.Button_Explorer.Text = "Explorer"
        Me.Button_Explorer.UseVisualStyleBackColor = True
        '
        'Button_Copy
        '
        Me.Button_Copy.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button_Copy.Location = New System.Drawing.Point(551, 18)
        Me.Button_Copy.Name = "Button_Copy"
        Me.Button_Copy.Size = New System.Drawing.Size(98, 24)
        Me.Button_Copy.TabIndex = 23
        Me.Button_Copy.Text = "Copy"
        Me.Button_Copy.UseVisualStyleBackColor = True
        '
        'TextBox_OutputPath
        '
        Me.TextBox_OutputPath.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox_OutputPath.Location = New System.Drawing.Point(89, 17)
        Me.TextBox_OutputPath.Name = "TextBox_OutputPath"
        Me.TextBox_OutputPath.Size = New System.Drawing.Size(456, 24)
        Me.TextBox_OutputPath.TabIndex = 22
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(18, 20)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 17)
        Me.Label13.TabIndex = 21
        Me.Label13.Text = "出力パス："
        '
        'GroupBox_AppLog
        '
        Me.GroupBox_AppLog.Controls.Add(Me.RichTextBox_AppLog)
        Me.GroupBox_AppLog.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox_AppLog.Location = New System.Drawing.Point(12, 444)
        Me.GroupBox_AppLog.Name = "GroupBox_AppLog"
        Me.GroupBox_AppLog.Size = New System.Drawing.Size(655, 101)
        Me.GroupBox_AppLog.TabIndex = 24
        Me.GroupBox_AppLog.TabStop = False
        Me.GroupBox_AppLog.Text = "Log"
        '
        'RichTextBox_AppLog
        '
        Me.RichTextBox_AppLog.Location = New System.Drawing.Point(6, 23)
        Me.RichTextBox_AppLog.Name = "RichTextBox_AppLog"
        Me.RichTextBox_AppLog.Size = New System.Drawing.Size(643, 72)
        Me.RichTextBox_AppLog.TabIndex = 0
        Me.RichTextBox_AppLog.Text = ""
        '
        'TextBox_LogName
        '
        Me.TextBox_LogName.Location = New System.Drawing.Point(110, 105)
        Me.TextBox_LogName.Name = "TextBox_LogName"
        Me.TextBox_LogName.Size = New System.Drawing.Size(186, 24)
        Me.TextBox_LogName.TabIndex = 14
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(18, 108)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 17)
        Me.Label14.TabIndex = 22
        Me.Label14.Text = "ログの種類："
        '
        'TextBox_MacCount
        '
        Me.TextBox_MacCount.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox_MacCount.Location = New System.Drawing.Point(110, 294)
        Me.TextBox_MacCount.Name = "TextBox_MacCount"
        Me.TextBox_MacCount.Size = New System.Drawing.Size(186, 24)
        Me.TextBox_MacCount.TabIndex = 24
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(20, 297)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(73, 17)
        Me.Label15.TabIndex = 23
        Me.Label15.Text = "最大件数："
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(308, 297)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(110, 17)
        Me.Label16.TabIndex = 25
        Me.Label16.Text = "イベント1種類あたり"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(308, 108)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(179, 17)
        Me.Label17.TabIndex = 26
        Me.Label17.Text = "「,」カンマ区切りで複数指定可能"
        '
        'FormWinEventLogSaver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(679, 557)
        Me.Controls.Add(Me.GroupBox_AppLog)
        Me.Controls.Add(Me.GroupBox_Output)
        Me.Controls.Add(Me.GroupBox_Condition)
        Me.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FormWinEventLogSaver"
        Me.Text = "WinEventLogSaver"
        Me.GroupBox_Condition.ResumeLayout(False)
        Me.GroupBox_Condition.PerformLayout()
        Me.GroupBox_Output.ResumeLayout(False)
        Me.GroupBox_Output.PerformLayout()
        Me.GroupBox_AppLog.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox_Condition As GroupBox
    Friend WithEvents Label12 As Label
    Friend WithEvents TextBox_EventMachineName As TextBox
    Friend WithEvents TextBox_EventUserName As TextBox
    Friend WithEvents TextBox_EventCategory As TextBox
    Friend WithEvents TextBox_EventId As TextBox
    Friend WithEvents TextBox_EventMessage As TextBox
    Friend WithEvents TextBox_EventKind As TextBox
    Friend WithEvents TextBox_AppName As TextBox
    Friend WithEvents TextBox_EndDateTime As TextBox
    Friend WithEvents TextBox_StartDateTime As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox_Output As GroupBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Button_Save As Button
    Friend WithEvents Button_Explorer As Button
    Friend WithEvents Button_Copy As Button
    Friend WithEvents TextBox_OutputPath As TextBox
    Friend WithEvents GroupBox_AppLog As GroupBox
    Friend WithEvents RichTextBox_AppLog As RichTextBox
    Friend WithEvents TextBox_LogName As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents TextBox_MacCount As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
End Class
