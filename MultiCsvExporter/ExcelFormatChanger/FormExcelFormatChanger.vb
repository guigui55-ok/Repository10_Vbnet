Public Class FormExcelFormatChanger

    Public _logger As AppLogger
    Public _formLog As FormLog
    Public _mainProc As ExcelFormatChangerProc

    Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub

    Sub New(ByRef logger As AppLogger)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        _logger = logger

    End Sub

    Private Sub FormExcelFormatChanger_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
