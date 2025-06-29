
Imports System.Diagnostics
Imports System.IO

Module ModulePathUtil

    Public Class FileExplorerHelper
        ''' <summary>
        ''' 指定されたパスをエクスプローラーで開く
        ''' </summary>
        ''' <param name="path">開きたいフォルダまたはファイルのパス</param>
        Public Shared Sub OpenInExplorer(path As String)
            If String.IsNullOrWhiteSpace(path) Then
                Throw New ArgumentException("パスが指定されていません。")
            End If

            If Directory.Exists(path) Then
                ' フォルダの場合はそのまま開く
                Process.Start("explorer.exe", path)
            ElseIf File.Exists(path) Then
                ' ファイルの場合はそのファイルを選択状態で開く
                Process.Start("explorer.exe", "/select,""" & path & """")
            Else
                Throw New FileNotFoundException("指定されたパスが存在しません: " & path)
            End If
        End Sub
    End Class
End Module
