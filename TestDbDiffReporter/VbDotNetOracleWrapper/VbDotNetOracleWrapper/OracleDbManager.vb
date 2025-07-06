#Disable Warning BC40000

Imports System.Data.OracleClient
'Imports Oracle.ManagedDataAccess.Client

Public Class OracleDbManager
    Private _connectionString As String
    Private _connection As OracleConnection
    Private _transaction As OracleTransaction
    Private _logger As Object ' DebugLogger互換のログオブジェクト

    Public Sub New(connectionString As String, Optional logger As Object = Nothing)
        _connectionString = connectionString
        _connection = New OracleConnection(_connectionString)
        _logger = logger
    End Sub
    ''' <summary>
    ''' Oracle 用の接続文字列を生成します（Oracle ManagedDataAccess 用）
    ''' </summary>
    ''' <param name="userId">ユーザーID</param>
    ''' <param name="password">パスワード</param>
    ''' <param name="host">ホスト名（例: localhost）</param>
    ''' <param name="port">ポート番号（例: 1521）</param>
    ''' <param name="serviceName">サービス名（例: XEPDB1）</param>
    ''' <returns>接続文字列</returns>
    Public Shared Function CreateConnectionString(userId As String, password As String, host As String, port As String, serviceName As String) As String
        Dim dataSource = $"//{host}:{port}/{serviceName}"
        Dim connStr = $"User Id={userId};Password={password};Data Source={dataSource};"
        Return connStr
    End Function

    ''' <summary>
    ''' Oracle用の詳細な接続文字列を生成します（DESCRIPTION構文）
    ''' </summary>
    ''' <param name="userId">ユーザーID</param>
    ''' <param name="password">パスワード</param>
    ''' <param name="host">ホスト名</param>
    ''' <param name="port">ポート番号</param>
    ''' <param name="serviceName">サービス名</param>
    ''' <returns>接続文字列</returns>
    Public Shared Function CreateDetailedConnectionString(userId As String, password As String, host As String, port As String, serviceName As String) As String
        Dim dataSource = $"(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={serviceName})))"
        Dim connStr = $"User Id={userId};Password={password};Data Source={dataSource};"
        Return connStr
    End Function

    Public Sub Open()
        Try
            If _connection.State <> ConnectionState.Open Then
                _connection.Open()
                LogInfo("DB接続を開きました")
            End If
        Catch ex As Exception
            LogError("DB接続エラー", ex)
            Throw
        End Try
    End Sub

    Public Sub Close()
        Try
            If _connection IsNot Nothing AndAlso _connection.State <> ConnectionState.Closed Then
                _connection.Close()
                LogInfo("DB接続を閉じました")
            End If
        Catch ex As Exception
            LogError("DB切断エラー", ex)
        End Try
    End Sub


    Public Function ExecuteQueryMulti(sqlArray() As String, Optional parameters As OracleParameter() = Nothing) As DataSet
        Dim ds As DataSet = New DataSet()
        Try
            Open()
            BeginTransaction()
            For Each sql As String In sqlArray
                Dim newdt = ExecuteQuery(sql, parameters)
                If newdt IsNot Nothing Then
                    ds.Tables.Add(newdt)
                End If
            Next
            Commit()
        Catch ex As Exception
            Rollback()
            _logger.Err("ExecuteQueryMulti Error", ex)
        Finally
            Dispose()
        End Try
        Return ds
    End Function

    Public Function ExecuteQuery(sql As String, Optional parameters As OracleParameter() = Nothing) As DataTable
        Dim dt As DataTable = Nothing
        Try
            Open()
            '_logger.info($"SQL = {sql}")
            dt = _ExecuteQuery(sql, parameters)
            _logger.Info("Result(DataTable)=")
            'OutputDataTable(_logger, dt)

        Catch ex As Exception
            _logger.Err("ExecuteQuery Error", ex)
        Finally
        End Try
        Return dt
    End Function

    Public Function _ExecuteQuery(sql As String, Optional parameters As OracleParameter() = Nothing) As DataTable
        Dim dt As New DataTable()
        Try
            Using cmd As New OracleCommand(sql, _connection)
                If _transaction IsNot Nothing Then cmd.Transaction = _transaction
                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters)
                End If
                Using adapter As New OracleDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
                LogInfo("QUERY=")
                LogInfo(sql)
            End Using
        Catch ex As Exception
            LogError("クエリ実行中にエラーが発生しました: " & sql, ex)
            Throw
        End Try
        Return dt
    End Function

    Public Function ExecuteNonQuery(sql As String, Optional parameters As OracleParameter() = Nothing) As Integer
        Try
            Using cmd As New OracleCommand(sql, _connection)
                If _transaction IsNot Nothing Then cmd.Transaction = _transaction
                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters)
                End If
                Dim affected = cmd.ExecuteNonQuery()
                LogInfo("非クエリ実行: " & sql & " (影響行数: " & affected & ")")
                Return affected
            End Using
        Catch ex As Exception
            LogError("非クエリ実行中にエラーが発生しました: " & sql, ex)
            Throw
        End Try
    End Function

    Public Function ExecuteScalar(sql As String, Optional parameters As OracleClient.OracleParameter() = Nothing) As Object
        Try
            Using cmd As New OracleCommand(sql, _connection)
                If _transaction IsNot Nothing Then cmd.Transaction = _transaction
                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters)
                End If
                Dim result = cmd.ExecuteScalar()
                LogInfo("スカラー実行: " & sql)
                Return result
            End Using
        Catch ex As Exception
            LogError("スカラー実行中にエラーが発生しました: " & sql, ex)
            Throw
        End Try
    End Function

    Public Sub BeginTransaction()
        Try
            If _connection.State <> ConnectionState.Open Then
                _connection.Open()
            End If
            _transaction = _connection.BeginTransaction()
            LogInfo("トランザクション開始")
        Catch ex As Exception
            LogError("トランザクション開始エラー", ex)
            Throw
        End Try
    End Sub

    Public Sub Commit()
        Try
            _transaction?.Commit()
            LogInfo("トランザクションをコミットしました")
        Catch ex As Exception
            LogError("トランザクションコミット失敗", ex)
            Throw
        Finally
            _transaction = Nothing
        End Try
    End Sub

    Public Sub Rollback()
        Try
            _transaction?.Rollback()
            LogInfo("トランザクションをロールバックしました")
        Catch ex As Exception
            LogError("トランザクションロールバック失敗", ex)
            Throw
        Finally
            _transaction = Nothing
        End Try
    End Sub

    Public Sub Dispose()
        Close()
        If _connection IsNot Nothing Then
            _connection.Dispose()
            _connection = Nothing
            LogInfo("リソースを解放しました")
        End If
    End Sub

    Private Sub LogInfo(message As String)
        Try
            If _logger IsNot Nothing Then
                '    Dim method = _logger.GetType().GetMethod("Info")
                '    'If method IsNot Nothing Then method.Invoke(_logger, {message})
                '    If method IsNot Nothing Then method.Invoke(_logger, {message})
                _logger.info(message)
            End If
        Catch ex As Exception
            Debug.WriteLine(vbCrLf + "LogInfo Error")
            Debug.WriteLine(ex.GetType.ToString + ":" + ex.Message)
            Debug.WriteLine(ex.StackTrace)
        End Try
    End Sub

    Private Sub LogError(message As String, ex As Exception)
        Try
            If _logger IsNot Nothing Then
                Dim method = _logger.GetType().GetMethod("Err")
                If method IsNot Nothing Then method.Invoke(_logger, {message, ex})
            End If
        Catch
            ' ログ失敗時は何もしない
        End Try
    End Sub
End Class
