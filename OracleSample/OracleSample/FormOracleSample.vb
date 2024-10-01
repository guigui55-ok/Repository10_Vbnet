Imports System.Data.OracleClient

Public Class FormOracleSample
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim userName As String = ""
        Dim password As String = ""
        Dim host As String = ""
        Dim port As String = ""
        Dim ServiceName As String = ""
        ConnectTestA(userName, password, host, port, ServiceName)
    End Sub

    Private Sub ConnectTestA(userName As String, password As String, host As String, port As String, ServiceName As String)
        Try
            ConsoleWriteLine("ConnectTestA")

            ' Oracleデータベースへの接続文字列
            Dim connectionString As String
            'Dim connectionString As String = "User Id=your_username;Password=your_password;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=your_host)(PORT=your_port))(CONNECT_DATA=(SERVICE_NAME=your_service_name)));"
            connectionString = String.Format("User Id={0};Password={1};Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={2})(PORT={3}))(CONNECT_DATA=(SERVICE_NAME={4})));", userName, password, host, port, ServiceName)

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
                    Console.WriteLine("エラーが発生しました: " & ex.Message)
                Finally
                    ' 接続を閉じる
                    connection.Close()
                End Try
            End Using

            Console.WriteLine("処理終了")
            Console.ReadLine()

        Catch ex As Exception
            ConsoleWriteLine(" ########## ")
            ConsoleWriteLine(ex.Message)
            ConsoleWriteLine(ex.StackTrace)
            ConsoleWriteLine(" ########## ")
        End Try
    End Sub

    ' ユーザー作成およびテーブル作成用のメソッド
    Sub CreateOracleDatabase(userName As String, password As String, host As String, port As String, serviceName As String)
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
            Console.WriteLine("エラーが発生しました: " & ex.Message)
        End Try
    End Sub


    Private Sub ConsoleWriteLine(value As String)
        Console.WriteLine(value)
    End Sub

    Private Sub FormOracleSample_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
