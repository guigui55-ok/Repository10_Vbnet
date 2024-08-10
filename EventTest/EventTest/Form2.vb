'Public Class Form2
'    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
'        gStatus = StatusValue.PROCESSING
'        Label1.Text = "Processing"
'    End Sub

'    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
'        gStatus = StatusValue.NONE
'        Label1.Text = "None"
'    End Sub
'End Class

'Module TestModule
'    Enum StatusValue
'        NONE
'        PROCESSING
'    End Enum

'    Public gStatus As Integer

'End Module


Public Class Form2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        UpdateStatus(StatusValue.PROCESSING)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        UpdateStatus(StatusValue.NONE)
    End Sub

    Private Sub UpdateStatus(status As StatusValue)
        StatusHandler.SetStatus(status)
        Label1.Text = StatusHandler.GetStatusText()
    End Sub
End Class

Module StatusHandler
    Enum StatusValue
        NONE
        PROCESSING
    End Enum

    Private gStatus As StatusValue

    Public Sub SetStatus(status As StatusValue)
        gStatus = status
    End Sub

    Public Function GetStatus() As StatusValue
        Return gStatus
    End Function

    Public Function GetStatusText() As String
        Select Case gStatus
            Case StatusValue.PROCESSING
                Return "Processing"
            Case StatusValue.NONE
                Return "None"
            Case Else
                Return "Unknown"
        End Select
    End Function
End Module



