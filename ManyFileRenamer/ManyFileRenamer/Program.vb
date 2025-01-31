Imports System.IO
Imports System.Text

Module Program
    Private csvPath As String = "file_list.csv" ' CSVファイルのパス
    Private readFolder As String
    Private writeFolder As String
    Private includePatterns As List(Of String)
    Private ignorePatterns As List(Of String)
    Private renamer As ManyFileRenamerClass

    Public Sub Main()
        OutputLog("##########")
        csvPath = "C:\Users\OK\source\repos\Repository10_VBnet\FileListMaker\FileListMaker\bin\Debug\FileList_250201_032546.csv"
        OutputLog("csvPath = " + csvPath)
        LoadCsvData()
        'ExecuteRenameProcess()
        ExecuteUndoProcess()
        Console.ReadKey()
    End Sub

    ' CSVデータを読み込む
    Private Sub LoadCsvData()
        Dim lines As List(Of String) = File.ReadAllLines(csvPath, Encoding.UTF8).ToList()

        If lines.Count < 5 Then
            OutputLog("CSVファイルのフォーマットが不正です。")
            Exit Sub
        End If

        readFolder = lines(0).Trim()
        readFolder = readFolder.Replace(",", "")
        OutputLog("readFolder = " + readFolder)

        writeFolder = lines(1).Trim()
        writeFolder = writeFolder.Replace(",", "")
        If String.IsNullOrWhiteSpace(writeFolder) Then
            OutputLog("【エラー】writeFolder が空白です。リネーム処理を中止します。")
            Exit Sub
        End If
        OutputLog("writeFolder = " + writeFolder)

        includePatterns = lines(2).Trim().Trim(""""c).Split(","c).ToList()
        ignorePatterns = lines(3).Trim().Trim(""""c).Split(","c).ToList()

        ' ManyFileRenamerClass のインスタンス作成
        renamer = New ManyFileRenamerClass()
        renamer.IncludeList = includePatterns
        renamer.ignoreList = ignorePatterns

        ' リネーム対象リストの作成
        For i As Integer = 4 To lines.Count - 1
            Dim parts As String() = lines(i).Split(","c)
            If parts.Length < 2 Then
                parts = lines(i).Split(vbTab)
            End If
            If parts.Length < 2 Then
                OutputLog($"[エラー] parts.Length < 2: {lines(i)}")
                Continue For
            End If

            Dim srcPath As String = Path.Combine(readFolder, parts(0).Trim())
            Dim distPath As String = Path.Combine(writeFolder, parts(1).Trim())

            If String.IsNullOrWhiteSpace(distPath) Then
                OutputLog($"【エラー】リネーム先パスが空です: {srcPath}")
                Continue For
            End If

            renamer.AddPathList_(srcPath, distPath)
        Next
    End Sub


    ' リネームを実行
    Private Sub ExecuteRenameProcess()
        If renamer IsNot Nothing Then
            renamer.ExecuteRename()
            OutputLog("リネーム処理が完了しました。")
        End If
    End Sub

    ' Undoリネームを実行
    Public Sub ExecuteUndoProcess()
        If renamer IsNot Nothing Then
            renamer.UndoRename()
            OutputLog("Undo処理が完了しました。")
        End If
    End Sub

    ' ログ出力
    Public Sub OutputLog(value As String)
        'Debug.WriteLine(value)
        'Console.WriteLine(value)
        Dim logger As New Logger("__test_log.txt")
        logger.OutputLog(value)
    End Sub


    Public Class Logger
        Private logFilePath As String

        Public Sub New(Optional ByVal logFile As String = "log.txt")
            logFilePath = logFile
        End Sub

        ' ログ出力
        Public Sub OutputLog(value As String)
            Debug.WriteLine(value)
            Console.WriteLine(value)

            ' ファイルに出力
            Try
                Using writer As StreamWriter = New StreamWriter(logFilePath, True, System.Text.Encoding.UTF8)
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {value}")
                End Using
            Catch ex As Exception
                Debug.WriteLine($"ログ書き込みエラー: {ex.Message}")
            End Try
        End Sub
    End Class

End Module
