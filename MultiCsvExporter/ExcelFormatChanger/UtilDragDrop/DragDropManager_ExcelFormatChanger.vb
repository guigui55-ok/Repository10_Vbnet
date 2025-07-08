Public Class DragDropManager_FormExcelFormatChanger
    Public _logger As AppLogger
    Public _form As FormExcelFormatChanger

    Private _dragAndDropControl_Src As DragAndDropControl
    Private _dragAndDropFile_Src As DragAndDropFile
    Private _dragAndDropControl_Dest As DragAndDropControl
    Private _dragAndDropFile_Dest As DragAndDropFile

    Sub New(logger As AppLogger, form As FormExcelFormatChanger)
        _logger = logger
        _form = form

        '// set DragDrop
        'src path
        _dragAndDropControl_Src = New DragAndDropControl(_logger, form.GroupBox_Conditions)
        _dragAndDropControl_Src.AddRecieveControl(form.TextBox_SrcFilePath)  '別コントロールにもイベントを追加
        _dragAndDropFile_Src = New DragAndDropFile(_logger, _dragAndDropControl_Src)
        _dragAndDropControl_Src._dragAndDropForFile = _dragAndDropFile_Src
        AddHandler _dragAndDropControl_Src.DragAndDropAfterEvent, AddressOf DragDropEvent_Src

        'dest path
        _dragAndDropControl_Dest = New DragAndDropControl(_logger, form.GroupBox_DestCondition)
        _dragAndDropControl_Dest.AddRecieveControl(form.TextBox_DestFilePath)  '別コントロールにもイベントを追加
        _dragAndDropFile_Dest = New DragAndDropFile(_logger, _dragAndDropControl_Dest)
        _dragAndDropControl_Dest._dragAndDropForFile = _dragAndDropFile_Dest
        AddHandler _dragAndDropControl_Dest.DragAndDropAfterEvent, AddressOf DragDropEvent_Dest

    End Sub

    Public Sub Init()

    End Sub

    Private Sub DragDropEvent_Src(sender As Object, e As EventArgs)
        Try
            _logger.AddLog(Me, " > DragDropEvent_Src")

            Dim _dragAndDropFile As DragAndDropFile = _dragAndDropControl_Src._dragAndDropForFile
            If (_dragAndDropFile.FileList Is Nothing) Then
                _logger.PrintInfo("Files == null")
                Return
            End If
            If (_dragAndDropFile.FileList.Count < 1) Then
                _logger.PrintInfo("Files.Length < 1")
                Return
            End If
            Dim targetPath As String = _dragAndDropFile.FileList(0)
            _logger.PrintInfo(String.Format("targetPath={0}", targetPath))
            If (IO.Directory.Exists(targetPath)) Then
                _logger.PrintInfo("Path Is Directory")
                Exit Sub
            Else
                'pass
            End If
            _form.TextBox_SrcFilePath.Text = targetPath

        Catch ex As Exception
            _logger.AddException(ex, Me, "DragDropEvent_Src")
        End Try
    End Sub


    Private Sub DragDropEvent_Dest(sender As Object, e As EventArgs)
        Try
            _logger.AddLog(Me, " > DragDropEvent_Dest")

            Dim _dragAndDropFile As DragAndDropFile = _dragAndDropControl_Dest._dragAndDropForFile
            If (_dragAndDropFile.FileList Is Nothing) Then
                _logger.PrintInfo("Files == null")
                Return
            End If
            If (_dragAndDropFile.FileList.Count < 1) Then
                _logger.PrintInfo("Files.Length < 1")
                Return
            End If
            Dim targetPath As String = _dragAndDropFile.FileList(0)
            _logger.PrintInfo(String.Format("targetPath={0}", targetPath))
            If (IO.Directory.Exists(targetPath)) Then
                _logger.PrintInfo("Path Is Directory")
                Exit Sub
            Else
                'pass
            End If
            _form.TextBox_DestFilePath.Text = targetPath

        Catch ex As Exception
            _logger.AddException(ex, Me, "DragDropEvent_Dest")
        End Try
    End Sub

End Class
