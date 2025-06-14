Imports System.Diagnostics.Eventing.Reader
Imports System.IO

Public Class WindowsEventLogSaver
    Public _logger As AppLogger
    Public Class LogEntry
        Public Property TimeCreated As DateTime?
        Public Property LogName As String
        Public Property EventId As Integer
        Public Property Level As String
        Public Property Source As String
        Public Property User As String
        Public Property MachineName As String
        Public Property Message As String
    End Class

    ' ログ収集処理
    Public Function GetFilteredLogs(logName As String, condition As EventLogFilterCondition, maxCount As Integer) As List(Of LogEntry)

        Dim result As New List(Of LogEntry)()
        _logger.Info($"logName = '{logName}'")
        If logName = "" Then
            _logger.Err("logName is Nothing > Exit")
            Return result
        End If

        Dim query As New EventLogQuery(logName, PathType.LogName)
        Dim reader As New EventLogReader(query)

        Dim count As Integer = 0

        While True
            Dim record As EventRecord = reader.ReadEvent()
            If record Is Nothing Then Exit While

            ' 条件で判定
            If Not condition.IsMatch(record) Then Continue While

            ' 抽出
            Dim entry As New LogEntry With {
                .TimeCreated = record.TimeCreated,
                .LogName = record.LogName,
                .EventId = record.Id,
                .Level = record.LevelDisplayName,
                .Source = record.ProviderName,
                .User = record.UserId?.ToString(),
                .MachineName = record.MachineName,
                .Message = record.FormatDescription()
            }

            result.Add(entry)
            count += 1
            If count >= maxCount Then Exit While
        End While

        Return result
    End Function

    Public Sub ExportLogEntriesToTextFile(entries As List(Of WindowsEventLogSaver.LogEntry), filePath As String)
        Using writer As New StreamWriter(filePath, False, System.Text.Encoding.UTF8)
            Dim count As Integer = 1

            For Each entry In entries
                writer.WriteLine($"[{count}]")
                writer.WriteLine($"日時: {entry.TimeCreated?.ToString("yyyy-MM-dd HH:mm:ss")}")
                writer.WriteLine($"イベントID: {entry.EventId}")
                writer.WriteLine($"ソース: {entry.Source}")
                writer.WriteLine($"レベル: {entry.Level}")
                writer.WriteLine($"ユーザー: {If(String.IsNullOrWhiteSpace(entry.User), "N/A", entry.User)}")
                writer.WriteLine($"マシン名: {If(String.IsNullOrWhiteSpace(entry.MachineName), "N/A", entry.MachineName)}")
                writer.WriteLine("メッセージ:")
                writer.WriteLine(entry.Message)
                writer.WriteLine(New String("-"c, 50)) ' 区切り線
                writer.WriteLine()
                count += 1
            Next
        End Using
    End Sub


    ''' <summary>
    '''  実行PCのすべてのイベントログ名（種類）を列挙する
    ''' </summary>
    ''' <returns></returns>
    Public Function GetListAllLogNames() As String()
        Dim _list = New List(Of String)
        For Each log As EventLog In EventLog.GetEventLogs()
            _list.Add(log.Log) ' 内部ログ名（logName に指定する値）
        Next
        Return _list.ToArray()
    End Function

    Public Function GetAllFilteredLogs(condition As EventLogFilterCondition, maxPerLogCount As Integer) As List(Of LogEntry)
        Dim allResults As New List(Of LogEntry)

        For Each log As EventLog In EventLog.GetEventLogs()
            Try
                Dim logName As String = log.Log
                Dim partialResults = GetFilteredLogs(logName, condition, maxPerLogCount)
                allResults.AddRange(partialResults)
            Catch ex As Exception
                ' アクセス権エラーなどを無視する（必要ならログ出力）
                ' _logger.Info($"ログ {log.Log} にアクセスできませんでした: {ex.Message}")
            End Try
        Next

        Return allResults
    End Function


    ' ##########
    Sub TestMain()
        ' ログ名（例: Application, System, Security など）
        Dim logName As String = "Application"

        ' クエリを作成（XPath式でもOK）
        Dim query As New EventLogQuery(logName, PathType.LogName)

        ' リーダーを作成
        Dim reader As New EventLogReader(query)

        Console.WriteLine("=== 詳細イベントログ一覧（最大10件） ===")

        Dim count As Integer = 0
        Dim maxCount As Integer = 10

        While True
            Dim record As EventRecord = reader.ReadEvent()
            If record Is Nothing OrElse count >= maxCount Then Exit While

            Console.WriteLine($"【ログ名】{record.LogName}")
            Console.WriteLine($"【イベントID】{record.Id}")
            Console.WriteLine($"【日時】{record.TimeCreated}")
            Console.WriteLine($"【レベル】{record.LevelDisplayName}")
            Console.WriteLine($"【ソース】{record.ProviderName}")
            Console.WriteLine($"【ユーザー】{record.UserId}")
            Console.WriteLine($"【マシン名】{record.MachineName}")
            Console.WriteLine("【メッセージ】")
            Console.WriteLine(record.FormatDescription())
            Console.WriteLine("【XML】")
            Console.WriteLine(record.ToXml())
            Console.WriteLine("======================================")

            count += 1
        End While

        Console.WriteLine("完了しました。Enterキーで終了")
        Console.ReadLine()
    End Sub
End Class
