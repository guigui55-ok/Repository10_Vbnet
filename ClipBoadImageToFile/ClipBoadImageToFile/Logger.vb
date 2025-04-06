Public Class Logger

    Dim _timeFormat As String = "[yy/MM/dd HH:mm:ss.fff]"
    Public Sub Print(value As String)
        Dim timeStr = DateTime.Now.ToString(_timeFormat)
        Dim logVal = timeStr + " " + value.ToString()
        OutputConsole(logVal)
    End Sub

    Public Sub OutputConsole(value As Object)
        Debug.WriteLine(value.ToString())
    End Sub
End Class
