Public Class ExcelFormatChangerProc
    Public _logger As AppLogger
    Public _formMain As FormExcelFormatChanger


    Sub New(ByRef logger As AppLogger, formMain As FormExcelFormatChanger)
        _logger = logger
        _formMain = formMain
    End Sub

End Class
