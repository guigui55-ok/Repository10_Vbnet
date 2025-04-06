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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox_ProcessName.Text = "" Then
            Exit Sub
        End If
        Dim left As Integer
        Dim top As Integer
        Dim width As Integer
        Dim height As Integer
        Dim processName As String = TextBox_ProcessName.Text
        If GetWindowSizeAndPosition(processName, left, top, width, height) Then
            Debug.WriteLine($"ウィンドウ位置: ({left}, {top})")
            Debug.WriteLine($"ウィンドウサイズ: {width} x {height}")
        Else
            Debug.WriteLine("ウィンドウ情報の取得に失敗しました。")
            Exit Sub
        End If
        screenshotHelper = New ScreenshotHelper()
        Dim path = ScreenshotHelper.GenerateScreenshotFilePath()
        ScreenshotHelper.SaveScreenshotByRect(path, left, top, width, height)
        Debug.WriteLine("savePath = " + path)
    End Sub
End Class
