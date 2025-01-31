Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Public Class FormFileListMaker
    Private WithEvents _dragDropHandler As DragAndDropHandler

    Private Sub FormFileListMaker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _dragDropHandler = New DragAndDropHandler(Me)
        _dragDropHandler.Initialize()

        TextBoxPath.Text = My.Settings.Path
        TextBoxIncludeList.Text = My.Settings.IncludeList
        TextBoxIgnoreList.Text = My.Settings.IgnoreList

        Me.KeyPreview = True
    End Sub

    Private Sub KeyDown_FormFileListMaker(sendr As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.NumPad0 AndAlso e.Control Then
            OutputLog("KeyDonw NumPad0 + Ctrl")
            Dim dir = Path.GetDirectoryName(Application.ExecutablePath)
            dir = Path.Combine(dir, "__test_Files")
            Directory.CreateDirectory(dir)
            OutputLog("TargetDir : " + dir)
            Dim baseFileName = "__test_file"
            Dim ext = ".txt"
            Dim fileName = ""
            Dim targetPath = ""
            For num = 0 To 10
                fileName = baseFileName + "_" + num.ToString("D2") + ext
                targetPath = Path.Combine(dir, fileName)
                File.WriteAllText(targetPath, "")
                OutputLog("CreateFile : " + fileName)
            Next
        End If
    End Sub

    Private Sub _dragDropHandler_FileDropped(filePath As String) Handles _dragDropHandler.FileDropped
        TextBoxPath.Text = filePath
        OutputLog($"File dropped: {filePath}")
    End Sub

    Private Sub ButtonCreateFile_Click(sender As Object, e As EventArgs) Handles ButtonCreateFile.Click
        ProcessFiles()
    End Sub

    Private Sub FormFileListMaker_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        ProcessFiles()
    End Sub

    Private Sub ProcessFiles()
        TextBoxOutputPath.Text = ""
        Dim targetPath As String = TextBoxPath.Text
        If Not Directory.Exists(targetPath) Then
            OutputLog("Invalid directory path.")
            Return
        End If

        OutputLog("Starting file processing...")
        Dim includePatterns As String() = ExtractPatterns(TextBoxIncludeList.Text)
        Dim ignorePatterns As String() = ExtractPatterns(TextBoxIgnoreList.Text)
        Dim allFiles As String() = Directory.GetFiles(targetPath, "*", SearchOption.AllDirectories)

        Dim filteredFiles = allFiles.Where(Function(f)
                                               Dim fileName As String = Path.GetFileName(f)
                                               Return includePatterns.Any(Function(p) Regex.IsMatch(fileName, p)) AndAlso
                                                      Not ignorePatterns.Any(Function(p) Regex.IsMatch(fileName, p))
                                           End Function).Select(Function(f) Path.GetFileName(f)).ToList()

        Dim outputFilePath As String = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), $"FileList_{DateTime.Now:yyMMdd_HHmmss}.csv")
        Dim csvLines As New List(Of String)
        csvLines.Add(targetPath)
        csvLines.Add("")
        csvLines.Add(TextBoxIncludeList.Text)
        csvLines.Add(TextBoxIgnoreList.Text)
        csvLines.AddRange(filteredFiles)
        File.WriteAllLines(outputFilePath, csvLines, Encoding.UTF8)
        OutputLog($"File list created: {outputFilePath}")

        Me.TextBoxOutputPath.Text = outputFilePath
    End Sub

    Private Function ExtractPatterns(input As String) As String()
        Return input.Split(","c).Select(Function(p) p.Trim().Trim(""""c)).Where(Function(p) Not String.IsNullOrEmpty(p)).ToArray()
    End Function

    Public Sub OutputLog(message As String)
        Console.WriteLine(message)
    End Sub

    Private Sub FormFileListMaker_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        My.Settings.Path = TextBoxPath.Text
        My.Settings.IncludeList = TextBoxIncludeList.Text
        My.Settings.IgnoreList = TextBoxIgnoreList.Text
        My.Settings.Save()
    End Sub

    Private Sub ButtonShowExplorer_Click(sender As Object, e As EventArgs) Handles ButtonShowExplorer.Click
        Dim openPath = Path.GetDirectoryName(Me.TextBoxOutputPath.Text)
        OpenDirectoryInExplorer(openPath)
    End Sub

    Private Sub OpenDirectoryInExplorer(directoryPath As String)
        If Directory.Exists(directoryPath) Then
            Process.Start("explorer.exe", directoryPath)
        Else
            MessageBox.Show("指定されたディレクトリは存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class
