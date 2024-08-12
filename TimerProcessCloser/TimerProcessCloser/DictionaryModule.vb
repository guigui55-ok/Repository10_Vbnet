Module DictionaryModule

    ''' <summary>
    ''' Dictionary を文字列（JSON形式、改行インデント無し）に変換する
    ''' </summary>
    ''' <param name="dict"></param>
    ''' <returns></returns>
    Function ConvertDictionaryToString(ByVal dict As Dictionary(Of String, Object)) As String
        Dim result As New System.Text.StringBuilder()
        result.Append("{")
        For Each kvp In dict
            result.AppendFormat("""{0}"": ", kvp.Key)
            If TypeOf kvp.Value Is Dictionary(Of String, Object) Then
                result.Append(ConvertDictionaryToString(DirectCast(kvp.Value, Dictionary(Of String, Object))))
            ElseIf TypeOf kvp.Value Is List(Of String) Then
                result.Append("[")
                result.Append(String.Join(", ", DirectCast(kvp.Value, List(Of String)).Select(Function(v) """" & v & """")))
                result.Append("]")
            ElseIf TypeOf kvp.Value Is String Then
                result.AppendFormat("""{0}""", kvp.Value)
            Else
                result.Append(kvp.Value.ToString())
            End If
            result.Append(", ")
        Next
        If result.Length > 1 Then
            result.Length -= 2 ' 最後の ", " を削除
        End If
        result.Append("}")
        Return result.ToString()
    End Function
End Module
