
Imports TimerT = System.Threading.Timer
Imports Timer = System.Windows.Forms.Timer

Module ModuleGetDataSampleAsync
    Dim bufTimer As Timer
    Dim gMainForm As FormDataGridViewAuto
    Dim bridgeValue As String
    Public Sub GetDataSampleAsync(argDataGridView As DataGridView, mainForm As FormDataGridViewAuto)
        gMainForm = mainForm
        '繰り返し
        bridgeValue = mainForm.GetRandomDoubleStr()
        Dim interval As Integer = 2000
        interval = 500
        'StartTimer(2000)
        'タスクを作成してタスクを起動
        Dim task As Task = Task.Run(
            Sub()
                '処理
                Console.WriteLine("TaskRun")
                System.Threading.Thread.Sleep(interval)
                gMainForm.m_recieveValue = bridgeValue
            End Sub
        )

        mainForm.m_recieveValue = ""
        '待ち受け
        Dim timeout = 5000
        Dim beginTime As Date = DateTime.Now()
        While True
            If mainForm.m_recieveValue <> "" Then
                Console.WriteLine(String.Format("Recieved [{0}]", mainForm.m_recieveValue))
                Exit While
            End If
            If timeout < (DateTime.Now() - beginTime).TotalMilliseconds Then
                Console.WriteLine("### TIME_OUT")
                Exit Sub
                Exit While
            End If
            Threading.Thread.Sleep(100)
        End While


    End Sub

    Public Sub UpdateValueInTable(argDataGridView As DataGridView, mainForm As FormDataGridViewAuto, indexNum As Integer, colNum As Integer)
        gMainForm = mainForm
        '//
        '値取得済みであること
        '//
        Dim TargetHeaderText As String = "Value" & colNum.ToString()
        Dim endFlag As Boolean = False
        Dim cols = argDataGridView.Columns
        For Each row As DataGridViewRow In argDataGridView.Rows
            If row.Cells("No").Value <> indexNum.ToString() Then
                Continue For
            End If
            For Each col As DataGridViewColumn In argDataGridView.Columns
                Dim bufVal = row.Cells(col.HeaderText).Value
                If col.HeaderText.Contains(TargetHeaderText) Then
                    row.Cells(col.HeaderText).Value = gMainForm.m_recieveValue
                    endFlag = True
                End If
                If endFlag Then Exit For
            Next
            If endFlag Then Exit For
        Next
    End Sub



    Private Sub StartTimer(interval As Integer)
        If bufTimer IsNot Nothing Then
            If bufTimer.Enabled Then
                Exit Sub
            End If
            bufTimer.Stop()
            bufTimer.Dispose()
            RemoveHandler bufTimer.Tick, AddressOf TimerTick
            bufTimer = Nothing
        End If
        bufTimer = New Timer()
        bufTimer.Interval = interval
        AddHandler bufTimer.Tick, AddressOf TimerTick
        bufTimer.Start()
    End Sub

    Private Sub TimerTick(sender As Object, e As EventArgs)
        Console.WriteLine("TimeTick")
        gMainForm.m_recieveValue = bridgeValue
        bufTimer.Stop()
    End Sub

End Module
