Imports System.Data.OracleClient

Public Class FormOracleSample

    Dim _oracleServerInfo As OracleServerInfo
    Dim _oracleManager As OracleManager
    Dim _ini As IniStream


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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        InitializeOracleAccessInfo()
        'Dim sqlStr = "SELECT * FROM CUSTOMER_INFO"
        '_oracleManager.ConnectTest(_oracleServerInfo, sqlStr)
        Dim columnNameList = _oracleManager.GetColumnNameList(_oracleServerInfo, "CUSTOMER_INFO")
        'Dim buf = String.Join(", ", columnNameList)
        Dim buf As String
        For Each value In columnNameList
            buf += value.ToString() + ", "
        Next
        buf = buf.Substring(0, buf.Length - 2)
        Console.WriteLine("columnNames = " + buf)
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        InitializeOracleAccessInfo()
        '_oracleManager.ConnectOracleDataAccess(_oracleServerInfo, "CUSTOMER_INFO")

        Dim sqlStr = "SELECT * FROM CUSTOMER_INFO"
        Dim colNames = _oracleManager.GetColumnNames(_oracleServerInfo, sqlStr)
        Dim defStr = _oracleManager.GetColumnDefString(colNames)
        Console.WriteLine("defStr=" + defStr)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        InitializeOracleAccessInfo()
        Dim defStrArray = {"ID", "NAME", "AGE", "CITY", "MEMBERSHIP_TYPE_"}
        Dim ret = _oracleManager.CheckValidColumnName(_oracleServerInfo, "CUSTOMER_INFO", defStrArray.ToList())
        Console.WriteLine(String.Format("ret = {0}", ret))
    End Sub
End Class
