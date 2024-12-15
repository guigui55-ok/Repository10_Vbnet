Public Module ModuleCommon
    Public Sub LogOutput(value)
        Dim buf = String.Format("{0}", value)
        Debug.WriteLine(buf)
    End Sub
End Module
