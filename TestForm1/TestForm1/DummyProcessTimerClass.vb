Public Class DummyProcessTimerClass
    Shared Sub DummyMain()
        Dim ftt As DummyProcessTimerClass = New DummyProcessTimerClass()
        ftt.Run()
    End Sub

    Public Sub Run()
        Dim timer As Timer = New Timer()
        AddHandler timer.Tick, New EventHandler(AddressOf MyClock)
        timer.Interval = 1000
        timer.Enabled = True ' timer.Start()と同じ

        Application.Run() ' メッセージループを開始
    End Sub

    Public Sub MyClock(sender As Object, e As EventArgs)
        Console.WriteLine(DateTime.Now)
        ' 出力例：
        ' 2005/11/08 19:59:10
        ' 2005/11/08 19:59:11
        ' 2005/11/08 19:59:12
        ' ……
    End Sub
End Class
