Public Class DragAndDropFile

    Public _logger As AppLogger
    Public _dragAndDropOnControl As DragAndDropControl
    Public FileList As List(Of String) = New List(Of String)()
    Public Event DragAndDropEventAfterEventForFile As EventHandler


    Sub New(logger As AppLogger, dragAndDropControl As DragAndDropControl)
        _logger = logger
        _dragAndDropOnControl = dragAndDropControl
        AddHandler _dragAndDropOnControl.DragAndDropAfterEvent, AddressOf DragAndDropAfterEvent
    End Sub

    Private Sub DragAndDropAfterEvent(sender As Object, e As EventArgs)
        Try
            _logger.AddLog(Me, "DragAndDropAfterEvent")
            '// DragDrop の e を配列へ
            Me.FileList = GetFilesByDragAndDrop(e).ToList()

        Catch ex As Exception
            _logger.AddException(ex, Me, "DragAndDropAfterEvent")
        Finally
            RaiseEvent DragAndDropEventAfterEventForFile(sender, e)
        End Try
    End Sub

    Public Function GetFilesByDragAndDrop(e As DragEventArgs) As String()
        Dim retFiles() As String = {}
        Try
            _logger.AddLog(Me, "GetFilesByDragAndDrop")
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then

                '// ドラッグ中のファイルやディレクトリの取得
                retFiles = e.Data.GetData(DataFormats.FileDrop)
                Return retFiles
            Else
                _logger.AddLogWarning(Me, "GetDataPresent :e.Data.GetDataPresent(DataFormats.FileDrop)=false")
                Return retFiles
            End If
        Catch ex As Exception
            _logger.AddException(ex, Me, "GetFilesByDragAndDrop")
            Return retFiles
        End Try
    End Function


End Class
