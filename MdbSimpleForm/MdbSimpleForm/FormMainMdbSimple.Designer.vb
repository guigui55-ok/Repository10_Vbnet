<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMainMdbSimple
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ButtonGetTableNames = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(524, 88)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(126, 25)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Connect And Write"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "MDB Path"
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Yu Gothic UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.TextBox1.Location = New System.Drawing.Point(78, 12)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(572, 25)
        Me.TextBox1.TabIndex = 2
        '
        'ButtonGetTableNames
        '
        Me.ButtonGetTableNames.Location = New System.Drawing.Point(324, 88)
        Me.ButtonGetTableNames.Name = "ButtonGetTableNames"
        Me.ButtonGetTableNames.Size = New System.Drawing.Size(112, 25)
        Me.ButtonGetTableNames.TabIndex = 3
        Me.ButtonGetTableNames.Text = "GetTableNames"
        Me.ButtonGetTableNames.UseVisualStyleBackColor = True
        '
        'FormMainMdbSimple
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(662, 125)
        Me.Controls.Add(Me.ButtonGetTableNames)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Name = "FormMainMdbSimple"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents ButtonGetTableNames As Button
End Class
