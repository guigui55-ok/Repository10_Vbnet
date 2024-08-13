
Imports System.Reflection
Public Module ConstantsModule

    Public Enum ErrorNumber
        NONE
        INVALID_INPUT_TIMER_TIME
        INVALID_TIME_FORMAT
        UNEXPECTED_STATUS
    End Enum

    Public ErrorDescDictionary = New Dictionary(Of ErrorNumber, String) _
                From {
                {ErrorNumber.NONE, "エラーはありません。"},
                {ErrorNumber.INVALID_INPUT_TIMER_TIME, "入力時間が正しくありません"},
                {ErrorNumber.INVALID_TIME_FORMAT, "時間の文字列が正しくありません"},
                {ErrorNumber.UNEXPECTED_STATUS, "タイマーステータスフラグが想定外です"}
    }

    Public Enum TimerItemStatusFlags
        NONE
        LOAD_CONTROL
        INIT_TIMER
        START_TIMER
        PROCESSING_TIMER
        PAUSE_TIMER
        FINISHED_TIMER
        STOP_TIMER
    End Enum

    Public TimerItemStatusDescDictionary = New Dictionary(Of TimerItemStatusFlags, String) _
                From {
                {TimerItemStatusFlags.NONE, "None"},
                {TimerItemStatusFlags.LOAD_CONTROL, "コントロール読込中"},
                {TimerItemStatusFlags.INIT_TIMER, "タイマー初期化"},
                {TimerItemStatusFlags.START_TIMER, "タイマー開始"},
                {TimerItemStatusFlags.PROCESSING_TIMER, "タイマー実行中"},
                {TimerItemStatusFlags.PAUSE_TIMER, "タイマー一時停止"},
                {TimerItemStatusFlags.FINISHED_TIMER, "タイマー実行完了"},
                {TimerItemStatusFlags.STOP_TIMER, "タイマー中止"}
    }

    Public Class TimerItemStatus
        Public _NowStatus As Integer
        Public _logger As MainLogger
        Public _endTimeSpan As TimeSpan
        Public _timer_number As Integer

        Public Event ChangeStatusEvent()
        Sub New()
            Me._NowStatus = TimerItemStatusFlags.NONE
        End Sub

        Public Sub ChangeStatus(Value As Integer)
            Me._NowStatus = Value
            Me.PrintStatus()
            RaiseEvent ChangeStatusEvent()
        End Sub


        Public Sub PrintStatus()
            'Me._logger.PrintInfo(String.Format("ChangeStatus = {0} [{1}]", Me._NowStatus, TimerItemStatusDescDictionary(Me._NowStatus)))
            'FormLoad時にloggerが渡せないので一旦以下で対応
            Dim buf As String = Me.GetPrintStatusValue()
            'Debug.WriteLine(buf)
        End Sub

        'ログ出力用のステータス情報を取得する
        Public Function GetPrintStatusValue(Optional addItemNumber As Boolean = False)
            Dim buf As String = String.Format("ChangeStatus = {0} [{1}]", Me._NowStatus, TimerItemStatusDescDictionary(Me._NowStatus))
            If addItemNumber Then
                buf = Me.GetItemStr() & buf
            End If
            Return buf
        End Function

        ''' <summary>
        ''' タイマーが開始状態可能かどうかを判定する。
        ''' （一時停止、実行中、開始処理中、コントロール読み込み中、以外の状態）
        ''' </summary>
        ''' <returns></returns>
        Public Function NowStatusIsStartAble()
            'タイマーが開始状態可能かどうか
            If (Me._NowStatus = TimerItemStatusFlags.START_TIMER) _
                Or (Me._NowStatus = TimerItemStatusFlags.PAUSE_TIMER) _
                Or (Me._NowStatus = TimerItemStatusFlags.PROCESSING_TIMER) _
                Or (Me._NowStatus = TimerItemStatusFlags.LOAD_CONTROL) Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Function GetNowStatusName()
            Dim enumNames As String() = [Enum].GetNames(GetType(TimerItemStatusFlags))
            Return enumNames(Me._NowStatus)
        End Function
        Public Function GetNowStatusDescription()
            Return TimerItemStatusDescDictionary(Me._NowStatus)
        End Function

        '"[Item1]"を取得する、ログ出力用
        Private Function GetItemStr(Optional beforeSpacing As String = "", Optional afterSpacing As String = " ")
            Dim buf As String = "Item" & Me._timer_number
            buf = beforeSpacing & "[" & buf & "]" & afterSpacing
            Return buf
        End Function
    End Class


    '/////
    'Public ReadOnly TIMER_INTERVAL As Integer = 1000
    'タイマー表示1秒飛び対策 240813
    Public ReadOnly TIMER_INTERVAL As Integer = 200

    '/////

    Public Enum LangageFlags
        EN
        JA
    End Enum

    Public Class ConstItemFrameWording
        Public Const START_BUTTON = "Start"
        Public Const RESTART_BUTTON = "ReStart"
        Public Const PAUSE_BUTTON = "Pause"
        Public Const STOP_BUTTON = "Stop"
        Public Const LABEL_PROCESS_NAME = "LabelProcessName"
        Public Const TIMER_TIME = "Time"
        Public Const TIMER_TIME_RIGHT = " (hh:mm:ss)"
        Public Const REMAINING_TIME = "Remaining Time"
        Public Const REMAINING_TIME_DISP_DEFAULT = "00:00:00"
        Public Const NOTIFICATION = "Notification"
        Public Const GROUP_BOX_TITLE = "Item"
        Public Const LABEL_STATUS = "Status"
        Public Const CHECK_BOX_AUTO_RUN = "Auto Run Timer at Startup (起動時に実行)"
        Public Const CHECK_BOX_AUTO_RUN_EN = "Execute the timer at startup"
        'Auto Run Timer at Startup / Automatically Run Timer at Startup
        '/
        'Public Shared Widening Operator CType(v As ConstItemFrameWordingJA) As ConstItemFrameWording
        '    Throw New NotImplementedException()
        'End Operator

        'Public Shared Widening Operator CType(v As ConstItemFrameWording) As VariantType
        '    Throw New NotImplementedException()
        'End Operator
    End Class
    Public Class ConstItemFrameWordingJA
        Public Const START_BUTTON = "開始"
        Public Const RESTART_BUTTON = "再開"
        Public Const PAUSE_BUTTON = "一時停止"
        Public Const STOP_BUTTON = "停止"
        Public Const LABEL_PROCESS_NAME = "プロセス名"
        Public Const TIMER_TIME = "設定時間"
        Public Const TIMER_TIME_RIGHT = " (hh:mm:ss)"
        Public Const REMAINING_TIME = "経過時間"
        Public Const REMAINING_TIME_DISP_DEFAULT = "00:00:00"
        Public Const NOTIFICATION = "通知設定"
        Public Const GROUP_BOX_TITLE = "項目"
        Public Const LABEL_STATUS = "ステータス"
        Public Const CHECK_BOX_AUTO_RUN = "起動時にタイマーを実行する"
        Public Const CHECK_BOX_AUTO_RUN_EN = "Execute the timer at startup"
    End Class

    'インターフェースは定義部の実装が長くなるので保留
    'ただこのままだと、テキスト変更部の実装が長くなるが、これは検討中
    'Public Class WordingFactory
    '    Public Shared Function GetWording(ByVal lang As Integer) As ConstItemFrameWording
    '        Select Case lang
    '            Case LangageFlags.JA
    '                Return New ConstItemFrameWordingJA()
    '            Case Else
    '                'LangageFlags.EN
    '                Return New ConstItemFrameWording()
    '        End Select
    '    End Function
    'End Class

    'Notification設定用定数クラス
    Public Class ConstNotificationValueInItem
        Public Const SETTING_NONE = "None"
        Public Const SETTING_TASK_BAR = "TaskBar"

        'Public Shared Function GetMembarSettingVariantList()
        '    'このクラスのメンバ変数の設定の値をリストで取得する
        '    'Imports System.Reflection
        '    Dim settingList As New List(Of String)()

        '    ' リフレクションを使ってこのクラスのフィールドを取得
        '    Dim fields As FieldInfo() = Me.GetType().GetFields(BindingFlags.Public Or BindingFlags.Static Or BindingFlags.FlattenHierarchy)

        '    ' 各フィールドが Const であるかを確認し、値をリストに追加
        '    For Each field As FieldInfo In fields
        '        If field.IsLiteral AndAlso Not field.IsInitOnly Then ' Const フィールドかどうか確認
        '            settingList.Add(field.GetValue(Nothing).ToString())
        '        End If
        '    Next

        '    Return settingList
        'End Function

        Public Shared Function GetMembarSettingVariantList() As List(Of String)
            Dim settingList As New List(Of String)()

            ' リフレクションを使ってこのクラスのフィールドを取得
            Dim fields As FieldInfo() = GetType(ConstNotificationValueInItem).GetFields(BindingFlags.Public Or BindingFlags.Static Or BindingFlags.FlattenHierarchy)

            ' 各フィールドが Const であるかを確認し、値をリストに追加
            For Each field As FieldInfo In fields
                If field.IsLiteral AndAlso Not field.IsInitOnly Then ' Const フィールドかどうか確認
                    settingList.Add(field.GetValue(Nothing).ToString())
                End If
            Next

            Return settingList
        End Function

        Public Shared Function IsIncludeValueInSettingList(Value As String) As Boolean
            'Valueがメンバ変数リストのいずれかと合致するか判定する
            ' リフレクションを使ってこのクラスのフィールドを取得
            Dim fields As FieldInfo() = GetType(ConstNotificationValueInItem).GetFields(BindingFlags.Public Or BindingFlags.Static Or BindingFlags.FlattenHierarchy)

            ' 各フィールドが Const であるかを確認し、値が一致するかを確認
            For Each field As FieldInfo In fields
                If field.IsLiteral AndAlso Not field.IsInitOnly Then ' Const フィールドかどうか確認
                    If field.GetValue(Nothing).ToString() = Value Then
                        Return True
                    End If
                End If
            Next

            Return False
        End Function

        'Valueがメンバ変数リストのいずれかと合致する場合、配列からインデックスを取得する
        'ない場合は-1を返す
        Public Shared Function GetIndexInMatchSettingList(Value As String) As Integer
            ' リフレクションを使ってこのクラスのフィールドを取得
            Dim fields As FieldInfo() = GetType(ConstNotificationValueInItem).GetFields(BindingFlags.Public Or BindingFlags.Static Or BindingFlags.FlattenHierarchy)

            ' 各フィールドが Const であるかを確認し、値が一致する場合のインデックスを返す
            For i As Integer = 0 To fields.Length - 1
                If fields(i).IsLiteral AndAlso Not fields(i).IsInitOnly Then ' Const フィールドかどうか確認
                    If fields(i).GetValue(Nothing).ToString() = Value Then
                        Return i
                    End If
                End If
            Next

            Return -1 ' 一致する値が見つからなかった場合
        End Function


    End Class
End Module
