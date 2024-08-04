Imports TestForm1.ConstGeneral.ConstGeneralModule

Public Class Form1
    Dim NowStatus As ProcessStatus = New ProcessStatus()
    Dim dummyTimer As Timer = New Timer()
    Dim dummyTimerCount As Integer
    Dim logger As SimpleLogger = New SimpleLogger()
    'Dim userControl1 As UserControl1
    Dim formChild1 As FormChild1
    Dim formChild2 As FormChild2
    Dim nowOpenChildFormName As String
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

        Me.formChild1 = New FormChild1()
        Me.formChild2 = New FormChild2()
        'Me.formChild1.Show()
        'Me.formChild1.Visible = False
        'Me.formChild2.Show()
        'Me.formChild2.Visible = False
        'Me.formChild2.Show()
        AddHandler Me.dummyTimer.Tick, New EventHandler(AddressOf ChangeProcessStatusByTimerInDummy)
        'Me.dummyTimer.Interval = 1000
        Me.dummyTimer.Interval = 1200
        '/
        Me.InitComboBox()
    End Sub

    Private Sub InitComboBox()
        ' テキストボックス部分は編集不可にする
        'Me.ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList

        '' コンボボックスに項目を追加する
        'With Me.ComboBox1
        '    .Items.Add("FromChild1")
        '    .Items.Add("FormChild2")
        '    .Items.Add("ETC")
        'End With
        'Dim strItem() As String = {"北海道", "東北", "関東", "中部", "近畿", "中国", "四国", "九州・沖縄"}
        Dim strItem() As String
        strItem = ConstGeneral.ConstGeneralModule.ExecuteMainModeName.Values.ToArray()

        ' コンボボックスに配列項目を追加する
        Me.ComboBox1.Items.AddRange(strItem)

        AddHandler Me.ComboBox1.SelectionChangeCommitted, New EventHandler(AddressOf ChangedSelectItemInRunMode)
    End Sub

    Private Sub ChangedSelectItemInRunMode()
        Me.logger.PrintInfo("ChangedSelectItemInRunMode")
        ' 選択されているインデックスを取得する
        Dim idx As Integer = Me.ComboBox1.SelectedIndex
        Me.logger.PrintInfo(idx)
        ' 選択されている文字列を取得する
        Dim strItem As String = Me.ComboBox1.SelectedItem
        Me.logger.PrintInfo(strItem)
        '/
        Dim value As String = String.Empty
        'ConstGeneral.ConstGeneralModule.ExecuteMainModeName.TryGetValue(
        '    ConstGeneral.ConstExecuteMainMode.ONE, value)

        Me.CloseAllChildForm(strItem)
        If strItem = ConstGeneral.ExecuteMainModeName(ConstGeneral.ConstExecuteMainMode.ONE) Then
            Me.formChild1.Visible = True
        ElseIf strItem = ConstGeneral.ExecuteMainModeName(ConstGeneral.ConstExecuteMainMode.TWO) Then
            Me.formChild2.Visible = True
        End If
        '/
        '他の子ウィンドウを閉じて、選択されたものを開く
    End Sub

    Private Sub CloseAllChildForm(ignoreFormName As String)
        If Me.formChild1.Visible Then
            If ignoreFormName <> ConstGeneral.ExecuteMainModeName(ConstGeneral.ConstExecuteMainMode.ONE) Then
                Me.formChild1.Visible = False
            End If
        ElseIf Me.formChild2.Visible Then
            If ignoreFormName <> ConstGeneral.ExecuteMainModeName(ConstGeneral.ConstExecuteMainMode.TWO) Then
                Me.formChild2.Visible = False
            End If
        End If
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
        'Me.formChild1.Show()
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

    Private Sub GroupBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles GroupBox1.MouseDoubleClick
        Me.formChild1.Visible = True
    End Sub

    Private Sub ComboBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox1.KeyDown
        Me.logger.PrintInfo("ComboBox1_KeyPress")
        Me.logger.PrintInfo(CStr(e.KeyCode))
        'Me.logger.PrintInfo(CStr(e.KeyData))
        'Me.logger.PrintInfo(CStr(e.KeyValue))
        Dim formName As String
        If (e.KeyCode = Keys.D1) Or (e.KeyCode = Keys.NumPad1) Then
            Me.formChild1.Visible = True
            formName = ConstGeneral.ExecuteMainModeName(ConstGeneral.ConstExecuteMainMode.ONE)
        ElseIf (e.KeyCode = Keys.D2) Or (e.KeyCode = Keys.NumPad2) Then
            Me.formChild2.Visible = True
            formName = ConstGeneral.ExecuteMainModeName(ConstGeneral.ConstExecuteMainMode.TWO)
        Else
            formName = ""
        End If
        ComboBox1.SelectedItem = formName
    End Sub

    'Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
    '    Me.logger.PrintInfo("ComboBox1_KeyPress")
    'End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        '数字キーでコンボボックスを選択した場合
        'コンボボックスの選択アイテムを変更した後に、入力キーの文字列が残ってしまうため
        ' KeyPressイベントでキー入力をキャンセルする
        e.Handled = True
    End Sub
End Class
