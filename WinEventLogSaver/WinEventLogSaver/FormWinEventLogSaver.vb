Imports System.IO

Public Class FormWinEventLogSaver

    Dim EVENT_LOG_MAX_COUNT_DEFAULT As Integer = 10000

    Dim _logger As AppLogger
    Dim _iniFilePath As String
    Dim _resultReportFilePath As String
    Dim _settingKeys = {
        "StartDatetime",
        "EndDatetime",
        "AppName",
        "LogName",
        "EventKind",
        "EventId",
        "EventMachineName",
        "EventMessage"}

    Dim _textBoxArray = {
        TextBox_StartDateTime,
        TextBox_EndDateTime,
        TextBox_AppName,
        TextBox_LogName,
        TextBox_EventKind,
        TextBox_EventId,
        TextBox_EventMachineName,
        TextBox_EventMessage}

    Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        '// log
        _logger = New AppLogger()
        Dim logfilePath = _logger.GetDefaultLogFilePath()
        _logger.SetFilePath(logfilePath)
        _logger.LogOutPutMode += AppLogger.OutputMode.FILE
        _logger.MakeLogDir()
        _logger.Info($"LogFilePath=")
        _logger.Info(logfilePath)
        AddHandler _logger.AddLogEvent, AddressOf UpdateLogControlText

        '// ini
        Dim exePath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Dim iniFilePath = Path.Combine(exePath, "setting.ini")
        _iniFilePath = iniFilePath

        '// event find result report write path
        Dim reportFileName = "report" + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + ".txt"
        _resultReportFilePath = Path.Combine(exePath, reportFileName)
    End Sub


    Private Sub FormWinEventLogSaver_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' set form
        _textBoxArray = {
        TextBox_StartDateTime,
        TextBox_EndDateTime,
        TextBox_AppName,
        TextBox_LogName,
        TextBox_EventKind,
        TextBox_EventId,
        TextBox_EventMachineName,
        TextBox_EventMessage}

        ' ini
        ReadIniSettings(_iniFilePath)

        '///
        'Debug
        Dim logNames = New WindowsEventLogSaver().GetListAllLogNames()
        Dim logNamesStr = String.Join(", ", logNames)
        _logger.Info("All Log Names = " + logNamesStr)
        ' All Log Names = Application, HardwareEvents, Internet Explorer, Key Management Service, ODiag, OSession, Security, System, Visual Studio, Windows Azure, Windows PowerShell
        ' Application, Security, System
    End Sub

    Private Sub Button_Save_Click(sender As Object, e As EventArgs) Handles Button_Save.Click
        _logger.Info("# Execute Save")

        TextBox_OutputPath.Text = ""

        'set max count
        Dim maxCount As Long
        If Not Integer.TryParse(TextBox_MacCount.Text, maxCount) Then
            maxCount = EVENT_LOG_MAX_COUNT_DEFAULT
            TextBox_MacCount.Text = maxCount.ToString
        End If

        Dim con = CreateFilterConditionFromUI(_logger)
        Dim logNames = Me.GetLogNames()
        Dim allEventList = New List(Of WindowsEventLogSaver.LogEntry)
        Dim saver = New WindowsEventLogSaver()
        saver._logger = Me._logger
        Try
            ' execute
            For Each logName In logNames
                Dim eventList = saver.GetFilteredLogs(logName, con, maxCount)
                _logger.Info("GetLogList.Count = " + eventList.Count.ToString)
                allEventList.AddRange(eventList)
            Next

            'log
            LogEntriesToSummaryLog(allEventList, _logger)

            'write file
            saver.ExportLogEntriesToTextFile(allEventList, _resultReportFilePath)
            _logger.Info("Write " + Path.GetFileName(_resultReportFilePath))
            _logger.Info(_resultReportFilePath)

            TextBox_OutputPath.Text = _resultReportFilePath

        Catch ex As Exception
            _logger.Err(ex, "EventSaveError")
        End Try
        _logger.Info("Save Done.")
    End Sub

    Private ExecuteGetEvent()

    Private Function GetLogNames() As String()
        Dim logNames = TextBox_LogName.Text.Split(",")
        For i = 0 To logNames.Length - 1
            logNames(i) = logNames(i).Trim
        Next

        '管理者でしか取得できないログがあるので、
        'アプリが管理者で実行されていない場合は、このイベントログは除外する
        '取得しようとすると（System.UnauthorizedAccessException:許可されていない操作を実行しようとしました。）エラーとなる
        Dim newLogNames = New List(Of String)
        For Each logName In logNames
            If logName = "Security" Then
                If ModuleCheckAdmin.IsAdministrator() Then
                    newLogNames.Add(logName)
                Else
                    _logger.Warn($"[WARNING] IsAdministrator = False , Remove Security EventLog")
                    Continue For
                End If
            End If
            newLogNames.Add(logName)
        Next

        _logger.Info("logNames = " + String.Join(", ", newLogNames))
        Return newLogNames.ToArray()
    End Function


    Public Sub LogEntriesToSummaryLog(entries As List(Of WindowsEventLogSaver.LogEntry), _logger As AppLogger)
        Dim count As Integer = 1
        For Each entry In entries
            Dim summary As String = String.Format("[{0}] ID={1} Source={2} {3} ユーザー={4} PC={5}",
            count,
            entry.EventId,
            entry.Source,
            entry.Level,
            If(String.IsNullOrWhiteSpace(entry.User), "N/A", entry.User),
            If(String.IsNullOrWhiteSpace(entry.MachineName), "N/A", entry.MachineName)
        )

            _logger.Info(summary)
            count += 1
        Next
    End Sub


    ''' <summary>
    ''' イベントログを取得するための条件をセット（コントロールのTextBoxから）
    ''' </summary>
    ''' <param name="_logger"></param>
    ''' <returns></returns>
    Private Function CreateFilterConditionFromUI(_logger As AppLogger) As EventLogFilterCondition
        Dim condition As New EventLogFilterCondition()

        ' 日付解析関数（複数書式 + ログ出力付き）
        Dim functionParseDateTime As Func(Of String, String, DateTime?) =
        Function(text As String, label As String) As DateTime?
            If String.IsNullOrWhiteSpace(text) Then Return Nothing
            Dim formats() As String = {"yyyy/M/d H:m:s", "yyyyMMdd HHmmss"}
            Dim dt As DateTime
            If DateTime.TryParseExact(text, formats, Nothing, Globalization.DateTimeStyles.None, dt) Then
                Return dt
            Else
                _logger.Info($"{label}が無効な形式です: {text}")
                Return Nothing
            End If
        End Function

        ' 開始・終了日時の取得とログ出力
        Dim startTime = functionParseDateTime(TextBox_StartDateTime.Text, "開始日時")
        If startTime.HasValue Then condition.StartTime = startTime.Value

        Dim endTime = functionParseDateTime(TextBox_EndDateTime.Text, "終了日時")
        If endTime.HasValue Then condition.EndTime = endTime.Value

        ' 条件設定
        condition.MachineContains = TextBox_EventMachineName.Text
        condition.UserContains = TextBox_EventUserName.Text
        condition.SourceContains = TextBox_AppName.Text
        condition.EventIdContains = TextBox_EventId.Text
        condition.LevelContains = TextBox_EventKind.Text
        condition.MessageContains = TextBox_EventMessage.Text

        _logger.Info($"StartTime = {condition.StartTime}")
        _logger.Info($"EndTime = {condition.EndTime}")
        _logger.Info($"MachineContains = {condition.MachineContains}")
        _logger.Info($"UserContains = {condition.UserContains}")
        _logger.Info($"SourceContains = {condition.SourceContains}")
        _logger.Info($"EventIdContains = {condition.EventIdContains}")
        _logger.Info($"LevelContains = {condition.LevelContains}")
        _logger.Info($"MessageContains = {condition.MessageContains}")

        Return condition
    End Function

    Public Sub ReadIniSettings(filePath As String)
        Dim ini As New IniReaderWriter()
        ini.Init(filePath)
        ini.Load()

        Dim readBuf = ""
        Dim bufDict = New Dictionary(Of String, String)
        ' セクションとキーの取得
        For Each key In _settingKeys
            readBuf = ini.GetValue("Condition1", key)
            bufDict(key) = If(readBuf Is Nothing, "", readBuf.ToString())
        Next

        ' 存在しないキーの場合 Nothing
        'Dim email As String = ini.GetValue("User", "Email") ' → Nothing

        ' 出力
        _logger.Info("readIni")
        For Each key In _settingKeys
            _logger.Info($"{key} = " + bufDict(key))
        Next

        'コントロールにセット
        _logger.Info("SetToControlText")
        For i = 0 To _textBoxArray.Length - 1
            Dim ui As TextBox = _textBoxArray(i)
            If _textBoxArray(i) Is Nothing Then
                _logger.Info($"_textBoxArray({i}) Is Nothing > continue")
                Continue For
            End If
            Dim value = bufDict(_settingKeys(i))
            ui.Text = If(value Is Nothing, "", value.ToString())
            _logger.Info($"{ui.Name} = " + ui.Text)
        Next
    End Sub

    Public Sub WriteIniSettings(filePath As String)
        Dim ini As New IniReaderWriter()
        ini.Init(filePath)
        ini.Load() ' ← 既存のINIを読み込む（コメント保持のため）

        Dim SaveIniDict = New Dictionary(Of String, String)

        'コントロールから値を取得して、セット
        _logger.Info("SaveControlText")
        For i = 0 To _textBoxArray.Length - 1
            Dim ui As TextBox = _textBoxArray(i)
            SaveIniDict(_settingKeys(i)) = ui.Text
            _logger.Info($"{ui.Name} = " + ui.Text)
        Next

        ' 値の追加または更新
        For Each key In SaveIniDict.Keys
            ini.SetValue("Condition1", key, SaveIniDict(key))
        Next

        '' セクション追加（存在しなければ）
        'ini.AddSection("NewSection")
        'ini.SetValue("NewSection", "Flag", "True")

        ' 保存
        ini.Save()
        _logger.Info("Write Ini Completed")
    End Sub


    Private Sub FormWinEventLogSaver_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        WriteIniSettings(_iniFilePath)
    End Sub

    Private Sub Button_Copy_Click(sender As Object, e As EventArgs) Handles Button_Copy.Click
        _logger.Info($" Clipboard.SetTex = {TextBox_OutputPath.Text}")
        Clipboard.SetText(TextBox_OutputPath.Text)
    End Sub

    Private Sub Button_Explorer_Click(sender As Object, e As EventArgs) Handles Button_Explorer.Click
        _logger.Info($" OpenInExplorer = {TextBox_OutputPath.Text}")
        FileExplorerHelper.OpenInExplorer(TextBox_OutputPath.Text)
    End Sub

    Private Sub UpdateLogControlText(sender As Object, e As EventArgs)
        Dim lastLog = _logger.LastLog + vbNewLine
        Me.RichTextBox_AppLog.AppendText(lastLog)
        RichTextBox_AppLog.ScrollToCaret()
    End Sub
End Class
