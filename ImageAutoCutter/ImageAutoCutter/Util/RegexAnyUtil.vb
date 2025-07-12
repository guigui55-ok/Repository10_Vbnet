Imports System.Text.RegularExpressions

Public Class RegexAnyUtil

    ' 単一パターンマッチ判定
    Public Shared Function IsMatchSingle(input As String, pattern As String, Optional options As RegexOptions = RegexOptions.None) As Boolean
        If String.IsNullOrEmpty(input) OrElse String.IsNullOrEmpty(pattern) Then Return False
        Return Regex.IsMatch(input, pattern, options)
    End Function

    ' 複数パターンマッチ判定
    Public Shared Function IsMatchMultiple(input As String, patterns As IEnumerable(Of String), Optional options As RegexOptions = RegexOptions.None) As Boolean
        If String.IsNullOrEmpty(input) OrElse patterns Is Nothing Then Return False
        For Each pattern In patterns
            If Not String.IsNullOrEmpty(pattern) AndAlso Regex.IsMatch(input, pattern, options) Then
                Return True
            End If
        Next
        Return False
    End Function

    ' 全マッチ文字列取得
    Public Shared Function GetMatchedStrings(input As String, pattern As String, Optional options As RegexOptions = RegexOptions.None) As List(Of String)
        Dim result As New List(Of String)()
        If String.IsNullOrEmpty(input) OrElse String.IsNullOrEmpty(pattern) Then Return result

        Dim matches = Regex.Matches(input, pattern, options)
        For Each match As Match In matches
            result.Add(match.Value)
        Next
        Return result
    End Function

    ' 最初のマッチのみ取得
    Public Shared Function GetFirstMatchedString(input As String, pattern As String, Optional options As RegexOptions = RegexOptions.None) As String
        If String.IsNullOrEmpty(input) OrElse String.IsNullOrEmpty(pattern) Then Return String.Empty
        Dim match = Regex.Match(input, pattern, options)
        Return If(match.Success, match.Value, String.Empty)
    End Function

    ' グループキャプチャ（すべてのマッチの各グループをリスト化）
    Public Shared Function GetAllCapturedGroups(input As String, pattern As String, Optional options As RegexOptions = RegexOptions.None) As List(Of List(Of String))
        Dim result As New List(Of List(Of String))()
        If String.IsNullOrEmpty(input) OrElse String.IsNullOrEmpty(pattern) Then Return result

        Dim matches = Regex.Matches(input, pattern, options)
        For Each match As Match In matches
            Dim groupValues As New List(Of String)
            ' Group(0) は全体、Group(1) 以降がキャプチャ
            For i As Integer = 1 To match.Groups.Count - 1
                groupValues.Add(match.Groups(i).Value)
            Next
            result.Add(groupValues)
        Next
        Return result
    End Function


    Private Sub UsageSample()
        Dim text = "Name: John, Age: 30 / Name: Alice, Age: 25"
        Dim pattern = "Name:\s*(\w+),\s*Age:\s*(\d+)"

        ' 単一マッチ判定
        Console.WriteLine(RegexAnyUtil.IsMatchSingle(text, pattern)) ' True

        ' 複数マッチ文字列取得
        Dim allMatches = RegexAnyUtil.GetMatchedStrings(text, pattern)
        Console.WriteLine(String.Join(" | ", allMatches)) ' "Name: John, Age: 30 | Name: Alice, Age: 25"

        ' 最初の1件だけ取得
        Dim first = RegexAnyUtil.GetFirstMatchedString(text, pattern)
        Console.WriteLine(first) ' "Name: John, Age: 30"

        ' グループキャプチャ取得
        Dim captured = RegexAnyUtil.GetAllCapturedGroups(text, pattern)
        For Each groupSet In captured
            Console.WriteLine(String.Join(", ", groupSet)) ' "John, 30" と "Alice, 25"
        Next
    End Sub
End Class
