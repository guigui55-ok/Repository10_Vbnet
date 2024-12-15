Module ModuleFactoryPattern
    Sub Main()
        ' 環境に応じてクラスを生成する
        Dim factory As New TestClassFactory()
        Dim testClass As ITestClass = factory.CreateTestClass("Development") ' "Production" に切り替えることで本番環境用を生成
        testClass.Execute()
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

    ' ファクトリークラス
    Public Class TestClassFactory
        Public Function CreateTestClass(environment As String) As ITestClass
            Select Case environment
                Case "Development"
                    Return New TestClassDev()
                Case "Production"
                    Return New TestClassRelease()
                Case Else
                    Throw New ArgumentException("Invalid environment specified.")
            End Select
        End Function
    End Class
End Module
