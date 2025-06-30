Module ModuleMainExcelFormatChanger

    Sub Main()
        Dim logger As AppLogger = Nothing
        Dim mainProc As ExcelFormatChangerProc
        Dim dataManager As ChangeFormatDataPairManager = Nothing
        Try
            logger = New AppLogger()
            logger.LogOutPutMode = AppLogger.OutputMode.FILE + AppLogger.OutputMode.CONSOLE
            logger.SetFilePath(logger.GetDefaultLogFilePath())
            logger.Info($"LogFilePath = {logger.FilePath}")

            'xml読み込みをして、dataManagerに格納する（未実装）
            SetTestData(logger, dataManager)

            Dim formMain = New FormExcelFormatChanger(logger)
            Dim formLog = New FormLog
            formMain._formLog = formLog
            mainProc = New ExcelFormatChangerProc(logger, formMain, dataManager)
            formMain._mainProc = mainProc


            logger.Info("***** StartApp ExcelFormatChanger *****")
            Application.Run(formMain)
        Catch ex As Exception
            logger.Info(vbCrLf + "==========" + vbCrLf)
            logger.Err(ex, "MainMethod Error")
            SharedExcelApp.Quit()
            Throw ex
        Finally
            If dataManager IsNot Nothing And logger IsNot Nothing Then
                AddHandler dataManager.LogoutEvent, AddressOf logger.RecieveLogoutEvent
            End If
            SharedExcelApp.Quit()
        End Try
    End Sub

    Public Sub SetTestData(logger As AppLogger, ByRef _ChangeDataManager As ChangeFormatDataPairManager)
        Dim srcFilePath = "C:\Users\OK\source\repos\Repository10_VBnet\MultiCsvExporter\MultiCsvExporter\bin\Debug\Output\FormatSrc.xlsx"
        Dim destFilePath = "C:\Users\OK\source\repos\Repository10_VBnet\MultiCsvExporter\MultiCsvExporter\bin\Debug\Output\Export.xlsx"

        _ChangeDataManager = New ChangeFormatDataPairManager()
        AddHandler _ChangeDataManager.LogoutEvent, AddressOf logger.RecieveLogoutEvent

        _ChangeDataManager._srcFilePath = srcFilePath
        _ChangeDataManager._destFilePath = destFilePath

        Dim _pairData = New ChangeFormatDataPairManager.DataPair()
        Dim filSrc = New ChangeFormatDataPairManager.ChangeFormatData()
        filSrc.FindSheetName = "" '無いときはシートインデックス1
        filSrc.FindRangeString = "A:A"
        filSrc.FindValue = "■TableA"
        filSrc.FindMode = ChangeFormatDataPairManager.ConstfilterMode.CONTAINS
        filSrc.EntireRow = True
        'filSrc.TargetCountRow '読み取りfileから動的に設定

        Dim fildest = New ChangeFormatDataPairManager.ChangeFormatData()
        fildest.FindSheetName = "" '無いときはシートインデックス1
        fildest.FindRangeString = "A:A"
        fildest.FindValue = "■TableA"
        fildest.FindMode = ChangeFormatDataPairManager.ConstfilterMode.CONTAINS
        fildest.EntireRow = True

        _pairData.ResetValue(filSrc, fildest)

        _ChangeDataManager._dataPairList.Add(_pairData)

    End Sub
End Module
