Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms

Public Class ScreenshotUtilAny

    ''' <summary>
    ''' PNGで画面全体のスクリーンショットを保存（従来の処理）
    ''' </summary>
    Public Shared Sub SaveCaptureFullScreenPng(filePath As String)
        Dim screenshot As Bitmap = CaptureFullScreen()
        screenshot.Save(filePath, ImageFormat.Png)
        screenshot.Dispose()
    End Sub

    ''' <summary>
    ''' 画面全体のスクリーンショットを取得する関数
    ''' </summary>
    Public Shared Function CaptureFullScreen() As Bitmap
        Dim screenBounds As Rectangle = Rectangle.Empty
        For Each screen As Screen In Screen.AllScreens
            screenBounds = Rectangle.Union(screenBounds, screen.Bounds)
        Next

        Dim bmp As New Bitmap(screenBounds.Width, screenBounds.Height)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.CopyFromScreen(screenBounds.Left, screenBounds.Top, 0, 0, bmp.Size)
        End Using

        Return bmp
    End Function

    ''' <summary>
    ''' 画面全体をJPEGまたはPNGで保存（品質・サイズ変更可能）
    ''' </summary>
    ''' <param name="filePath">保存先ファイルパス</param>
    ''' <param name="format">ImageFormat.Jpeg または ImageFormat.Png</param>
    ''' <param name="resizeWidth">リサイズ後の幅（0以下はリサイズなし）</param>
    ''' <param name="resizeHeight">リサイズ後の高さ（0以下はリサイズなし）</param>
    ''' <param name="jpegQuality">JPEG保存時の画質（1～100、他形式では無視）</param>
    Public Shared Sub SaveCaptureFullScreenWithOptions(filePath As String,
                                                       format As ImageFormat,
                                                       Optional resizeWidth As Integer = 0,
                                                       Optional resizeHeight As Integer = 0,
                                                       Optional jpegQuality As Long = 90)
        Using originalBmp As Bitmap = CaptureFullScreen()
            Dim saveBmp As Bitmap = originalBmp

            If resizeWidth > 0 AndAlso resizeHeight > 0 Then
                saveBmp = ResizeImage(originalBmp, resizeWidth, resizeHeight)
            End If

            If format.Equals(ImageFormat.Jpeg) Then
                SaveJpegWithQuality(filePath, saveBmp, jpegQuality)
            Else
                saveBmp.Save(filePath, format)
            End If

            If Not Object.ReferenceEquals(saveBmp, originalBmp) Then
                saveBmp.Dispose()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' 画像を指定サイズにリサイズする
    ''' </summary>
    Private Shared Function ResizeImage(image As Image, width As Integer, height As Integer) As Bitmap
        Dim resized As New Bitmap(width, height)
        Using g As Graphics = Graphics.FromImage(resized)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(image, 0, 0, width, height)
        End Using
        Return resized
    End Function

    Private Shared Sub SaveJpegWithQuality(filePath As String, image As Image, quality As Long)
        Dim encoder As ImageCodecInfo = ImageCodecInfo.GetImageEncoders().FirstOrDefault(Function(c) c.FormatID = ImageFormat.Jpeg.Guid)
        If encoder IsNot Nothing Then
            Dim encParams As New EncoderParameters(1)
            encParams.Param(0) = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality)
            image.Save(filePath, encoder, encParams)
        Else
            image.Save(filePath, ImageFormat.Jpeg)
        End If
    End Sub

    ''' <summary>
    ''' 指定されたファイルのサイズ（バイト単位）を取得する
    ''' </summary>
    ''' <param name="filePath">ファイルのフルパス</param>
    ''' <returns>ファイルサイズ（バイト）</returns>
    Public Shared Function GetFileSizeBytes(filePath As String) As Long
        If System.IO.File.Exists(filePath) Then
            Dim fileInfo As New System.IO.FileInfo(filePath)
            Return fileInfo.Length
        Else
            Throw New System.IO.FileNotFoundException("ファイルが見つかりません: " & filePath)
        End If
    End Function

    Private Sub UsageSample()
        ' 画面全体をJPEGで保存、サイズ800x600に縮小し、品質85で保存
        ScreenshotUtilAny.SaveCaptureFullScreenWithOptions("screenshot.jpg", ImageFormat.Jpeg, 800, 600, 85)

        ' PNG形式でサイズ変更なしで保存
        ScreenshotUtilAny.SaveCaptureFullScreenWithOptions("screenshot.png", ImageFormat.Png)
    End Sub

End Class