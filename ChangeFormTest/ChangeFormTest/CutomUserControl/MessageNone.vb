Public Class MessageNone
    Public attr As MessageAttribute = New MessageAttribute(Me.GetType())

    Private Sub MessageNone_Loas(sender As Object, e As EventArgs) Handles Me.Load
        DebugPrint("MessageNone_Loas")
    End Sub
End Class
