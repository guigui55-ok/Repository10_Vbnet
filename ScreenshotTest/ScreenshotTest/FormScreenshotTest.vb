Public Class FormScreenshotTest

    Dim screenshotHelper As ScreenshotHelper

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Debug.WriteLine("Execute SaveScreenshot")
        screenshotHelper = New ScreenshotHelper()
        Dim path = ScreenshotHelper.GenerateScreenshotFilePath()
        ScreenshotHelper.SaveScreenshot(Me, path)
        Debug.WriteLine("savePath = " + path)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Debug.WriteLine("Execute SaveFullScreenScreenshot")
        screenshotHelper = New ScreenshotHelper()
        Dim path = ScreenshotHelper.GenerateScreenshotFilePath()
        ScreenshotHelper.SaveFullScreenScreenshot(path)
        Debug.WriteLine("savePath = " + path)
    End Sub
End Class
