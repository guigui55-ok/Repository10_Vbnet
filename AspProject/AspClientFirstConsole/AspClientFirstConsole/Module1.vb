Module Module1

    Sub Main()

        Dim client As ServiceReference1.CalcClient = New ServiceReference1.CalcClient()
        ' 'client' 変数を使用して、このサービスで操作を呼び出してください。
        Dim ret = client.Add(1, 2)
        Output("ret =" + ret.ToString())
        ' 常にクライアントを閉じてください。
        client.Close()
        Output("Done.")
        Console.ReadKey()
    End Sub

    Public Sub Output(value As Object)
        Dim buf As String = String.Format("{0}", value)
        Console.WriteLine(buf)
        Debug.WriteLine(buf)
    End Sub
End Module
