Imports System.Data

Module MathFomulaParserMainModule

    Sub Main()
        Output("******")
        DataTableComputeTestB()
    End Sub

    Sub Output(value As Object)
        Dim buf = String.Format("{0}", value)
        Console.WriteLine(buf)
        Debug.WriteLine(buf)
    End Sub

    Sub DataTableComputeTestA()
        Dim expression As String = "(2+3)*4" ' ここでは x=2, y=3, z=4 を使用
        Dim dt As New DataTable()

        ' 式を計算する
        Dim result As Double = Convert.ToDouble(dt.Compute(expression, String.Empty))
        Output("計算結果: " & result)
    End Sub

    Sub DataTableComputeTestB()
        Dim expression As String
        expression = "(x+y)z"
        expression = "((x+y)z)"
        expression = "(((x+y))z)"
        'expression = "(((x+y))z)()" 'error System.Data.SyntaxErrorException: '式に構文エラーがあります。'
        'expression = "(((x+y))z)0" 'error System.Data.SyntaxErrorException: '構文エラー : '0' 演算子の後にオペランドがありません。'
        expression = "(((x+y))z)*0"
        'expression = "(((x+y))z)x" 'System.Data.SyntaxErrorException' 演算子の後にオペランドがありません。
        expression = "(((x+y))*z)*x" ' 演算子を明示的に追加
        expression = "(x+y)/z"

        ' 変数を置換
        Dim expressionB
        'expressionB = expression.Replace("x", "2").Replace("y", "3").Replace("z", "*4")
        expressionB = expression.Replace("x", "2.22").Replace("y", "3.33").Replace("z", "4.44")

        Dim dt As New DataTable()
        Dim result As Double = Convert.ToDouble(dt.Compute(expressionB, String.Empty))

        Output("計算結果: " & result)
    End Sub

End Module
