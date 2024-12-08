Imports System.Data.OracleClient

Public Class FormOracleSample

    Dim _oracleServerInfo As OracleServerInfo
    Dim _oracleManager As OracleManager
    Dim _ini As IniStream

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        InitializeOracleAccessInfo()
        Dim sqlStr = "SELECT * FROM CUSTOMER_INFO"
        _oracleManager.ConnectTest(_oracleServerInfo, sqlStr)
    End Sub

    Private Sub FormOracleSample_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _oracleServerInfo = New OracleServerInfo()
        _oracleManager = New OracleManager()
        _ini = New IniStream("")
    End Sub

    Private Sub InitializeOracleAccessInfo()

        Dim iniPath = "C:\Users\OK\source\repos\test_media_files\vbnet_oracle\oracle_setting.ini"
        _ini = New IniStream(iniPath)

        ' 値を書き込む
        _ini.WriteValue("Settings", "UserName", "test_user")

        ' 値を読み込む
        Dim UserName As String = _ini.ReadValue("Settings", "UserName")
        Console.WriteLine($"UserName: {UserName}")
        Dim Password As String = _ini.ReadValue("Settings", "Password")
        Console.WriteLine($"Password: {Password}")
        Dim Host As String = _ini.ReadValue("Settings", "Host")
        Console.WriteLine($"Host: {Host}")
        Dim Port As String = _ini.ReadValue("Settings", "Port")
        Console.WriteLine($"Port: {Port}")
        Dim ServiceName As String = _ini.ReadValue("Settings", "ServiceName")
        Console.WriteLine($"ServiceName: {ServiceName}")

        Try
            _oracleServerInfo.UserName = UserName
            _oracleServerInfo.Password = Password
            _oracleServerInfo.Host = Host
            Dim bufint = Integer.Parse(Port)
            _oracleServerInfo.Port = bufint
            _oracleServerInfo.ServiceName = ServiceName
        Catch ex As Exception
            ConsoleOutputError(ex)
        End Try
    End Sub

End Class
