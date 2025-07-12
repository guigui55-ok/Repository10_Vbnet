Imports System.Drawing

Module Module1

    ' NuGetで 以下をインストール
    'Emgu.CV
    'Emgu.CV.runtime.windows
    'Emgu.CV.Bitmap

    Dim arch = "
 クラス設計方針
ImageMatcher：画像マッチングを担当するメインクラス

MatchResult：一致結果を保持するクラス

将来的な拡張（類似度の閾値変更、マルチマッチ対応、別アルゴリズム追加）に対応できるよう、柔軟な構造にします。

"
    Sub Main()

        Dim matcher As New ImageMatcher()
        matcher.Threshold = 0.9 ' 任意でしきい値を調整可能
        Dim MessageBox As Object = Nothing

        Dim basePath = "C:\Users\OK\Pictures\250712_testImage\image_2025_07_12_083409_531.png"
        Dim tempPath = "C:\Users\OK\Pictures\250712_testImage\_image_2025_07_12_083409_531_templete.png"

        Dim result = matcher.MatchTemplate(basePath, tempPath)

        If result.IsMatch Then
            Logout($"一致しました！座標: {result.MatchLocation}, 類似度: {result.MatchValue:F3}")
        Else
            Logout("一致しませんでした。")
            Exit Sub
        End If

        'log base
        Dim bufSize = matcher.GetImageSize(basePath)
        Logout($"basePath = {IO.Path.GetFileName(basePath)}  [{bufSize}]")

        'log temp
        bufSize = matcher.GetImageSize(tempPath)
        Logout($"tempPath = {IO.Path.GetFileName(tempPath)}  [{bufSize}]")

        'log loc
        Dim beginLoc = result.MatchLocation
        Logout($"result.MatchLocation = {beginLoc}")

        'log cut size input
        Dim cutSize = New Point(671, 239)
        cutSize = New Point(653, 228)
        Logout($"cutSize = {cutSize}")

        'log endLoc
        Dim endLoc = New Point(cutSize.X + beginLoc.X, cutSize.Y + beginLoc.Y)
        Logout($"rightDownPos = {endLoc}")

        'write path
        Dim wdir = IO.Path.GetDirectoryName(basePath)
        Dim wFilename = IO.Path.GetFileNameWithoutExtension(basePath) + "_cut" + IO.Path.GetExtension(basePath)
        Dim wPath = IO.Path.Combine(wdir, wFilename)
        Logout($"wPath = {wPath}")

        'execute
        matcher.CropAndSaveImage(basePath, beginLoc, endLoc, wPath)

    End Sub

    Private Sub Logout(value As String)
        Debug.WriteLine(value)
    End Sub

End Module
