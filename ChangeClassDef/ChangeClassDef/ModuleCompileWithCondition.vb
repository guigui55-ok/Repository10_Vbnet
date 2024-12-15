Module ModuleCompileWithCondition
#If DEBUG Then
    Class TestClassA
        Inherits TestClassADev ' 開発環境
    End Class
#Else
    Class TestClassA
        Inherits TestClassARelease ' 本番環境
    End Class
#End If

    Class TestClassADev
        Public Sub New()
            Console.WriteLine("TestClassDev is used.")
        End Sub
    End Class

    Class TestClassARelease
        Public Sub New()
            Console.WriteLine("TestClassRelease is used.")
        End Sub
    End Class
End Module
