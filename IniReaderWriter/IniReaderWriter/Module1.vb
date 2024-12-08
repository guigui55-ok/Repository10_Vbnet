Module Module1

    Sub Main()
        Try
            Dim iniPath = "C:\Users\OK\source\repos\test_media_files\vbnet_oracle\oracle_setting.ini"
            Dim ini As New IniStream(iniPath)

            ' 値を書き込む
            ini.WriteValue("Settings", "UserName", "test_user")

            ' 値を読み込む
            Dim UserName As String = ini.ReadValue("Settings", "UserName")
            Console.WriteLine($"Username: {UserName}")
            Dim Password As String = ini.ReadValue("Settings", "Password")
            Console.WriteLine($"Password: {Password}")
            Dim Host As String = ini.ReadValue("Settings", "Host")
            Console.WriteLine($"Host: {Host}")
            Dim Port As String = ini.ReadValue("Settings", "Port")
            Console.WriteLine($"Port: {Port}")
            Dim ServiceName As String = ini.ReadValue("Settings", "ServiceName")
            Console.WriteLine($"ServiceName: {ServiceName}")

            ' セクションを削除
            'ini.DeleteSection("Settings")

            ' キーを削除
            'ini.DeleteKey("Settings", "Username")
        Catch ex As Exception
            Console.WriteLine(ex.GetType().ToString())
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.StackTrace)
        End Try
        Console.ReadKey()
    End Sub

End Module
