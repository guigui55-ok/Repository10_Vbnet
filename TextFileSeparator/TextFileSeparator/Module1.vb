Imports System.IO
Imports System.Text.RegularExpressions

Module Module1

    ' 設定情報
    Dim sourcefileName = "secs_messages.txt"
    Dim dirPath = AppDomain.CurrentDomain.BaseDirectory

    ' 入力ファイル
    Dim sourceFilePath As String = Path.Combine(dirPath, sourcefileName)
    ' 出力フォルダ
    Dim outputFolder As String = Path.Combine(dirPath, "output")

    ' 除外対象（無視する行）
    Dim ignoreList As String() = {
        "^\s*$", ' 空行
        "^#.*" ' #で始まるコメント行
    }

    ' 分割ルール
    Dim splitList As String() = {
        "^【送信】",
        "^【受信】"
    }

    Sub Main()
        Dim i As Integer
        Try
            OutputLog("処理開始")

            OutputLog("dirPath = " + dirPath)
            OutputLog("sourcefileName = " + sourcefileName)
            OutputLog("outputFolder = " + outputFolder)

            ' 出力フォルダを作成
            If Not Directory.Exists(outputFolder) Then
                Directory.CreateDirectory(outputFolder)
                OutputLog("出力フォルダ作成: " & outputFolder)
            End If

            ' ファイル読み込み
            Dim lines As String() = File.ReadAllLines(sourceFilePath)
            OutputLog("ファイル読み込み完了: " & sourceFilePath)

            ' 行を処理
            Dim editLinesList As New List(Of EditLines)()
            Dim nowRow As Integer = 0
            Dim currentEditLines As EditLines = Nothing

            For i = 0 To lines.Length - 1
                Dim line As String = lines(i)

                ' ignoreList にマッチするか確認（スキップ）
                If ignoreList.Any(Function(pattern) Regex.IsMatch(line, pattern)) Then
                    OutputLog(String.Format("[{0:D2}] ", i) & "無視: " & line)
                    Continue For
                End If

                ' splitList にマッチするか確認
                If splitList.Any(Function(pattern) Regex.IsMatch(line, pattern)) Then
                    ' 現在のデータを保存
                    If currentEditLines IsNot Nothing Then
                        editLinesList.Add(currentEditLines)
                    End If

                    ' 新しい分割セクションの作成
                    currentEditLines = New EditLines()
                    currentEditLines.MatchStr = line ' マッチした行を保存
                    OutputLog(String.Format("[{0:D2}] ", i) & "分割マッチ: " & line)
                End If

                ' 現在のデータに行を追加
                If currentEditLines Is Nothing Then
                    currentEditLines = New EditLines()
                End If
                currentEditLines.Lines.Add(line)
            Next

            ' 最後のブロックをリストに追加
            If currentEditLines IsNot Nothing Then
                editLinesList.Add(currentEditLines)
            End If

            ' 分割データをファイルに出力
            OutputLog("ファイル出力開始")

            For i = 0 To editLinesList.Count - 1
                Dim editLines As EditLines = editLinesList(i)
                Dim safeFileName As String = Regex.Replace(editLines.MatchStr, "[^\w]", "_") ' ファイル名に使えない文字を置換
                Dim fileName As String = $"{i + 1}_{safeFileName}.txt"
                Dim filePath As String = Path.Combine(outputFolder, fileName)

                File.WriteAllText(filePath, editLines.LinsToString())
                OutputLog("出力完了: " & Path.GetFileName(filePath))
            Next

            OutputLog("処理完了")
        Catch ex As Exception
            OutputLog("エラー発生: " & ex.Message)
            OutputLog("index: " & String.Format("[{0}] ", i))
        End Try
        Console.ReadKey()
    End Sub

    ' ログ出力
    Public Sub OutputLog(value As String)
        Console.WriteLine(value)
        Debug.WriteLine(value) ' デバッグ用
    End Sub
End Module


' EditLines クラス
Class EditLines
    Public MatchStr As String
    Public Lines As List(Of String)

    Sub New()
        Me.MatchStr = ""
        Me.Lines = New List(Of String)
    End Sub

    ''' <summary>
    ''' lines を繋げて文字列にする
    ''' </summary>
    ''' <returns>改行付きの文字列</returns>
    Public Function LinsToString() As String
        Dim ary() As String = {}
        ary = Lines.ToArray()
        Dim retLines As List(Of String) = ary.ToList()
        If retLines.Count < 1 Then
            Return ""
        End If
        retLines.RemoveAt(0)
        Return String.Join(Environment.NewLine, retLines)
    End Function
End Class