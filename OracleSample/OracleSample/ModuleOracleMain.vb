Public Module ModuleOracleMain
    Public Class OracleServerInfo
        Public UserName As String = ""
        Public Password As String = ""
        Public Host As String = ""
        Public Port As Integer
        Public ServiceName As String = ""

        Public Function GetConnectionString()
            Dim connectionString As String = String.Format(
                "User Id={0};Password={1};Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={2})(PORT={3}))(CONNECT_DATA=(SERVICE_NAME={4})));",
                UserName, Password, Host, Port, ServiceName)
            Return connectionString
        End Function

        Public Function GetConnectionStringDataAccess()
            Dim connectionString As String = String.Format(
                "Data Source={0};User ID={1};Password={2};", ServiceName, UserName, Password)
            Return connectionString
        End Function

    End Class
End Module
