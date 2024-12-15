Module Module1

    Sub Main()

    End Sub

    Class TestClass
        Inherits TestClassDev '開発時の環境はこちらを使用したい
        'Inherits TestClassRelease '本番時はこちら切り替えたい
    End Class

    Class TestClassDev

    End Class

    Class TestClassRelease

    End Class

End Module
