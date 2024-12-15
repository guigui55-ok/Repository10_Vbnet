Module ModuleDoubleParseTest

    Sub Main()
        Dim exponentialString As String = "8.00E+5"
        Console.WriteLine(exponentialString)
        Dim result As String = ConvertExponentialToDecimal(exponentialString)
        Console.WriteLine(result) ' 出力: 800000
        Console.ReadKey()
    End Sub

    Public Function ConvertExponentialToDecimal(input As String) As String
        Try
            ' 数値に変換してから通常の文字列形式に変換する
            Dim number As Double = Double.Parse(input, Globalization.NumberStyles.Float)
            Return number.ToString("0.######") ' 必要に応じてフォーマットを調整
        Catch ex As Exception
            ' 変換できなかった場合は元の文字列を返す
            Return input
        End Try
    End Function

End Module
