Module Module1

    Sub Main()
        Dim exponentValue As String = "9.87654231E+10"
        exponentValue = "9.87054231E+10"
        exponentValue = "9.87154231E+10"
        exponentValue = "9.8744231E+10"
        exponentValue = "9.8754231E+10"

        ' 通常の数値表記に変換
        Dim normalValue As String = ConvertToNormalNumber(exponentValue)
        Debug.WriteLine("通常の数値表記: " & normalValue)

        ' 小数点以下3桁目を四捨五入して指数表記
        Dim roundedValue As String = RoundToExponent(exponentValue, 2)
        Debug.WriteLine("四捨五入した指数表記: " & roundedValue)

        '#############
        Dim value As Double = 98765423100.0

        ' 小数点以下2桁に丸めて指数表記
        Dim rounded As String = value.ToString("E2")
        Debug.WriteLine(rounded) ' 出力: 9.88E+10

        ' 小数点以下4桁に丸めて指数表記
        Dim rounded4 As String = value.ToString("E4")
        Debug.WriteLine(rounded4) ' 出力: 9.8765E+10
    End Sub

    Public Function ConvertToNormalNumber(input As String) As String
        Dim result As Double
        If Double.TryParse(input, result) Then
            ' 通常の数値表記に変換
            Return result.ToString("F")
        Else
            Throw New ArgumentException("入力が正しい数値ではありません。")
        End If
    End Function

    Public Function RoundToExponent(input As String, digits As Integer) As String
        Dim result As Double
        If Double.TryParse(input, result) Then
            ' 四捨五入して指数表記に変換
            Return result.ToString("E" & digits)
        Else
            Throw New ArgumentException("入力が正しい数値ではありません。")
        End If
    End Function


End Module
