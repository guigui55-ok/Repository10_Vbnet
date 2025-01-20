Public Class FileManager
    Public Sub CopyCsvFiles(sourceDir As String, destDir As String)
        If Not IO.Directory.Exists(destDir) Then
            IO.Directory.CreateDirectory(destDir)
        End If
        For Each file In IO.Directory.GetFiles(sourceDir, "*.csv")
            IO.File.Copy(file, IO.Path.Combine(destDir, IO.Path.GetFileName(file)), True)
        Next
    End Sub

    Public Function GetCsvFiles(directory As String) As List(Of String)
        Return IO.Directory.GetFiles(directory, "*.csv").ToList()
    End Function
End Class
