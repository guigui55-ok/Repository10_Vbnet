

Public Class Renamer


    ''' <summary>
    ''' 同じファイルパスが存在した場合、ファイルの後ろに番号を付与する
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="prefixPattern"></param>
    ''' <param name="prefixFormat"></param>
    ''' <param name="count"></param>
    ''' <returns></returns>
    Public Shared Function AddNumberIfExists(
            filePath As String,
            Optional prefixPattern As String = "_\d{1,2}",
            Optional prefixFormat As String = "_\d",
            Optional count As Integer = 0) As String

        '// note
        '数字を含んだパターンにしないこと ex) "_123_\d{1,2}"
        'formatの数字を入れたい部分は"\d"にすること

        If count > 1000 Then Throw New OverflowException($"count > 1000")
        If Not IO.File.Exists(filePath) Then Return filePath
        If IO.Directory.Exists(filePath) Then Throw New DuplicateNameException(filePath) 'ファイルを扱いたいが、すでに同じパスでディレクトリが存在している

        Dim filename = IO.Path.GetFileName(filePath)
        Dim filenameNoExt = IO.Path.GetFileNameWithoutExtension(filePath)
        Dim prefixStr = GetMatchStr(filenameNoExt, prefixPattern)
        Dim newNum = 0
        If prefixStr = "" Then
            '番号がない（初めてのケース）
            newNum = 1
        Else
            newNum = ExtractNumber(prefixStr) + 1
            filenameNoExt = filenameNoExt.Replace(prefixStr, "")
        End If
        Dim ext = IO.Path.GetExtension(filePath)
        Dim newFilename = filenameNoExt + prefixFormat.Replace("\d", newNum.ToString()) + ext
        Dim newPath = IO.Path.Combine(IO.Path.GetDirectoryName(filePath), newFilename)
        Dim retPath = AddNumberIfExists(newPath, prefixPattern, prefixFormat, count)
        Return retPath
    End Function

    Private Shared Function GetMatchStr(input As String, pattern As String) As String
        Dim matches As System.Text.RegularExpressions.MatchCollection
        matches = System.Text.RegularExpressions.Regex.Matches(input, pattern, System.Text.RegularExpressions.RegexOptions.None)
        Dim matchStr = ""
        For Each match As System.Text.RegularExpressions.Match In matches
            matchStr = match.Value.ToString
            Exit For
        Next
        Return matchStr
    End Function

    ''' <summary>
    ''' 文字列から最初に現れる数値を抽出する
    ''' </summary>
    ''' <param name="input">入力文字列</param>
    ''' <returns>抽出された数値（整数）</returns>
    Public Shared Function ExtractNumber(input As String) As Integer
        Dim match As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(input, "\d+")
        If match.Success Then
            Return Integer.Parse(match.Value)
        Else
            Return -1 ' 数値が見つからない場合のデフォルト値（必要に応じて変更可）
        End If
    End Function
End Class
