Imports System.IO
Imports System.Text.RegularExpressions

Public Class ManyFileRenamerClass
    Public ignoreList As List(Of String) = New List(Of String)()
    Public IncludeList As List(Of String) = New List(Of String)()
    Public TargetInfoList As List(Of RenamePathInfo) = New List(Of RenamePathInfo)()

    ' IncludeList に一致するか判定
    Private Function IsMatchInclude(srcPath As String) As Boolean
        If IncludeList.Count = 0 Then Return True ' 空ならすべて許可
        For Each pattern In IncludeList
            If Regex.IsMatch(Path.GetFileName(srcPath), pattern) Then
                Return True
            End If
        Next
        Return False
    End Function

    ' IgnoreList に一致するか判定
    Private Function IsMatchIgnore(srcPath As String) As Boolean
        If ignoreList.Count < 1 Then
            Return False
        End If
        For Each pattern In ignoreList
            If pattern = "" Then
                Continue For
            End If
            If Regex.IsMatch(Path.GetFileName(srcPath), pattern) Then
                Return True
            End If
        Next
        Return False
    End Function

    ' リネーム対象リストに追加
    Public Sub AddPathList_(srcPath As String, distPath As String)
        'Undoの場合は見つからない
        'If Not File.Exists(srcPath) Then
        '    OutputLog($"ファイルが見つかりません: {srcPath}")
        '    Return
        'End If

        If Not IsMatchInclude(srcPath) Then
            OutputLog($"Includeリストに含まれないためスキップ: {srcPath}")
            Return
        End If

        If IsMatchIgnore(srcPath) Then
            OutputLog($"Ignoreリストに一致: {srcPath}")
            Return
        End If

        TargetInfoList.Add(New RenamePathInfo With {.SrcPath = srcPath, .DistPath = distPath})
        OutputLog($"リネーム対象追加: {srcPath} → {distPath}")
    End Sub

    ' ファイルリネーム処理 (通常 & Undo)
    Public Sub ProcessRename(isUndo As Boolean)
        For Each info In TargetInfoList
            Dim srcPath As String = If(isUndo, info.DistPath, info.SrcPath)
            Dim distPath As String = If(isUndo, info.SrcPath, info.DistPath)

            Try
                If Not File.Exists(srcPath) Then
                    OutputLog($"リネーム元が見つからないためスキップ: {srcPath}")
                    Continue For
                End If

                If File.Exists(distPath) Then
                    OutputLog($"既に存在するためスキップ: {distPath}")
                    Continue For
                End If

                File.Move(srcPath, distPath)
                OutputLog(If(isUndo, $"Undo成功: {srcPath} → {distPath}", $"リネーム成功: {srcPath} → {distPath}"))

            Catch ex As Exception
                OutputLog(If(isUndo, $"Undo失敗: {srcPath} → {distPath}, エラー: {ex.Message}", $"リネーム失敗: {srcPath} → {distPath}, エラー: {ex.Message}"))
            End Try
        Next
    End Sub

    ' 通常のリネーム処理
    Public Sub ExecuteRename()
        ProcessRename(False)
    End Sub

    ' Undoリネーム処理
    Public Sub UndoRename()
        ProcessRename(True)
    End Sub

    ' リネーム対象の情報クラス
    Public Class RenamePathInfo
        Public SrcPath As String
        Public DistPath As String
    End Class
End Class
