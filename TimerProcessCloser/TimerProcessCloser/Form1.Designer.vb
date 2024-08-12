Imports System.IO
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.MainItemFrame1 = New TimerProcessCloser.MainItemFrame()
        Me.MainItemFrame2 = New TimerProcessCloser.MainItemFrame()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.SuspendLayout()
        '
        'MainItemFrame1
        '
        Me.MainItemFrame1.Location = New System.Drawing.Point(2, 3)
        Me.MainItemFrame1.Name = "MainItemFrame1"
        Me.MainItemFrame1.Size = New System.Drawing.Size(549, 108)
        Me.MainItemFrame1.TabIndex = 0
        '
        'MainItemFrame2
        '
        Me.MainItemFrame2.Location = New System.Drawing.Point(-1, 113)
        Me.MainItemFrame2.Name = "MainItemFrame2"
        Me.MainItemFrame2.Size = New System.Drawing.Size(549, 108)
        Me.MainItemFrame2.TabIndex = 1
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(547, 222)
        Me.Controls.Add(Me.MainItemFrame2)
        Me.Controls.Add(Me.MainItemFrame1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FormMain"
        Me.Text = "Timer Process Closer"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MainItemFrame1 As MainItemFrame
    Friend WithEvents MainItemFrame2 As MainItemFrame
    Public _logger As MainLogger
    Friend WithEvents NotifyIcon1 As NotifyIcon
End Class
