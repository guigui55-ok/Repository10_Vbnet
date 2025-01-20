Imports System.Data.Common
Imports System.Data.OracleClient
Imports Oracle.DataAccess.Client
Imports OracleCommand = System.Data.OracleClient.OracleCommand
Imports OracleConnection = System.Data.OracleClient.OracleConnection
Imports OracleDataAdapter = System.Data.OracleClient.OracleDataAdapter
Imports OracleDataReader = System.Data.OracleClient.OracleDataReader
Imports OracleException = System.Data.OracleClient.OracleException
Imports OracleParameter = System.Data.OracleClient.OracleParameter

Public Module ModuleOracleManager
    Public Sub ConsoleWriteLine(value As String)
        Console.WriteLine(value)
    End Sub
    Public Sub ConsoleOutputError(ex As Exception)
        Console.WriteLine("# エラーが発生しました: ")
        Console.WriteLine(ex.GetType().ToString())
        Console.WriteLine(ex.Message)
        Console.WriteLine(ex.StackTrace)
        '
    End Sub


    Public Class OracleManager

        Public Sub ConnectTest(serverInfo As OracleServerInfo, sqlStr As String, ByRef dataSetValue As DataSet)
            Console.WriteLine($"現在のプロセスは: {(If(Environment.Is64BitProcess, "64ビット", "32ビット"))} で動作中です。")
            'ConnectTestB(
            '    serverInfo.UserName,
            '    serverInfo.Password,
            '    serverInfo.Host,
            '    serverInfo.Port,
            '    serverInfo.ServiceName,
            '    sqlStr)
            dataSetValue = ConnectTestC(
                serverInfo.UserName,
                serverInfo.Password,
                serverInfo.Host,
                serverInfo.Port,
                serverInfo.ServiceName,
                sqlStr)
            Dim dictList = ConvertDataSetToDictList(dataSetValue)
            DebugOutputDictionaryList(dictList)

        End Sub

        Public Sub GetDataTableByExecuteSql(serverInfo As OracleServerInfo, sqlStr As String, ByRef dataSetValue As DataSet)
            dataSetValue = ConnectTestC(
                serverInfo.UserName,
                serverInfo.Password,
                serverInfo.Host,
                serverInfo.Port,
                serverInfo.ServiceName,
                sqlStr)
            Dim dictList = ConvertDataSetToDictList(dataSetValue)
            DebugOutputDictionaryList(dictList)

        End Sub

        Public Function ExecutePlSqlWithError(serverInfo As OracleServerInfo, sql As String) As Boolean
            Console.WriteLine("##########")
            Console.WriteLine("ExecutePlSqlWithError")
            Try
                Dim connectionString As String = serverInfo.GetConnectionString()
                Console.WriteLine(String.Format("ConnectionString = {0}", connectionString))

                Using connection As New OracleConnection(connectionString)
                    connection.Open()
                    Console.WriteLine("接続成功!")

                    ' DBMS_OUTPUTを有効化する
                    Using command As New OracleCommand("BEGIN DBMS_OUTPUT.ENABLE(); END;", connection)
                        command.ExecuteNonQuery()
                    End Using

                    ' SQLを実行
                    Using command As New OracleCommand(sql, connection)
                        command.CommandType = CommandType.Text
                        Try
                            command.ExecuteNonQuery()
                            Console.WriteLine("SQL 実行成功!")
                            Return True
                        Catch ex As OracleException
                            Console.WriteLine("Oracle エラーが発生しました: " & ex.Message)
                            Return False
                        End Try
                    End Using

                    ' DBMS_OUTPUTの内容を取得
                    Using command As New OracleCommand("BEGIN DBMS_OUTPUT.GET_LINE(:line, :status); END;", connection)
                        command.Parameters.Add("line", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output
                        command.Parameters.Add("status", OracleDbType.Int32).Direction = ParameterDirection.Output

                        While True
                            command.ExecuteNonQuery()
                            Dim status As Integer = Convert.ToInt32(command.Parameters("status").Value)
                            If status <> 0 Then
                                Exit While
                            End If
                            Console.WriteLine("DBMS_OUTPUT: " & command.Parameters("line").Value.ToString())
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Console.WriteLine("エラーが発生しました: " & ex.Message)
                Return False
            End Try
        End Function


        '配列の宣言文字列を作成（コード貼り付けよう）
        Public Function GetColumnDefString(columnList As List(Of String))
            'Dim bufList = strValueWithConmma.Split(",")
            Dim ret = ""
            For i As Integer = 0 To columnList.Count - 1
                Dim buf = columnList(i)
                buf = buf.Trim()
                buf = String.Format("""{0}""", buf)
                columnList(i) = buf
                ret += buf + ", "
            Next
            ret = ret.Substring(0, ret.Length - 2)
            ret = "{ " + ret + " }"
            'Dim ret = String.Join(",", bufList)
            'ret = String.Format("{ {0} }", ret)
            Return ret
        End Function


        '特定のテーブルのカラム一覧取得
        Public Function GetColumnNames(
                serverInfo As OracleServerInfo,
                sqlStr As String) As List(Of String)
            Console.WriteLine(" ########## ")
            Console.WriteLine("GetColumnNames")
            Dim columnNames As New List(Of String)()

            Try
                Dim connectionString As String = serverInfo.GetConnectionString()
                Console.WriteLine(String.Format("ConnectionString = {0}", connectionString))

                Using connection As New OracleConnection(connectionString)
                    connection.Open()
                    Console.WriteLine("接続成功!")

                    ' OracleDataAdapterを使用してクエリ結果を取得
                    Dim result As New DataSet()
                    Using adapter As New OracleDataAdapter(sqlStr, connection)
                        adapter.Fill(result, 1) ' DataSetに最初の1行だけを格納
                    End Using

                    ' DataSetにテーブルが存在するか確認し、カラム名を取得
                    If result.Tables.Count > 0 Then
                        Dim table As DataTable = result.Tables(0)
                        For Each column As DataColumn In table.Columns
                            columnNames.Add(column.ColumnName)
                        Next
                    Else
                        Console.WriteLine("データが存在しません。")
                    End If
                End Using
            Catch ex As Exception
                Console.WriteLine("エラーが発生しました: " & ex.Message)
            End Try

            Return columnNames
        End Function



        'DataAccessでSelect実行
        Public Sub ConnectOracleDataAccess(serverInfo As OracleServerInfo, tableName As String)
            'Oracle.DataAccess.dll 使用Ver
            Dim providerName As String = "Oracle.DataAccess.Client"
            Dim connectionString As String = serverInfo.GetConnectionStringDataAccess()
            Console.WriteLine(String.Format("ConnectionString = {0}", connectionString))
            Try
                ' DbProviderFactoryの取得
                Dim factory As DbProviderFactory = DbProviderFactories.GetFactory(providerName)

                ' コネクションの作成
                Using connection As DbConnection = factory.CreateConnection()
                    connection.ConnectionString = connectionString
                    connection.Open()

                    ' コマンドの作成
                    Using command As DbCommand = connection.CreateCommand()
                        command.CommandText = "SELECT * FROM " + tableName

                        ' データリーダーの取得
                        Using reader As DbDataReader = command.ExecuteReader()
                            While reader.Read()
                                ' データの処理
                                Console.WriteLine(reader(0).ToString())
                            End While
                        End Using
                    End Using
                End Using
            Catch ex As DbException
                Console.WriteLine("データベースエラー: " & ex.Message)
            Catch ex As Exception
                Console.WriteLine("一般的なエラー: " & ex.Message)
            End Try
        End Sub


        'カラム名一覧取得 COLUMN_NAME, USER_TAB_COLUMNS 使用
        Public Function GetColumnNameList(serverInfo As OracleServerInfo, tableName As String)
            Dim columnNames = New List(Of String)
            Using connection As New OracleConnection(serverInfo.GetConnectionString())
                Try
                    connection.Open()

                    ' SQLクエリでカラム名を取得
                    Dim query As String = "SELECT COLUMN_NAME FROM USER_TAB_COLUMNS WHERE TABLE_NAME = :TableName"

                    Using command As New OracleCommand(query, connection)
                        ' テーブル名をパラメータとして設定
                        command.Parameters.Add(New OracleParameter(":TableName", tableName.ToUpper()))

                        Using reader As OracleDataReader = command.ExecuteReader()
                            ' カラム名をリストに追加
                            While reader.Read()
                                Dim buf = reader.GetString(0)
                                columnNames.Add(buf.ToString())
                            End While
                        End Using
                    End Using

                Catch ex As Exception
                    Console.WriteLine($"エラー: {ex.Message}")
                Finally
                    connection.Close()
                End Try
            End Using

            Return columnNames
        End Function


        Public Function CheckValidColumnName(serverInfo As OracleServerInfo, tableName As String, columns As List(Of String))
            Dim errorColumns As New List(Of String)
            Dim connectionString = serverInfo.GetConnectionString()
            Using connection As New OracleConnection(connectionString)
                Try
                    Console.WriteLine(String.Format("ConnectionString = {0}", connectionString))
                    connection.Open()
                    ' 各カラムを1つずつSELECTしてテスト
                    Dim isValidColumn = False
                    Dim count = 0
                    For Each column As String In columns
                        Try
                            Dim testQuery As String = $"SELECT {column} FROM {tableName} WHERE ROWNUM = 1"
                            Using command As New OracleCommand(testQuery, connection)
                                command.ExecuteScalar() ' 実行して確認
                            End Using
                            isValidColumn = True
                        Catch ex As Exception
                            ' エラーが発生したカラムを記録
                            errorColumns.Add(column)
                            Console.WriteLine($"エラー発生: カラム {column}, エラー: {ex.Message}")
                            isValidColumn = False
                        End Try
                        Console.WriteLine(String.Format("[{0}] {1} = {2}", count, column, isValidColumn))
                        count += 1
                    Next
                Catch ex As Exception
                    Console.WriteLine($"接続エラー: {ex.Message}")
                Finally
                    connection.Close()
                End Try
            End Using

            ' エラーのあるカラムを出力
            If errorColumns.Count > 0 Then
                Console.WriteLine("エラーが発生したカラム一覧:")
                For Each column As String In errorColumns
                    Console.WriteLine(column)
                Next
                Return False
            Else
                Console.WriteLine("すべてのカラムでエラーは発生しませんでした。")
                Return True
            End If
        End Function


        Public Sub DebugOutputDictionaryList(
            dictList As List(Of Dictionary(Of String, Object)),
            Optional separator As String = ", ")
            For Each dict As Dictionary(Of String, Object) In dictList
                Dim formattedEntries As String = String.Join(
                    separator,
                    dict.Select(Function(kv) String.Format("{0}:{1}", kv.Key, kv.Value))
                )
                ConsoleWriteLine(formattedEntries)
            Next
        End Sub

        Public Function ConvertDataSetToDictList(dataSet As DataSet) As List(Of Dictionary(Of String, Object))
            Dim result As New List(Of Dictionary(Of String, Object))()

            If dataSet IsNot Nothing AndAlso dataSet.Tables.Count > 0 Then
                For Each row As DataRow In dataSet.Tables(0).Rows
                    Dim rowDict As New Dictionary(Of String, Object)()
                    For Each column As DataColumn In dataSet.Tables(0).Columns
                        rowDict(column.ColumnName) = If(row.IsNull(column), Nothing, row(column))
                    Next
                    result.Add(rowDict)
                Next
            End If

            Return result
        End Function

        Public Function GetDataByExecuteSqlServerInfo(serverInfo As OracleServerInfo, sqlStr As String, ByRef result As DataSet)
            Return GetDataByExecuteSql(
                serverInfo.UserName,
                serverInfo.Password,
                serverInfo.Host,
                serverInfo.Port,
                serverInfo.ServiceName,
                sqlStr, result)
        End Function

        Public Function GetDataByExecuteSql(
                userName As String,
                password As String,
                host As String,
                port As String,
                ServiceName As String,
                sqlStr As String,
                ByRef result As DataSet) As Integer
            ConsoleWriteLine(" ########## ")
            ConsoleWriteLine("GetDataByExecuteSql")
            Try
                Dim connectionString As String = String.Format(
                    "User Id={0};Password={1};Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={2})(PORT={3}))(CONNECT_DATA=(SERVICE_NAME={4})));",
                    userName, password, host, port, ServiceName)

                Using connection As New OracleConnection(connectionString)
                    connection.Open()
                    ConsoleWriteLine("OracleConnection.Open Success.")

                    ConsoleWriteLine(String.Format("SQL = {0}", sqlStr))
                    ' OracleDataAdapterを使用してクエリ結果を取得
                    Using adapter As New OracleDataAdapter(sqlStr, connection)
                        adapter.Fill(result) ' DataSetにデータを格納
                    End Using
                End Using
                Return 1
            Catch ex As Exception
                ConsoleOutputError(ex)
                Return -1
            End Try
        End Function

        Public Function ConnectTestC(
                userName As String,
                password As String,
                host As String,
                port As String,
                ServiceName As String,
                sqlStr As String) As DataSet
            ConsoleWriteLine(" ########## ")
            ConsoleWriteLine("ConnectTestC")
            Dim result As New DataSet()
            Try
                Dim connectionString As String = String.Format(
                    "User Id={0};Password={1};Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={2})(PORT={3}))(CONNECT_DATA=(SERVICE_NAME={4})));",
                    userName, password, host, port, ServiceName)

                Using connection As New OracleConnection(connectionString)
                    connection.Open()
                    ConsoleWriteLine("接続成功!")

                    ' OracleDataAdapterを使用してクエリ結果を取得
                    Using adapter As New OracleDataAdapter(sqlStr, connection)
                        adapter.Fill(result) ' DataSetにデータを格納
                    End Using
                End Using
            Catch ex As Exception
                ConsoleOutputError(ex)
            End Try
            Return result
        End Function

        Public Sub ConnectTestB(
                userName As String,
                password As String,
                host As String,
                port As String,
                ServiceName As String,
                sqlStr As String)
            Try
                ConsoleWriteLine(" ########## ")
                ConsoleWriteLine("ConnectTestB")
                Dim connectionString As String = String.Format(
                    "User Id={0};Password={1};Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={2})(PORT={3}))(CONNECT_DATA=(SERVICE_NAME={4})));",
                    userName, password, host, port, ServiceName)

                Using connection As New OracleConnection(connectionString)
                    connection.Open()
                    ConsoleWriteLine("接続成功!")
                    Dim command As New OracleCommand(sqlStr, connection)
                    Using reader As OracleDataReader = command.ExecuteReader()
                        'While reader.Read()
                        '    ConsoleWriteLine(reader.GetString(0))
                        'End While
                        While reader.Read()
                            Dim columnValue As Object = reader.GetValue(1) '*番目のカラムの値が取得される
                            If Not IsDBNull(columnValue) Then
                                Dim buf = columnValue.ToString()
                                ConsoleWriteLine(buf)
                            Else
                                ConsoleWriteLine("NULL 値")
                            End If
                        End While
                    End Using
                End Using
            Catch ex As Exception
                ConsoleOutputError(ex)
            End Try
        End Sub

        Public Sub ConnectTestA(userName As String, password As String, host As String, port As String, ServiceName As String)
            Try
                'Error 241207
                ConsoleWriteLine("ConnectTestA")

                ' Oracleデータベースへの接続文字列
                Dim connectionString As String
                'Dim connectionString As String = "User Id=your_username;Password=your_password;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=your_host)(PORT=your_port))(CONNECT_DATA=(SERVICE_NAME=your_service_name)));"
                connectionString = String.Format("User Id={0};Password={1};Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={2})(PORT={3}))(CONNECT_DATA=(SERVICE_NAME={4})));", userName, password, host, port, ServiceName)

                ConsoleWriteLine(String.Format(" connectionString = {0}", connectionString))

                ' Oracleコネクションオブジェクトを作成
                Using connection As New OracleConnection(connectionString)
                    Try
                        ' データベース接続を開く
                        connection.Open()
                        Console.WriteLine("接続成功!")

                        ' SQLクエリを実行
                        Dim command As New OracleCommand("SELECT * FROM your_table", connection)
                        Using reader As OracleDataReader = command.ExecuteReader()
                            While reader.Read()
                                ' カラムのデータを取得（例：カラム1の値）
                                Console.WriteLine(reader.GetString(0)) ' 0は最初のカラム
                            End While
                        End Using

                    Catch ex As Exception
                        ConsoleOutputError(ex)
                        '
                        'エラーが発生しました:         Oracle クライアント ライブラリを読み込もうとしましたが、BadImageFormatException が発行されました。この問題は、32 ビットの Oracle クライアント コンポーネントがインストールされている環境で 64 ビット モードを実行すると発生します。
                    Finally
                        ' 接続を閉じる
                        connection.Close()
                    End Try
                End Using

                Console.WriteLine("処理終了")
                Console.ReadLine()

            Catch ex As Exception
                ConsoleOutputError(ex)
            End Try
        End Sub

        ' ユーザー作成およびテーブル作成用のメソッド
        Public Sub CreateOracleDatabase(userName As String, password As String, host As String, port As String, serviceName As String)
            Try
                Console.WriteLine("CreateOracleDatabase")

                ' 管理者ユーザーとして接続する（例：SYSTEMユーザー）
                Dim adminConnectionString As String = String.Format("User Id=system;Password=admin_password;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SERVICE_NAME={2})));", host, port, serviceName)

                Using connection As New OracleConnection(adminConnectionString)
                    connection.Open()
                    Console.WriteLine("接続成功: SYSTEMユーザー")

                    ' 新しいユーザー（スキーマ）を作成
                    Dim createUserCommand As New OracleCommand("CREATE USER " & userName & " IDENTIFIED BY " & password, connection)
                    createUserCommand.ExecuteNonQuery()
                    Console.WriteLine("新しいユーザー作成成功")

                    ' 作成したユーザーに権限を付与
                    Dim grantPrivilegesCommand As New OracleCommand("GRANT CONNECT, RESOURCE TO " & userName, connection)
                    grantPrivilegesCommand.ExecuteNonQuery()
                    Console.WriteLine("権限付与成功")

                    ' 新しいユーザーに接続するための文字列
                    Dim userConnectionString As String = String.Format("User Id={0};Password={1};Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={2})(PORT={3}))(CONNECT_DATA=(SERVICE_NAME={4})));", userName, password, host, port, serviceName)

                    ' 作成したユーザーに切り替えて、テーブルを作成
                    Using userConnection As New OracleConnection(userConnectionString)
                        userConnection.Open()
                        Console.WriteLine("新しいユーザーに接続成功")

                        ' テーブル作成のSQL
                        Dim createTableCommand As New OracleCommand("CREATE TABLE TestTable (ID NUMBER PRIMARY KEY, Name VARCHAR2(100))", userConnection)
                        createTableCommand.ExecuteNonQuery()
                        Console.WriteLine("テーブル作成成功")
                    End Using
                End Using
            Catch ex As Exception
                ConsoleOutputError(ex)
            End Try
        End Sub
    End Class
End Module
