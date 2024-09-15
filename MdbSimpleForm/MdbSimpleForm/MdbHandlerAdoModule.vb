
Imports ADODB
Imports System.IO

'COM＞「Microsoft ActiveX Data Objects 6.1 Library」または「Microsoft ActiveX Data Objects 2.x Library」をインストール
Module MdbHandlerAdoModule

    Public Class MDBHandler
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

        Public Sub Connect()

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
    End Class

    Public Sub DebugPrint(value As String)
        Debug.WriteLine(value)
    End Sub

End Module
