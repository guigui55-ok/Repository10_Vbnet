Public Class FormLog
    Private Sub FormLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub AddLogToControl(sender As Object, e As EventArgs)
        Dim logMsg = DirectCast(sender, String)
        If RichTextBox_Log.Text.Length > 0 Then logMsg = vbCrLf + logMsg
        RichTextBox_Log.AppendText(logMsg)
        RichTextBox_Log.ScrollToCaret()
    End Sub
End Class