Imports System

Module Program
    Sub Main(args As String())
        'Console.WriteLine("Hello World!")
        TestJoinPath()
    End Sub
End Module

Module ModuleSample
    Function SampleMethod(
                         valueA As String,
                         ValueB As String)
        Dim SampleProc = valueA + ValueB
        Return SampleProc
    End Function

    Function SampleLongName______________________________Method(
                                                               valueA As String,
                                                               ValueB As String)
        Dim SampleProc = valueA + ValueB + "_________"
        Return SampleProc
    End Function
End Module


Public Module CommonAnyModule
    Public Sub DebugPrint(Value As Object)
        Console.WriteLine(Value)
    End Sub
    Public Function TestJoinPath()
        DebugPrint("TestJoinPath")
        Dim pathA As String
        Dim pathB As String
        pathA = "C:\Windows\\"
        pathB = "\log\log.log"
        Dim pathRet As String = JoinPath(pathA, pathB)
        DebugPrint("pathRet : " & pathRet)
    End Function

    ''' <summary>
    ''' pathA��pathB��A��������
    ''' </summary>
    ''' <remarks>�p�X�̋�؂蕶�����Ȃǂ��d�����Ȃ��悤�ɂ��Ă���</remarks>
    ''' <param name="pathA"></param>
    ''' <param name="pathB"></param>
    ''' <returns></returns>
    Public Function JoinPath(pathA As String, pathB As String)
        Dim ret As String = ""
        '///////////
        ' �����̊֐��Ƃ��Ă�Lambda��
        '�h�ŏ��h�́��}�[�N�����ׂď�������
        Dim RemoveLastCharIfMatchToSepaleteChar As Func(Of String, String) =
            Function(path As String)
                If path(path.Length - 1) = "\" Then
                    path = path.Substring(0, path.Length - 1)
                    path = RemoveLastCharIfMatchToSepaleteChar(path)
                End If
                Return path
            End Function
        '�h�ŏ��h�́��}�[�N�����ׂď�������
        Dim RemoveFirstCharIfMatchToSepaleteChar As Func(Of String, String) =
            Function(path As String)
                If path(0) = "\" Then
                    path = path.Substring(1, path.Length - 1)
                    path = RemoveFirstCharIfMatchToSepaleteChar(path)
                End If
                Return path
            End Function
        '///////////
        ' �����̊֐����Ăяo��
        pathA = RemoveLastCharIfMatchToSepaleteChar(pathA)
        pathB = RemoveFirstCharIfMatchToSepaleteChar(pathB)
        Dim result As String = pathA & "\" & pathB
        Return result
    End Function
    '///////////


End Module
