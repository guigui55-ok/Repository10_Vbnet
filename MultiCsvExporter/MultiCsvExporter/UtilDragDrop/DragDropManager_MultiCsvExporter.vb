Public Class DragDropManager_MultiCsvExporter
    Public _logger As AppLogger
    Public _form As FormMultiCsvExporter

    Private _dragAndDropControl_InputDir As DragAndDropControl
    Private _dragAndDropFile_InputDir As DragAndDropFile
    Private _dragAndDropControl_DesttDir As DragAndDropControl
    Private _dragAndDropFile_DestDir As DragAndDropFile

    Sub New(logger As AppLogger, form As FormMultiCsvExporter)

        '// set DragDrop
        'src path
        _dragAndDropControl_InputDir = New DragAndDropControl(_logger, form.GroupBox_Input)
        _dragAndDropControl_InputDir.AddRecieveControl(form.RichTextBox_DirPaths)  '別コントロールにもイベントを追加
        _dragAndDropFile_InputDir = New DragAndDropFile(_logger, _dragAndDropControl_InputDir)
        AddHandler _dragAndDropControl_InputDir.DragAndDropAfterEvent, AddressOf DragDropEvent_Src

        'src path
        _dragAndDropControl_DesttDir = New DragAndDropControl(_logger, form.GroupBox_Output)
        _dragAndDropControl_DesttDir.AddRecieveControl(form.TextBox_OutputDirPath)  '別コントロールにもイベントを追加
        _dragAndDropFile_DestDir = New DragAndDropFile(_logger, _dragAndDropControl_DesttDir)
        AddHandler _dragAndDropControl_DesttDir.DragAndDropAfterEvent, AddressOf DragDropEvent_Dest

    End Sub

    Public Sub Init()

    End Sub

    Private Sub DragDropEvent_Src(sender As Object, e As EventArgs)
        Try
            _logger.AddLog(Me, " > DragDropEvent_Src")

            Dim _dragAndDropFile As DragAndDropFile = _dragAndDropControl_InputDir._dragAndDropForFile
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
                'pass
            Else
                _logger.PrintInfo("Path Is File")
                targetPath = IO.Path.GetDirectoryName(targetPath)
            End If
            Dim newLine = vbCrLf
            If _form.RichTextBox_DirPaths.Text.Length < 1 Then
                newLine = ""
            End If
            _form.RichTextBox_DirPaths.Text = targetPath

        Catch ex As Exception
            _logger.AddException(ex, Me, "DragDropEvent_Src")
        End Try
    End Sub


    Private Sub DragDropEvent_Dest(sender As Object, e As EventArgs)
        Try
            _logger.AddLog(Me, " > DragDropEvent_Src")

            Dim _dragAndDropFile As DragAndDropFile = _dragAndDropControl_InputDir._dragAndDropForFile
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
                'pass
            Else
                _logger.PrintInfo("Path Is File")
                targetPath = IO.Path.GetDirectoryName(targetPath)
            End If
            'Dim newLine = vbCrLf
            'If _form.TextBox_SrcFilePath.Text.Length < 1 Then
            '    newLine = ""
            'End If
            _form.TextBox_OutputDirPath.Text = targetPath

        Catch ex As Exception
            _logger.AddException(ex, Me, "DragDropEvent_Src")
        End Try
    End Sub

End Class
