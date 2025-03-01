Public Class FormOpenExePath
    Dim _processPathFinder As ProcessPathFinder
    Dim _processPathFinderWmi As WmiProcessPathFinder
    Private Sub FormOpenExePath_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _processPathFinder = New ProcessPathFinder()
        _processPathFinderWmi = New WmiProcessPathFinder()
        'TextBoxProcessName.Multiline = True
    End Sub
    'KeyPressイベントハンドラ
    Private Sub TextBox1_KeyPress(ByVal sender As Object,
        ByVal e As KeyPressEventArgs) _
        Handles TextBoxProcessName.KeyPress
        'EnterやEscapeキーでビープ音が鳴らないようにする
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) OrElse
        e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Escape) Then
            e.Handled = True
        End If
    End Sub

    Private Sub KeyDown_TextBox(sender As Object, e As KeyEventArgs) Handles TextBoxProcessName.KeyDown
        If Keys.Enter = e.KeyCode Then
            Button1_Click(sender, EventArgs.Empty)
        End If
    End Sub

    Public Sub OutPut(value As String)
        Console.WriteLine(value)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenPath(TextBoxProcessName.Text)
    End Sub

    Private Sub OpenPath(processName As String)
        Dim processPathList As List(Of String)
        processPathList = ProcessPathFinder.GetProcessPaths(processName)
        processPathList = WmiProcessPathFinder.GetProcessPaths(processName)
        Dim targetPath = ""
        OutPut("*OpenPath")
        Dim count = 0
        For Each processPath In processPathList
            If count = 0 Then
                targetPath = processPath
            End If
            OutPut($"[{count}] {processPath}")
            count += 1
        Next
        OutPut($"=====")
        If IO.File.Exists(targetPath) Then
            targetPath = IO.Path.GetDirectoryName(targetPath)
            OutPut($"IsExistsFile  > TargetChange > DirectoryPath  > targetPath = {targetPath}")
        Else
            OutPut($"targetPath = {targetPath}")
        End If
        Process.Start("explorer.exe", targetPath)
    End Sub
End Class
