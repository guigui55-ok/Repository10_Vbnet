Imports System.Diagnostics
Imports System

Module Module1

    Sub Main()
        Dim upperByte As Byte = 1 ' 上位バイト
        Dim lowerByte As Byte = 10 ' 下位バイト
        Dim result As Integer

        ' 16ビットの数値として組み立てる
        result = upperByte * 256 + lowerByte

        Console.WriteLine(result) ' 結果は 266

        '###
        ' バイト配列を初期化し、最初の値に100を代入
        Dim byteArray As Byte() = New Byte() {100, 50, 200}
        Dim buf As Double

        ' byteArray(0) の値に 256 を掛ける
        buf = byteArray(0) * 256.0#
        Console.WriteLine(buf)

        Console.WriteLine("Program Done. Press Any Key.")
        Console.ReadKey()
    End Sub

End Module
