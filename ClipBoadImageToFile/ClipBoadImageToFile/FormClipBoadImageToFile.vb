Imports System.IO

Public Class FormClipBoadImageToFile

    Dim _logger As Logger
    Dim _observer As ObserverClipboard
    Dim _savedImage As Image
    Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        _logger = New Logger()
        _observer = New ObserverClipboard(1000,
            ObserverClipboard.WatchMode.Text Or
            ObserverClipboard.WatchMode.Image Or
            ObserverClipboard.WatchMode.FileDrop)

        AddHandler _observer.ClipboardTextChanged, AddressOf OnClipboardTextChanged
        AddHandler _observer.ClipboardImageChanged, AddressOf OnClipboardImageChanged
        AddHandler _observer.ClipboardFileDropChanged, AddressOf OnClipboardFileDropChanged

        _observer.StartWatching()

        TextBox_FileName.Text = ""
        Dim extList = New List(Of String) From {
            "png", "jpg", "bmp"}
        ComboBox_Ext.Items.AddRange(extList.ToArray())
        ComboBox_Ext.SelectedIndex = 0

        Dim appPath As String = System.Reflection.Assembly.GetExecutingAssembly().Location
        Dim dirPath As String = Path.GetDirectoryName(appPath)
        dirPath = Path.Combine(dirPath, "Image")
        Directory.CreateDirectory(dirPath)
        TextBox_DirPath.Text = dirPath

    End Sub
    Private Sub OnClipboardTextChanged(newText As String)
        _logger.Print($"Changed ClipBoad Text: '{newText}'")
    End Sub
    Private Sub OnClipboardImageChanged(newImage As Image)
        If TypeOf _savedImage Is Image Then
            ResetSizeText(_savedImage)
        End If
        If ImageRelation.AreImagesEqual(newImage, _savedImage) Then
            Exit Sub
        End If
        _savedImage = newImage
        _logger.Print($"Changed ClipBoad Image: {_savedImage.Size.ToString()}")
        ResetSizeText(_savedImage)
    End Sub

    Private Sub OnClipboardFileDropChanged(fileList As Specialized.StringCollection)
        Dim paths As String = String.Join(Environment.NewLine, fileList.Cast(Of String)())
        _logger.Print($"Changed ClipBoad Path: { vbCrLf & paths}")
    End Sub

    Private Sub ResetSizeText(inputImage As Image)
        If inputImage Is Nothing Then
            Label_Size.Text = "Size : -"
            Label_ClipboadState.Text = "ClipBoadImage : FALSE"
        End If
        Label_Size.Text = "Size : " + inputImage.Size.ToString()
        Label_ClipboadState.Text = "ClipBoadImage : TRUE"
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_Ext.SelectedIndexChanged

    End Sub

    Private Sub FormClipBoadImageToFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button_Save_Click(sender As Object, e As EventArgs) Handles Button_Save.Click
        _logger.Print("Button_Save_Click")
        If _savedImage Is Nothing Then
            _logger.Print("_savedImage Is Nothing")
            Exit Sub
        End If
        Dim fileName As String = TextBox_FileName.Text
        If fileName = "" Then
            fileName = DateTime.Now.ToString("yy-MM-dd HH-mm-fff") + "_Image"
        End If
        fileName += "." + ComboBox_Ext.Text
        If Not IsValidFileName(fileName) Then
            _logger.Print("IsInvalidFileName=False, " + fileName)
            Exit Sub
        End If
        Dim savePath = Path.Combine(TextBox_DirPath.Text, fileName)
        Dim ex As Exception = Nothing
        ImageRelation.SaveImageToFile(_savedImage, savePath, ex)
        If Not ex Is Nothing Then
            _logger.Print("## ImageRelation.SaveImageToFile Failed")
            _logger.Print(ex.GetType().ToString() + ":" + ex.Message)
            _logger.Print(ex.StackTrace)
        End If
        _logger.Print("Saved Image." + vbCrLf + savePath)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button_Open.Click
        If Not Directory.Exists(TextBox_DirPath.Text) Then
            _logger.Print("not Directory.Exists :" + TextBox_DirPath.Text)
            Exit Sub
        End If
        System.Diagnostics.Process.Start(TextBox_DirPath.Text)
    End Sub


    '**********************
    ''' <summary>
    ''' 指定された文字列がファイル名として有効かどうかを判定します。
    ''' </summary>
    ''' <param name="fileName">判定対象の文字列</param>
    ''' <returns>有効ならTrue、無効ならFalse</returns>
    Public Function IsValidFileName(fileName As String) As Boolean
        ' 無効なファイル名に使えない文字
        Dim invalidChars As Char() = System.IO.Path.GetInvalidFileNameChars()

        ' 無効文字が含まれていないか判定
        If fileName.IndexOfAny(invalidChars) >= 0 Then
            Return False
        End If

        ' 空文字や長すぎるファイル名も無効
        If String.IsNullOrWhiteSpace(fileName) Then
            Return False
        End If

        ' Windowsの最大ファイル名長（255文字）を超えていないか
        If fileName.Length > 255 Then
            Return False
        End If

        ' ファイル名として禁止されている予約名をチェック（CON, PRN, AUX, NUL, COM1～COM9, LPT1～LPT9）
        Dim reservedNames As String() = {
            "CON", "PRN", "AUX", "NUL",
            "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
            "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
        }

        Dim upperFileName As String = System.IO.Path.GetFileNameWithoutExtension(fileName).ToUpperInvariant()
        If reservedNames.Contains(upperFileName) Then
            Return False
        End If

        Return True
    End Function
End Class
