Public Class FormUiTestTryApp
    Public _logger As AppLogger
    Public _subForm As FormUiTestTrySub

    Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        _logger = New AppLogger()
        Dim logPath = Application.StartupPath + "\" + "Log" + "\" + "Log_" + ".log"
        _logger.SetFilePath(logPath)
        _logger.LogOutPutMode = AppLoggerModules.OutputMode.FILE Or AppLoggerModules.OutputMode.DEBUG_WINDOW

        _subForm = New FormUiTestTrySub()
        _subForm._logger = _logger

        _logger.PrintInfo("New")
    End Sub

    Private Sub FormUiTestTryApp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _logger.PrintInfo("FormUiTestTryApp_Load")

    End Sub

    Private Sub Button_SubForm_Click(sender As Object, e As EventArgs) Handles Button_SubForm.Click
        _logger.PrintInfo("Button_SubForm_Click")
        _subForm.Visible = True
    End Sub

    Private Sub Button_Execute_Click(sender As Object, e As EventArgs) Handles Button_Execute.Click
        _logger.PrintInfo("Button_Execute_Click")

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown

        If e.KeyCode = Keys.Enter Then
            _logger.PrintInfo($"'{TextBox1.Text}' ENTER")
        End If
    End Sub
End Class
