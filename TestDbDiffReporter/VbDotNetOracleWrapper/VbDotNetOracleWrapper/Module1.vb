Imports Oracle.ManagedDataAccess.Client

Module Module1
    'Install  OracleDataAccess v19.28
    Sub Main_()
        TestMain()
    End Sub

    Sub TestMain()
        Dim logger As New DummyLogger()
        logger.Info(vbCrLf + "**** TestMain")

        Dim Host = ""
        Dim ServerName = ""
        Dim UserName = ""
        Dim Password = ""
        Dim Port = ""

        Dim readPath = "C:\Users\OK\source\repos\test_media_files\vbnet_oracle\oracle_setting.ini"
        Dim readlines = Nothing
        Dim readbuf = ""
        Using st = New IO.StreamReader(readPath)
            readbuf = st.ReadToEnd()
        End Using
        readlines = readbuf.Split(vbCrLf)
        Host = GetValue(readlines, "Host=")
        ServerName = GetValue(readlines, "ServiceName=")
        UserName = GetValue(readlines, "UserName=")
        Password = GetValue(readlines, "Password=")
        Port = GetValue(readlines, "Port=")

        Dim connStr = ""
        connStr = OracleDbManager.CreateConnectionString(UserName, Password, Host, Port, ServerName)
        'connStr = OracleDbManager.CreateDetailedConnectionString(UserName, Password, Host, Port, ServerName)
        logger.Info($"connStr = {connStr}" + vbCrLf)

        Dim db As New OracleDbManager(connStr, logger)
        Try
            db.Open()
            db.BeginTransaction()

            Dim sql = ""
            sql = "SELECT table_name FROM user_tables"
            sql = "SELECT * FROM CUSTOMER_INFO WHERE ROWNUM = 1"
            'db.ExecuteNonQuery(sql)
            'db.ExecuteNonQuery(sql, {New OracleClient.OracleParameter(":id", 1)}) 'プレースホルダーを含める場合

            Dim dt As DataTable = db.ExecuteQuery(sql)
            logger.Info("Result(DataTable)=")
            OutputDataTable(logger, dt)

            db.Commit()
        Catch ex As Exception
            db.Rollback()
        Finally
            db.Dispose()
        End Try
        logger.Info("TestMain Done." + vbCrLf)
    End Sub

    Function GetValue(lines() As String, keyName As String) As String
        For Each line In lines
            If line.Trim.Contains(keyName) Then
                Dim buf = line.Replace(keyName, "")
                buf = buf.Trim
                Return buf
            End If
        Next
        Return ""
    End Function

    Sub UsageSample()
        Dim Host = "localhost"
        Dim UserName = "myuser"
        Dim Password = "mypass"
        Dim Port = "1521"
        Dim pdb = "XEPDB1" 'Pluggable Database (PDB)

        Dim connStr = ""
        'connStr As String = "User Id=myuser;Password=mypass;Data Source=//localhost:1521/XEPDB1"
        connStr = OracleDbManager.CreateConnectionString(UserName, Password, Host, Port, pdb)

        connStr = OracleDbManager.CreateDetailedConnectionString(UserName, Password, Host, Port, pdb)

        Dim logger As New DummyLogger()
        Dim db As New OracleDbManager(connStr, logger)
        Try
            db.Open()
            db.BeginTransaction()

            Dim sql = ""
            'sql = "UPDATE users SET balance = balance - 100 WHERE user_id = :id"
            sql = "SELECT table_name FROM user_tables;"
            Dim v = db.ExecuteNonQuery(sql, {New OracleClient.OracleParameter(":id", 1)})

            '##########
            'サンプル1：      件数を取得
            Dim sqlB As String = "SELECT COUNT(*) FROM users"
            Dim count As Integer = Convert.ToInt32(db.ExecuteScalar(sqlB))

            '✅ サンプル2：1つの値を取得（WHERE句付き）
            Dim sqlC As String = "SELECT balance FROM users WHERE user_id = :id"
            Dim param As New OracleClient.OracleParameter(":id", 1)
            Dim balance As Decimal = Convert.ToDecimal(db.ExecuteScalar(sqlC, {param}))

            db.Commit()
        Catch ex As Exception
            db.Rollback()
        Finally
            db.Dispose()
        End Try
    End Sub

    Sub OutputDataTable(logger As Object, dt As DataTable)
        logger.Info($"DataTable.Name = [{dt.TableName}]")
        Dim _retlist = New List(Of String)
        Dim count = 0
        For Each row As DataRow In dt.Rows
            Dim _list = New List(Of String)
            For Each col As DataColumn In dt.Columns
                Dim buf = row(col)
                If buf Is Nothing Or IsDBNull(buf) Then
                    buf = ""
                End If
                buf = $"{col.ColumnName} = buf"
                _list.Add($"[{count}] " + buf)
            Next
            Dim bufLog = String.Join(", ", _list)
            _retlist.Add(bufLog)
            logger.info(bufLog)
        Next
        Dim log = String.Join(vbCrLf, _retlist)
        'logger.Info(log)
    End Sub

End Module
