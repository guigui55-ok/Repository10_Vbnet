Imports Oracle.ManagedDataAccess.Client

Public Module OracleSampleModule

    Public Class OracleSampleClass
        Public Sub Test1()
            ' Oracle接続用の接続文字列を設定
            ' サーバー名、ポート番号、サービス名（またはSID）、ユーザー名、パスワードを指定
            Dim connectionString As String = "User Id=your_username;Password=your_password;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=your_host)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=your_service_name)));"


            ' OracleConnectionオブジェクトの作成
            Using conn As New OracleConnection(connectionString)
                Try
                    ' 接続を開く
                    conn.Open()
                    Console.WriteLine("Oracleデータベースに接続しました。")

                    ' SQLクエリの作成
                    Dim sql As String = "SELECT * FROM your_table"

                    ' OracleCommandオブジェクトの作成
                    Using cmd As New OracleCommand(sql, conn)
                        ' OracleDataReaderでデータを読み取る
                        Using reader As OracleDataReader = cmd.ExecuteReader()
                            ' データを読み込みながら表示
                            While reader.Read()
                                Console.WriteLine("カラム1: " & reader("column1").ToString() & ", カラム2: " & reader("column2").ToString())
                            End While
                        End Using
                    End Using

                Catch ex As Exception
                    ' エラーハンドリング
                    Console.WriteLine("エラー: " & ex.Message)
                Finally
                    ' 接続を閉じる
                    conn.Close()
                    Console.WriteLine("接続を閉じました。")
                End Try
            End Using
        End Sub
    End Class
End Module
