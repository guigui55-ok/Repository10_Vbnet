Imports System.IO
Imports System.Text.RegularExpressions

Public Class Form1
    Const NEW_LINE As String = vbCrLf
    Dim _logger As SimpleLogger = New SimpleLogger()
    'テスト用、ファイル書き込み制御フラグ
    Dim _isWriteFile As Boolean = True
    'デバッグ用 ブレーク設定用に使用する変数
    Dim _break As String = ""

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AddVersionInfoToFormTitle()
        Dim currentDirectory As String = Directory.GetCurrentDirectory()
        Dim loggerPath As String = currentDirectory & "\__test_log.log"
        Debug.WriteLine("loggerPath: " & loggerPath)

        Me.TestDebug()
    End Sub

    Private Sub TestDebug()
        Me.TextBoxDirPath.Text = "C:\Users\OK\source\repos\Repository10_VBnet\TestProject01"
        Me.TextBoxOldName.Text = "TestProject01"
        Me.TextBoxNewName.Text = "TestVbBasic"
    End Sub

    Private Sub AddVersionInfoToFormTitle()
        '自分自身のバージョン情報を取得する
        Dim ver As System.Diagnostics.FileVersionInfo =
            System.Diagnostics.FileVersionInfo.GetVersionInfo(
            System.Reflection.Assembly.GetExecutingAssembly().Location)

        'Debug.WriteLine(ver.ToString())
        'File:   C : \Users\OK\source\repos\Repository10_VBnet\RenameProjectFileName\RenameProjectFileName\bin\Debug\netcoreapp3.1\RenameProjectFileName.dll
        'InternalName: RenameProjectFileName.dll
        'OriginalFilename: RenameProjectFileName.dll
        'FileVersion:      0.0.1.0
        'FileDescription: RenameProjectFileName
        'Product: RenameProjectFileName
        'ProductVersion:   0.0.1
        'Debug:            False
        'Patched:          False
        'PreRelease:       False
        'PrivateBuild:     False
        'SpecialBuild:     False
        'Language: ニュートラル言語

        'AssemblyProductの取得
        Dim asmprd As System.Reflection.AssemblyProductAttribute =
            CType(Attribute.GetCustomAttribute(
                System.Reflection.Assembly.GetExecutingAssembly(),
                GetType(System.Reflection.AssemblyProductAttribute)),
                    System.Reflection.AssemblyProductAttribute)

        MyBase.Text = asmprd.Product & " v" & CStr(ver.ProductVersion)
    End Sub

    '////////////////////////////////////////////////////////////////////////////////////////
    Private Sub ButtonExecute_Click(sender As Object, e As EventArgs) Handles ButtonExecute.Click
        '/
        'フォルダ名を変更したプロジェクトを他のプロジェクトから参照している場合は、未対応
        'これに対応する場合は、TextBos、Multiline=Trueにして、複数行のフォルダ内のvbprojファイルに対して、replaceを行う
        'プロジェクトファイル（.csproj／.vbprojなど）
        '/
        '宣言部
        Dim oldName As String = Me.TextBoxOldName.Text
        Dim newName As String = Me.TextBoxNewName.Text
        Dim ext As String
        Dim matchPathList As List(Of String)
        Dim targetPath As String
        Dim lines As String()
        Dim renamePath As String
        '/
        _logger.PrintInfo("==========") '区切り
        '/
        'slnファイル修正
        ext = ".sln"
        'matchPathList =
        '    Me.GetFileListInDirectoryByIncludeFileName(Me.TextBoxDirPath.Text, ext)
        matchPathList =
            Me.GetFileListInDirectoryByMatchRegixPattern(Me.TextBoxDirPath.Text, String.Format("\{0}$", ext))
        _logger.PrintInfo(String.Format("# ext = {0}, matchPathList.Count = {1}", ext, matchPathList.Count))
        If matchPathList.Count < 1 Then
            Me.ShowError(String.Format("DirPathに[{0}]ファイルが見つかりません", ext))
            Exit Sub
        End If
        targetPath = matchPathList(0)
        _logger.PrintInfo(String.Format("[{0}] Path = {1}", ext, targetPath))
        lines = Me.ReadFile(targetPath)
        lines = Me.ChangeReadFileArrayForSolutionFile(lines, oldName, newName)
        Me.WriteStringArrayToFile(targetPath, lines)
        renamePath = GetParentPath(targetPath) & "\" & newName & ext
        Me.RenameFile(targetPath, renamePath)
        '/
        'vbproj ファイル修正
        ext = ".vbproj"
        matchPathList =
            Me.GetFileListInDirectoryByMatchRegixPattern(Me.TextBoxDirPath.Text, String.Format("\{0}$", ext), subDirectory:=True)
        _logger.PrintInfo(String.Format("# ext = {0}, matchPathList.Count = {1}", ext, matchPathList.Count))
        If matchPathList.Count < 1 Then
            Me.ShowError(String.Format("DirPathに[{0}]ファイルが見つかりません", ext))
            Exit Sub
        End If
        targetPath = matchPathList(0)
        _logger.PrintInfo(String.Format("[{0}] Path = {1}", ext, targetPath))
        lines = Me.ReadFile(targetPath)
        lines = Me.ChangeReadFileArrayForProjectFile(lines, Me.TextBoxOldName.Text, Me.TextBoxNewName.Text)
        Me.WriteStringArrayToFile(targetPath, lines)
        renamePath = GetParentPath(targetPath) & "\" & newName & ext
        Me.RenameFile(targetPath, renamePath)
        '/
        targetPath = Me.TextBoxDirPath.Text & "\" & oldName
        renamePath = Me.TextBoxDirPath.Text & "\" & newName
        Me.RenameFile(targetPath, renamePath)
        '/
        _logger.PrintInfo("ButtonExecute_Click Done.")
    End Sub

    '////////////////////////////////////////////////////////////////////////////////////////
    'ファイル文字列処理
    Public Function ChangeReadFileArrayForProjectFile(array As String(), oldProjectName As String, newProjectName As String) As String()
        'ファイルから読み込んだ文字列に対して処理をする
        'プロジェクト情報を目的の情報に置き換える
        Dim buf As String = ""
        For i As Integer = 0 To array.Length - 1
            ' 行に "Project" という文字列が含まれているかチェック
            'If i = 5 Then
            '    Dim bufb As String = ""
            'End If
            '<RootNamespace>TestProject01</RootNamespace>
            buf = Me._ChangeProjectNameInLineForProjectFileFile_PatternA(
                array(i), oldProjectName, newProjectName)
            If buf <> "" Then
                array(i) = buf
            End If
        Next
        Return array
    End Function

    '読み込みfile文字列"1行"に対して、slnファイル用のプロジェクト名変更用文字列置換処理を行う（置換パターンA）
    Public Function _ChangeProjectNameInLineForProjectFileFile_PatternA(line As String, beforeStr As String, afterStr As String)
        Dim result_a As Boolean = Regex.IsMatch(line, "<RootNamespace>.*</RootNamespace>")
        If result_a Then
            _logger.PrintInfo("BeforeStr = " & line)
            Dim result As Match = Regex.Match(line, "<RootNamespace>.*</RootNamespace>")
            Dim buf As String = ""
            If result.Success Then
                buf = result.Value
                buf = buf.Replace(beforeStr, afterStr)
            Else
                _ChangeProjectNameInLineForProjectFileFile_PatternA = line
                Exit Function
            End If
            'Dim afterLine As String = String.Format("<RootNamespace>{0}</RootNamespace>", buf)
            Dim afterLine As String = buf
            _logger.PrintInfo(String.Format("AfterLine = {0}", afterLine))
            _ChangeProjectNameInLineForProjectFileFile_PatternA = afterLine
            Exit Function
        Else
            _ChangeProjectNameInLineForProjectFileFile_PatternA = line
            Exit Function
        End If
        _ChangeProjectNameInLineForProjectFileFile_PatternA = line
    End Function


    '読み込みfile文字列に対して、slnファイル用のプロジェクト名変更用文字列置換処理を行う
    Public Function ChangeReadFileArrayForSolutionFile(array As String(), oldProjectName As String, newProjectName As String) As String()
        'ファイルから読み込んだ文字列に対して処理をする
        'プロジェクト情報を目的の情報に置き換える
        Dim buf As String = ""
        For i As Integer = 0 To array.Length - 1
            ' 行に "Project" という文字列が含まれているかチェック
            'If i = 5 Then
            '    Dim bufb As String = ""
            'End If
            buf = Me._ChangeProjectNameInLineForSolutionFile_PatternA(array(i), oldProjectName, newProjectName)
            If buf <> "" Then
                array(i) = buf
            End If
        Next
        Return array
    End Function


    '読み込みfile文字列”1行”に対して、slnファイル用のプロジェクト名変更用文字列置換処理を行う（置換パターンA）
    Public Function _ChangeProjectNameInLineForSolutionFile_PatternA(line As String, beforeStr As String, afterStr As String)
        Dim result_a As Boolean = Regex.IsMatch(line, "Project\(""{.*}""\)")
        If result_a Then  'Project("{
            _logger.PrintInfo("BeforeStr = " & line)
            ' 文字列の最後に "★" を付与
            Dim result As Boolean = Regex.IsMatch("line", " """)
            Dim matchAry As String() = Me.GetMatchedArrayByRegix(" "".*""", line)
            'Project("{778DAE3C-4631-46EA-AA77-85C1314464D9}") = "TestVbBasic", "TestProject01\TestVbBasic.vbproj", "{07942975-0F0C-469D-A2C0-73D91537F77A}"
            'この文字列のダブルクオーテーションに囲まれた0番目、1番目の文字列をリプレイスする
            Dim afterLine As String = line
            Dim repStr As String = ""
            For i As Integer = 0 To matchAry.Length - 1
                If i = 2 Then Exit For
                repStr = matchAry(i).Replace(beforeStr, afterStr)
                afterLine = afterLine.Replace(matchAry(i), repStr)
            Next
            _logger.PrintInfo(String.Format("AfterLine = {0}", afterLine))
            _ChangeProjectNameInLineForSolutionFile_PatternA = afterLine
            Exit Function
        Else
            _ChangeProjectNameInLineForSolutionFile_PatternA = line
            Exit Function
        End If
        _ChangeProjectNameInLineForSolutionFile_PatternA = line
    End Function

    ''' <summary>
    ''' 正規表現でパターンにマッチした文字列の配列を取得する
    ''' </summary>
    ''' <param name="pattern"></param>
    ''' <param name="check_str"></param>
    ''' <returns></returns>
    Public Function GetMatchedArrayByRegix(pattern As String, check_str As String) As String()
        '"<(h[1-6])\b[^>]*>(.*?)</\1>"
        '正規表現パターンとオプションを指定してRegexオブジェクトを作成 
        Dim r As New System.Text.RegularExpressions.Regex(
            pattern,
            System.Text.RegularExpressions.RegexOptions.IgnoreCase Or
            System.Text.RegularExpressions.RegexOptions.Singleline)
        'TextBox1.Text内で正規表現と一致する対象をすべて検索 
        Dim mc As System.Text.RegularExpressions.MatchCollection =
            r.Matches(check_str)
        ' マッチした部分を格納するリスト
        Dim matchedValues As New List(Of String)

        '/
        '' mcの各要素を処理
        'For Each m As System.Text.RegularExpressions.Match In mc
        '    ' 各グループを処理（グループ0は全体なので1から始める）
        '    For i As Integer = 0 To m.Groups.Count - 1
        '        Debug.Print(m.Groups(i).GetType().ToString())
        '        Debug.Print(m.Groups(i).Value)
        '        If m.Success Then
        '            Dim loopCnt As Integer = 0
        '            Dim match As Match = m
        '            While m.Success
        '                matchedValues.Add(match.Value)
        '                match = match.NextMatch()
        '                loopCnt += 1
        '                If loopCnt Then Exit While
        '            End While
        '        End If
        '    Next
        'Next
        '/
        'Dim loopCount As Integer
        'Dim match As MatchCollection
        'match = mc
        'While loopCount > 1000

        '    match = match.Next
        '    loopCount += 1
        'End While
        '/
        ' マッチした値を格納するリスト
        'Dim matchedValues As New List(Of String)

        '' マッチした値をすべてリストに追加
        'While mc.su
        '    matchedValues.Add(Match.Value)
        '    Match = Match.NextMatch()
        'End While
        '/
        ' mcの各要素を処理
        For Each m As System.Text.RegularExpressions.Match In mc
            ' マッチした全体の文字列を取得
            Dim matchedString As String = m.ToString()

            ' カンマ区切りで分割してリストに追加
            Dim parts As String() = matchedString.Split(New String() {""", """}, StringSplitOptions.None)
            matchedValues.AddRange(parts)

            '' デバッグ出力（オプション）
            'For Each part As String In parts
            '    _logger.PrintInfo(part)
            'Next
        Next

        ' リストを文字列配列に変換
        Dim matchedArray As String() = matchedValues.ToArray()

        ' 配列の内容を出力
        matchedArray = StripArrayString(matchedArray, {""""})
        For Each value As String In matchedArray
            _logger.PrintInfo("Matched value: " & value)
        Next

        ' リストを配列に変換して返す
        Return matchedArray
    End Function

    '////////////////////////////////////////////////////////////////////////////////////////
    '文字列処理_共通
    ''' <summary>
    ''' 文字列配列の各要素の先頭と末尾から、空白文字（スペース、タブ、改行など）を削除する
    ''' </summary>
    ''' <param name="ary"></param>
    ''' <param name="addRemoveCharAry"></param>
    ''' <returns></returns>
    Public Function StripArrayString(ByVal ary As String(), Optional ByVal addRemoveCharAry As String() = Nothing) As String()
        For i As Integer = 0 To ary.Length - 1
            ary(i) = Strip(ary(i), addRemoveCharAry)
        Next
        Return ary
    End Function

    ''' <summary>
    ''' 文字列の先頭と末尾から、空白文字（スペース、タブ、改行など）を削除する
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="AddRemoveCharAry"></param>
    ''' <returns></returns>
    Public Function Strip(ByVal value As String, Optional ByVal AddRemoveCharAry As String() = Nothing) As String
        Dim RemoveList As List(Of String) = New List(Of String)({" ", """", vbCrLf})
        If Not AddRemoveCharAry Is Nothing Then
            RemoveList.AddRange(AddRemoveCharAry)
        End If
        Dim _value As String = value
        Dim isChanged As Boolean = False
        For Each rmChar As String In RemoveList
            If _value.StartsWith(rmChar) Then
                _value = _value.Substring(1, Len(_value) - 1)
                If Not isChanged Then isChanged = True
            End If
            If _value.EndsWith(rmChar) Then
                _value = _value.Substring(0, Len(_value) - 1)
                If Not isChanged Then isChanged = True
            End If
        Next
        If isChanged Then
            '変更したらさらにもう一度実行する
            _value = Strip(_value)
        End If
        Return _value
    End Function
    '////////////////////////////////////////////////////////////////////////////////////////
    'ファイル処理
    ''' <summary>
    ''' ディレクトリのパスを取得する（引数がディレクトリパスの場合はその親のパスを取得する）
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <returns></returns>
    Function GetParentPath(filePath As String) As String
        Dim parentPath As String
        If File.Exists(filePath) Then
            'FIle
            parentPath = Path.GetDirectoryName(filePath)
        Else
            'Directory
            parentPath = System.IO.Path.GetDirectoryName(filePath.TrimEnd(System.IO.Path.DirectorySeparatorChar))
            parentPath = System.IO.Path.GetDirectoryName(parentPath)
        End If
        Return parentPath
    End Function


    ''' <summary>
    ''' ファイル・フォルダをリネームする
    ''' </summary>
    ''' <param name="oldFilePath"></param>
    ''' <param name="newFilePath"></param>
    ''' <returns></returns>
    Function RenameFile(oldFilePath As String, newFilePath As String)
        RenameFile = False
        Try
            If File.Exists(oldFilePath) Then
                File.Move(oldFilePath, newFilePath)
                _logger.PrintInfo("ファイル名が正常に変更されました。")
                _logger.PrintInfo(String.Format("Path {0}, => {1}", oldFilePath, Path.GetFileName(newFilePath)))
                Return True
                Exit Function
            Else
                'Me.ShowError("指定されたファイルが存在しません。")
            End If
        Catch ex As Exception
            Me.ShowError("エラーが発生しました: " & vbCrLf & ex.Message)
        End Try
        Try
            If Directory.Exists(oldFilePath) Then
                Directory.Move(oldFilePath, newFilePath)
                _logger.PrintInfo("ディレクトリ名が正常に変更されました。")
                _logger.PrintInfo(String.Format("Path {0}, => {1}", oldFilePath, Path.GetFileName(newFilePath)))
                Return True
                Exit Function
            Else
                _logger.PrintInfo("指定されたファイル、または、ディレクトリが存在しません。")
                _logger.PrintInfo(String.Format("oldFilePath = {0}", oldFilePath))
            End If
        Catch ex As Exception
            Me.ShowError("エラーが発生しました: " & vbCrLf & ex.Message)
        End Try
        Return False
    End Function

    Public Function ReadFile(filePath As String) As String()
        'Imports System.IO
        ' 読み込むファイルのパスを指定
        ' ファイルが存在するか確認
        If Not File.Exists(filePath) Then
            Dim msg As String = "ファイルが存在しません: " & NEW_LINE & filePath
            Me.ShowError(msg)
            ReadFile = New String() {}
            Exit Function
        End If
        ReadFile = File.ReadLines(filePath).ToArray()
        Exit Function
    End Function

    ''' <summary>
    ''' ディレクトリのファイルパス一覧を取得する
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="includeValue"></param>
    ''' <param name="subDirectory">サブディレクトリも対象にする</param>
    ''' <returns></returns>
    Public Function GetFileListInDirectoryByIncludeFileName(
            filePath As String,
            includeValue As String,
            Optional subDirectory As Boolean = False
        ) As List(Of String)
        Dim allFiles As String()
        If subDirectory Then
            ' 指定されたディレクトリ内およびサブディレクトリ内のすべてのファイルを取得
            allFiles = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories)
        Else
            ' 指定されたディレクトリ内のすべてのファイルを取得
            allFiles = Directory.GetFiles(filePath)
        End If

        '' 指定された文字列をファイル名に含むファイルパスのみをフィルタリング
        'Dim filteredFiles As String() = allFiles.Where(Function(file) Path.GetFileName(file).Contains(includeValue)).ToArray()
        '' 結果を返す
        'Return filteredFiles
        '//////////
        ' 結果を格納するリスト
        Dim filteredFiles As New List(Of String)

        ' ファイル名に特定の文字列を含むファイルパスのみをリストに追加
        For Each file As String In allFiles
            If Path.GetFileName(file).Contains(includeValue) Then
                filteredFiles.Add(file)
            End If
        Next

        ' リストを配列に変換して返す
        Return filteredFiles
    End Function


    ''' <summary>
    ''' ディレクトリのファイルパス一覧を取得する（正規表現）
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="includeValue"></param>
    ''' <param name="subDirectory">サブディレクトリも対象にする</param>
    ''' <returns></returns>
    Public Function GetFileListInDirectoryByMatchRegixPattern(
            filePath As String,
            pattern As String,
            Optional subDirectory As Boolean = False
        ) As List(Of String)
        Dim allFiles As String()
        If subDirectory Then
            ' 指定されたディレクトリ内およびサブディレクトリ内のすべてのファイルを取得
            allFiles = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories)
        Else
            ' 指定されたディレクトリ内のすべてのファイルを取得
            allFiles = Directory.GetFiles(filePath)
        End If

        '' 指定された文字列をファイル名に含むファイルパスのみをフィルタリング
        'Dim filteredFiles As String() = allFiles.Where(Function(file) Path.GetFileName(file).Contains(includeValue)).ToArray()
        '' 結果を返す
        'Return filteredFiles
        '//////////
        ' 結果を格納するリスト
        Dim filteredFiles As New List(Of String)

        ' 正規表現オブジェクトの作成
        Dim regex As New Regex(pattern)
        ' マッチ結果を取得
        Dim matches As MatchCollection

        ' ファイル名に特定の文字列を含むファイルパスのみをリストに追加
        For Each file As String In allFiles
            matches = regex.Matches(file)
            ' マッチ結果を出力
            'For Each match As Match In matches
            '    Debug.WriteLine(" match.Value= " & match.Value)
            'Next
            If 0 < matches.Count Then
                filteredFiles.Add(file)
            End If
        Next

        ' リストを配列に変換して返す
        Return filteredFiles
    End Function

    Sub WriteStringArrayToFile(filePath As String, stringArray() As String)
        If Not Me._isWriteFile Then
            _logger.PrintInfo("Me._isWriteFile = False")
            Exit Sub
        End If
        ' ファイルが既に存在するか確認
        If File.Exists(filePath) Then
            ' 元のファイルを読み込む
            Dim existingFileContent As String() = File.ReadAllLines(filePath)

            ' 元のファイル内容と書き込む文字列配列が一致するか確認
            If existingFileContent.SequenceEqual(stringArray) Then
                ' 内容が一致する場合、処理を中断
                _logger.PrintInfo("ファイル内容が一致しています。書き込み処理を中断します。")
                Return
            Else
                ' 内容が一致しない場合、元ファイルをリネーム
                Dim ext As String = Path.GetExtension(filePath)
                Dim filename_only As String = Path.GetFileNameWithoutExtension(filePath)
                Dim renameFilePath As String = $"{filename_only}_{DateTime.Now:yyMMdd_HHmmss}." & ext & ".back"
                Dim backupFileName As String = Path.Combine(Path.GetDirectoryName(filePath), renameFilePath)

                File.Move(filePath, backupFileName)
                _logger.PrintInfo($"元ファイルは{backupFileName}にリネームされました。")
            End If
        End If

        ' 新しいファイルに文字列配列を書き込む
        File.WriteAllLines(filePath, stringArray)
        _logger.PrintInfo("新しいファイルに文字列配列を書き込みました。")
    End Sub

    Function GetCallerInfo() As String
        Dim ret As String = ""
        Dim frame As StackFrame
        Dim method As String
        Dim filePath As String
        Dim lineNumber As Integer

        Dim stackTrace As New StackTrace(True)
        ' インデックス1は呼び出し元のメソッドに対応します。
        '/
        frame = stackTrace.GetFrame(2)
        method = frame.GetMethod().Name
        filePath = frame.GetFileName()
        lineNumber = frame.GetFileLineNumber()
        ret = $"Method: {method}, File: {Path.GetFileName(filePath)}, Line: {lineNumber}" + vbCrLf
        '/
        frame = stackTrace.GetFrame(3)
        method = frame.GetMethod().Name
        filePath = frame.GetFileName()
        lineNumber = frame.GetFileLineNumber()
        ret &= $"Method: {method}, File: {Path.GetFileName(filePath)}, Line: {lineNumber}"
        '/
        Return ret
    End Function

    Public Sub ShowError(msg As String)
        Dim stackStr As String = GetCallerInfo()
        Dim errMsg As String = msg & NEW_LINE & "処理を中断します。" & NEW_LINE & "[ERROR詳細]" & NEW_LINE & GetCallerInfo()
        _logger.PrintInfo("ERROR: " & errMsg)
        MessageBox.Show(errMsg, "ERROR")
    End Sub

End Class
