Module GeneralModule
    Public Function ConvertBoolToInt(Value As Boolean) As Integer
        If Value = True Then
            Return 1
        Else
            Return 0
        End If
    End Function

    Public Function ConvertIntToBool(Value As Integer) As Boolean
        If 0 < Value Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ConverToBool(Value As Object) As Boolean
        If TypeOf Value Is String Then
            Dim strValue As String = Value.ToString().ToLower()

            ' 文字列処理
            Select Case strValue
                Case "1", "true"
                    Return True
                Case "0", "false"
                    Return False
                Case Else
                    ' 不明な文字列の場合はFalseを返す
                    Return False
            End Select
        ElseIf TypeOf Value Is Integer Then
            ' Integer処理
            Return ConvertIntToBool(CType(Value, Integer))
        Else
            ' 不明な型の場合もFalseを返す
            Return False
        End If
    End Function
End Module
