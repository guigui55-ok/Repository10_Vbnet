Imports System.Threading

Public Class FormDisplayProgress

    Private progressA As ProgressDisplay
    Private progressB As ProgressDisplay

    Private _progressBarForm As ProgressBarForm

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        _progressBarForm = New ProgressBarForm()
        _progressBarForm.Initialize(
            Me, Label_Progress2A, Label_Progress2B, Button_Start2, Button_Stop2, ProgressBar1, ProgressBar2)

    End Sub

    Private Sub FormDisplayProgress_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Label1 を進捗表示対象に設定
        progressA = New ProgressDisplay()
        'progressA.Initialize(Label_ProgressA, 500, Sub(text) Label_ProgressA.Text = text)
        progressA.initText = "停止中A"
        progressB = New ProgressDisplay()
        'progressB.Initialize(Label_ProgressB, 600, Sub(text) Label_ProgressB.Text = text)
        progressB.initText = "停止中B"

        ' Initialize メソッドで displayAction を設定する際に、InvokeRequired を考慮
        progressA.Initialize(Label_ProgressA, 500, Sub(text) UpdateControlTextSafely(Label_ProgressA, text))
        progressB.Initialize(Label_ProgressB, 600, Sub(text) UpdateControlTextSafely(Label_ProgressB, text))

    End Sub

    ' UpdateControlTextSafely を追加
    Private Sub UpdateControlTextSafely(control As Control, newText As String)
        If control.InvokeRequired Then
            control.Invoke(New Action(Sub() control.Text = newText))
        Else
            control.Text = newText
        End If
    End Sub

    Public Sub LogOutputError(ex As Exception)
        LogOutput("##### Error #####")
        LogOutput(ex.GetType().ToString() + ":" + ex.Message)
        LogOutput(ex.StackTrace)
    End Sub

    Private Sub ButtonA_Start_Click(sender As Object, e As EventArgs) Handles ButtonA_Start.Click
        Try
            If Not progressA.IsRunning() Then
                LogOutput("progressA.Start")
                progressA.Start()
            Else
                If Not progressB.IsRunning() Then
                    LogOutput("progressB.Start")
                    progressB.Start()
                End If
            End If
        Catch ex As Exception
            LogOutputError(ex)
        End Try
    End Sub

    Private Sub ButtonA_Stop_Click(sender As Object, e As EventArgs) Handles ButtonA_Stop.Click

        Try

            If progressB.IsRunning() Then
                LogOutput("progressB.Stop")
                progressB.Stop()
                Exit Sub
            Else
                LogOutput("progressA.Stop")
                progressA.Stop()
            End If
        Catch ex As AggregateException
            For Each innerEx In ex.InnerExceptions
                LogOutput($"内部例外: {innerEx.GetType()}: {innerEx.Message}")
            Next
        Catch ex As Exception
            LogOutput($"その他の例外: {ex.GetType()}: {ex.Message}")
        End Try
    End Sub

    Public Sub StopAll()
        LogOutput("StopAll")
        ' 進行中の非同期処理をすべて停止
        Try
            progressA?.[Stop]()
            LogOutput("progressA Stopped")
        Catch ex As Exception
            Debug.WriteLine($"StopAll中のエラー[A]: {ex.Message}")
        End Try
        Try
            progressB?.[Stop]()
            LogOutput("progressA Stopped")
        Catch ex As Exception
            Debug.WriteLine($"StopAll中のエラー[B]: {ex.Message}")
        End Try
    End Sub

    Private Sub LogOutput(value)
        Dim buf = String.Format("{0}", value)
        Debug.WriteLine(buf)
    End Sub

    Private Sub FormDisplayProgress_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        StopAll()
    End Sub
End Class
