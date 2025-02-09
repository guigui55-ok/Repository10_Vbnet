Public Class FindStringForm

    Public Class ConstFindString
        Public Const RESULT_MATCH_TRUE As Integer = 1
        Public Const RESULT_ERROR As Integer = -1
        Public Const RESULT_NOT_FOUND As Integer = -2
    End Class

    Dim _logger As AppLogger
    Dim _findMainControl As Control
    Dim _parentControl As Control
    Dim _mainForm As Form
    Dim _nextButton As Control
    Dim _prevButton As Control

    Public _nowFindPos As Long
    Public _selectPosStart As Long
    Public _selectPosEnd As Long
    Public _selectLength As Long

    Sub New(logger As AppLogger, findMainControl As Control, parentControl As Control)
        _logger = logger
        _findMainControl = findMainControl
        _parentControl = parentControl
    End Sub

    Sub InitForm(form As Form)
        _mainForm = form
        _mainForm.KeyPreview = True
        AddHandler _mainForm.KeyDown, AddressOf MainForm_KeyDown
    End Sub

    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs)
        If e.Control And e.KeyCode = Keys.F Then
            If IsShowFindControl() Then
                HideFindControl()
            Else
                ShowFindControl()
            End If
        End If
    End Sub

    Public Function IsShowFindControl()
        Return Me._parentControl.Visible
    End Function

    Public Sub HideFindControl()
        'PrentControlはMenuStripの想定、その中にUserControl（Panel｛TextBox,Button, Button｝）の構成
        _parentControl.Visible = False
        _findMainControl.Visible = False
    End Sub

    Public Sub ShowFindControl()
        _parentControl.Visible = True
        _findMainControl.Visible = True
    End Sub

    'Public Function ExecuteFindWithControl()

    'End Function


    Public Function ExecuteNext(content As String, findValue As String, nowPos As Integer)
        Try
            If findValue.Length <= 0 Then
                _selectPosStart = nowPos
                _selectLength = 0
                Return ConstFindString.RESULT_NOT_FOUND
            End If
            Dim findTarget As String
            Dim addNum As Long
            If 0 < nowPos Then
                findTarget = content.Substring(nowPos)
                addNum = nowPos
            Else
                findTarget = content
            End If
            If findTarget = "" Then
                _selectPosStart = nowPos
                _selectLength = 0
                Return ConstFindString.RESULT_NOT_FOUND
            End If
            Dim ret = ExecuteFind(findTarget, findValue)
            If ret = ConstFindString.RESULT_MATCH_TRUE Then
                _selectPosStart += addNum
                _selectPosEnd += addNum
            End If
            Return ret
        Catch ex As Exception
            Return ConstFindString.RESULT_MATCH_TRUE
        End Try
    End Function
    Public Function ExecutePrev(content As String, findValue As String, nowPos As Integer)
        Try
            findValue = New String(findValue.Reverse().ToArray())
            Dim findTarget As String
            If 0 < nowPos Then
                findTarget = content.Substring(0, nowPos)
            Else
                findTarget = content
            End If
            findTarget = New String(findTarget.Reverse().ToArray())
            Dim retflag = ExecuteFind(findTarget, findValue)
            '逆順の位置となっているので、直す
            If retflag = ConstFindString.RESULT_MATCH_TRUE Then
                _selectPosStart = findTarget.Length - _selectPosStart
                _selectPosEnd = findTarget.Length = _selectPosEnd
                _selectPosStart -= _selectLength
                _selectPosEnd -= _selectLength
                '_selectLength -= 1
            Else
                _selectPosStart = 0
                _selectPosEnd = 0
                _selectLength = 0
                Return retflag
            End If
            If _selectPosStart < 0 Then '念のため
                _selectPosEnd = 0
                _selectLength = 0
            End If
            Return ConstFindString.RESULT_MATCH_TRUE
        Catch ex As Exception
            _logger.AddException(ex, Me, "ExecutePrev")
            Return ConstFindString.RESULT_ERROR
        End Try
    End Function

    Public Function ExecuteFind(content As String, findValue As String)
        Try
            Dim pos = content.IndexOf(findValue)
            If pos < 0 Then
                Return ConstFindString.RESULT_NOT_FOUND
            End If
            _selectPosStart = pos
            _selectPosEnd = pos + findValue.Length
            _selectLength = findValue.Length
            Return ConstFindString.RESULT_MATCH_TRUE
        Catch ex As Exception
            Return ConstFindString.RESULT_ERROR
        End Try
    End Function

End Class
