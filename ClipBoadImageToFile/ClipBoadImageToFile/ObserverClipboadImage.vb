Imports System.Windows.Forms
Imports System.Collections.Specialized

Public Class ObserverClipboard
    Private WithEvents timer As Timer
    Private lastText As String = ""
    Private lastImageHash As Integer = 0
    Private lastFileListHash As Integer = 0

    <Flags()>
    Public Enum WatchMode
        None = 0
        Text = 1
        Image = 2
        FileDrop = 4
    End Enum

    Private _mode As WatchMode = WatchMode.Text

    Public Property Mode As WatchMode
        Get
            Return _mode
        End Get
        Set(value As WatchMode)
            _mode = value
        End Set
    End Property

    Public Event ClipboardTextChanged(newText As String)
    Public Event ClipboardImageChanged(newImage As Image)
    Public Event ClipboardFileDropChanged(fileList As StringCollection)

    Public Sub New(intervalMs As Integer, Optional mode As WatchMode = WatchMode.Text)
        timer = New Timer()
        timer.Interval = intervalMs
        _mode = mode
    End Sub

    Public Sub StartWatching()
        timer.Start()
    End Sub

    Public Sub StopWatching()
        timer.Stop()
    End Sub

    Private Sub timer_Tick(sender As Object, e As EventArgs) Handles timer.Tick
        Try
            If (_mode And WatchMode.Text) = WatchMode.Text Then
                If Clipboard.ContainsText() Then
                    Dim currentText = Clipboard.GetText()
                    If currentText <> lastText Then
                        lastText = currentText
                        RaiseEvent ClipboardTextChanged(currentText)
                    End If
                End If
            End If

            If (_mode And WatchMode.Image) = WatchMode.Image Then
                If Clipboard.ContainsImage() Then
                    Dim currentImage = Clipboard.GetImage()
                    If currentImage IsNot Nothing Then
                        Dim hash = currentImage.GetHashCode()
                        If hash <> lastImageHash Then
                            lastImageHash = hash
                            RaiseEvent ClipboardImageChanged(currentImage)
                        End If
                    End If
                End If
            End If

            If (_mode And WatchMode.FileDrop) = WatchMode.FileDrop Then
                If Clipboard.ContainsFileDropList() Then
                    Dim fileList = Clipboard.GetFileDropList()
                    Dim hash = GetFileListHash(fileList)
                    If hash <> lastFileListHash Then
                        lastFileListHash = hash
                        RaiseEvent ClipboardFileDropChanged(fileList)
                    End If
                End If
            End If
        Catch ex As Exception
            ' Clipboardがロックされているなどの一時的な失敗を無視
        End Try
    End Sub

    ' ファイルリストの内容を簡易的にハッシュ化
    Private Function GetFileListHash(fileList As StringCollection) As Integer
        Dim combined As String = String.Join("|", fileList.Cast(Of String)())
        Return combined.GetHashCode()
    End Function
End Class
