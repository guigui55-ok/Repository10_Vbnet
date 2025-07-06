Public Class OracleSettingReader

    Public Class OracleSettingData
        Public Host As String = ""
        Public Port As String = ""
        Public Pdb As String = ""
        Public User As String = ""
        Public Password As String = ""
    End Class

    Public _data As OracleSettingData

    Sub New()
        _data = New OracleSettingData()
    End Sub

    Public Function GetSettingStr() As String
        Dim _list = New List(Of String)
        Dim buf = ""
        buf = $"Pdb = {_data.Pdb}"
        _list.Add(buf)
        buf = $"Host = {_data.Host}"
        _list.Add(buf)
        buf = $"Port = {_data.Port}"
        _list.Add(buf)
        buf = $"User = {_data.User}"
        _list.Add(buf)
        buf = $"Pass = {_data.Password}"
        _list.Add(buf)
        Dim ret = String.Join(", ", _list)
        Return ret
    End Function

    Public Sub ReadSettingData(iniLikeFilePath As String)

        'def
        Dim Host = ""
        Dim pdb = ""
        Dim UserName = ""
        Dim Password = ""
        Dim Port = ""

        'read file
        Dim readlines = Nothing
        Dim readbuf = ""
        Using st = New IO.StreamReader(iniLikeFilePath)
            readbuf = st.ReadToEnd()
        End Using
        readlines = readbuf.Split(vbCrLf)
        pdb = GetValueStringArray(readlines, "ServiceName=")
        Host = GetValueStringArray(readlines, "Host=")
        Port = GetValueStringArray(readlines, "Port=")
        UserName = GetValueStringArray(readlines, "UserName=")
        Password = GetValueStringArray(readlines, "Password=")

        'set value
        _data.Pdb = pdb
        _data.Host = Host
        _data.Port = Port
        _data.User = UserName
        _data.Password = Password

    End Sub

    Public Sub ReadSettingDataSample()
        'read file
        Dim readPath = "C:\Users\OK\source\repos\test_media_files\vbnet_oracle\oracle_setting.ini"
        ReadSettingData(readPath)
    End Sub

    Private Function GetValueStringArray(lines() As String, keyName As String) As String
        For Each line In lines
            If line.Trim.Contains(keyName) Then
                Dim buf = line.Replace(keyName, "")
                buf = buf.Trim
                Return buf
            End If
        Next
        Return ""
    End Function

End Class
