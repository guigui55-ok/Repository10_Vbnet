Public Class AppLogger

    Public Sub PrintInfo(message As String)
        message = CreateMessage(Nothing, message, "Info")
        AddLog(message)
    End Sub
    Public Sub AddLog(message As String)
        Debug.WriteLine(String.Format("{0}", message))
    End Sub

    Public Sub AddLog(objectValue As Object, message As String)
        message = CreateMessage(objectValue, message)
        AddLog(message)
    End Sub

    Private Function CreateMessage(objectValue As Object, message As String, Optional LogType As String = "")
        If Not (objectValue Is Nothing) Then
            message = objectValue.ToString() + " > " + message
        End If
        If LogType <> "" Then
            LogType = String.Format("[{0}]", LogType)
            message = LogType + " " + message
        End If
        Return message
    End Function

    Public Sub AddLogAlert(message As String)
        message = CreateMessage(Nothing, message, "Alert")
        AddLog(message)
    End Sub

    Public Sub AddException(ex As Exception, objectValue As Object, message As String)
        AddLog(objectValue, message)
        AddLog(objectValue, ex.GetType().ToString() + ":" + ex.Message)
        AddLog(objectValue, ex.StackTrace)
    End Sub

    Public Sub AddLogWarning(objectValue As Object, message As String)
        message = CreateMessage(objectValue, message, "Warning")
        AddLog(message)
    End Sub

End Class
