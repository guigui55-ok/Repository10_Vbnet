Imports System.IO
Imports System.Reflection

Public Module CommonGeneralModule


    Private Function GetApplicationDirectory()
        Dim myAssembly As Assembly = Assembly.GetEntryAssembly()
        Dim appFilePath As String = myAssembly.Location
        Dim appDirPath = GetParentPath(appFilePath)
        Return appDirPath
    End Function


    Function GetParentPath(filePath As String) As String
        Dim parentPath As String
        If File.Exists(filePath) Then
            'FIle
            parentPath = Path.GetDirectoryName(filePath)
        Else
            'Directory
            parentPath = System.IO.Path.GetDirectoryName(filePath.TrimEnd(System.IO.Path.DirectorySeparatorChar))
            parentPath = System.IO.Path.GetDirectoryName(parentPath)
        End If
        Return parentPath
    End Function


    Public Function GetMatchString(pattern As String, value As String) As String
        Dim retList As List(Of String) = GetMatchedArrayByRegix(pattern, value)
        If 0 < retList.Count Then
            Return retList(0)
        Else
            Return ""
        End If
    End Function

    Public Function GetMatchedArrayByRegix(pattern As String, value As String) As List(Of String)
        '正規表現パターンとオプションを指定してRegexオブジェクトを作成 
        Dim r As New System.Text.RegularExpressions.Regex(
            pattern,
            System.Text.RegularExpressions.RegexOptions.IgnoreCase Or
            System.Text.RegularExpressions.RegexOptions.Singleline)
        'TextBox1.Text内で正規表現と一致する対象をすべて検索 
        Dim mc As System.Text.RegularExpressions.MatchCollection =
            r.Matches(value)
        ' マッチした部分を格納するリスト
        Dim matchedValues As New List(Of String)

        ' mcの各要素を処理
        For Each m As System.Text.RegularExpressions.Match In mc
            ' マッチした全体の文字列を取得
            Dim matchedString As String = m.ToString()

            ' カンマ区切りで分割してリストに追加
            Dim parts As String() = matchedString.Split(New String() {""", """}, StringSplitOptions.None)
            matchedValues.AddRange(parts)
        Next
        ' リストを文字列配列に変換
        Dim matchedArray As String() = matchedValues.ToArray()

        ' 配列の内容を出力
        'matchedArray = StripArrayString(matchedArray, {""""})
        Dim printStr = String.Join(", ", matchedArray)
        Console.WriteLine("Matched value: " & printStr)
        'For Each value As String In matchedArray
        '    _logger.PrintInfo("Matched value: " & value)
        'Next

        ' リストを配列に変換して返す
        Return matchedValues
    End Function

    Public Function ConvertDataTableToListString(dataTable As DataTable)
        Dim rowList As List(Of List(Of String)) = New List(Of List(Of String))()
        Dim colList As List(Of String)

        colList = New List(Of String)()
        For Each col In dataTable.Columns
            colList.Add(col.ToString())
        Next
        rowList.Add(colList)

        For Each row As DataRow In dataTable.Rows
            colList = New List(Of String)()
            For Each cell In row.ItemArray
                colList.Add(cell.ToString())
            Next
            rowList.Add(colList)
        Next
        Return rowList
    End Function
End Module
