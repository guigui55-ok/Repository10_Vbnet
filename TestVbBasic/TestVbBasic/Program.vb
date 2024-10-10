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
        '-2,147,483,648 から 2,147,483,647 までの符号付き 32 ビット (4 バイト) の整数
        bufInt = 2147483647
        'bufInt = 2147483648 'BC30439	型 'Integer' では表現できない定数式です。
        'Console.WriteLine("bufInt=" + Str(bufInt))

        Dim strAry As String() = New String() {"今日", "は", "いい", "天気", "です", "。"}
        '////
        'キャスト
    End Sub


    Class ThisProgramConstants
        Public Const VALUE_A As Integer = 0
        Public Const VALUE_B As Integer = 1
    End Class
End Module