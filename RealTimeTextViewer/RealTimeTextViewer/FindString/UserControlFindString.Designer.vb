<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserControlFindString
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label_Find = New System.Windows.Forms.Label()
        Me.Button_Prev = New System.Windows.Forms.Button()
        Me.Button_Next = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button_Next)
        Me.Panel1.Controls.Add(Me.Button_Prev)
        Me.Panel1.Controls.Add(Me.Label_Find)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Location = New System.Drawing.Point(1, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(320, 23)
        Me.Panel1.TabIndex = 0
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(46, 2)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(178, 19)
        Me.TextBox1.TabIndex = 0
        '
        'Label_Find
        '
        Me.Label_Find.AutoSize = True
        Me.Label_Find.Location = New System.Drawing.Point(5, 5)
        Me.Label_Find.Name = "Label_Find"
        Me.Label_Find.Size = New System.Drawing.Size(35, 12)
        Me.Label_Find.TabIndex = 1
        Me.Label_Find.Text = "検索："
        '
        'Button_Prev
        '
        Me.Button_Prev.Location = New System.Drawing.Point(233, 1)
        Me.Button_Prev.Name = "Button_Prev"
        Me.Button_Prev.Size = New System.Drawing.Size(39, 19)
        Me.Button_Prev.TabIndex = 2
        Me.Button_Prev.Text = "前へ"
        Me.Button_Prev.UseVisualStyleBackColor = True
        '
        'Button_Next
        '
        Me.Button_Next.Location = New System.Drawing.Point(278, 1)
        Me.Button_Next.Name = "Button_Next"
        Me.Button_Next.Size = New System.Drawing.Size(39, 19)
        Me.Button_Next.TabIndex = 3
        Me.Button_Next.Text = "次へ"
        Me.Button_Next.UseVisualStyleBackColor = True
        '
        'UserControlFindString
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel1)
        Me.Name = "UserControlFindString"
        Me.Size = New System.Drawing.Size(334, 24)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button_Next As Button
    Friend WithEvents Button_Prev As Button
    Friend WithEvents Label_Find As Label
    Friend WithEvents TextBox1 As TextBox
End Class
