Imports System.IO
Imports System.Threading
Imports System.Windows.Forms

''' <summary>
''' 検索オプション
''' </summary>
Public Class FileSearchOptions
    Public Property SearchPattern As String = "*.*"
    Public Property SleepPerFolderMs As Integer = 0

    ' 除外条件（必要に応じて null チェック）
    Public Property ExcludeFolderPredicate As Func(Of String, Boolean) = Nothing
    Public Property ExcludeFilePredicate As Func(Of String, Boolean) = Nothing
End Class

''' <summary>
''' 検索キャンセル制御用クラス
''' </summary>
Public Class FileSearchController
    Public Property IsCancelRequested As Boolean = False
    Public Sub Cancel()
        IsCancelRequested = True
    End Sub
End Class

''' <summary>
''' ファイル検索ロジック本体
''' </summary>
Public Class FileSearcher
    Private ReadOnly _options As FileSearchOptions
    Private ReadOnly _controller As FileSearchController
    Private ReadOnly _logger As AppLogger
    Private ReadOnly _outputBox As RichTextBox

    Public Event FileFound(path As String) ' ← 進捗イベント

    Public Sub New(options As FileSearchOptions,
                   controller As FileSearchController,
                   logger As AppLogger,
                   Optional outputBox As RichTextBox = Nothing)

        _options = options
        _controller = controller
        _logger = logger
        _outputBox = outputBox
    End Sub

#Region ""
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="folderPath"></param>
    ''' <param name="StartsWithStr"></param>
    ''' <returns></returns>
    Public Shared Function IsMatchFilterPathName(folderPath As String, StartsWithStr As String) As Boolean
        If StartsWithStr = "" Then Return False
        Return IO.Path.GetFileName(folderPath).StartsWith(StartsWithStr)
    End Function

#End Region

    Public Function SearchFiles(rootPath As String) As List(Of String)
        Dim results As New List(Of String)
        _logger.Info($"検索開始: {rootPath}")
        AppendLogToUI($"[INFO] 検索開始: {rootPath}")
        SearchRecursive(rootPath, results)
        _logger.Info($"検索終了: {results.Count} 件のファイルを取得")
        AppendLogToUI($"[INFO] 検索終了: {results.Count} 件のファイルを取得")
        Return results
    End Function

    Private Sub SearchRecursive(currentPath As String, results As List(Of String))
        If _controller.IsCancelRequested Then Return

        Try
            Dim files = Directory.GetFiles(currentPath, _options.SearchPattern, SearchOption.TopDirectoryOnly)
            For Each file In files
                If _controller.IsCancelRequested Then Exit For
                If _options.ExcludeFilePredicate IsNot Nothing AndAlso _options.ExcludeFilePredicate(file) Then
                    _logger.Info($"除外ファイル: {file}")
                    Continue For
                End If

                results.Add(file)
                _logger.Info($"ファイル取得: {file}")
                AppendLogToUI($"[FILE] {file}")
                RaiseEvent FileFound(file)
            Next
        Catch ex As Exception
            _logger.Err(ex, $"ファイル取得失敗: {currentPath}")
            AppendLogToUI($"[ERROR] ファイル取得失敗: {currentPath}")
        End Try

        If _controller.IsCancelRequested Then Return

        Try
            Dim subDirs = Directory.GetDirectories(currentPath)
            For Each _dir In subDirs
                If _controller.IsCancelRequested Then Exit For
                Dim flagA = _options.ExcludeFolderPredicate(_dir)
                If _options.ExcludeFolderPredicate IsNot Nothing AndAlso flagA Then
                    _logger.Info($"除外フォルダ: {_dir}")
                    Continue For
                End If

                If _options.SleepPerFolderMs > 0 Then
                    Thread.Sleep(_options.SleepPerFolderMs)
                End If

                SearchRecursive(_dir, results)
            Next
        Catch ex As Exception
            _logger.Err(ex, $"フォルダ取得失敗: {currentPath}")
            AppendLogToUI($"[ERROR] フォルダ取得失敗: {currentPath}")
        End Try
    End Sub

    Private Sub AppendLogToUI(text As String)
        If _outputBox Is Nothing Then Return

        If _outputBox.InvokeRequired Then
            _outputBox.Invoke(Sub()
                                  _outputBox.AppendText(text & Environment.NewLine)
                                  _outputBox.ScrollToCaret()
                              End Sub)
        Else
            _outputBox.AppendText(text & Environment.NewLine)
            _outputBox.ScrollToCaret()
        End If
    End Sub
End Class