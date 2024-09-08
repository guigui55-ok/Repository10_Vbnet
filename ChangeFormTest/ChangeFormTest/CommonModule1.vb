Imports StateController
Module CommonModule1

    Public Sub DebugPrint(value As String)
        Debug.Print(value)
    End Sub

    Class AppStateI
        Inherits AppStatus
    End Class
End Module
