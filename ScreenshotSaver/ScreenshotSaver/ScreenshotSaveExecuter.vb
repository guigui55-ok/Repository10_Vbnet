Public Class ScreenshotSaveExecuter

    Enum EnumSaveStatus
        NONE = 0
        SAVING
    End Enum

    Dim status As EnumSaveStatus = EnumSaveStatus.NONE
    Public _logger As AppLogger

    Sub New(logger As AppLogger)
        _logger = logger
    End Sub

    Public Function CreateFilePath(fileDirPath As String, fileName As String)
        If fileName = "" Then
            'default filename
            fileName = "image_" + DateTime.Now.ToString("yyyy_MM_dd_HHmmss_fff") + ".png"
        End If
        Dim filePath = IO.Path.Combine(fileDirPath, fileName)
        Return filePath
    End Function

    Public Sub SaveScreenshot(fileDirPath As String, fileName As String)
        Dim filePath = CreateFilePath(fileDirPath, fileName)
        status = EnumSaveStatus.SAVING
        IO.Directory.CreateDirectory(fileDirPath)
        ScreenshotUtilAny.SaveCaptureFullScreenPng(filePath)
        status = EnumSaveStatus.NONE
        _logger.Info($"Image Saved = {filePath}")
    End Sub

End Class
