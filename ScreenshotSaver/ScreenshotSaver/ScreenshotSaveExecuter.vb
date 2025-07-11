Imports System.Drawing.Imaging

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

    Public Sub SaveScreenshot(fileDirPath As String, fileName As String, quality As Integer)
        Dim filePath = CreateFilePath(fileDirPath, fileName)
        status = EnumSaveStatus.SAVING
        IO.Directory.CreateDirectory(fileDirPath)

        'save
        'ScreenshotUtilAny.SaveCaptureFullScreenPng(filePath)
        ' 画面全体をJPEGで保存、サイズ800x600に縮小し、品質85で保存
        ScreenshotUtilAny.SaveCaptureFullScreenWithOptions(filePath, ImageFormat.Jpeg, -1, -1, quality)

        status = EnumSaveStatus.NONE
        Dim sizeByte = ScreenshotUtilAny.GetFileSizeBytes(filePath)
        Dim byteStr = FormatFileSize(sizeByte)
        _logger.Info($"Image Saved = {filePath} [{byteStr}]")
    End Sub


    ''' <summary>
    ''' バイト数を KB/MB/GB/TB 単位の文字列に変換して返す
    ''' </summary>
    ''' <param name="bytes">バイト数</param>
    ''' <returns>例: "1.23 MB"</returns>
    Public Function FormatFileSize(bytes As Long) As String
        Dim unitNames() As String = {"B", "KB", "MB", "GB", "TB"}
        Dim size As Double = bytes
        Dim unitIndex As Integer = 0

        While size >= 1024 AndAlso unitIndex < unitNames.Length - 1
            size /= 1024
            unitIndex += 1
        End While

        Return String.Format("{0:0.##} {1}", size, unitNames(unitIndex))
    End Function
End Class
