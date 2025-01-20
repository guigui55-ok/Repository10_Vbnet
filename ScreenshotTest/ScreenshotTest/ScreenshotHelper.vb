Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Public Class ScreenshotHelper
    ' スクリーンショットを取得しファイルに保存する関数
    Public Shared Sub SaveScreenshot(form As Form, savePath As String)
        Try
            ' フォームの境界を含む矩形を取得
            Dim bounds As Rectangle = form.Bounds

            ' Bitmapを作成してフォームのサイズ分確保
            Using screenshot As New Bitmap(bounds.Width, bounds.Height)
                ' グラフィックスオブジェクトを作成
                Using g As Graphics = Graphics.FromImage(screenshot)
                    ' フォームのスクリーンショットを描画
                    g.CopyFromScreen(form.Location, Point.Empty, bounds.Size)
                End Using

                ' スクリーンショットをファイルに保存
                screenshot.Save(savePath, ImageFormat.Png)

                'MessageBox.Show($"スクリーンショットを保存しました: {savePath}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Debug.WriteLine($"スクリーンショットを保存しました: {savePath}")
            End Using
        Catch ex As Exception
            Debug.WriteLine("Error")
            Debug.WriteLine(ex.GetType().ToString() + ":" + ex.Message)
            Debug.WriteLine(ex.StackTrace)
            'MessageBox.Show($"スクリーンショットの保存中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' スクリーンショットのファイルパスを作成する関数
    Public Shared Function GenerateScreenshotFilePath() As String
        ' 実行ファイルのディレクトリを取得
        Dim exeDirectory As String = AppDomain.CurrentDomain.BaseDirectory

        ' 保存先のディレクトリパスを作成
        Dim imageDirectory As String = Path.Combine(exeDirectory, "image")

        ' 保存先のディレクトリが存在しない場合は作成
        If Not Directory.Exists(imageDirectory) Then
            Directory.CreateDirectory(imageDirectory)
        End If

        ' 現在時刻をフォーマットしてファイル名を作成
        Dim timestamp As String = DateTime.Now.ToString("yyyyMMdd_HHmmss")
        Dim fileName As String = $"screenshot_{timestamp}.png"

        ' 保存先のフルパスを作成
        Dim fullPath As String = Path.Combine(imageDirectory, fileName)

        Return fullPath
    End Function


    ' 画面全体のスクリーンショットを保存する関数
    Public Shared Sub SaveFullScreenScreenshot(savePath As String)
        Try
            ' プライマリスクリーンのサイズを取得
            Dim screenBounds As Rectangle = Screen.PrimaryScreen.Bounds

            ' Bitmapを作成して画面全体のサイズ分確保
            Using screenshot As New Bitmap(screenBounds.Width, screenBounds.Height)
                ' グラフィックスオブジェクトを作成
                Using g As Graphics = Graphics.FromImage(screenshot)
                    ' 画面全体のスクリーンショットを取得
                    g.CopyFromScreen(Point.Empty, Point.Empty, screenBounds.Size)
                End Using

                ' スクリーンショットをファイルに保存
                screenshot.Save(savePath, ImageFormat.Png)

                'MessageBox.Show($"スクリーンショットを保存しました: {savePath}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Debug.WriteLine($"スクリーンショットを保存しました: {savePath}")
            End Using
        Catch ex As Exception
            Debug.WriteLine("Error")
            Debug.WriteLine(ex.GetType().ToString() + ":" + ex.Message)
            Debug.WriteLine(ex.StackTrace)
            'MessageBox.Show($"スクリーンショットの保存中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
