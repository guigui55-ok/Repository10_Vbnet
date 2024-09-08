Imports StateController

Public Class MessageBase
    Public attr As MessageAttribute = New MessageAttribute(Me.GetType())
    Private Sub MessageBase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.Initialize_Values(-1, -1)
        DebugPrint("MessageBase_Load")
    End Sub

End Class
