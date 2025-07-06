Public Class DummyLogger
    Public Sub Info(value As String)
        Output(value, "INFO")
    End Sub

    Public Sub Err(value As String, ex As Exception)
        Output(value, "ERROR")
        Output(ex.GetType.ToString + ":" + ex.Message, "ERROR")
        Output(ex.StackTrace, "ERROR")
    End Sub

    Private Sub Output(value As String, logtype As String)
        Dim values = value.Split(vbCrLf)
        For Each element In values
            element = element.Trim
            OutputCore(element, logtype)
        Next
    End Sub
    Private Sub OutputCore(value As String, logtype As String)
        If logtype <> "" Then logtype = $"[{logtype}] "
        Debug.WriteLine(logtype + value)
    End Sub
End Class
