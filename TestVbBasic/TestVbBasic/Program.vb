Imports System

Module Program

    Sub Main(args As String())
        ExecuteFactoryTestMain()
        Console.ReadKey()
    End Sub


    Sub MainB()
        Console.WriteLine("Hello World!")
        Dim bufStr As String
        bufStr = "TestString"
        Console.WriteLine("BusStr=" & bufStr)
        Dim bufInt As Integer
        bufInt = 11
        Console.WriteLine("BufInt=" + Str(bufInt))
        '-2,147,483,648 ���� 2,147,483,647 �܂ł̕����t�� 32 �r�b�g (4 �o�C�g) �̐���
        bufInt = 2147483647
        'bufInt = 2147483648 'BC30439	�^ 'Integer' �ł͕\���ł��Ȃ��萔���ł��B
        'Console.WriteLine("bufInt=" + Str(bufInt))

        Dim strAry As String() = New String() {"����", "��", "����", "�V�C", "�ł�", "�B"}
        '////
        '�L���X�g
    End Sub


    Class ThisProgramConstants
        Public Const VALUE_A As Integer = 0
        Public Const VALUE_B As Integer = 1
    End Class
End Module