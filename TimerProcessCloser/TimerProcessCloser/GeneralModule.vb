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


    ''' <summary>
    ''' pathAとpathBを連結させる
    ''' </summary>
    ''' <remarks>パスの区切り文字￥などが重複しないようにしている</remarks>
    ''' <param name="pathA"></param>
    ''' <param name="pathB"></param>
    ''' <returns></returns>
    Public Function JoinPath(pathA As String, pathB As String)
        Dim ret As String = ""
        '///////////
        ' 内側の関数としてのLambda式
        '”最初”の￥マークをすべて除去する
        Dim RemoveLastCharIfMatchToSepaleteChar As Func(Of String, String) =
            Function(path As String)
                If path(path.Length - 1) = "\" Then
                    path = path.Substring(0, path.Length - 1)
                    path = RemoveLastCharIfMatchToSepaleteChar(path)
                End If
                Return path
            End Function
        '”最初”の￥マークをすべて除去する
        Dim RemoveFirstCharIfMatchToSepaleteChar As Func(Of String, String) =
            Function(path As String)
                If path(0) = "\" Then
                    path = path.Substring(1, path.Length - 1)
                    path = RemoveFirstCharIfMatchToSepaleteChar(path)
                End If
                Return path
            End Function
        '///////////
        ' 内側の関数を呼び出す
        pathA = RemoveLastCharIfMatchToSepaleteChar(pathA)
        pathB = RemoveFirstCharIfMatchToSepaleteChar(pathB)
        Dim result As String = pathA & "\" & pathB
        Return result
    End Function


End Module

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
