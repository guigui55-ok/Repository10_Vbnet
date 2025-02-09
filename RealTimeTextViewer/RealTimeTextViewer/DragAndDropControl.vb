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
            _logger.AddLog(Me, "AddRecieveControls")
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
        _logger.AddLog(Me, "AddRecieveControls")
        Try
            con.AllowDrop = True
            AddHandler con.DragDrop, AddressOf Control_DragDrop
            AddHandler con.DragEnter, AddressOf Control_DragEnter
        Catch ex As Exception
            _logger.AddException(ex, Me, "AddRecieveControl")
        End Try
    End Sub

    Private Sub Control_DragEnter(sender As Object, e As DragEventArgs)
        _logger.AddLog(Me, _control.Name + " > Control_DragEnter")
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
            _logger.AddLog(Me, _control.Name + " > Control_DragDrop Finally")
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

    End Sub

End Class
