Imports System.Data.OracleClient

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

        Public Sub ConnectTest(serverInfo As OracleServerInfo, sqlStr As String)
            Console.WriteLine($"現在のプロセスは: {(If(Environment.Is64BitProcess, "64ビット", "32ビット"))} で動作中です。")
            ConnectTestB(
                serverInfo.UserName,
                serverInfo.Password,
                serverInfo.Host,
                serverInfo.Port,
                serverInfo.ServiceName,
                sqlStr)
        End Sub


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
