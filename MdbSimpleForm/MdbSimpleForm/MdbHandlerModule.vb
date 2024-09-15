
'Imports System.Data.OleDb
'Imports System.IO

'Module MdbHandlerModule

'    Public Class MDBHandler
'        Private mdbPath As String
'        Private connectionString As String
'        Private conn As OleDbConnection

'        ' Constructor
'        Public Sub New()
'            Initialize()
'        End Sub

'        ' Initialize method
'        Private Sub Initialize()
'            conn = New OleDbConnection()
'        End Sub

'        ' SetValues method to configure the MDB file path
'        Public Sub SetValues(mdbPath As String)
'            Me.mdbPath = mdbPath
'            connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={mdbPath};"
'            conn.ConnectionString = connectionString
'        End Sub

'        ' Method to get all table names from the MDB file
'        Public Function GetAllTables() As List(Of String)
'            Dim tableNames As New List(Of String)()
'            Try
'                conn.Open()
'                Dim dt As DataTable = conn.GetSchema("Tables")
'                For Each row As DataRow In dt.Rows
'                    Dim tableName As String = row("TABLE_NAME").ToString()
'                    tableNames.Add(tableName)
'                Next
'            Catch ex As Exception
'                ' Handle the exception or log it as needed
'                Console.WriteLine("Error: " & ex.Message)
'            Finally
'                conn.Close()
'            End Try
'            Return tableNames
'        End Function
'    End Class

'End Module
