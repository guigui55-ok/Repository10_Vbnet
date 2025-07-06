Public Class FormLog

    Public parentForm As Form

    Private Sub FormLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If parentForm IsNot Nothing Then
            Dim newPoint = New Point(parentForm.Location.X, parentForm.Location.Y + parentForm.Height + 12)
            Me.Location = newPoint
        End If
    End Sub

    Public Sub AddLogToControl(sender As Object, e As EventArgs)
        Dim logMsg = DirectCast(sender, String)
        If RichTextBox_Log.Text.Length > 0 Then logMsg = vbCrLf + logMsg
        RichTextBox_Log.AppendText(logMsg)
        RichTextBox_Log.ScrollToCaret()
    End Sub

    Private Sub FormLog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Me.Visible = False
        e.Cancel = True
    End Sub

End Class