
Imports ADODB
Imports System.IO

'COM＞「Microsoft ActiveX Data Objects 6.1 Library」または「Microsoft ActiveX Data Objects 2.x Library」をインストール
Module MdbHandlerAdoModule

    Public Class MdbManager
        Private mdbPath As String
        Private conn As ADODB.Connection

        ' コンストラクタ
        Public Sub New()
            Initialize()
        End Sub

        ' Initializeメソッド
        Private Sub Initialize()
            conn = New ADODB.Connection()
        End Sub

        ' MDBファイルパスを設定するSetValuesメソッド
        Public Sub SetValues(mdbPath As String)
            Me.mdbPath = mdbPath
            Dim conStr As String = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={mdbPath};"
            DebugPrint(String.Format("ConnectionStr = {0}", conStr))
            conn.ConnectionString = conStr
        End Sub

        ' MDBファイルからすべてのテーブル名を取得するメソッド
        Public Function GetAllTables() As List(Of String)
            Dim tableNames As New List(Of String)()
            Dim schema As Recordset
            Try
                If (Not File.Exists(mdbPath)) Then
                    DebugPrint("path is Nothing")
                    DebugPrint(String.Format("mdbPath = {0}", mdbPath))
                    Return New List(Of String)()
                End If
                conn.Open()
                DebugPrint("Connection.Open")

                ' テーブル情報を取得
                schema = conn.OpenSchema(SchemaEnum.adSchemaTables)
                DebugPrint("OpenSchema")

                While Not schema.EOF
                    ' テーブルの種類が「TABLE」のものをリストに追加
                    If schema.Fields("TABLE_TYPE").Value.ToString() = "TABLE" Then
                        tableNames.Add(schema.Fields("TABLE_NAME").Value.ToString())
                    End If
                    schema.MoveNext()
                End While
                DebugPrint("GetTables Done.")
            Catch ex As Exception
                ' エラーハンドリング
                DebugPrint("エラー: " & ex.Message)
            Finally
                conn.Close()
            End Try
            Return tableNames
        End Function

        ' レコードを取得するメソッド
        Public Function GetRecords(tableName As String, Optional startRecord As Integer = 0, Optional recordCount As Integer = -1, Optional columnList As List(Of String) = Nothing) As List(Of Dictionary(Of String, Object))
            Dim records As New List(Of Dictionary(Of String, Object))()

            Try
                conn.Open()

                ' カラムの指定がない場合はすべてのカラムを取得
                Dim columns As String = "*"
                If columnList IsNot Nothing AndAlso columnList.Count > 0 Then
                    columns = String.Join(", ", columnList)
                End If

                ' SQLクエリの作成
                Dim query As String = $"SELECT {columns} FROM {tableName}"

                ' レコードの範囲指定がある場合
                If recordCount > 0 Then
                    query &= $" OFFSET {startRecord} ROWS FETCH NEXT {recordCount} ROWS ONLY"
                End If

                Dim rs As New Recordset
                rs.Open(query, conn)

                ' レコードを取得してリストに格納
                While Not rs.EOF
                    Dim record As New Dictionary(Of String, Object)()

                    For i As Integer = 0 To rs.Fields.Count - 1
                        Dim fieldName As String = rs.Fields(i).Name
                        Dim fieldValue As Object = rs.Fields(i).Value
                        record(fieldName) = fieldValue
                    Next

                    records.Add(record)
                    rs.MoveNext()
                End While
                rs.Close()
            Catch ex As Exception
                DebugPrint("エラー: " & ex.Message)
            Finally
                conn.Close()
            End Try

            Return records
        End Function

    End Class
    ' デバッグ用の出力メソッド（適宜実装）
    Public Sub DebugPrint(message As String)
        Console.WriteLine(message)
    End Sub

End Module
