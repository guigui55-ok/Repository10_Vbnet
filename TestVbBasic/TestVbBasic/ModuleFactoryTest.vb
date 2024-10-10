Module ModuleFactoryTest

    Public Sub ExecuteFactoryTestMain()
        ModuleFactoryTest.ExecuteFactory(New TestClassA())
        ModuleFactoryTest.ExecuteFactory(New TestClassB())
    End Sub

    Public Sub ExecuteFactory(obj As Object)
        'If (Not obj.GetType().ToString().Contains("TestClass")) Then
        'End If

        Dim objB
        If (obj.GetType().ToString().EndsWith("TestClassA")) Then
            objB = DirectCast(obj, TestClassA)
        ElseIf (obj.GetType().ToString().EndsWith("TestClassB")) Then
            objB = DirectCast(obj, TestClassB)
        Else
            Console.WriteLine("obj is not TestClass")
            Return
        End If
        objB.TestPrint()
    End Sub


    'Public Class ITestClass
    '    Inherits TestClassA
    'End Class


    Public Class TestClassA
        Public Sub TestPrint()
            Console.WriteLine("TestPrint TestClassA")
        End Sub
    End Class


    Public Class TestClassB
        Public Sub TestPrint()
            Console.WriteLine("TestPrint TestClassB")
        End Sub
    End Class

End Module
