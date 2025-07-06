Imports VbDotNetOracleWrapper

Public Class FormOracleManager
    Public _formLog As FormLog
    Public _logger As AppLogger
    Private Sub FormOracleManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '動作確認用

        'def
        Dim Host = ""
        Dim pdb = ""
        Dim UserName = ""
        Dim Password = ""
        Dim Port = ""

        'read file
        Dim readPath = "C:\Users\OK\source\repos\test_media_files\vbnet_oracle\oracle_setting.ini"
        Dim readlines = Nothing
        Dim readbuf = ""
        Using st = New IO.StreamReader(readPath)
            readbuf = st.ReadToEnd()
        End Using
        readlines = readbuf.Split(vbCrLf)
        Host = GetValueStringArray(readlines, "Host=")
        pdb = GetValueStringArray(readlines, "ServiceName=")
        UserName = GetValueStringArray(readlines, "UserName=")
        Password = GetValueStringArray(readlines, "Password=")
        Port = GetValueStringArray(readlines, "Port=")

        'set control
        TextBox_Pdb.Text = pdb
        TextBox_Host.Text = Host
        TextBox_Port.Text = Port
        TextBox_UserName.Text = UserName
        TextBox_Password.Text = Password

        Dim sql = ""
        sql = "SELECT * FROM CUSTOMER_INFO WHERE ROWNUM = 1"
        RichTextBox_Sql.Text = sql
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

    Private Sub FormOracleManager_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        '_formLog.Show()
        '_formLog.Activate()
        _logger.Info("FormOracleManager Shown")
    End Sub

    Public Sub AddLogToControl(sender As Object, e As EventArgs)
        Dim logMsg = DirectCast(sender, String)
        If RichTextBox_Log.Text.Length > 0 Then logMsg = vbCrLf + logMsg
        RichTextBox_Log.AppendText(logMsg)
        RichTextBox_Log.ScrollToCaret()
    End Sub

    Private Sub Button_Execute_Click(sender As Object, e As EventArgs) Handles Button_Execute.Click
        Dim user = TextBox_UserName.Text
        Dim pw = TextBox_Password.Text
        Dim host = TextBox_Host.Text
        Dim port = TextBox_Port.Text
        Dim pdb = TextBox_Pdb.Text
        Dim sql = RichTextBox_Sql.Text
        _logger.Info("# Button_Execute_Click")
        ExecuteSqlMain(user, pw, host, port, pdb, sql)
    End Sub


    Public Sub ExecuteSqlMain(
            user As String, pw As String, host As String, port As String, pdb As String, sql As String)
        _logger.Info("# ExecuteSql")
        Dim conStr = OracleDbManager.CreateConnectionString(user, pw, host, port, pdb)
        Dim db = New OracleDbManager(conStr, _logger)

        Try
            db.Open()
            db.BeginTransaction()

            _logger.Info($"sql = {sql}")
            Dim dt As DataTable = db.ExecuteQuery(sql)
            _logger.Info("Result(DataTable)=")
            OutputDataTable(_logger, dt)

            '##########
            'サンプル1：      件数を取得
            'Dim sqlB As String = "SELECT COUNT(*) FROM users"
            'Dim count As Integer = Convert.ToInt32(db.ExecuteScalar(sqlB))

            ''✅ サンプル2：1つの値を取得（WHERE句付き）
            'Dim sqlC As String = "SELECT balance FROM users WHERE user_id = :id"
            'Dim param As New OracleClient.OracleParameter(":id", 1)
            'Dim balance As Decimal = Convert.ToDecimal(db.ExecuteScalar(sqlC, {param}))

            db.Commit()
        Catch ex As Exception
            _logger.Err("ExecuteSqlError", ex)
            _logger.Info($"*" + ex.GetType.ToString + ":" + ex.Message)
            db.Rollback()
        Finally
            db.Dispose()
        End Try
        _logger.Info("ExecuteSql Done.")
    End Sub

    Sub OutputDataTable(logger As AppLogger, dt As DataTable)
        logger.Info($"DataTable.Name = [{dt.TableName}]")
        Dim _list = New List(Of String)
        Dim count = 0
        For Each row As DataRow In dt.Rows
            For Each col As DataColumn In dt.Columns
                Dim buf = row(col)
                If buf Is Nothing Or IsDBNull(buf) Then
                    buf = ""
                End If
                buf = $"{col.ColumnName} = buf"
                _list.Add(buf)
            Next
        Next
        Dim log = String.Join(", ", _list)
        logger.Info(log)
    End Sub


    Sub UsageSample()
        Dim Host = "localhost"
        Dim UserName = "myuser"
        Dim Password = "mypass"
        Dim Port = "1521"
        Dim pdb = "XEPDB1" 'Pluggable Database (PDB)

        Dim connStr = ""
        'connStr As String = "User Id=myuser;Password=mypass;Data Source=//localhost:1521/XEPDB1"
        connStr = OracleDbManager.CreateConnectionString(UserName, Password, Host, Port, pdb)

        Dim logger As New DummyLogger()
        Dim db As New OracleDbManager(connStr, logger)
        Try
            db.Open()
            db.BeginTransaction()

            Dim sql = ""
            Dim dt As DataTable = db.ExecuteQuery(sql)
            logger.Info("Result(DataTable)=")

            '##########
            'サンプル1：      件数を取得
            'Dim sqlB As String = "SELECT COUNT(*) FROM users"
            'Dim count As Integer = Convert.ToInt32(db.ExecuteScalar(sqlB))

            ''✅ サンプル2：1つの値を取得（WHERE句付き）
            'Dim sqlC As String = "SELECT balance FROM users WHERE user_id = :id"
            'Dim param As New OracleClient.OracleParameter(":id", 1)
            'Dim balance As Decimal = Convert.ToDecimal(db.ExecuteScalar(sqlC, {param}))

            db.Commit()
        Catch ex As Exception
            db.Rollback()
        Finally
            db.Dispose()
        End Try
    End Sub
End Class
