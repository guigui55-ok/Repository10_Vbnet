<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormClipBoadImageToFile
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
        Me.Label_ClipboadState = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label_Size = New System.Windows.Forms.Label()
        Me.TextBox_FileName = New System.Windows.Forms.TextBox()
        Me.Label_FileName = New System.Windows.Forms.Label()
        Me.ComboBox_Ext = New System.Windows.Forms.ComboBox()
        Me.Label_DirPath = New System.Windows.Forms.Label()
        Me.TextBox_DirPath = New System.Windows.Forms.TextBox()
        Me.Button_Save = New System.Windows.Forms.Button()
        Me.Button_Open = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label_ClipboadState
        '
        Me.Label_ClipboadState.AutoSize = True
        Me.Label_ClipboadState.Location = New System.Drawing.Point(10, 8)
        Me.Label_ClipboadState.Name = "Label_ClipboadState"
        Me.Label_ClipboadState.Size = New System.Drawing.Size(126, 12)
        Me.Label_ClipboadState.TabIndex = 0
        Me.Label_ClipboadState.Text = "ClipBoadImage : FALSE"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label_ClipboadState)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(147, 28)
        Me.Panel1.TabIndex = 1
        '
        'Label_Size
        '
        Me.Label_Size.AutoSize = True
        Me.Label_Size.Location = New System.Drawing.Point(179, 20)
        Me.Label_Size.Name = "Label_Size"
        Me.Label_Size.Size = New System.Drawing.Size(42, 12)
        Me.Label_Size.TabIndex = 2
        Me.Label_Size.Text = "Size : -"
        '
        'TextBox_FileName
        '
        Me.TextBox_FileName.Location = New System.Drawing.Point(81, 56)
        Me.TextBox_FileName.Name = "TextBox_FileName"
        Me.TextBox_FileName.Size = New System.Drawing.Size(349, 19)
        Me.TextBox_FileName.TabIndex = 3
        '
        'Label_FileName
        '
        Me.Label_FileName.AutoSize = True
        Me.Label_FileName.Location = New System.Drawing.Point(12, 59)
        Me.Label_FileName.Name = "Label_FileName"
        Me.Label_FileName.Size = New System.Drawing.Size(63, 12)
        Me.Label_FileName.TabIndex = 4
        Me.Label_FileName.Text = "FileName : "
        '
        'ComboBox_Ext
        '
        Me.ComboBox_Ext.FormattingEnabled = True
        Me.ComboBox_Ext.Location = New System.Drawing.Point(436, 56)
        Me.ComboBox_Ext.Name = "ComboBox_Ext"
        Me.ComboBox_Ext.Size = New System.Drawing.Size(75, 20)
        Me.ComboBox_Ext.TabIndex = 5
        '
        'Label_DirPath
        '
        Me.Label_DirPath.AutoSize = True
        Me.Label_DirPath.Location = New System.Drawing.Point(12, 84)
        Me.Label_DirPath.Name = "Label_DirPath"
        Me.Label_DirPath.Size = New System.Drawing.Size(53, 12)
        Me.Label_DirPath.TabIndex = 7
        Me.Label_DirPath.Text = "DirPath : "
        '
        'TextBox_DirPath
        '
        Me.TextBox_DirPath.Location = New System.Drawing.Point(81, 81)
        Me.TextBox_DirPath.Name = "TextBox_DirPath"
        Me.TextBox_DirPath.Size = New System.Drawing.Size(349, 19)
        Me.TextBox_DirPath.TabIndex = 6
        '
        'Button_Save
        '
        Me.Button_Save.Location = New System.Drawing.Point(436, 114)
        Me.Button_Save.Name = "Button_Save"
        Me.Button_Save.Size = New System.Drawing.Size(75, 23)
        Me.Button_Save.TabIndex = 8
        Me.Button_Save.Text = "Save Image"
        Me.Button_Save.UseVisualStyleBackColor = True
        '
        'Button_Open
        '
        Me.Button_Open.Location = New System.Drawing.Point(436, 82)
        Me.Button_Open.Name = "Button_Open"
        Me.Button_Open.Size = New System.Drawing.Size(75, 23)
        Me.Button_Open.TabIndex = 9
        Me.Button_Open.Text = "Open"
        Me.Button_Open.UseVisualStyleBackColor = True
        '
        'FormClipBoadImageToFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(523, 149)
        Me.Controls.Add(Me.Button_Open)
        Me.Controls.Add(Me.Button_Save)
        Me.Controls.Add(Me.Label_DirPath)
        Me.Controls.Add(Me.TextBox_DirPath)
        Me.Controls.Add(Me.ComboBox_Ext)
        Me.Controls.Add(Me.Label_FileName)
        Me.Controls.Add(Me.TextBox_FileName)
        Me.Controls.Add(Me.Label_Size)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FormClipBoadImageToFile"
        Me.Text = "ClipBoadImageToFile"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label_ClipboadState As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label_Size As Label
    Friend WithEvents TextBox_FileName As TextBox
    Friend WithEvents Label_FileName As Label
    Friend WithEvents ComboBox_Ext As ComboBox
    Friend WithEvents Label_DirPath As Label
    Friend WithEvents TextBox_DirPath As TextBox
    Friend WithEvents Button_Save As Button
    Friend WithEvents Button_Open As Button
End Class
