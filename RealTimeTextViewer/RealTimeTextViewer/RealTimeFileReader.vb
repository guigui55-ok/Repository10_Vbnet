Public Class RealTimeFileReader
    Implements IDisposable

    Public _fileName As String
    Private _watcher As IO.FileSystemWatcher
    Private _disposed As Boolean = False
    Private _logger As AppLogger

    ' Event to notify file updates
    Public Event FileUpdated(content As String)

    ' Constructor
    Public Sub New(logger As AppLogger, fileName As String)
        _logger = logger
        _fileName = fileName
        If fileName = "" Then
            Exit Sub
        End If

        If String.IsNullOrEmpty(fileName) Then
            Throw New ArgumentException("File name cannot be null or empty.")
        End If

        If Not IO.File.Exists(fileName) Then
            Throw New IO.FileNotFoundException($"File '{fileName}' does not exist.")
        End If

        _watcher = New IO.FileSystemWatcher()
    End Sub

    ' Method to monitor the file and read changes in real-time
    Public Sub MonitorFile()
        Try
            _watcher.Path = IO.Path.GetDirectoryName(_fileName)
            _watcher.Filter = IO.Path.GetFileName(_fileName)
            _watcher.NotifyFilter = IO.NotifyFilters.LastWrite

            AddHandler _watcher.Changed, AddressOf OnFileChanged
            _watcher.EnableRaisingEvents = True

            _logger.PrintInfo("Monitoring file for changes: " & _fileName)
        Catch ex As Exception
            _logger.PrintInfo("Error while monitoring file: " & ex.Message)
        End Try
    End Sub

    ' Event handler for file changes
    'Private Sub OnFileChanged(source As Object, e As IO.FileSystemEventArgs)
    '    Try
    '        Dim content As String = IO.File.ReadAllText(_fileName)
    '        '_logger.PrintInfo("File updated. New content:")
    '        '_logger.PrintInfo(content)
    '        _logger.PrintInfo("File updated. New content length:" + content.Length.ToString())

    '        ' Raise the FileUpdated event
    '        RaiseEvent FileUpdated(content)
    '    Catch ex As Exception
    '        _logger.PrintInfo("Error reading updated file: " & ex.Message)
    '    End Try
    'End Sub

    '[Info] Error reading updated file: 有効ではないスレッド間の操作: コントロールが作成されたスレッド以外のスレッドからコントロール 'RichTextBox_Main' がアクセスされました。’
    '画面描画時にエラーとなるため以下に変更

    Private Sub OnFileChanged(source As Object, e As IO.FileSystemEventArgs)
        Try
            Dim content As String = IO.File.ReadAllText(_fileName)
            _logger.PrintInfo("File updated. New content length: " & content.Length)

            ' Ensure FileUpdated event is invoked on the UI thread
            If Application.OpenForms.Count > 0 Then
                Application.OpenForms(0).Invoke(New Action(Sub()
                                                               RaiseEvent FileUpdated(content)
                                                           End Sub))
            Else
                RaiseEvent FileUpdated(content)
            End If
        Catch ex As Exception
            _logger.PrintInfo("Error reading updated file: " & ex.Message)
        End Try
    End Sub



    ' Dispose method to clean up resources
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not _disposed Then
            If disposing Then
                ' Free managed resources
                If _watcher IsNot Nothing Then
                    RemoveHandler _watcher.Changed, AddressOf OnFileChanged
                    _watcher.Dispose()
                    _watcher = Nothing
                End If
            End If

            ' Free unmanaged resources (if any)
            _disposed = True
        End If
    End Sub

    ' Destructor
    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub
End Class
