Module ModuleSmallValueHandler
    ''' <summary>
    ''' 非常に小さい値を0に変換する関数
    ''' </summary>
    ''' <param name="value">対象の値</param>
    ''' <param name="threshold">閾値（この値より小さい場合、0にする）</param>
    ''' <returns>0に変換された値または元の値</returns>
    Public Function ChangeSmallValueToZero(value As Double, Optional threshold As Double = 0.000000000001) As Double
        If Math.Abs(value) < threshold Then
            Return 0
        Else
            Return value
        End If
    End Function

    Sub MainTest()
        Dim smallValue As Double = 0.0000000000000123456789
        Dim normalValue As Double = 0.0001

        ' 閾値を1E-12として使用
        Dim resultSmall As Double = ChangeSmallValueToZero(smallValue)
        Dim resultNormal As Double = ChangeSmallValueToZero(normalValue)

        Console.WriteLine($"小さい値: {smallValue} -> {resultSmall}")
        Console.WriteLine($"通常の値: {normalValue} -> {resultNormal}")
    End Sub
End Module
