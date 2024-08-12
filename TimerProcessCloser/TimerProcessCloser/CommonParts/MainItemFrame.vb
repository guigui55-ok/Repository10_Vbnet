Public Class MainItemFrame
    Dim _mainTimer As Timer
    Dim _stopWatch As System.Diagnostics.Stopwatch
    Public _logger As MainLogger
    'Dim _timerStatus As TimerItemStatus
    'Public WithEvents _timerStatus As New TimerItemStatus
    'エラー	BC30909	'_timerStatus' は、型 'ConstantsModule.TimerItemStatus' を class 'MainItemFrame' 経由でプロジェクトの外側に公開できません。
    Public WithEvents _timerStatus As New TimerItemStatus
    'Dim _ConstItemFrameWording As ConstItemFrameWording


    '初期化時（MainFormデザイナ内のNew MainItemFrame ）のすぐ後で実行すること
    ' New TimerItemStatusとLoggerをセットして、Load関数内でloggerを使用したいため
    Public Sub SetLogger(argLogger As MainLogger)
        _logger = argLogger
        _timerStatus = New TimerItemStatus()
        _timerStatus._logger = _logger
        '_ConstItemFrameWording = WordingFactory.GetWording(Me.GetParentControlAsFormMain()._langageFlag)
    End Sub

    'Control_LOAD
    Private Sub MainItemFrame_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me._logger.PrintInfo(String.Format("MainItemFrame_Load [{0}]", Me.Name))
        'LoggerをForm1.Designer.vb内での値渡しはできなく、ここではLoggerがないため一旦Debug.Writelineで対応
        Debug.WriteLine(String.Format("MainItemFrame_Load [{0}]", Me.Name))
        Me.ChangeTextInFrameControls()
        Me._timerStatus.ChangeStatus(TimerItemStatusFlags.LOAD_CONTROL)
        Me._mainTimer = New Timer()
        Me._mainTimer.Interval = TIMER_INTERVAL
        Me._stopWatch = System.Diagnostics.Stopwatch.StartNew()
        AddHandler _mainTimer.Tick, AddressOf _mainTimer_Tick
        Me._timerStatus.ChangeStatus(TimerItemStatusFlags.INIT_TIMER)
        Me.Initialize_NotificationItems()
    End Sub

    'ComBoboxを初期化
    Private Sub Initialize_NotificationItems()
        Dim settingList As List(Of String) = ConstNotificationValueInItem.GetMembarSettingVariantList()
        'For Each settingVal In settingList
        '    Me.ComboBoxNotification.Items.Add(settingList)
        'Next
        Me.ComboBoxNotification.Items.AddRange(settingList.ToArray())
    End Sub

    'テキストから読み込んだ値を、コンボボックスに設定する
    '値が選択肢になければNoneにする
    Private Sub Select_NotificationItem(readValue As String)
        Dim Index As Integer = ConstNotificationValueInItem.GetIndexInMatchSettingList(readValue)
        If Index < 0 Then
            Me.LoggerPrintInfo(String.Format("Set NotificationItem readValue Is Nothing [value={0}]", readValue))
            'Dim index
            'Me.ComboBoxNotification.SelectedIndex = ConstNotificationValueInItem.SETTING_NONE
            ' アイテム名がコンボボックスに存在する場合、そのアイテムを選択
            Me.ComboBoxNotification.SelectedItem = ConstNotificationValueInItem.SETTING_NONE
            Exit Sub
        End If
        Me.ComboBoxNotification.SelectedIndex = Index
    End Sub

    'ステータスを変更する
    Public Sub MainItemFrame_ChangeStatus() Handles _timerStatus.ChangeStatusEvent
        Me.ChangeStatusLabel()
        LoggerPrintInfo(Me._timerStatus.GetPrintStatusValue())
    End Sub

    Public Sub ChangeStatusLabel()
        Dim buf As String
        buf = TimerItemStatusDescDictionary(Me._timerStatus._NowStatus)
        Me.LabelStatus.Text = ConstItemFrameWording.LABEL_STATUS & " : " & buf
    End Sub

    '親コントロール（FormMain）を取得する
    'キャスト処理をしている
    Public Function GetParentControlAsFormMain() As FormMain
        If TypeOf Me.Parent Is FormMain Then
            Return DirectCast(Me.Parent, FormMain)
        Else
            Return Nothing
        End If
    End Function

    'GroupBoxのTextを変更する
    'Load時に実行される
    Public Sub ChangeTextGroupBox()
        Dim bufText As String

        If Me.GetParentControlAsFormMain()._langageFlag = LangageFlags.JA Then
            bufText = ConstItemFrameWordingJA.GROUP_BOX_TITLE
        Else
            bufText = ConstItemFrameWording.GROUP_BOX_TITLE
        End If
        'コントロール名「MainItemFrame1」のように最後に数値があるものとする
        'この数字をItem番号とした使う
        Dim num As Integer
        num = Integer.Parse(Me.Name(Me.Name.Length - 1))
        Me.GroupBoxItemFrame.Text = bufText & num
        Me._timerStatus._timer_number = num
    End Sub


    '"[Item1]"を取得する、ログ出力用
    Private Function GetItemStr(Optional beforeSpacing As String = "", Optional afterSpacing As String = " ")
        Dim buf As String = ConstItemFrameWording.GROUP_BOX_TITLE & Me._timerStatus._timer_number
        buf = beforeSpacing & "[" & buf & "]" & afterSpacing
        Return buf
    End Function

    'ファイルから読み込んで、Dictionaryに変換した変数から、値をセットする
    Public Sub SetValueFromDict(itemValueDict As Dictionary(Of String, Object))
        Me.LoggerPrintInfo(String.Format("SetValueFromDict[{0}]", Me._timerStatus._timer_number))
        'ChangeTextGroupBox の後に実行する
        Me.TextBoxProcessName.Text = itemValueDict(ConstInfoFile.KEY_PROCESS_NAME)
        Me.TextBoxTimerTime.Text = itemValueDict(ConstInfoFile.KEY_TIMER_TIME)
        Me.LabelRemainingTime.Text = itemValueDict(ConstInfoFile.KEY_REMAINING_TIME)
        Me.ComboBoxNotification.Text = itemValueDict(ConstInfoFile.KEY_NOTIFICATION)
        Me.Select_NotificationItem(itemValueDict(ConstInfoFile.KEY_NOTIFICATION))
        Me.CheckBoxAutoRun.Checked = ConverToBool(itemValueDict(ConstInfoFile.KEY_AUTO_RUN))
    End Sub

    Private Sub ChangeTextInFrameControls()
        'コントロールのテキストを変更する
        Dim Colon As String = " :"
        If Me.GetParentControlAsFormMain()._langageFlag = LangageFlags.JA Then
            Me.LabelNotification.Text = ConstItemFrameWordingJA.NOTIFICATION & Colon
            Me.LabelProcessName.Text = ConstItemFrameWordingJA.LABEL_PROCESS_NAME & Colon
            Me.LabelRemainingTime.Text = ConstItemFrameWordingJA.REMAINING_TIME & Colon
            Me.LabelRemainingTimeDisp.Text = ConstItemFrameWordingJA.REMAINING_TIME_DISP_DEFAULT & Colon
            Me.LabelTimerTIme.Text = ConstItemFrameWordingJA.TIMER_TIME & ConstItemFrameWordingJA.TIMER_TIME_RIGHT & Colon
            Me.ButtonStartOrPause.Text = ConstItemFrameWordingJA.START_BUTTON
            Me.ButtonStop.Text = ConstItemFrameWordingJA.STOP_BUTTON
            Me.GroupBoxItemFrame.Text = ConstItemFrameWordingJA.GROUP_BOX_TITLE & Me._timerStatus._timer_number
            Me.LabelStatus.Text = ConstItemFrameWordingJA.LABEL_STATUS & " : "
            Me.CheckBoxAutoRun.Text = ConstItemFrameWordingJA.CHECK_BOX_AUTO_RUN
        Else
            'Default
            Me.LabelNotification.Text = ConstItemFrameWording.NOTIFICATION & Colon
            Me.LabelProcessName.Text = ConstItemFrameWording.LABEL_PROCESS_NAME & Colon
            Me.LabelRemainingTime.Text = ConstItemFrameWording.REMAINING_TIME & Colon
            Me.LabelRemainingTimeDisp.Text = ConstItemFrameWording.REMAINING_TIME_DISP_DEFAULT & Colon
            Me.LabelTimerTIme.Text = ConstItemFrameWording.TIMER_TIME & ConstItemFrameWording.TIMER_TIME_RIGHT & Colon
            Me.ButtonStartOrPause.Text = ConstItemFrameWording.START_BUTTON
            Me.ButtonStop.Text = ConstItemFrameWording.STOP_BUTTON
            Me.GroupBoxItemFrame.Text = ConstItemFrameWording.GROUP_BOX_TITLE & Me._timerStatus._timer_number
            Me.LabelStatus.Text = ConstItemFrameWording.LABEL_STATUS & " : "
            Me.CheckBoxAutoRun.Text = ConstItemFrameWording.CHECK_BOX_AUTO_RUN
        End If
    End Sub


    'Startボタンクリック
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonStartOrPause.Click
        Me._timerStatus.PrintStatus()
        If Me._timerStatus._NowStatus = TimerItemStatusFlags.PROCESSING_TIMER Then
            Me.LoggerPrintInfo("Button1_Click Processing > Pause")
            '実行中なら一時停止をする
            Me._mainTimer.Stop()
            Me._stopWatch.Stop()
            ButtonStartOrPause.Text = ConstItemFrameWording.RESTART_BUTTON
            Me._timerStatus.ChangeStatus(TimerItemStatusFlags.PAUSE_TIMER)

        ElseIf Me._timerStatus._NowStatus = TimerItemStatusFlags.PAUSE_TIMER Then
            Me.LoggerPrintInfo("Button1_Click Pause > Processing  Restart")
            '一時停止中なら再開する
            Me._mainTimer.Start()
            Me._stopWatch.Start()
            ButtonStartOrPause.Text = ConstItemFrameWording.PAUSE_BUTTON
            Me._timerStatus.ChangeStatus(TimerItemStatusFlags.PROCESSING_TIMER)

        ElseIf Me._timerStatus.NowStatusIsStartAble() Then
            Me.LoggerPrintInfo("Button1_Click Stop > Start")
            Dim timerTimeSec As Integer = Me.ConvertInputTimerTime(Me.TextBoxTimerTime.Text)
            If timerTimeSec < 0 Then
                ShowError(Me._logger, ErrorDescDictionary(ErrorNumber.INVALID_INPUT_TIMER_TIME))
            End If
            Me._timerStatus._endTimeSpan = Me.ConvertSecondsToTime(timerTimeSec)
            Me._mainTimer.Enabled = True
            Me._timerStatus.ChangeStatus(TimerItemStatusFlags.START_TIMER)
            Me.ResetStopWatch()
            Me._stopWatch.Restart()
            'Me.ChangeButtonStartToPause()
            ButtonStartOrPause.Text = ConstItemFrameWording.PAUSE_BUTTON
            Me._timerStatus.ChangeStatus(TimerItemStatusFlags.PROCESSING_TIMER)
        Else
            ShowError(Me._logger, ErrorDescDictionary(ErrorNumber.UNEXPECTED_STATUS))
        End If
    End Sub

    'StopWatchをリセットする（カウントリセット）
    Private Sub ResetStopWatch()
        Me._stopWatch.Reset()
    End Sub

    'Start/Pauseを切り替え
    '今は使用していない
    Private Sub ChangeButtonStartToPause()
        If ButtonStartOrPause.Text = ConstItemFrameWording.PAUSE_BUTTON Then
            ButtonStartOrPause.Text = ConstItemFrameWording.START_BUTTON
        Else
            ButtonStartOrPause.Text = ConstItemFrameWording.PAUSE_BUTTON
        End If
    End Sub

    '停止ボタンをクリック
    Private Sub ButtonStop_Click(sender As Object, e As EventArgs) Handles ButtonStop.Click
        Me.LoggerPrintInfo("ButtonStop_Click")
        Me._mainTimer.Enabled = False
        Me._stopWatch.Stop()
        Me.ResetStopWatch()
        ButtonStartOrPause.Text = ConstItemFrameWording.START_BUTTON
        Me.UpdateRemainingTimeDisp()
        Me._timerStatus.ChangeStatus(TimerItemStatusFlags.STOP_TIMER)
    End Sub
    Private Sub _mainTimer_Tick(sender As Object, e As EventArgs)
        Me.UpdateRemainingTimeDisp()
        '時間が過ぎたら、タイマー、ストップウォッチ、を止めて、状態を完了にする
        Me.OnConditionTimeOver()
    End Sub

    'タイマーが完走したとき（中止されたときは実行されない）
    Private Sub OnConditionTimeOver()
        If Me._stopWatch.Elapsed < Me._timerStatus._endTimeSpan Then
            Exit Sub
        End If
        Me.LoggerPrintInfo("OnConditionTimeOver  TimeOver")
        Me._mainTimer.Enabled = False
        Me._stopWatch.Stop()
        Me.ResetStopWatch()
        ButtonStartOrPause.Text = ConstItemFrameWording.START_BUTTON
        'Me.UpdateRemainingTimeDisp()
        Me._timerStatus.ChangeStatus(TimerItemStatusFlags.FINISHED_TIMER)
        Me.KillProcessForThisItem()
        '親フォームに通知する
        Dim msg As String = ConstInfoFile.KEY_ITEM & Me._timerStatus._timer_number
        msg += String.Format(" - Finished Timer [{0}]", Me.TextBoxTimerTime)
        msg += " - Close " & Me.TextBoxProcessName.Text
        Me.SendToNotification(msg:=msg)
    End Sub


    'Notificationの設定値を文字列で取得する
    Private Function GetNotificationValue()
        Return Me.ComboBoxNotification.SelectedItem.ToString()
    End Function

    '通知を送信する（メイン）
    Private Sub SendToNotification(Optional msgTitle As String = "", Optional msg As String = "", Optional flag As Integer = -1)
        Dim notifiSettingStr As String = GetNotificationValue()
        If notifiSettingStr = ConstNotificationValueInItem.SETTING_TASK_BAR Then
            Dim ParentForm = Me.GetParentControlAsFormMain()
            ParentForm.ShowNotification(ballonMsg:=msg)
        ElseIf notifiSettingStr = ConstNotificationValueInItem.SETTING_NONE Then
            Me.LoggerPrintInfo("SendToNotification SettingNone")
        Else
            Me.LoggerPrintInfo("SendToNotification Setting Unknown")
        End If
    End Sub

    'プロセスを終了する
    Private Sub KillProcessForThisItem()
        Dim isSuccess As Boolean = KillProcess(Me._logger, Me.TextBoxProcessName.Text)
    End Sub

    'タイマーカウント表示を更新
    Private Sub UpdateRemainingTimeDisp()
        Me.LabelRemainingTimeDisp.Text = _stopWatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Public Sub ExecuteAutoRun()
        If Me.CheckBoxAutoRun.Checked Then
            Me.LoggerPrintInfo("ExecuteAutoRun")
            Me.Button1_Click(New Object(), New EventArgs())
        End If
    End Sub

    '///////////////////////////////////////////////////////////////////////////////////////////////////
    '値の設定、取得など

    '"00:00:00"文字列を秒_Integer型に変換する
    Private Function ConvertInputTimerTime(Value As String) As Integer
        'タイマー時間をセットした時間が正しい値かどうかを判定する
        'hh:mm:ss と mm:ss と ss に対応する
        'つまり 10 が入力されたら 10秒にになり、 10:00 と入力すると10分になる
        ' hh:mm:ss, mm:ss, or ss を判定して秒数に変換する
        Dim timeParts As String() = Value.Split(":"c)

        Select Case timeParts.Length
            Case 1
                ' ss の場合
                Return Convert.ToInt32(timeParts(0))
            Case 2
                ' mm:ss の場合
                Dim minutes As Integer = Convert.ToInt32(timeParts(0))
                Dim seconds As Integer = Convert.ToInt32(timeParts(1))
                Return (minutes * 60) + seconds
            Case 3
                ' hh:mm:ss の場合
                Dim hours As Integer = Convert.ToInt32(timeParts(0))
                Dim minutes As Integer = Convert.ToInt32(timeParts(1))
                Dim seconds As Integer = Convert.ToInt32(timeParts(2))
                Return (hours * 3600) + (minutes * 60) + seconds
            Case Else
                Dim errMsg As String = "Invalid time format."
                _logger.PrintInfo(errMsg)
                '_logger.PrintInfo(ErrorDescDictionary(ErrorNumber.INVALID_TIME_FORMAT))
                Return -1
                'Throw New ArgumentException(errMsg)
        End Select
    End Function

    'TimeSpan型をIntに変換する
    Function ConvertSecondsToTime(seconds As Integer) As TimeSpan
        Return TimeSpan.FromSeconds(seconds)
    End Function
    'DateTime型をIntに変換する
    Function ConvertSecondsToDateTime(seconds As Integer) As DateTime
        Dim baseDate As DateTime = New DateTime(1, 1, 1, 0, 0, 0) ' 基準となる日付を設定
        Return baseDate.AddSeconds(seconds)
    End Function

    Public Shared Function GetDictDefaultInThisItem() As Dictionary(Of String, Object)
        '設定がないときのデフォルト値
        Dim itemValue As New Dictionary(Of String, Object)
        itemValue.Add(ConstInfoFile.KEY_PROCESS_NAME, "")
        itemValue.Add(ConstInfoFile.KEY_TIMER_TIME, "1:00:00")
        itemValue.Add(ConstInfoFile.KEY_REMAINING_TIME, "00:00:00")
        itemValue.Add(ConstInfoFile.KEY_NOTIFICATION, "TaskBar")
        itemValue.Add(ConstInfoFile.KEY_AUTO_RUN, "0")
        Return itemValue
    End Function

    Public Function GetDictInThisItem() As Dictionary(Of String, Object)
        Me.LoggerPrintInfo(String.Format("GetDictInThisItem[{0}]", Me._timerStatus._timer_number))
        'Dictionary型を1行ずつAddする
        'Dim logData As New Dictionary(Of String, Object)

        'logData.Add(ConstInfoFile.KEY_LOG_PATH, "Log\__log_{TimeFormat}.log")
        'logData.Add(ConstInfoFile.KEY_LOG_FILE_TIME_FORMAT, "yyyymmdd_hhmmss")

        ' データを1行ずつ追加
        ' Item用のDictionaryを作成してデータを追加
        Dim itemValue As New Dictionary(Of String, Object)
        itemValue.Add(ConstInfoFile.KEY_PROCESS_NAME, Me.TextBoxProcessName.Text)
        itemValue.Add(ConstInfoFile.KEY_TIMER_TIME, Me.TextBoxTimerTime.Text)
        itemValue.Add(ConstInfoFile.KEY_REMAINING_TIME, Me.LabelRemainingTime.Text)
        itemValue.Add(ConstInfoFile.KEY_NOTIFICATION, Me.ComboBoxNotification.SelectedText)
        'Dim buf = ConvertBoolToInt(Me.CheckBoxAutoRun.Checked)
        itemValue.Add(ConstInfoFile.KEY_AUTO_RUN, ConvertBoolToInt(Me.CheckBoxAutoRun.Checked))

        ' Item1をメインのDictionaryに追加
        'logData.Add(ConstInfoFile.KEY_ITEM & Me._timerStatus._timer_number, itemValue)
        Return itemValue
    End Function


    Private Sub GroupBoxItemFrame_Enter(sender As Object, e As EventArgs) Handles GroupBoxItemFrame.Enter

    End Sub

    'Loggerのログに出力する"[Item1]も自動的に付与している"
    Private Sub LoggerPrintInfo(value As String)
        value = Me.GetItemStr() & value
        If Not _logger Is Nothing Then
            Me._logger.PrintInfo(value)
        Else
            Debug.Write(value)
        End If
    End Sub

End Class

Public Module MainItemFrameModule
    Public Class ConstInfoFile
        Public Const KEY_LOG_PATH As String = "LogPath"
        Public Const KEY_LOG_FILE_TIME_FORMAT As String = "LogFileTimeFormat"
        Public Const KEY_PROCESS_NAME As String = "ProcessName"
        Public Const KEY_TIMER_TIME As String = "TimerTime"
        Public Const KEY_REMAINING_TIME As String = "RemainingTime"
        Public Const KEY_NOTIFICATION As String = "WindowsNotification"
        Public Const KEY_ITEM As String = "Item"
        Public Const KEY_AUTO_RUN As String = "AutoRun"
    End Class
End Module