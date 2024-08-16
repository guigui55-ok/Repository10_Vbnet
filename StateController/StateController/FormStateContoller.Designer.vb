<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormStateContoller
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
        Me.GroupBoxStatus = New System.Windows.Forms.GroupBox()
        Me.LabelStatusName = New System.Windows.Forms.Label()
        Me.LabelStatusNumber = New System.Windows.Forms.Label()
        Me.TextBoxStateNumber = New System.Windows.Forms.TextBox()
        Me.GroupBoxStatusList = New System.Windows.Forms.GroupBox()
        Me.LabelStatusList = New System.Windows.Forms.Label()
        Me.ButtonPrevState = New System.Windows.Forms.Button()
        Me.ButtonNextState = New System.Windows.Forms.Button()
        Me.LabelMode = New System.Windows.Forms.Label()
        Me.LabelErrorNumber = New System.Windows.Forms.Label()
        Me.TextBoxErrorNumber = New System.Windows.Forms.TextBox()
        Me.LabelErrorName = New System.Windows.Forms.Label()
        Me.GroupBoxStatus.SuspendLayout()
        Me.GroupBoxStatusList.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBoxStatus
        '
        Me.GroupBoxStatus.Controls.Add(Me.LabelErrorName)
        Me.GroupBoxStatus.Controls.Add(Me.TextBoxErrorNumber)
        Me.GroupBoxStatus.Controls.Add(Me.LabelErrorNumber)
        Me.GroupBoxStatus.Controls.Add(Me.LabelStatusName)
        Me.GroupBoxStatus.Controls.Add(Me.LabelStatusNumber)
        Me.GroupBoxStatus.Controls.Add(Me.TextBoxStateNumber)
        Me.GroupBoxStatus.Location = New System.Drawing.Point(6, 36)
        Me.GroupBoxStatus.Name = "GroupBoxStatus"
        Me.GroupBoxStatus.Size = New System.Drawing.Size(274, 79)
        Me.GroupBoxStatus.TabIndex = 0
        Me.GroupBoxStatus.TabStop = False
        Me.GroupBoxStatus.Text = "ステータス"
        '
        'LabelStatusName
        '
        Me.LabelStatusName.AutoSize = True
        Me.LabelStatusName.Location = New System.Drawing.Point(113, 23)
        Me.LabelStatusName.Name = "LabelStatusName"
        Me.LabelStatusName.Size = New System.Drawing.Size(63, 15)
        Me.LabelStatusName.TabIndex = 5
        Me.LabelStatusName.Text = "ステータス名"
        '
        'LabelStatusNumber
        '
        Me.LabelStatusNumber.AutoSize = True
        Me.LabelStatusNumber.Location = New System.Drawing.Point(6, 23)
        Me.LabelStatusNumber.Name = "LabelStatusNumber"
        Me.LabelStatusNumber.Size = New System.Drawing.Size(26, 15)
        Me.LabelStatusNumber.TabIndex = 4
        Me.LabelStatusNumber.Text = "No."
        '
        'TextBoxStateNumber
        '
        Me.TextBoxStateNumber.Location = New System.Drawing.Point(66, 20)
        Me.TextBoxStateNumber.Name = "TextBoxStateNumber"
        Me.TextBoxStateNumber.Size = New System.Drawing.Size(41, 23)
        Me.TextBoxStateNumber.TabIndex = 2
        '
        'GroupBoxStatusList
        '
        Me.GroupBoxStatusList.Controls.Add(Me.LabelStatusList)
        Me.GroupBoxStatusList.Location = New System.Drawing.Point(6, 195)
        Me.GroupBoxStatusList.Name = "GroupBoxStatusList"
        Me.GroupBoxStatusList.Size = New System.Drawing.Size(274, 135)
        Me.GroupBoxStatusList.TabIndex = 1
        Me.GroupBoxStatusList.TabStop = False
        Me.GroupBoxStatusList.Text = "ステータス一覧"
        '
        'LabelStatusList
        '
        Me.LabelStatusList.AutoSize = True
        Me.LabelStatusList.Location = New System.Drawing.Point(6, 19)
        Me.LabelStatusList.Name = "LabelStatusList"
        Me.LabelStatusList.Size = New System.Drawing.Size(41, 15)
        Me.LabelStatusList.TabIndex = 5
        Me.LabelStatusList.Text = "Label1"
        '
        'ButtonPrevState
        '
        Me.ButtonPrevState.Location = New System.Drawing.Point(90, 121)
        Me.ButtonPrevState.Name = "ButtonPrevState"
        Me.ButtonPrevState.Size = New System.Drawing.Size(107, 31)
        Me.ButtonPrevState.TabIndex = 2
        Me.ButtonPrevState.Text = "前の状態へ"
        Me.ButtonPrevState.UseVisualStyleBackColor = True
        '
        'ButtonNextState
        '
        Me.ButtonNextState.Location = New System.Drawing.Point(90, 158)
        Me.ButtonNextState.Name = "ButtonNextState"
        Me.ButtonNextState.Size = New System.Drawing.Size(107, 31)
        Me.ButtonNextState.TabIndex = 3
        Me.ButtonNextState.Text = "次の状態へ"
        Me.ButtonNextState.UseVisualStyleBackColor = True
        '
        'LabelMode
        '
        Me.LabelMode.AutoSize = True
        Me.LabelMode.Location = New System.Drawing.Point(12, 9)
        Me.LabelMode.Name = "LabelMode"
        Me.LabelMode.Size = New System.Drawing.Size(79, 15)
        Me.LabelMode.TabIndex = 4
        Me.LabelMode.Text = "Mode：None"
        '
        'LabelErrorNumber
        '
        Me.LabelErrorNumber.AutoSize = True
        Me.LabelErrorNumber.Location = New System.Drawing.Point(6, 52)
        Me.LabelErrorNumber.Name = "LabelErrorNumber"
        Me.LabelErrorNumber.Size = New System.Drawing.Size(54, 15)
        Me.LabelErrorNumber.TabIndex = 6
        Me.LabelErrorNumber.Text = "Error No."
        '
        'TextBoxErrorNumber
        '
        Me.TextBoxErrorNumber.Location = New System.Drawing.Point(66, 49)
        Me.TextBoxErrorNumber.Name = "TextBoxErrorNumber"
        Me.TextBoxErrorNumber.Size = New System.Drawing.Size(41, 23)
        Me.TextBoxErrorNumber.TabIndex = 7
        '
        'LabelErrorName
        '
        Me.LabelErrorName.AutoSize = True
        Me.LabelErrorName.Location = New System.Drawing.Point(113, 52)
        Me.LabelErrorName.Name = "LabelErrorName"
        Me.LabelErrorName.Size = New System.Drawing.Size(63, 15)
        Me.LabelErrorName.TabIndex = 8
        Me.LabelErrorName.Text = "ステータス名"
        '
        'FormStateContoller
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(287, 337)
        Me.Controls.Add(Me.LabelMode)
        Me.Controls.Add(Me.ButtonNextState)
        Me.Controls.Add(Me.ButtonPrevState)
        Me.Controls.Add(Me.GroupBoxStatusList)
        Me.Controls.Add(Me.GroupBoxStatus)
        Me.Name = "FormStateContoller"
        Me.Text = "FormStateController"
        Me.GroupBoxStatus.ResumeLayout(False)
        Me.GroupBoxStatus.PerformLayout()
        Me.GroupBoxStatusList.ResumeLayout(False)
        Me.GroupBoxStatusList.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBoxStatus As GroupBox
    Friend WithEvents TextBoxStateNumber As TextBox
    Friend WithEvents GroupBoxStatusList As GroupBox
    Friend WithEvents ButtonPrevState As Button
    Friend WithEvents ButtonNextState As Button
    Friend WithEvents LabelStatusName As Label
    Friend WithEvents LabelStatusNumber As Label
    Friend WithEvents LabelMode As Label
    Friend WithEvents LabelStatusList As Label
    Friend WithEvents LabelErrorName As Label
    Friend WithEvents TextBoxErrorNumber As TextBox
    Friend WithEvents LabelErrorNumber As Label
End Class
