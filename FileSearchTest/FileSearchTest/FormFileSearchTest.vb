Public Class FormFileSearchTest

    Public _logger As AppLogger



    Private Sub FormFileSearchTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _logger = New AppLogger()


    End Sub



    '###################

    ' メンバー変数
    Private _searchTask As Task = Nothing
    Private _searchController As FileSearchController = Nothing

    Private Async Sub Button_Start_Click(sender As Object, e As EventArgs) Handles Button_Start.Click
        'TextBox_Filter.Text
        'TextBox_TargetDir.Text

        Dim searchPattern = "*.png"
        Dim targetPath = "C:\ZMyFolder\newDoc\0ProgramingAll\ImageViewerMain"
        Dim FolderStartsWithStr = ""
        Dim FileStartsWithStr = ""

        Button_Start.Enabled = False
        Button_Cancel.Enabled = True
        RichTextBox1.Clear()

        _searchController = New FileSearchController()

        '    Dim options As New FileSearchOptions With {
        '    .SearchPattern = searchPattern,
        '    .SleepPerFolderMs = 100,
        '    .ExcludeFolderPredicate = Function(p) IO.Path.GetFileName(p).StartsWith(FolderStartsWithStr),
        '    .ExcludeFilePredicate = Function(p) IO.Path.GetFileName(p).StartsWith("~")
        '}
        Dim options As New FileSearchOptions With {
        .SearchPattern = searchPattern,
        .SleepPerFolderMs = 100,
        .ExcludeFolderPredicate = Function(p) FileSearcher.IsMatchFilterPathName(p, FolderStartsWithStr),
        .ExcludeFilePredicate = Function(p) FileSearcher.IsMatchFilterPathName(p, FileStartsWithStr)
    }

        Dim logger As New AppLogger()
        Dim searcher As New FileSearcher(options, _searchController, logger, RichTextBox1)

        AddHandler searcher.FileFound, Sub(path)
                                           ' 必要ならUI更新やログ追加も可
                                       End Sub

        ' 非同期で検索処理を開始
        Dim _searchTask As Task(Of List(Of String)) = Task.Run(Function()
                                                                   Return searcher.SearchFiles(targetPath)
                                                               End Function)

        ' 完了を待機（UIスレッドをブロックしない）
        Try
            Dim result As List(Of String) = Await _searchTask
            RichTextBox1.AppendText($"完了: {result.Count} 件のファイルが見つかりました。" & Environment.NewLine)
        Catch ex As Exception
            RichTextBox1.AppendText("エラー: " & ex.Message & Environment.NewLine)
        Finally
            Button_Start.Enabled = True
            Button_Cancel.Enabled = False
        End Try
    End Sub


    '###################

    Private Sub UsageSample()

        Dim _logger = New AppLogger

        Dim filterValue = "^$"

        Dim options As New FileSearchOptions With {
            .SearchPattern = "*.txt",
            .SleepPerFolderMs = 50,
            .ExcludeFolderPredicate = Function(path) FileSearcher.IsMatchFilterPathName(path, "TEMP"),
            .ExcludeFilePredicate = Function(path) FileSearcher.IsMatchFilterPathName(path, filterValue)
        }


        Dim controller As New FileSearchController()

        Dim searcher As New FileSearcher(options, controller, _logger)
        Dim resultFiles As List(Of String) = searcher.SearchFiles("Z:\SharedFolder")

        For Each file In resultFiles
            Console.WriteLine(file)
        Next


    End Sub

    Private Sub ButtonUp_Click(sender As Object, e As EventArgs) Handles ButtonUp.Click
        CountUpDown(TextBox_Count, 1)
    End Sub

    Private Sub ButtonDown_Click(sender As Object, e As EventArgs) Handles ButtonDown.Click
        CountUpDown(TextBox_Count, -1)
    End Sub

    Private Sub CountUpDown(con As Control, addValue As Integer)
        Dim _val = con.Text
        Dim _num = 0
        Integer.TryParse(_val, _num)
        _num += addValue
        con.Text = _num.ToString

        Dim log = $"{con.Name}.Text = {_num}"
        _logger.Info(log)
        RichTextBox1.AppendText(log + vbCrLf)
        RichTextBox1.ScrollToCaret()
    End Sub
End Class
