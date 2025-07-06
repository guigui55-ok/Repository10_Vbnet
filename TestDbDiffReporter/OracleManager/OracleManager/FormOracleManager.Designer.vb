<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormOracleManager
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox_Port = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox_Host = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox_Password = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox_UserName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox_Pdb = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RichTextBox_Sql = New System.Windows.Forms.RichTextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RichTextBox_Log = New System.Windows.Forms.RichTextBox()
        Me.Button_Execute = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox_Port)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.TextBox_Host)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.TextBox_Password)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TextBox_UserName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TextBox_Pdb)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 15)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(430, 176)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Settings"
        '
        'TextBox_Port
        '
        Me.TextBox_Port.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox_Port.Location = New System.Drawing.Point(122, 80)
        Me.TextBox_Port.Name = "TextBox_Port"
        Me.TextBox_Port.Size = New System.Drawing.Size(80, 24)
        Me.TextBox_Port.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 84)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 15)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Port"
        '
        'TextBox_Host
        '
        Me.TextBox_Host.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox_Host.Location = New System.Drawing.Point(122, 50)
        Me.TextBox_Host.Name = "TextBox_Host"
        Me.TextBox_Host.Size = New System.Drawing.Size(291, 24)
        Me.TextBox_Host.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Host"
        '
        'TextBox_Password
        '
        Me.TextBox_Password.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox_Password.Location = New System.Drawing.Point(122, 140)
        Me.TextBox_Password.Name = "TextBox_Password"
        Me.TextBox_Password.Size = New System.Drawing.Size(291, 24)
        Me.TextBox_Password.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 145)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "PassWord"
        '
        'TextBox_UserName
        '
        Me.TextBox_UserName.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox_UserName.Location = New System.Drawing.Point(122, 110)
        Me.TextBox_UserName.Name = "TextBox_UserName"
        Me.TextBox_UserName.Size = New System.Drawing.Size(291, 24)
        Me.TextBox_UserName.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 114)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "UserName"
        '
        'TextBox_Pdb
        '
        Me.TextBox_Pdb.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox_Pdb.Location = New System.Drawing.Point(122, 20)
        Me.TextBox_Pdb.Name = "TextBox_Pdb"
        Me.TextBox_Pdb.Size = New System.Drawing.Size(291, 24)
        Me.TextBox_Pdb.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "PDB"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RichTextBox_Sql)
        Me.GroupBox2.Location = New System.Drawing.Point(14, 198)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(664, 114)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "SQL"
        '
        'RichTextBox_Sql
        '
        Me.RichTextBox_Sql.Location = New System.Drawing.Point(6, 18)
        Me.RichTextBox_Sql.Name = "RichTextBox_Sql"
        Me.RichTextBox_Sql.Size = New System.Drawing.Size(652, 90)
        Me.RichTextBox_Sql.TabIndex = 0
        Me.RichTextBox_Sql.Text = ""
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RichTextBox_Log)
        Me.GroupBox3.Location = New System.Drawing.Point(14, 318)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(664, 129)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Log"
        '
        'RichTextBox_Log
        '
        Me.RichTextBox_Log.Location = New System.Drawing.Point(6, 18)
        Me.RichTextBox_Log.Name = "RichTextBox_Log"
        Me.RichTextBox_Log.Size = New System.Drawing.Size(652, 105)
        Me.RichTextBox_Log.TabIndex = 1
        Me.RichTextBox_Log.Text = ""
        '
        'Button_Execute
        '
        Me.Button_Execute.Location = New System.Drawing.Point(546, 29)
        Me.Button_Execute.Name = "Button_Execute"
        Me.Button_Execute.Size = New System.Drawing.Size(126, 37)
        Me.Button_Execute.TabIndex = 3
        Me.Button_Execute.Text = "Execute"
        Me.Button_Execute.UseVisualStyleBackColor = True
        '
        'FormOracleManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(694, 459)
        Me.Controls.Add(Me.Button_Execute)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FormOracleManager"
        Me.Text = "OracleManager"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TextBox_Password As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox_UserName As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox_Pdb As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents RichTextBox_Sql As RichTextBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents RichTextBox_Log As RichTextBox
    Friend WithEvents Button_Execute As Button
    Friend WithEvents TextBox_Port As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox_Host As TextBox
    Friend WithEvents Label4 As Label
End Class
