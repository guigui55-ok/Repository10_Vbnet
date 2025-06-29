Module ModuleMain
    Sub Main()
        Dim logger As AppLogger = Nothing
        Dim filterCondition As FilterCondition = Nothing
        Try
            logger = New AppLogger()
            filterCondition = New FilterCondition(New List(Of String) From {}, New List(Of String) From {}, True, False)
            AddHandler filterCondition.LogoutEvent, AddressOf logger.RecieveLogoutEvent
            Dim formMain = New FormMultiCsvExporter(logger, filterCondition)
            Dim formLog = New FormLog
            formMain._formLog = formLog

            logger.Info("***** StartApp MultiCsvExporter *****")
            Application.Run(formMain)
        Catch ex As Exception
            Throw ex
        Finally
            If filterCondition IsNot Nothing And logger IsNot Nothing Then
                RemoveHandler filterCondition.LogoutEvent, AddressOf logger.RecieveLogoutEvent
            End If
        End Try
    End Sub
End Module
