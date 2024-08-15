Module PathModule
    Public Class PathUtil
        Public Function GetAppExePath()
            ' 実行中のアプリケーションの実行パスを取得する
            Dim executionPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
            Return executionPath
        End Function
    End Class

End Module
