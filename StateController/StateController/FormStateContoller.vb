Public Class FormStateContoller
    Public _mainStatus As AppStatus
    'テキストボックスTextをソース上から更新したときのフラグ
    'テキストボックス直接入力時以外は、このフラグをTrueとして、TextChangedイベントのStatus更新処理をスキップさせる
    Public _isTextUpdatingProgrammatically As Boolean


    Private Sub FormStateContoller_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ChangeMode(Me._mainStatus)
        ChangeErrorStatus(ErrorStateFlags.NONE)

    End Sub
    Private Sub FormStateContoller_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Me.Visible = False
        e.Cancel = True
    End Sub

    Public Sub ChangeMode(argAppStatus As AppStatus)
        Me._mainStatus = argAppStatus
        Me.LabelMode.Text = "Mode：" & ExecuteMode_DescDictionary(Me._mainStatus._exeMode)
        Me.TextBoxStateNumber.Text = _mainStatus._nowState
        Me.LabelStatusName.Text = _mainStatus.GetNowStateName()
        Dim statusList As List(Of String) = _mainStatus.GetEnumStrValueList()
        Dim statusListStr As String = String.Join(Environment.NewLine, statusList)
        Me.LabelStatusList.Text = statusListStr
    End Sub

    Public Sub ChangeErrorStatus(argErrorStatus As Integer)
        Me._mainStatus._nowErrorStatus._nowState = argErrorStatus
        Me.TextBoxErrorNumber.Text = _mainStatus._nowErrorStatus._nowState
        Me.LabelErrorName.Text = _mainStatus._nowErrorStatus.GetNowStateName()
    End Sub

    Public Sub ChangeStatus()
        Me.TextBoxStateNumber.Text = _mainStatus._nowState
        Me.LabelStatusName.Text = _mainStatus.GetNowStateName()
    End Sub

    Private Sub ButtonPrevState_Click_1(sender As Object, e As EventArgs) Handles ButtonPrevState.Click
        Me._isTextUpdatingProgrammatically = True
        Me._mainStatus.CountDownState() 'TextChangedを通るので更新処理が2回走るので注意
        Me.ChangeStatus()
        Me._isTextUpdatingProgrammatically = False
    End Sub

    Private Sub ButtonNextState_Click(sender As Object, e As EventArgs) Handles ButtonNextState.Click
        Me._isTextUpdatingProgrammatically = True
        Me._mainStatus.CountUpState()
        Me.ChangeStatus()
        Me._isTextUpdatingProgrammatically = False
    End Sub

    Private Sub TextBoxStateNumber_TextChanged(sender As Object, e As EventArgs) Handles TextBoxStateNumber.TextChanged
        If Me._isTextUpdatingProgrammatically Then
            Exit Sub
        Else
            If Not IsNumeric(TextBoxStateNumber.Text) Then
                Dim msg As String = String.Format("入力値が数字でない（{0})", TextBoxStateNumber.Text)
                PrintInfo(msg)
                Exit Sub
            End If
            Dim value As Integer = Integer.Parse(TextBoxStateNumber.Text)
            If Not Me._mainStatus.IsValueInEnum(value) Then
                Dim msg As String = String.Format("入力値がEnumに含まれない（{0})", TextBoxStateNumber.Text)
                PrintInfo(msg)
                Exit Sub
            End If
            Me._mainStatus.ChangeStatus(value)
        End If
    End Sub

    Private Sub TextBoxErrorNumber_TextChanged(sender As Object, e As EventArgs) Handles TextBoxErrorNumber.TextChanged
        If Me._isTextUpdatingProgrammatically Then
            Exit Sub
        Else
            If Not IsNumeric(TextBoxErrorNumber.Text) Then
                Dim msg As String = String.Format("入力値が数字でない（{0})", TextBoxErrorNumber.Text)
                PrintInfo(msg)
                Exit Sub
            End If
            Dim value As Integer = Integer.Parse(TextBoxErrorNumber.Text)
            If Not Me._mainStatus._nowErrorStatus.IsValueInEnum(value) Then
                Dim msg As String = String.Format("入力値がEnumに含まれない（{0})", TextBoxErrorNumber.Text)
                PrintInfo(msg)
                Exit Sub
            End If
            Me._mainStatus._nowErrorStatus.ChangeStatus(value)
            Me.LabelErrorName.Text = Me._mainStatus._nowErrorStatus.GetNowStateName()
        End If
    End Sub

    Public Sub PrintInfo(value As String)
        Debug.WriteLine(value)
    End Sub
End Class

Public Module AppStatusModule

    Enum ExecuteMode
        MODE_A
        MODE_B
    End Enum

    Public ExecuteMode_DescDictionary = New Dictionary(Of ExecuteMode, String) _
                From {
                {ExecuteMode.MODE_A, "実行モード_A"},
                {ExecuteMode.MODE_B, "実行モード_B"}
    }

    Enum StateFlagsModeA
        A_INIT
        A_PREPARE
        A_EXE_MAIN
        A_AFTER_WORK
        A_END
    End Enum

    Public Status_ModeA_DescDictionary = New Dictionary(Of Integer, String) _
                From {
                {StateFlagsModeA.A_INIT, "初期化_A"},
                {StateFlagsModeA.A_PREPARE, "準備中_A"},
                {StateFlagsModeA.A_EXE_MAIN, "処理_A"},
                {StateFlagsModeA.A_AFTER_WORK, "後処理_A"},
                {StateFlagsModeA.A_END, "処理終了_A"}
    }

    Enum StateFlagsModeB
        B_INIT
        B_PREPARE
        B_EXE_MAIN
        B_END
    End Enum

    Public Status_ModeB_DescDictionary = New Dictionary(Of Integer, String) _
                From {
                {StateFlagsModeB.B_INIT, "初期化_B"},
                {StateFlagsModeB.B_PREPARE, "準備中_B"},
                {StateFlagsModeB.B_EXE_MAIN, "処理_B"},
                {StateFlagsModeB.B_END, "処理終了_B"}
    }


    Enum ErrorStateFlags
        NONE
        UNEXPECTED_ERROR
    End Enum

    Public Status_Error_DescDictionary = New Dictionary(Of Integer, String) _
                From {
                {ErrorStateFlags.NONE, "None"},
                {ErrorStateFlags.UNEXPECTED_ERROR, "不明なエラー"}
    }

    Public Class ErrorStatus
        Public _nowState As Integer
        Public _exeMode As Integer
        Public _stateDistDict As Dictionary(Of Integer, String)
        'Public _enumFlags As Object
        Public _enumType As Type
        Public _enumTypeName As String
        Public _enumObject As Type
        Public Event ChangeErrorStatusEvent()

        Sub New()
            Me._nowState = ErrorStateFlags.NONE
            Me._stateDistDict = Status_Error_DescDictionary
            Me._enumType = GetType(ErrorStateFlags)
            Me._enumTypeName = Me._enumType.Name
            Me._enumObject = Me.GetEnumTypeFromTypeName(Me._enumTypeName)
        End Sub
        Public Sub ChangeStatus(value As Integer)
            Me._nowState = value
            Me.PrintStatus()
            ' ステータスが変更されたときにイベントを発生させる
            RaiseEvent ChangeErrorStatusEvent()
        End Sub
        Public Sub PrintStatus()
            Dim buf As String = Me.GetPrintStatusValue()
            Debug.Print(buf)
        End Sub
        Public Function GetPrintStatusValue()
            Dim buf As String = String.Format("ChangeErrorStatus = {0} [{1}]", Me._nowState, Me._stateDistDict(Me._nowState))
            Return buf
        End Function
        Public Function GetNowStateName()
            Return Me._stateDistDict(Me._nowState)
        End Function
        Public Function GetEnumTypeFromTypeName(ByVal typeName As String) As Type
            ' 現在のアセンブリから全ての型を取得し、名前でフィルタリング
            Dim enumType As Type = Me.GetType().Assembly.GetTypes().FirstOrDefault(Function(t) t.Name = typeName)

            ' 取得した型がEnum型かどうかを確認
            If enumType IsNot Nothing AndAlso enumType.IsEnum Then
                Return enumType
            Else
                Throw New ArgumentException("指定された型名はEnumではありません: " & typeName)
            End If
        End Function

        'value が Enumに含まれるか判定する
        Function IsValueInEnum(ByVal value As Integer) As Boolean
            Dim enumType As Type
            enumType = Me.GetType().Assembly.GetTypes().FirstOrDefault(Function(t) t.Name = Me._enumTypeName)
            If enumType Is Nothing Then
                Throw New ArgumentException($"Enum type '{Me._enumTypeName}' not found.")
            End If
            Return [Enum].IsDefined(enumType, value)
        End Function
    End Class


    '///////////
    Public Class AppStatus
        Public _nowState As Integer
        Public _exeMode As Integer
        Public _stateDistDict As Dictionary(Of Integer, String)
        'Public _enumFlags As Object
        Public _enumType As Type
        Public _enumTypeName As String
        Public _enumObject As Type
        Public _nowErrorStatus As ErrorStatus

        Public Event ChangeStatusEvent()
        Public Event ChangeErrorStatusEvent()

        Public Sub New()
            Me._nowErrorStatus = New ErrorStatus()
        End Sub

        Public Sub SetErrorStatus(statusValue As Integer)
            Me._nowErrorStatus._nowState = statusValue
        End Sub

        Public Sub ChangeMode(mode As Integer)
            If mode = ExecuteMode.MODE_A Then
                Me._exeMode = mode
                Me._nowState = StateFlagsModeA.A_INIT
                Me._stateDistDict = Status_ModeA_DescDictionary
                'Me._stateDistDict = Status_ModeA_DescDictionary.ToDictionary(Function(k) CType(k.Key, [Enum]), Function(v) v.Value)
                Me._enumType = GetType(StateFlagsModeA)
                Me._enumTypeName = Me._enumType.Name
                Me._enumObject = Me.GetEnumTypeFromTypeName(Me._enumTypeName)
            ElseIf mode = ExecuteMode.MODE_B Then
                Me._exeMode = mode
                Me._nowState = StateFlagsModeB.B_INIT
                Me._stateDistDict = Status_ModeB_DescDictionary
                'Me._stateDistDict = Status_ModeB_DescDictionary.ToDictionary(Function(k) CType(k.Key, [Enum]), Function(v) v.Value)
                Me._enumType = GetType(StateFlagsModeB)
                Me._enumTypeName = Me._enumType.Name
                Me._enumObject = Me.GetEnumTypeFromTypeName(Me._enumTypeName)
            Else
                Debug.Print(String.Format("Unexpected Mode [{0}]", mode))
            End If
        End Sub

        Public Sub CountUpState()
            Dim val As Integer = Me._nowState + 1
            If Me._stateDistDict.Count <= val Then
                val = 0
            End If
            Me.ChangeStatus(val)
        End Sub
        Public Sub CountDownState()
            Dim val As Integer = Me._nowState - 1
            If val < 0 Then
                val = Me._stateDistDict.Count - 1
            End If
            Me.ChangeStatus(val)
        End Sub

        Public Sub ChangeErrorStatus(value As Integer)

        End Sub

        Public Sub ChangeStatus(value As Integer)
            Me._nowState = value
            Me.PrintStatus()
            ' ステータスが変更されたときにイベントを発生させる
            RaiseEvent ChangeStatusEvent()
        End Sub

        Public Sub PrintStatus()
            Dim buf As String = Me.GetPrintStatusValue()
            Debug.Print(buf)
        End Sub

        Public Function GetNowStateName()
            Return Me._stateDistDict(Me._nowState)
        End Function

        Public Function GetPrintStatusValue()
            Dim buf As String = String.Format("ChangeStatus = {0} [{1}]", Me._nowState, Me._stateDistDict(Me._nowState))
            Return buf
        End Function

        'value が Enumに含まれるか判定する
        Function IsValueInEnum(ByVal value As Integer) As Boolean
            Dim enumType As Type
            enumType = Me.GetType().Assembly.GetTypes().FirstOrDefault(Function(t) t.Name = Me._enumTypeName)
            If enumType Is Nothing Then
                Throw New ArgumentException($"Enum type '{Me._enumTypeName}' not found.")
            End If
            Return [Enum].IsDefined(enumType, value)
        End Function

        Public Function GetEnumStrValueList() As List(Of String)
            Dim result As New List(Of String)

            ' 列挙型の値をループしてディクショナリの値を取得
            For Each enumValue As Integer In [Enum].GetValues(Me._enumType)
                Dim keyName As String = enumValue.ToString()
                Dim dictValue As String = Me._stateDistDict(enumValue)
                result.Add($"""{keyName}"",""{dictValue}""")
            Next

            Return result
        End Function

        Public Function ConvertDictionaryToFormattedStringList(Of T)(ByVal dict As Dictionary(Of T, String)) As List(Of String)
            Dim retList As List(Of String) = New List(Of String)
            Dim buf As String
            For Each kvp In dict
                buf = $"""{kvp.Key.ToString}"",""{kvp.Value}"""
                retList.Add(buf)
            Next
            Return retList
        End Function

        Public Sub GetEnumValueFromTypeName(ByVal typeName As String, ByVal enumValue As String)
            ' 指定された型名からTypeオブジェクトを取得
            Dim enumType As Type = Type.GetType(typeName)

            If enumType IsNot Nothing AndAlso enumType.IsEnum Then
                ' Enumのオブジェクトを取得
                Dim enumObject As [Enum] = [Enum].Parse(enumType, enumValue)

                Debug.WriteLine("取得したEnumオブジェクト: " & enumObject.ToString())
            Else
                Debug.WriteLine("指定された型名はEnumではありません: " & typeName)
            End If
        End Sub
        'Public Function GetEnumTypeFromTypeName(ByVal typeName As String) As Type
        '    ' 指定された型名からTypeオブジェクトを取得
        '    Dim enumType As Type = Type.GetType(typeName)

        '    ' 取得した型がEnum型かどうかを確認
        '    If enumType IsNot Nothing AndAlso enumType.IsEnum Then
        '        Return enumType
        '    Else
        '        Throw New ArgumentException("指定された型名はEnumではありません: " & typeName)
        '    End If
        'End Function
        'Public Function GetEnumTypeFromTypeName(ByVal typeName As String) As Type
        '    ' 現在のアセンブリから型を取得
        '    Dim enumType As Type = Type.GetType(Me.GetType().Namespace & "." & typeName)

        '    ' 取得した型がEnum型かどうかを確認
        '    If enumType IsNot Nothing AndAlso enumType.IsEnum Then
        '        Return enumType
        '    Else
        '        Throw New ArgumentException("指定された型名はEnumではありません: " & typeName)
        '    End If
        'End Function

        Public Function GetEnumTypeFromTypeName(ByVal typeName As String) As Type
            ' 現在のアセンブリから全ての型を取得し、名前でフィルタリング
            Dim enumType As Type = Me.GetType().Assembly.GetTypes().FirstOrDefault(Function(t) t.Name = typeName)

            ' 取得した型がEnum型かどうかを確認
            If enumType IsNot Nothing AndAlso enumType.IsEnum Then
                Return enumType
            Else
                Throw New ArgumentException("指定された型名はEnumではありません: " & typeName)
            End If
        End Function

    End Class

End Module