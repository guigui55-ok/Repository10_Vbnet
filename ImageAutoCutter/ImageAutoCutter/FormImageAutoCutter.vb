Public Class FormImageAutoCutter


    '予定
    'DragDrop
    'Save_Load_Input
    'LogForm
    'LogFile

    '予定2
    'サブウィンドウ表示ボタンを設置
    'サブウィンドウ（Form）に
    'コメント、textboxと
    'ファイルリスト（リストボックス）と、
    '矢印キー上下（Button）と、
    'プレビュー（PictureBox）と、
    'フォトで表示ボタンと、
    '実行ボタンを配置

    'リストボックスの中のファイル名を選択すると、プレビューに表示、プレビューはウィンドウの大きさに合わせてFitする（フォトで表示ボタンで、フォトで表示（拡大などしたいときはこれで確認）
    '上下キーでファイルリストを移動、プレビューを変更（マウスクリックの時もプレビューを変更）
    '実行ボタンで、リストボックスのファイル名1つのみ、カットを実行（設定はメインウィンドウのものを使用する）
    '書き込みファイル名の後に、コメント（TextBox）を付与する


    Public _logger As AppLogger
    Public _mainProc As ImageAutoCutterProc

    Private Sub FormImageAutoCutter_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        _logger = New AppLogger()
        _mainProc = New ImageAutoCutterProc(_logger)

        'test set
        Dim rpath = "C:\Users\OK\Pictures\250712_testImage"
        TextBox_InputDirPath.Text = rpath
        TextBox_RegexPattern.Text = "image_2025*"
        TextBox_CutSize.Text = "653, 228"
        Dim tempPath = "C:\Users\OK\Pictures\250712_testImage\_image_2025_07_12_083409_531_templete.png"
        TextBox_TempletePath.Text = tempPath
        TextBox_Threshold.Text = "0.9"
    End Sub

    Private Sub Button_FindFile_Click(sender As Object, e As EventArgs) Handles Button_FindFile.Click
        _logger.Info("*Button_FindFile_Click")

        'get
        Dim inDirStr = TextBox_InputDirPath.Text
        Dim patStr = TextBox_RegexPattern.Text
        Dim outDIrStr = TextBox_OutputDir.Text

        'find
        _mainProc.FindPaths(inDirStr, patStr)

        'set richText
        Dim fNameList As List(Of String) = _mainProc._fileNameList
        Dim rt = RichTextBox_FileList
        Dim dataStr = String.Join(vbCrLf, fNameList)
        rt.Text = dataStr
        rt.ScrollToCaret()
    End Sub

    Private Sub Button_Execute_Click(sender As Object, e As EventArgs) Handles Button_Execute.Click
        'cnv
        _logger.Info($"TextBox_CutSize.Text = {TextBox_CutSize.Text}")
        Dim cutSize = ConvertStringToPoint(TextBox_CutSize.Text)
        _logger.Info($"cutSize = {cutSize}")

        'thirethold
        Dim threshold = 0.0
        Dim buf = TextBox_Threshold.Text
        If IsNumeric(buf) Then
            threshold = Double.Parse(buf)
        End If
        _logger.Info($"threshold = {threshold}")

        'out dir
        Dim outDirPath = SetAutoOutputDirPath()
        _logger.Info("outDirPath = {outDirPath}")

        'do
        _mainProc.ExecuteCutAutoMain(TextBox_TempletePath.Text, cutSize, threshold, outDirPath)
    End Sub


    Private Function SetAutoOutputDirPath() As String
        Dim outDirPath = ""
        If CheckBox_AutoInputOutputDir.Checked Then
            Dim inDirPath = TextBox_InputDirPath.Text
            Dim inDirParent = IO.Path.GetDirectoryName(inDirPath)
            Dim outDirName = IO.Path.GetFileName(inDirPath) + "_cut_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_fff")
            outDirPath = IO.Path.Combine(inDirParent, outDirName)

            TextBox_OutputDir.Text = outDirPath
        Else
            outDirPath = TextBox_OutputDir.Text
        End If
        Return outDirPath
    End Function

#Region "support"
    ''' <summary>
    ''' "500,600" のような文字列を Point 型に変換します。
    ''' </summary>
    ''' <param name="value">"X,Y" の形式の文字列</param>
    ''' <returns>Point 構造体（変換失敗時は Point.Empty）</returns>
    Public Function ConvertStringToPoint(value As String) As Point
        If String.IsNullOrWhiteSpace(value) Then
            Return Point.Empty
        End If

        Dim parts = value.Split(","c)
        If parts.Length <> 2 Then
            Return Point.Empty
        End If

        Dim x As Integer
        Dim y As Integer

        If Integer.TryParse(parts(0).Trim(), x) AndAlso Integer.TryParse(parts(1).Trim(), y) Then
            Return New Point(x, y)
        End If

        Return Point.Empty
    End Function
#End Region
End Class
