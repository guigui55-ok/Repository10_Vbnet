Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Public Class ImageRelation
    ''' <summary>
    ''' Image オブジェクトを指定ファイルに保存します。
    ''' </summary>
    ''' <param name="image">保存する Image オブジェクト</param>
    ''' <param name="filePath">保存先のファイルパス</param>
    ''' <returns>成功した場合 True、失敗した場合 False</returns>
    Public Shared Function SaveImageToFile(image As Image, filePath As String, ByRef outEx As Exception) As Boolean
        outEx = Nothing
        Try
            ' 拡張子を取得（小文字）
            Dim ext As String = Path.GetExtension(filePath).ToLower()

            ' 画像フォーマットを決定
            Dim format As ImageFormat = ImageFormat.Png ' デフォルトはPNG
            Select Case ext
                Case ".bmp"
                    format = ImageFormat.Bmp
                Case ".jpg", ".jpeg"
                    format = ImageFormat.Jpeg
                Case ".gif"
                    format = ImageFormat.Gif
                Case ".tiff", ".tif"
                    format = ImageFormat.Tiff
                Case ".png"
                    format = ImageFormat.Png
                Case Else
                    ' 未知の拡張子はPNGとして保存
                    format = ImageFormat.Png
            End Select

            ' 保存
            image.Save(filePath, format)
            Return True
        Catch ex As Exception
            outEx = ex
            ' 必要に応じてログ出力やエラーメッセージを追加
            Debug.WriteLine("画像保存エラー: " & ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function AreImagesEqual(img1 As Image, img2 As Image) As Boolean
        If img1 Is Nothing OrElse img2 Is Nothing Then
            Return False
        End If

        If img1.Size <> img2.Size OrElse img1.PixelFormat <> img2.PixelFormat Then
            Return False
        End If

        Using ms1 As New MemoryStream(), ms2 As New MemoryStream()
            img1.Save(ms1, Imaging.ImageFormat.Png)
            img2.Save(ms2, Imaging.ImageFormat.Png)

            Dim bytes1() As Byte = ms1.ToArray()
            Dim bytes2() As Byte = ms2.ToArray()

            If bytes1.Length <> bytes2.Length Then
                Return False
            End If

            For i As Integer = 0 To bytes1.Length - 1
                If bytes1(i) <> bytes2(i) Then
                    Return False
                End If
            Next
        End Using

        Return True
    End Function
End Class
