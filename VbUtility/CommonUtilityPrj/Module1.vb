Module Module1

    Sub Main()
    End Sub


    Function GetEnumArray(enumType As Type)
        If enumType.IsEnum Then
            Return [Enum].GetNames(enumType)
        Else
            Throw New ArgumentException("指定された型はEnumではありません。")
        End If
        Return 0
    End Function


    Sub MainEnum()
        Dim enumCount As Integer = [Enum].GetValues(GetType(TestEnum)).Length
        Console.WriteLine("Enum の値の個数: " & enumCount)

        Dim EnumAry As String() = GetEnumArray(GetType(TestEnum))
        For Each buf In EnumAry
            Console.WriteLine(buf.GetType().ToString())
            Console.WriteLine(buf)
        Next
        'Console.WriteLine("Enum 値: " & String.Join(", ", New List(Of String)().AddRange(EnumAry)))
        Console.WriteLine("Enum 値: " & String.Join(", ", EnumAry))

        Console.ReadKey()
    End Sub

    Enum TestEnum
        DEFAULT_VAL = 0
        VALUE_A = 1
        VALUE_B = 2
    End Enum

End Module
