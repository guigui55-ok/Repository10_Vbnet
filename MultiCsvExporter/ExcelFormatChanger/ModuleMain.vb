Module ModuleMainExcelFormatChanger

    Sub Main()
        Dim logger As AppLogger = Nothing
        Dim mainProc As ExcelFormatChangerProc
        Try
            logger = New AppLogger()
            Dim formMain = New FormExcelFormatChanger(logger)
            Dim formLog = New FormLog
            formMain._formLog = formLog
            mainProc = New ExcelFormatChangerProc(logger, formMain)
            formMain._mainProc = mainProc

            logger.Info("***** StartApp ExcelFormatChanger *****")
            Application.Run(formMain)
        Catch ex As Exception
            Throw ex
        Finally
            'pass
        End Try
    End Sub
End Module
