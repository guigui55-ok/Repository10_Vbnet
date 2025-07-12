Imports Emgu.CV
Imports Emgu.CV.Structure
Imports Emgu.CV.CvEnum
Imports System.Drawing

''' <summary>
''' テンプレート画像が対象画像に含まれるかを判定するクラス
''' </summary>
Public Class ImageMatcher
    Private _threshold As Double = 0.95 ' 類似度のしきい値

    ''' <summary>
    ''' 類似度のしきい値（0.0〜1.0）を設定
    ''' </summary>
    Public Property Threshold As Double
        Get
            Return _threshold
        End Get
        Set(value As Double)
            _threshold = Math.Min(1.0, Math.Max(0.0, value))
        End Set
    End Property

    Public Function MatchTemplate(sourcePath As String, templatePath As String) As MatchResult
        Dim result As New MatchResult()

        Using sourceImage As New Image(Of Bgr, Byte)(sourcePath)
            Using templateImage As New Image(Of Bgr, Byte)(templatePath)

                If templateImage.Width > sourceImage.Width OrElse templateImage.Height > sourceImage.Height Then
                    result.IsMatch = False
                    Return result
                End If

                Dim matchResult As Image(Of Gray, Single) = sourceImage.MatchTemplate(templateImage, TemplateMatchingType.CcoeffNormed)

                ' 配列として準備
                Dim minValArr(0) As Double
                Dim maxValArr(0) As Double
                Dim minLocArr(0) As Point
                Dim maxLocArr(0) As Point

                ' 最小/最大値と座標を取得
                matchResult.MinMax(minValArr, maxValArr, minLocArr, maxLocArr)

                result.MatchValue = maxValArr(0)
                result.IsMatch = maxValArr(0) >= _threshold
                result.MatchLocation = maxLocArr(0)
            End Using
        End Using

        Return result
    End Function

    ''' <summary>
    ''' 指定した矩形（左上と右下の座標）で画像を切り取る
    ''' </summary>
    ''' <param name="imagePath">元画像のパス</param>
    ''' <param name="topLeft">左上の座標</param>
    ''' <param name="bottomRight">右下の座標</param>
    ''' <returns>切り取った部分の Image(Of Bgr, Byte) オブジェクト</returns>
    Public Function CropImage(imagePath As String, topLeft As Point, bottomRight As Point) As Image(Of Bgr, Byte)
        ' 画像読み込み
        Using original As New Image(Of Bgr, Byte)(imagePath)
            ' 範囲の幅と高さを計算
            Dim rectWidth As Integer = bottomRight.X - topLeft.X
            Dim rectHeight As Integer = bottomRight.Y - topLeft.Y

            ' 切り取り領域の矩形
            Dim roi As New Rectangle(topLeft.X, topLeft.Y, rectWidth, rectHeight)

            ' 領域が画像を超えないように制限
            roi.Intersect(New Rectangle(Point.Empty, original.Size))

            ' 切り取り
            Return original.Copy(roi)

            '※画像サイズが不正なとき（left,topよりright,bottomが小さい）は、以下のエラーとなる
            'Emgu.CV.Util.CvException
        End Using
    End Function

    ''' <summary>
    ''' 指定した矩形（左上と右下の座標）で画像を切り取り、ファイルとして保存する
    ''' </summary>
    ''' <param name="imagePath">元画像のパス</param>
    ''' <param name="topLeft">左上の座標</param>
    ''' <param name="bottomRight">右下の座標</param>
    ''' <param name="outputPath">保存先のファイルパス</param>
    Public Sub CropAndSaveImage(imagePath As String, topLeft As Point, bottomRight As Point, outputPath As String)
        Dim cropped = CropImage(imagePath, topLeft, bottomRight)
        cropped.Save(outputPath)
    End Sub

    ''' <summary>
    ''' 指定した画像ファイルのサイズ（幅と高さ）を取得する
    ''' </summary>
    ''' <param name="imagePath">画像ファイルのパス</param>
    ''' <returns>画像サイズ（Size 構造体）</returns>
    Public Function GetImageSize(imagePath As String) As Size
        Using img As New Image(Of Bgr, Byte)(imagePath)
            Return img.Size
        End Using
    End Function



    Private Sub _UsageSample()
        Dim matcher As New ImageMatcher()
        matcher.Threshold = 0.9 ' 任意でしきい値を調整可能
        Dim MessageBox As Object = Nothing

        Dim result = matcher.MatchTemplate("C:\Images\screen.png", "C:\Images\icon.png")

        If result.IsMatch Then
            MessageBox.Show($"一致しました！座標: {result.MatchLocation}, 類似度: {result.MatchValue:F3}")
        Else
            MessageBox.Show("一致しませんでした。")
        End If
    End Sub

End Class