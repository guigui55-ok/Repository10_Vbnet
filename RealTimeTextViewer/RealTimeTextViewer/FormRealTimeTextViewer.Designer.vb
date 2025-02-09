<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormRealTimeTextViewer
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
        Me.RichTextBox_Main = New System.Windows.Forms.RichTextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.UserControlFindString1 = New RealTimeTextViewer.UserControlFindString()
        Me.SuspendLayout()
        '
        'RichTextBox_Main
        '
        Me.RichTextBox_Main.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox_Main.Location = New System.Drawing.Point(6, 29)
        Me.RichTextBox_Main.Name = "RichTextBox_Main"
        Me.RichTextBox_Main.Size = New System.Drawing.Size(716, 250)
        Me.RichTextBox_Main.TabIndex = 0
        Me.RichTextBox_Main.Text = ""
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(728, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'UserControlFindString1
        '
        Me.UserControlFindString1.Location = New System.Drawing.Point(0, 0)
        Me.UserControlFindString1.Name = "UserControlFindString1"
        Me.UserControlFindString1.Size = New System.Drawing.Size(334, 24)
        Me.UserControlFindString1.TabIndex = 2
        '
        'FormRealTimeTextViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(728, 286)
        Me.Controls.Add(Me.UserControlFindString1)
        Me.Controls.Add(Me.RichTextBox_Main)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FormRealTimeTextViewer"
        Me.Text = "RealTimeTextViewer"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RichTextBox_Main As RichTextBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents UserControlFindString1 As UserControlFindString
End Class
