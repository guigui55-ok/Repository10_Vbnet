Imports System.IO
Imports System.Reflection
Public Class FormMain
    Dim _itemList As List(Of MainItemFrame) = New List(Of MainItemFrame)({})
    'Dim _logger As MainLogger 'MainItemFrame.Load時にも使用したいため、Form1のデザイナ側で定義、生成する
    'Public _langageFlag As Integer = LangageFlags.JA
    Public _langageFlag As Integer = LangageFlags.EN

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Initialize_Logger()
        Me.Initialize_ItemControls()
        Me.Initialize_Notification()
        Me.ExecuteAutoRun()
    End Sub

    '初期設定値
    Public _settingFileName As String = "setting.xml"
    Public _settingFilePath As String = Directory.GetCurrentDirectory() & "\" & Me._settingFileName
    Public _itemAmount As Integer = 2
    Public _settingValueDict As Dictionary(Of String, Object)
    Public DEFAULT_DATETIME_FORMAT_LOG_FILE_NAME As String = "_yyyyMMdd_HHmmss"
    'Private Sub Initialize_memo()
    '    'Me._settingValueDict = GetDictionaryByReadXml(Me._logger, Me._settingFilePath)
    '    'Me._logger = New MainLogger()
    '    'Me.MainItemFrame1 = New TimerProcessCloser.MainItemFrame()
    '    'Me.MainItemFrame1.SetLogger(Me._logger)
    '    'Me.MainItemFrame1.Parent = Me
    '    'Me.MainItemFrame2 = New TimerProcessCloser.MainItemFrame()
    '    'Me.MainItemFrame2.SetLogger(Me._logger)
    '    'Me.MainItemFrame2.Parent = Me
    'End Sub

    Public Function GetDefaultLogPathFromDict()
        Dim logFilePath As String = Me._settingValueDict(ConstInfoFile.KEY_LOG_PATH)
        If logFilePath = "" Then
            logFilePath = Me.GetDefaultLogFilePath()
        End If
        Return logFilePath
    End Function

    'ログファイルパスのデフォルト値
    Public Function GetDefaultLogFilePath()
        Dim dateStr As String = Now.ToString(Me.DEFAULT_DATETIME_FORMAT_LOG_FILE_NAME)
        Dim currentDir As String = Directory.GetCurrentDirectory()
        'Dim loggerPath As String = String.Format("log\log{0}.log", dateStr)
        'Dim loggerPath As String = String.Format("log\log.log") '日付はLoggerクラスで勝手に付与される
        '240812
        'Release版初回起動時、ログディレクトリが\netcoreapp3.1log\log_20240812_084631.logとなる
        Dim loggerPath As String = String.Format("\log\log.log") '日付はLoggerクラスで勝手に付与される
        loggerPath = loggerPath.Replace(currentDir, "")
        Return loggerPath
    End Function

    '設定値のデフォルト値をDictionaryで取得する
    '読み込みに失敗したときなどに使用する
    Public Function GetDefaultSettingInfoDictionary() As Dictionary(Of String, Object)
        'logData.Add(ConstInfoFile.KEY_LOG_PATH, "Log\__log_{TimeFormat}.log")
        'logData.Add(ConstInfoFile.KEY_LOG_FILE_TIME_FORMAT, "yyyymmdd_hhmmss")
        'Dictionary型を1行ずつAddする
        Dim valueDict As New Dictionary(Of String, Object)
        ' データを1行ずつ追加
        'LoggerPathを取得する（CurrentPath\Log\LogFileName.Log となっているが CurrentPathはファイルには記載しないので除去する）
        'つまりこの場合書き込むのは「\Log\LogFileName.Log」となる
        'Dim LogFileTimeFormat As String = "_yyyyMMdd_HHmmss"
        '/
        'Dim dateStr As String = Now.ToString("_yyyyMMdd_HHmmss")
        'Dim currentDir As String = Directory.GetCurrentDirectory()
        'Dim loggerPath As String = String.Format("log{0}.log", dateStr)
        'loggerPath = loggerPath.Replace(currentDir, "")
        Dim loggerPath = Me.GetDefaultLogFilePath()
        valueDict.Add(ConstInfoFile.KEY_LOG_PATH, loggerPath)
        valueDict.Add(ConstInfoFile.KEY_LOG_FILE_TIME_FORMAT, Me._logger.LogFileTimeFormat)
        '/
        Dim key As String
        Dim value As Dictionary(Of String, Object)
        For num As Integer = 1 To Me._itemAmount
            key = ConstInfoFile.KEY_ITEM & num
            value = MainItemFrame.GetDictDefaultInThisItem()
            ' ItemをメインのDictionaryに追加
            valueDict.Add(key, value)
        Next
        Return valueDict
    End Function


    '初期化処理
    'Loggerの初期化
    '設定値をファイルから読み込み
    Private Sub Initialize_Logger()
        'Me._logger = New MainLogger()
        'デザイナ（Form1.Designer.vb）の中で行う（他のフォームの初期化中処理もログに取りたいため）

        Me._logger = New MainLogger()
        Me._settingValueDict = GetDictionaryByReadXml(Me._logger, Me._settingFilePath, Me.GetDefaultSettingInfoDictionary())
        Me._logger.LogFileTimeFormat = Me._settingValueDict(ConstInfoFile.KEY_LOG_FILE_TIME_FORMAT)
        Dim logFilePath As String = Me.GetDefaultLogPathFromDict()
        'settingから読み取ったパスには、DirectoryがないのでCurrentDirectoryを付与する
        Dim currentDir As String = Directory.GetCurrentDirectory()
        logFilePath = currentDir & logFilePath
        'Me._logger.SetFilePath(logFilePath, Me._logger.LogFileTimeFormat) '\log\20240811_233238_20240812_001130.log となるので直接格納
        Me._logger.FilePath = logFilePath
        Me._logger.LogOutPutMode += OutputMode.FILE
        'Me._logger.FilePath = Me._settingValueDict(ConstInfoFile.KEY_LOG_PATH)
        If Not File.Exists(Me._logger.FilePath) Then
            Debug.Print("LogFile Not Exists")
            Debug.Print(String.Format("Path = {0}", logFilePath))
            logFilePath = Me.GetDefaultLogFilePath()
            Me._logger.SetFilePath(currentDir & logFilePath)
        End If
        Me._logger.PrintInfo(String.Format("CurrentDir = {0}", currentDir))
        Me._logger.PrintInfo(String.Format("LogFilePath = {0}", Me._logger.FilePath))
        Me._logger.PrintInfo(String.Format("LogFileName = {0}", Path.GetFileName(Me._logger.FilePath)))
        Dim settingString As String = ConvertDictionaryToString(Me._settingValueDict)
        Me._logger.PrintInfo(String.Format("_settingValueDict = {0}", settingString))
    End Sub

    '初期化処理
    Private Sub Initialize_ItemControls()
        '処理の一部は、Form1のデザイナで行う > デザイナでエラーが出るのでこちらで実行
        Me.MainItemFrame1.SetLogger(Me._logger)
        Me.MainItemFrame1.Parent = Me
        _itemList.Add(Me.MainItemFrame1)
        Me.MainItemFrame2.SetLogger(Me._logger)
        Me.MainItemFrame2.Parent = Me
        _itemList.Add(Me.MainItemFrame2)
        Dim count As Integer = 1
        For Each itemControl As MainItemFrame In _itemList
            ''コントロール名「MainItemFrame1」のように最後に数値があるものとする
            ''この数字をItem番号とした使う
            'num = Integer.Parse(itemControl.Name(itemControl.Name.Length - 1))
            'itemControl.GroupBoxItemFrame.Text = "Item" & num
            itemControl.ChangeTextGroupBox()
            Dim key As String = ConstInfoFile.KEY_ITEM & count
            itemControl.SetValueFromDict(Me._settingValueDict(key))
            count += 1
        Next
    End Sub

    '初期化処理
    Private Sub Initialize_Notification()

        Dim asmprd As System.Reflection.AssemblyProductAttribute =
            CType(Attribute.GetCustomAttribute(
                System.Reflection.Assembly.GetExecutingAssembly(),
                GetType(System.Reflection.AssemblyProductAttribute)),
                    System.Reflection.AssemblyProductAttribute)
        ' アイコンにマウスオーバーした時に表示されるテキスト(アプリ名）
        Me.NotifyIcon1.Text = asmprd.Product

        Dim projectRoot As String = AppDomain.CurrentDomain.BaseDirectory
        projectRoot = System.IO.Path.GetFullPath(System.IO.Path.Combine(projectRoot, "..\..\.."))
        Me._logger.PrintInfo("Project Root Directory: " & projectRoot)

        ' アイコンを設定（カスタムアイコンも可能）
        Me.NotifyIcon1.Icon = SystemIcons.Application
        Dim iconPath As String = projectRoot & "\" & "Timer_Process_Closer.ico"
        Me.NotifyIcon1.Icon = New Icon(iconPath)
        Me.NotifyIcon1.Visible = True

        ' バルーンチップ（ポップアップ通知）の設定
        Me.NotifyIcon1.BalloonTipTitle = "通知"
        Me.NotifyIcon1.BalloonTipText = "ポップアップ通知メッセージ"
        'アイコンタイプを設定（Info, Warning, Errorなど）
        Me.NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info

        ' イベントハンドラの設定
        AddHandler Me.NotifyIcon1.BalloonTipClicked, AddressOf Me.OnBalloonTipClicked
    End Sub

    '自動実行フラグがONの時に、タイマーを実行する
    'タイマー実行側 MainFrameのメソッドを呼び出している
    Public Sub ExecuteAutoRun()
        Me._logger.PrintInfo("ExecuteAutoRun FormMain")
        Dim frameControlList As List(Of MainItemFrame) = Me.GetMainItemFrameControlList()
        For Each frameControl In frameControlList
            frameControl.ExecuteAutoRun()
        Next
    End Sub

    '通知を表示
    '現状ではタスクバーからのみ
    'タイマー実行側 MainFrameから呼び出される
    Public Sub ShowNotification(Optional ballonTitle As String = "Timer Process Closer", Optional ballonMsg As String = "Notification")
        Me._logger.PrintInfo("ShowNotification")
        Me._logger.PrintInfo(String.Format("ballonTitle = {0}", ballonTitle))
        Me._logger.PrintInfo(String.Format("ballonMsg = {0}", ballonMsg))
        Me.NotifyIcon1.BalloonTipTitle = ballonTitle
        Me.NotifyIcon1.BalloonTipText = ballonMsg
        ' バルーンチップを表示
        NotifyIcon1.ShowBalloonTip(3000) ' 3000ミリ秒（3秒間）表示
    End Sub

    ' バルーンチップがクリックされたときの処理
    Private Sub OnBalloonTipClicked(sender As Object, e As EventArgs)
        ' バルーンチップ（ポップアップ通知）の設定
        Me.NotifyIcon1.BalloonTipTitle = "通知"
        Me.NotifyIcon1.BalloonTipText = "ポップアップ通知メッセージ"
        Me._logger.PrintInfo("OnBalloonTipClicked")
    End Sub

    'ProcessingNumber の TimerStatus の番号をリストで取得する
    Private Function GetListProcessingNumberList() As List(Of Integer)
        Dim controls As List(Of MainItemFrame) = Me.GetMainItemFrameControlList()
        Dim isProcessingNow As Boolean = False
        Dim nowProcessingNumList As List(Of Integer) = New List(Of Integer)()
        For Each frame In controls
            If frame._timerStatus._NowStatus = TimerItemStatusFlags.PROCESSING_TIMER Then
                isProcessingNow = True
                nowProcessingNumList.Add(frame._timerStatus._timer_number)
            End If
        Next
        Return nowProcessingNumList
    End Function

    Private Sub Form1_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing
        Me._logger.PrintInfo("Form1_Closing")
        Dim nowProcessingNumList As List(Of Integer) = Me.GetListProcessingNumberList()
        Dim RetInt As DialogResult
        If 0 < nowProcessingNumList.Count Then
            Dim numberStr As String = String.Join(",", nowProcessingNumList)
            numberStr = "[" & numberStr & "]"
            RetInt = MessageBox.Show(
                    "実行中のタイマーがあります。終了しますか？" & numberStr,
                    "Info",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation)
        Else
            RetInt = DialogResult.OK
        End If
        If RetInt = MsgBoxResult.Cancel Then
            e.Cancel = True
        End If
        Me.SaveItemsInfo()
        ' 実行中のアプリケーションの実行パスを取得する
        Dim executionPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Me._logger.PrintInfo(String.Format("## executionPath"))
        Me._logger.PrintInfo(executionPath)
        Me._logger.PrintInfo(String.Format("##"))
    End Sub

    'Me（FormMain）に含まれるMainItemFrameのすべてを、リストで取得する
    Private Function GetMainItemFrameControlList() As List(Of MainItemFrame)
        Dim mainItemFrameControls As New List(Of MainItemFrame)()

        For Each ctrl As Control In Me.Controls
            ' TypeOfを使用してコントロールが「MainItemFrame」かどうかを確認
            If TypeOf ctrl Is MainItemFrame Then
                mainItemFrameControls.Add(DirectCast(ctrl, MainItemFrame))
            End If
        Next
        Return mainItemFrameControls
    End Function

    '設定情報を保存する
    Private Sub SaveItemsInfo()
        Me._logger.PrintInfo("SaveItemsInfo")
        Dim mainItemFrameControls = Me.GetMainItemFrameControlList()
        Dim valueDict = GetDictRoot()
        Dim key As String
        Dim value
        ' リストに追加された「MainItemFrame」のコントロールを使用する
        For Each itemFrame As MainItemFrame In mainItemFrameControls
            key = ConstInfoFile.KEY_ITEM & itemFrame._timerStatus._timer_number
            value = itemFrame.GetDictInThisItem()
            ' ItemをメインのDictionaryに追加
            valueDict.Add(key, value)
        Next
        Dim settingString As String = ConvertDictionaryToString(valueDict)
        Me._logger.PrintInfo(String.Format("_settingValueDict = {0}", settingString))

        ' XMLドキュメントの生成
        Dim xmlDocument As XDocument = ConvertXmlDocumentFromDictionary(valueDict)
        ' XMLファイルへの書き込み
        xmlDocument.Save(_settingFilePath)
        Me._logger.PrintInfo("XMLファイルにデータを書き込みました: " & _settingFilePath)

    End Sub

    '設定値DictionaryのRoot部（Item部以外）を取得する
    Public Function GetDictRoot() As Dictionary(Of String, Object)
        Me._logger.PrintInfo("GetDictRoot")
        'logData.Add(ConstInfoFile.KEY_LOG_PATH, "Log\__log_{TimeFormat}.log")
        'logData.Add(ConstInfoFile.KEY_LOG_FILE_TIME_FORMAT, "yyyymmdd_hhmmss")
        'Dictionary型を1行ずつAddする
        Dim logData As New Dictionary(Of String, Object)
        ' データを1行ずつ追加
        'LoggerPathを取得する（CurrentPath\Log\LogFileName.Log となっているが CurrentPathはファイルには記載しないので除去する）
        'つまりこの場合書き込むのは「\Log\LogFileName.Log」となる
        Dim loggerPath As String = Me._logger.FilePath
        Dim currentDir As String = Directory.GetCurrentDirectory()
        loggerPath = loggerPath.Replace(currentDir, "")
        logData.Add(ConstInfoFile.KEY_LOG_PATH, loggerPath)
        logData.Add(ConstInfoFile.KEY_LOG_FILE_TIME_FORMAT, Me._logger.LogFileTimeFormat)
        Return logData
    End Function


End Class

Public Module CommonModule
    Public Class MainLogger
        Inherits SimpleLogger
    End Class

    Function GetCallerInfo() As String
        Dim ret As String
        Dim frame As StackFrame
        Dim method As String
        Dim filePath As String
        Dim lineNumber As Integer

        Dim stackTrace As New StackTrace(True)
        ' インデックス1は呼び出し元のメソッドに対応します。
        '/
        frame = stackTrace.GetFrame(2)
        method = frame.GetMethod().Name
        filePath = frame.GetFileName()
        lineNumber = frame.GetFileLineNumber()
        ret = $"Method: {method}, File: {System.IO.Path.GetFileName(filePath)}, Line: {lineNumber}" + vbCrLf
        '/
        frame = stackTrace.GetFrame(3)
        method = frame.GetMethod().Name
        filePath = frame.GetFileName()
        lineNumber = frame.GetFileLineNumber()
        ret &= $"Method: {method}, File: {System.IO.Path.GetFileName(filePath)}, Line: {lineNumber}"
        '/
        Return ret
    End Function

    Public Sub ShowError(_logger As MainLogger, msg As String)
        Dim stackStr As String = GetCallerInfo()
        Dim errMsg As String = msg & vbCrLf & "処理を中断します。" & vbCrLf & "[ERROR詳細]" & vbCrLf & GetCallerInfo()
        _logger.PrintInfo("ERROR: " & errMsg)
        MessageBox.Show(errMsg, "ERROR")
    End Sub


End Module
