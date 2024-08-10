<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.ButtonExecute = New System.Windows.Forms.Button()
        Me.TextBoxDirPath = New System.Windows.Forms.TextBox()
        Me.LabelDirPath = New System.Windows.Forms.Label()
        Me.LabelOldName = New System.Windows.Forms.Label()
        Me.TextBoxOldName = New System.Windows.Forms.TextBox()
        Me.LabelNewName = New System.Windows.Forms.Label()
        Me.TextBoxNewName = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'ButtonExecute
        '
        Me.ButtonExecute.Location = New System.Drawing.Point(713, 118)
        Me.ButtonExecute.Name = "ButtonExecute"
        Me.ButtonExecute.Size = New System.Drawing.Size(75, 23)
        Me.ButtonExecute.TabIndex = 0
        Me.ButtonExecute.Text = "実行"
        Me.ButtonExecute.UseVisualStyleBackColor = True
        '
        'TextBoxDirPath
        '
        Me.TextBoxDirPath.Location = New System.Drawing.Point(98, 17)
        Me.TextBoxDirPath.Name = "TextBoxDirPath"
        Me.TextBoxDirPath.Size = New System.Drawing.Size(690, 23)
        Me.TextBoxDirPath.TabIndex = 1
        '
        'LabelDirPath
        '
        Me.LabelDirPath.AutoSize = True
        Me.LabelDirPath.Location = New System.Drawing.Point(12, 20)
        Me.LabelDirPath.Name = "LabelDirPath"
        Me.LabelDirPath.Size = New System.Drawing.Size(80, 15)
        Me.LabelDirPath.TabIndex = 2
        Me.LabelDirPath.Text = "Sln Dir Path："
        '
        'LabelOldName
        '
        Me.LabelOldName.AutoSize = True
        Me.LabelOldName.Location = New System.Drawing.Point(12, 55)
        Me.LabelOldName.Name = "LabelOldName"
        Me.LabelOldName.Size = New System.Drawing.Size(72, 15)
        Me.LabelOldName.TabIndex = 3
        Me.LabelOldName.Text = "Old Name："
        '
        'TextBoxOldName
        '
        Me.TextBoxOldName.Location = New System.Drawing.Point(98, 52)
        Me.TextBoxOldName.Name = "TextBoxOldName"
        Me.TextBoxOldName.Size = New System.Drawing.Size(690, 23)
        Me.TextBoxOldName.TabIndex = 4
        '
        'LabelNewName
        '
        Me.LabelNewName.AutoSize = True
        Me.LabelNewName.Location = New System.Drawing.Point(12, 87)
        Me.LabelNewName.Name = "LabelNewName"
        Me.LabelNewName.Size = New System.Drawing.Size(77, 15)
        Me.LabelNewName.TabIndex = 5
        Me.LabelNewName.Text = "New Name："
        '
        'TextBoxNewName
        '
        Me.TextBoxNewName.Location = New System.Drawing.Point(98, 84)
        Me.TextBoxNewName.Name = "TextBoxNewName"
        Me.TextBoxNewName.Size = New System.Drawing.Size(690, 23)
        Me.TextBoxNewName.TabIndex = 6
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 153)
        Me.Controls.Add(Me.TextBoxNewName)
        Me.Controls.Add(Me.LabelNewName)
        Me.Controls.Add(Me.TextBoxOldName)
        Me.Controls.Add(Me.LabelOldName)
        Me.Controls.Add(Me.LabelDirPath)
        Me.Controls.Add(Me.TextBoxDirPath)
        Me.Controls.Add(Me.ButtonExecute)
        Me.Name = "Form1"
        Me.Text = "Rename Project File Name"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButtonExecute As Button
    Friend WithEvents TextBoxDirPath As TextBox
    Friend WithEvents LabelDirPath As Label
    Friend WithEvents LabelOldName As Label
    Friend WithEvents TextBoxOldName As TextBox
    Friend WithEvents LabelNewName As Label
    Friend WithEvents TextBoxNewName As TextBox
End Class
