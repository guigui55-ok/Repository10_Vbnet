Module ModuleDepencyInjection

    Sub Main()
        ' 開発環境用のクラスを注入
        Dim serviceDev As New TestClass(New TestClassDev())
        serviceDev.Execute()

        ' 本番環境用のクラスを注入
        Dim serviceRelease As New TestClass(New TestClassRelease())
        serviceRelease.Execute()
    End Sub

    ' インターフェースを定義
    Public Interface ITestClass
        Sub Execute()
    End Interface

    ' 開発環境用のクラス
    Public Class TestClassDev
        Implements ITestClass

        Public Sub Execute() Implements ITestClass.Execute
            Console.WriteLine("TestClassDev is executed.")
        End Sub
    End Class

    ' 本番環境用のクラス
    Public Class TestClassRelease
        Implements ITestClass

        Public Sub Execute() Implements ITestClass.Execute
            Console.WriteLine("TestClassRelease is executed.")
        End Sub
    End Class

    ' 依存性を注入するメインクラス
    Public Class TestClass
        Private ReadOnly _dependency As ITestClass

        Public Sub New(dependency As ITestClass)
            _dependency = dependency
        End Sub

        Public Sub Execute()
            _dependency.Execute()
        End Sub
    End Class

End Module
