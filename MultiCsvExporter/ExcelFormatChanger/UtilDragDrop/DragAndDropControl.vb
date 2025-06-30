Public Class DragAndDropControl


    Public _logger As AppLogger
    Public _control As Control
    Public Event DragAndDropAfterEvent As EventHandler

    'ファイル受け取り用のクラス
    Public _dragAndDropForFile As DragAndDropFile
    Sub New(logger As AppLogger, recieveEventControl As Control)
        _logger = logger
        _control = recieveEventControl
        If (recieveEventControl Is Nothing) Then
            Return
        End If
        AddHandler _control.DragDrop, AddressOf Control_DragDrop
        AddHandler _control.DragEnter, AddressOf Control_DragEnter
    End Sub
    Public Sub AddRecieveControls(controls() As Control)
        Try
            '_logger.AddLog(Me, "AddRecieveControls")
            If (controls Is Nothing) Then
                _logger.AddLogAlert("  controls == null")
                Return
            End If
            If (controls.Length < 1) Then
                _logger.AddLogAlert("  controls.Length < 1")
                Return
            End If
            For Each con As Control In controls
                con.AllowDrop = True
                AddHandler con.DragDrop, AddressOf Control_DragDrop
                AddHandler con.DragEnter, AddressOf Control_DragEnter
            Next
        Catch ex As Exception
            _logger.AddException(ex, Me, "AddRecieveControls")
        End Try
    End Sub

    Public Sub AddRecieveControl(con As Control)
        '_logger.AddLog(Me, "AddRecieveControls")
        Try
            con.AllowDrop = True
            AddHandler con.DragDrop, AddressOf Control_DragDrop
            AddHandler con.DragEnter, AddressOf Control_DragEnter
        Catch ex As Exception
            _logger.AddException(ex, Me, "AddRecieveControl")
        End Try
    End Sub

    Private Sub Control_DragEnter(sender As Object, e As DragEventArgs)
        '_logger.AddLog(Me, _control.Name + " > Control_DragEnter")
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub Control_DragDrop(sender As Object, e As DragEventArgs)
        Try
            _logger.AddLog(Me, _control.Name + " > Control_DragDrop")
            '// 受け取った EventArgs はほかのクラスで処理する
            RaiseEvent DragAndDropAfterEvent(sender, e)

        Catch ex As Exception
            _logger.AddException(ex, Me, _control.Name + " > Control_DragDrop")
        Finally
            '_logger.AddLog(Me, _control.Name + " > Control_DragDrop Finally")
        End Try
    End Sub


    Private Sub _SampleUsage()
        Dim _panel As Panel = New Panel()
        Dim _groupBox As GroupBox = New GroupBox()
        Dim _dragAndDropOnControl = New DragAndDropControl(_logger, _panel) '//RecieveするControl
        _dragAndDropOnControl.AddRecieveControl(_groupBox) '//別コントロールにもイベントを追加
        AddHandler _dragAndDropOnControl.DragAndDropAfterEvent, AddressOf _SampleDragDropEvent

    End Sub

    Private Sub _SampleDragDropEvent(sender As Object, e As EventArgs)

        Dim _logger As Object = Nothing
        Dim targetControl As Control
        Dim _dragAndDropControl As DragAndDropControl = New DragAndDropControl(_logger, targetControl)
        Dim RichTextBox_Paths As RichTextBox = Nothing

        Try
            _logger.AddLog(Me, " > _SampleDragDropEvent")

            Dim _dragAndDropFile As DragAndDropFile = _dragAndDropControl._dragAndDropForFile
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
                Return
            End If
            Dim newLine = vbCrLf
            If RichTextBox_Paths.Text.Length < 1 Then
                newLine = ""
            End If
            RichTextBox_Paths.Text += newLine + targetPath

        Catch ex As Exception
            _logger.AddException(ex, Me, "_SampleDragDropEvent")
        End Try
    End Sub

End Class
