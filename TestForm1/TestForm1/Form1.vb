Imports TestForm1.ConstGeneral.ConstGeneralModule

Public Class Form1
    Dim NowStatus As ProcessStatus = New ProcessStatus()
    Dim dummyTimer As Timer = New Timer()
    Dim dummyTimerCount As Integer
    Dim logger As SimpleLogger = New SimpleLogger()
    Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.logger.PrintInfo("Form1.New")
        'Console.WriteLine("NowStatus = " & Str(Me.NowStatus))
        'System.InvalidCastException: 'Argument 'Number' cannot be converted to a numeric value.'
        'Console.WriteLine("NowStatus = " & CStr(Me.NowStatus))'BC30311	型 'ProcessStatus' の値を 'String' に変換できません。
        Me.logger.PrintInfo("NowStatus = " & CStr(Me.NowStatus.MainValue))
    End Sub
    'Dim userControl1 As UserControl1
    Dim form2 As Form2
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ExecuteDummy()
        'ウィンドウサイズ固定
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size
        'フォームの最大化ボタンの表示、非表示を切り替える
        'Me.MaximizeBox = Not Me.MaximizeBox
        Me.MaximizeBox = Nothing
        Me.logger.PrintInfo("Form1_Load")
        '/
        Dim Buf As String
        Buf = ConstGeneral.ConstGeneralModule.ProessStatusName(ConstGeneral.ConstProcessStatus.NOT_SET_DATA)
        Me.logger.PrintInfo("NowStatus = " & Buf)
        Me.Label2.Text = Buf
        '/
        'Me.userControl1 = New UserControl1()
        'Me.userControl1.Location = New System.Drawing.Point(10, 10)
        'Me.userControl1.Name = "UserControl1"
        'Me.userControl1.Size = New System.Drawing.Size(100, 200)
        'Me.userControl1.TabIndex = 0
        'Me.userControl1.TabStop = False
        'Me.userControl1.Text = "UserControl1_text"
        'Me.userControl1.Visible = True
        'Me.userControl1.BackColor = Color.White
        'Me.ResumeLayout(False)

        Me.form2 = New Form2()
        AddHandler Me.dummyTimer.Tick, New EventHandler(AddressOf ChangeProcessStatusByTimerInDummy)
        'Me.dummyTimer.Interval = 1000
        Me.dummyTimer.Interval = 1200
    End Sub

    Private Sub ChangeProcessStatusByTimerInDummy()
        Me.logger.PrintInfo("ChangeProcessStatusByTimerInDummy")
        If Me.NowStatus.MainValue = ConstGeneral.ConstProcessStatus.PROCESSING Then
            If 0 < dummyTimerCount Then
                dummyTimerCount += 1
            Else
                'dummyTimerCount = Me.NowStatus.MainValue
                dummyTimerCount = 1
            End If
        Else
            dummyTimerCount = 0
        End If
        If 3 < dummyTimerCount Then
            Me.logger.PrintInfo("ProcessEnd Dummy")
            Dim StatusValue As Integer
            StatusValue = ConstGeneral.ConstProcessStatus.END_PROCESS
            Me.NowStatus.MainValue = StatusValue
            dummyTimerCount = 0
            Me.dummyTimer.Enabled = False
            Me.ChengeStatus(StatusValue)
        End If
    End Sub

    Private Sub ChengeStatus(StatusValue As Integer)
        ' ConstGeneral.ConstProcessStatus.NOT_SET_DATA
        Me.NowStatus.MainValue = StatusValue
        Dim Buf As String
        Buf = ConstGeneral.ConstGeneralModule.ProessStatusName(StatusValue)
        Me.logger.PrintInfo("NowStatus = " & Buf)
        Me.Label2.Text = Buf
    End Sub

    Private Sub ExecuteDummy()
        'Dim buf As String
        'buf = ConstantsGeneralModule.DaysInYear
        If False Then
            Console.WriteLine(ConstantsGeneralModule.DaysInYear)
        End If
        ConstantsGeneralModule.TestMethod()
        Dim BufCls As TestConstGeneralModuleTestClass = New TestConstGeneralModuleTestClass()
        BufCls.TestB()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Me.UserControl11.Show()
        Me.logger.PrintInfo("Button1_Click")
        'Dim buf As String
        'buf = Me.UserControl11.Size.Width & ":" & Me.UserControl11.Size.Height
        'Me.logger.PrintInfo(buf)
        'buf = Me.UserControl11.Location.X & ":" & Me.UserControl11.Location.Y
        'Me.logger.PrintInfo(buf)
        'Me.UserControl11.Visible = True
        'Me.form2.Show()
        Me.dummyTimer.Enabled = True ' timer.Start()と同じ
        Dim StatusValue As Integer
        StatusValue = ConstGeneral.ConstProcessStatus.PROCESSING
        Me.NowStatus.MainValue = StatusValue
        Dim Buf As String
        Buf = ConstGeneral.ConstGeneralModule.ProessStatusName(StatusValue)
        Me.Label2.Text = Buf
        Me.MainProcess()
    End Sub

    Private Sub MainProcess()


    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub GroupBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles GroupBox1.MouseDoubleClick
        Me.form2.Visible = True
    End Sub

    Private Sub UserControl11_Load(sender As Object, e As EventArgs) Handles UserControl11.Load

    End Sub
End Class
