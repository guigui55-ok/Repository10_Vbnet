Module ModuleGenericTest

    Class DataClassA
        Public Name As String = "Name_A"
        Public DataA As String = "DataValue_A"
    End Class

    Class DataClassB
        Public Name As String = "Name_B"
        Public DataB As String = "DataValue_B"
    End Class

    Public Sub OutputName(value As Object)
        Debug.WriteLine($"object.Name = {value.Name}")
    End Sub

    Sub Main()
        Dim _dataA = New DataClassA
        Dim _dataB = New DataClassB
        OutputName(_dataA)
        OutputName(_dataB)

    End Sub

End Module
