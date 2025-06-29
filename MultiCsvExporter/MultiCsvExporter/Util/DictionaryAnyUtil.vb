Public Class DictionaryAnyUtil

    ''' <summary>
    ''' Dictionaryを1つの文字列に繋げる
    ''' </summary>
    ''' <param name="dict"></param>
    ''' <param name="delimiter"></param>
    ''' <returns></returns>
    Public Shared Function ConcatDictionaryToString(dict As Dictionary(Of String, Object), Optional delimiter As String = ", ") As String
        Dim _list = New List(Of String)
        For Each key In dict.Keys
            Dim _val = dict(key)
            If _val Is Nothing Or IsDBNull(_val) Then
                _val = ""
            End If
            _list.Add($"{key}:{_val}")
        Next
        Dim ret = String.Join(delimiter, _list)
        Return ret
    End Function
End Class
