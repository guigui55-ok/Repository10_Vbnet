Imports System.IO

Public Class FormRealTimeTextViewer

    Dim _logger As AppLogger
    Dim _dragAndDropControl As DragAndDropControl
    Dim _dragAndDropFile As DragAndDropFile
    Dim _realTimeFileReader As RealTimeFileReader
    Dim _findString As FindStringForm

    Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        _logger = New AppLogger()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '//
        _dragAndDropControl = New DragAndDropControl(_logger, Me.RichTextBox_Main) '//RecieveするControl
        _dragAndDropControl.AddRecieveControl(Me) '//別コントロールにもイベントを追加
        _dragAndDropControl.AddRecieveControl(Me.RichTextBox_Main) '//別コントロールにもイベントを追加
        '//
        _dragAndDropFile = New DragAndDropFile(_logger, _dragAndDropControl)
        _dragAndDropControl._dragAndDropForFile = _dragAndDropFile
        AddHandler _dragAndDropFile.DragAndDropEventAfterEventForFile, AddressOf DragAndDropEvent
        '//
        _realTimeFileReader = New RealTimeFileReader(_logger, "")
        '//
        _findString = New FindStringForm(_logger, UserControlFindString1, MenuStrip1)
        _findString.InitForm(Me)
        AddHandler UserControlFindString1.Button_Next.Click, AddressOf FindStringNext
        AddHandler UserControlFindString1.Button_Prev.Click, AddressOf FindStringPrev

        _findString.HideFindControl()
    End Sub

    Public Sub FitTextBox()
        _logger.AddLog("FitTextBox")
        Me.RichTextBox_Main.Location = New Point(0, 0)
        _logger.AddLog("RichTextBox_Main.Location = (0, 0)")
        Me.RichTextBox_Main.Size = Me.ClientSize()
        _logger.AddLog("Me.RichTextBox_Main.Size = " + Me.ClientSize().ToString())
    End Sub
    Public Sub FitTextBoxWithFindBar()
        _logger.AddLog("FitTextBoxWithFindBar")
        Dim findBarH = _findString.GetSizeParent().Height
        Me.RichTextBox_Main.Location = New Point(0, findBarH)
        _logger.AddLog($"RichTextBox_Main.Location = (0, {findBarH})")
        Dim w = Me.ClientSize().Width
        Dim h = Me.ClientSize().Height - findBarH
        Me.RichTextBox_Main.Size = New Size(w, h)
        _logger.AddLog("Me.RichTextBox_Main.Size = " + New Size(w, h).ToString())
    End Sub

    ' RichTextBoxのクリックイベントでカーソル位置を取得
    Private Sub RichTextBox_Main_Click(sender As Object, e As EventArgs) Handles RichTextBox_Main.Click
        ' 現在のキャレット位置を取得
        Dim caretPosition As Integer = RichTextBox_Main.SelectionStart

        ' キャレット位置を表示
        'MessageBox.Show($"現在のキャレット位置: {caretPosition}", "キャレット位置", MessageBoxButtons.OK, MessageBoxIcon.Information)
        _logger.PrintInfo($"現在のキャレット位置: {caretPosition}")
    End Sub

    Private Sub FindStringNext(sender As Object, e As EventArgs)
        _logger.PrintInfo("FindStringNext")
        Dim BeforePos = RichTextBox_Main.SelectionStart
        '//
        If 0 < _findString._selectLength Then
            '//現在の色を解除（カーソル位置が変わるので注意）
            RichTextBox_Main.Select(_findString._selectPosStart, _findString._selectLength)
            ' 選択部分を強調表示（背景色変更など）
            RichTextBox_Main.SelectionBackColor = Color.White
            ' カーソルを可視化
            RichTextBox_Main.ScrollToCaret()
        End If
        '//
        Dim addPos As Integer
        If 0 < _findString._selectPosStart Then '以前見つかった
            addPos = 1
        End If
        '//
        Dim resultCode = _findString.ExecuteNext(
            RichTextBox_Main.Text,
            UserControlFindString1.TextBox1.Text,
            BeforePos + addPos)
        _logger.PrintInfo(String.Format("result= {0}, {1}", _findString._selectPosStart, _findString._selectLength))
        '//
        If resultCode = FindStringForm.ConstFindString.RESULT_MATCH_TRUE Then
            RichTextBox_Main.Select(_findString._selectPosStart, _findString._selectLength)
            RichTextBox_Main.SelectionStart = _findString._selectPosStart
            ' 選択する文字数を設定
            RichTextBox_Main.SelectionLength = _findString._selectLength
            ' 選択部分を強調表示（背景色変更など）
            RichTextBox_Main.SelectionBackColor = Color.Yellow
            ' カーソルを可視化
            RichTextBox_Main.ScrollToCaret()
        Else
            RichTextBox_Main.Select(_findString._selectPosStart, _findString._selectLength)
            RichTextBox_Main.SelectionStart = BeforePos
            ' 選択する文字数を設定
            RichTextBox_Main.SelectionLength = 0
            ' 選択部分を強調表示（背景色変更など）
            RichTextBox_Main.SelectionBackColor = Color.White
            ' カーソルを可視化
            RichTextBox_Main.ScrollToCaret()
        End If
    End Sub

    Private Sub FindStringPrev()
        _logger.PrintInfo("FindStringPrev")

        Dim BeforePos = RichTextBox_Main.SelectionStart
        '//
        If 0 < _findString._selectLength Then
            '//現在の色を解除（カーソル位置が変わるので注意）
            RichTextBox_Main.Select(_findString._selectPosStart, _findString._selectLength)
            ' 選択部分を強調表示（背景色変更など）
            RichTextBox_Main.SelectionBackColor = Color.White
            ' カーソルを可視化
            RichTextBox_Main.ScrollToCaret()
            '//
        End If
        Dim addPos As Integer
        If 0 < _findString._selectPosStart Then '以前見つかった
            addPos = -1
        End If

        '//
        Dim resultCode = _findString.ExecutePrev(
            RichTextBox_Main.Text,
            UserControlFindString1.TextBox1.Text,
            BeforePos + addPos)
        _logger.PrintInfo(String.Format("result= {0}, {1}", _findString._selectPosStart, _findString._selectLength))
        '//
        If resultCode = FindStringForm.ConstFindString.RESULT_MATCH_TRUE Then
            RichTextBox_Main.Select(_findString._selectPosStart, _findString._selectLength)
            RichTextBox_Main.SelectionStart = _findString._selectPosStart
            ' 選択する文字数を設定
            RichTextBox_Main.SelectionLength = _findString._selectLength
            ' 選択部分を強調表示（背景色変更など）
            RichTextBox_Main.SelectionBackColor = Color.Yellow
            ' カーソルを可視化
            RichTextBox_Main.ScrollToCaret()
        Else
            RichTextBox_Main.Select(_findString._selectPosStart, _findString._selectLength)
            RichTextBox_Main.SelectionStart = BeforePos
            ' 選択する文字数を設定
            RichTextBox_Main.SelectionLength = 0
            ' 選択部分を強調表示（背景色変更など）
            RichTextBox_Main.SelectionBackColor = Color.White
            ' カーソルを可視化
            RichTextBox_Main.ScrollToCaret()
        End If
    End Sub

    Private Sub UpdateRichTextBox(content As String)
        If RichTextBox_Main.InvokeRequired Then
            RichTextBox_Main.Invoke(New Action(Sub() RichTextBox_Main.Text = content))
        Else
            RichTextBox_Main.Text = content
        End If
    End Sub


    'Private Sub UpdateRichTextBoxContent(content As String)
    '    ' 現在のカーソル位置を取得
    '    Dim currentCaretPosition As Integer = RichTextBox_Main.SelectionStart
    '    Dim isCaretAtEnd As Boolean = (currentCaretPosition = RichTextBox_Main.Text.Length)

    '    ' 現在の行数を取得
    '    Dim currentLineIndex As Integer = RichTextBox_Main.GetLineFromCharIndex(currentCaretPosition)
    '    Dim lineOffset As Integer = currentCaretPosition - RichTextBox_Main.GetFirstCharIndexFromLine(currentLineIndex)

    '    ' RichTextBox の内容を更新
    '    RichTextBox_Main.Text = content

    '    ' カーソル位置を調整
    '    If isCaretAtEnd Then
    '        ' カーソルを末尾に移動
    '        RichTextBox_Main.SelectionStart = RichTextBox_Main.Text.Length
    '    Else
    '        ' 同じ行数または文字数の位置にカーソルを移動
    '        Dim newLineStart As Integer = RichTextBox_Main.GetFirstCharIndexFromLine(Math.Min(currentLineIndex, RichTextBox_Main.Lines.Length - 1))
    '        RichTextBox_Main.SelectionStart = Math.Min(newLineStart + lineOffset, RichTextBox_Main.Text.Length)
    '    End If

    '    ' カーソルを可視化
    '    RichTextBox_Main.ScrollToCaret()
    'End Sub

    Private Sub UpdateRichTextBoxContent(content As String)
        If RichTextBox_Main.InvokeRequired Then
            RichTextBox_Main.Invoke(New Action(Sub() UpdateRichTextBoxContent(content)))
        Else
            ' 現在のカーソル位置を取得
            Dim currentCaretPosition As Integer = RichTextBox_Main.SelectionStart
            Dim isCaretAtEnd As Boolean = (currentCaretPosition = RichTextBox_Main.Text.Length)

            ' 現在の行数を取得
            Dim currentLineIndex As Integer = RichTextBox_Main.GetLineFromCharIndex(currentCaretPosition)
            Dim lineOffset As Integer = currentCaretPosition - RichTextBox_Main.GetFirstCharIndexFromLine(currentLineIndex)

            ' RichTextBox の内容を更新
            RichTextBox_Main.Text = content

            ' カーソル位置を調整
            If isCaretAtEnd Then
                ' カーソルを末尾に移動
                RichTextBox_Main.SelectionStart = RichTextBox_Main.Text.Length
            Else
                ' 同じ行数または文字数の位置にカーソルを移動
                Dim newLineStart As Integer = RichTextBox_Main.GetFirstCharIndexFromLine(Math.Min(currentLineIndex, RichTextBox_Main.Lines.Length - 1))
                RichTextBox_Main.SelectionStart = Math.Min(newLineStart + lineOffset, RichTextBox_Main.Text.Length)
            End If

            ' カーソルを可視化
            RichTextBox_Main.ScrollToCaret()
        End If
    End Sub




    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Dispose the file reader
        If _realTimeFileReader IsNot Nothing Then
            _realTimeFileReader.Dispose()
        End If
    End Sub

    Sub DragAndDropEvent(sender As Object, e As EventArgs)
        Try
            _logger.AddLog(Me, " > DragAndDropFileEvent")

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
            If (Directory.Exists(targetPath)) Then
                _logger.PrintInfo("Path Is Directory")
                Return
            End If
            If Not _realTimeFileReader Is Nothing Then
                _realTimeFileReader.Dispose()
                _realTimeFileReader = Nothing
            End If
            _realTimeFileReader = New RealTimeFileReader(_logger, targetPath)
            ' Subscribe to the FileUpdated event
            'AddHandler _realTimeFileReader.FileUpdated, AddressOf UpdateRichTextBox
            AddHandler _realTimeFileReader.FileUpdated, AddressOf UpdateRichTextBoxContent
            _realTimeFileReader.MonitorFile()

            Dim content As String = IO.File.ReadAllText(_realTimeFileReader._fileName)
            '_logger.PrintInfo("File updated. New content:")
            '_logger.PrintInfo(content)
            _logger.PrintInfo("File updated. New content length:" + content.Length.ToString())
            UpdateRichTextBox(content)
            ' カーソルを末尾に移動
            RichTextBox_Main.SelectionStart = RichTextBox_Main.Text.Length

            Me.Text = " " + Path.GetFileName(_realTimeFileReader._fileName)

        Catch ex As Exception
            _logger.AddException(ex, Me, "DragAndDropFileEvent")
        End Try
    End Sub

End Class
