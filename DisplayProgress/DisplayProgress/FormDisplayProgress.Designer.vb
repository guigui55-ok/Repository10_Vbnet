<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDisplayProgress
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
        Me.Label_ProgressA = New System.Windows.Forms.Label()
        Me.Label_ProgressB = New System.Windows.Forms.Label()
        Me.ButtonA_Start = New System.Windows.Forms.Button()
        Me.ButtonA_Stop = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ProgressBar2 = New System.Windows.Forms.ProgressBar()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label_Progress2A = New System.Windows.Forms.Label()
        Me.Button_Stop2 = New System.Windows.Forms.Button()
        Me.Label_Progress2B = New System.Windows.Forms.Label()
        Me.Button_Start2 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label_ProgressA
        '
        Me.Label_ProgressA.AutoSize = True
        Me.Label_ProgressA.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label_ProgressA.Location = New System.Drawing.Point(16, 24)
        Me.Label_ProgressA.Name = "Label_ProgressA"
        Me.Label_ProgressA.Size = New System.Drawing.Size(133, 16)
        Me.Label_ProgressA.TabIndex = 0
        Me.Label_ProgressA.Text = "Label_ProgressA"
        '
        'Label_ProgressB
        '
        Me.Label_ProgressB.AutoSize = True
        Me.Label_ProgressB.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label_ProgressB.Location = New System.Drawing.Point(16, 60)
        Me.Label_ProgressB.Name = "Label_ProgressB"
        Me.Label_ProgressB.Size = New System.Drawing.Size(133, 16)
        Me.Label_ProgressB.TabIndex = 1
        Me.Label_ProgressB.Text = "Label_ProgressB"
        '
        'ButtonA_Start
        '
        Me.ButtonA_Start.Location = New System.Drawing.Point(118, 97)
        Me.ButtonA_Start.Name = "ButtonA_Start"
        Me.ButtonA_Start.Size = New System.Drawing.Size(95, 23)
        Me.ButtonA_Start.TabIndex = 2
        Me.ButtonA_Start.Text = "ButtonA_Start"
        Me.ButtonA_Start.UseVisualStyleBackColor = True
        '
        'ButtonA_Stop
        '
        Me.ButtonA_Stop.Location = New System.Drawing.Point(17, 97)
        Me.ButtonA_Stop.Name = "ButtonA_Stop"
        Me.ButtonA_Stop.Size = New System.Drawing.Size(95, 23)
        Me.ButtonA_Stop.TabIndex = 3
        Me.ButtonA_Stop.Text = "ButtonA_Stop"
        Me.ButtonA_Stop.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label_ProgressA)
        Me.GroupBox1.Controls.Add(Me.ButtonA_Stop)
        Me.GroupBox1.Controls.Add(Me.Label_ProgressB)
        Me.GroupBox1.Controls.Add(Me.ButtonA_Start)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(242, 134)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "GroupBox1"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ProgressBar2)
        Me.GroupBox2.Controls.Add(Me.ProgressBar1)
        Me.GroupBox2.Controls.Add(Me.Label_Progress2A)
        Me.GroupBox2.Controls.Add(Me.Button_Stop2)
        Me.GroupBox2.Controls.Add(Me.Label_Progress2B)
        Me.GroupBox2.Controls.Add(Me.Button_Start2)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 163)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(454, 134)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "GroupBox2"
        '
        'ProgressBar2
        '
        Me.ProgressBar2.Location = New System.Drawing.Point(201, 58)
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.Size = New System.Drawing.Size(197, 23)
        Me.ProgressBar2.TabIndex = 5
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(201, 24)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(197, 23)
        Me.ProgressBar1.TabIndex = 4
        '
        'Label_Progress2A
        '
        Me.Label_Progress2A.AutoSize = True
        Me.Label_Progress2A.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label_Progress2A.Location = New System.Drawing.Point(16, 24)
        Me.Label_Progress2A.Name = "Label_Progress2A"
        Me.Label_Progress2A.Size = New System.Drawing.Size(142, 16)
        Me.Label_Progress2A.TabIndex = 0
        Me.Label_Progress2A.Text = "Label_Progress2A"
        '
        'Button_Stop2
        '
        Me.Button_Stop2.Location = New System.Drawing.Point(17, 97)
        Me.Button_Stop2.Name = "Button_Stop2"
        Me.Button_Stop2.Size = New System.Drawing.Size(95, 23)
        Me.Button_Stop2.TabIndex = 3
        Me.Button_Stop2.Text = "Button_Stop2"
        Me.Button_Stop2.UseVisualStyleBackColor = True
        '
        'Label_Progress2B
        '
        Me.Label_Progress2B.AutoSize = True
        Me.Label_Progress2B.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label_Progress2B.Location = New System.Drawing.Point(16, 58)
        Me.Label_Progress2B.Name = "Label_Progress2B"
        Me.Label_Progress2B.Size = New System.Drawing.Size(142, 16)
        Me.Label_Progress2B.TabIndex = 1
        Me.Label_Progress2B.Text = "Label_Progress2B"
        '
        'Button_Start2
        '
        Me.Button_Start2.Location = New System.Drawing.Point(118, 97)
        Me.Button_Start2.Name = "Button_Start2"
        Me.Button_Start2.Size = New System.Drawing.Size(95, 23)
        Me.Button_Start2.TabIndex = 2
        Me.Button_Start2.Text = "Button_Start2"
        Me.Button_Start2.UseVisualStyleBackColor = True
        '
        'FormDisplayProgress
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(524, 309)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FormDisplayProgress"
        Me.Text = "FormDisplayProgress"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label_ProgressA As Label
    Friend WithEvents Label_ProgressB As Label
    Friend WithEvents ButtonA_Start As Button
    Friend WithEvents ButtonA_Stop As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label_Progress2A As Label
    Friend WithEvents Button_Stop2 As Button
    Friend WithEvents Label_Progress2B As Label
    Friend WithEvents Button_Start2 As Button
    Friend WithEvents ProgressBar2 As ProgressBar
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
