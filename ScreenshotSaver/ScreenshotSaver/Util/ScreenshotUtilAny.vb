Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms

Public Class ScreenshotUtilAny

    Public Shared Sub SaveCaptureFullScreenPng(filePath As String)
        ' 例：スクリーンショットをファイルとして保存する
        Dim screenshot As Bitmap = CaptureFullScreen()
        screenshot.Save(filePath, ImageFormat.Png)
        screenshot.Dispose()
    End Sub

    ''' <summary>
    ''' 画面全体のスクリーンショットを取得する関数
    ''' </summary>
    ''' <returns>Bitmap 形式のスクリーンショット</returns>
    Public Shared Function CaptureFullScreen() As Bitmap
        ' すべてのスクリーンのサイズを結合して取得
        Dim screenBounds As Rectangle = Rectangle.Empty
        For Each screen As Screen In Screen.AllScreens
            screenBounds = Rectangle.Union(screenBounds, screen.Bounds)
        Next

        ' スクリーンサイズのBitmap作成
        Dim bmp As New Bitmap(screenBounds.Width, screenBounds.Height)

        ' グラフィックスオブジェクトに描画
        Using g As Graphics = Graphics.FromImage(bmp)
            g.CopyFromScreen(screenBounds.Left, screenBounds.Top, 0, 0, bmp.Size)
        End Using

        Return bmp
    End Function
End Class
