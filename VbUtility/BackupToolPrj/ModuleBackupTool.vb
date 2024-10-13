Imports System.IO

Public Module ModuleBackupTool
    Public Class BackupTool
        Dim _filteIgnore As FilterIgnore = New FilterIgnore()
        Public Sub New()

        End Sub

        ''' <summary>
        ''' バックアップ先に、srcDir_日付を付けて、コピーする（srcDirPathはファイルでも可能）
        ''' </summary>
        ''' <param name="srcDirPath"></param>
        ''' <param name="backupDirPath"></param>
        ''' <param name="includeDir"></param>
        ''' <param name="ignorePatternList"></param>
        Public Sub BackupDir(
                            srcDirPath As String,
                            backupDirPath As String,
                            includeDir As Boolean,
                            Optional ignorePatternList As List(Of String) = Nothing)
            Dim files As String()
            Dim backupDirName As String
            Dim distDirPath As String
            If Directory.Exists(srcDirPath) Then
                files = Directory.GetFiles(srcDirPath)
                'バックアップ先に、srcDir_日付を付けて、コピーする
                backupDirName = Path.GetFileName(srcDirPath) + DateTime.Now.ToString("_yyMMddHHmmss")
                distDirPath = Path.Combine(backupDirPath, backupDirName)
                'フォルダ作成
                If Not Directory.Exists(distDirPath) Then
                    Directory.CreateDirectory(distDirPath)
                End If
                ''ファイル除外処理
                '_filteIgnore._filterPatternList = ignorePatternList
                'Dim filesListB = _filteIgnore.FilterdValueListByMatchPattern(files.ToList())
                ''すべてコピー
                'For Each filepath As String In filesListB
                '    CopyFile(filepath, distDirPath, includeDir)
                'Next
            Else
                Dim srcFilePath = srcDirPath
                files = {srcFilePath}
                'バックアップ先に、srcDir_日付を付けて、コピーする
                backupDirName = Path.GetFileNameWithoutExtension(srcFilePath) + DateTime.Now.ToString("_yyMMddHHmmss") + Path.GetExtension(srcFilePath)
            End If
            distDirPath = Path.Combine(backupDirPath, backupDirName)
            'ファイルの場合
            'ファイル除外処理
            _filteIgnore._filterPatternList = ignorePatternList
            Dim filesListB = _filteIgnore.FilterdValueListByMatchPattern(files.ToList())
            'すべてコピー
            For Each filepath As String In filesListB
                CopyFile(filepath, distDirPath, includeDir)
            Next
        End Sub

        Private Sub CopyFile(srcPath As String, distDirPath As String, includeDir As Boolean)
            If File.Exists(srcPath) Then
                Dim distPath = Path.Combine(distDirPath, Path.GetFileName(srcPath))
                File.Copy(srcPath, distPath, True)
                Console.WriteLine(String.Format("COPIED File: src={0}, file={1}", srcPath, distPath))
            ElseIf Directory.Exists(srcPath) Then
                If includeDir Then
                    Dim distPath = Path.Combine(distDirPath, Path.GetDirectoryName(srcPath))
                    My.Computer.FileSystem.CopyDirectory(srcPath, distPath,
                        FileIO.UIOption.AllDialogs, FileIO.UICancelOption.DoNothing)
                    Console.WriteLine(String.Format("COPIED dir : src={0}, file={1}", srcPath, distPath))
                Else
                    Console.WriteLine(String.Format("SKIP dir : src={0}", srcPath))
                End If
            Else
                Console.WriteLine(String.Format("srcPath is Nothing [{0}]", srcPath))
            End If
        End Sub

        ''' <summary>
        ''' srcDirのファイルを、distDirにコピーする（srcDirPathはファイルでも可能）
        ''' </summary>
        ''' <param name="srcDirPath"></param>
        ''' <param name="distDirPath"></param>
        ''' <param name="includeDir"></param>
        ''' <param name="ignorePatternList"></param>
        Public Sub CopyDir(
                            srcDirPath As String,
                            distDirPath As String,
                            includeDir As Boolean,
                            Optional ignorePatternList As List(Of String) = Nothing)
            Dim files As String()
            If Directory.Exists(srcDirPath) Then
                files = Directory.GetFiles(srcDirPath)
                'フォルダ作成
                If Not Directory.Exists(distDirPath) Then
                    Directory.CreateDirectory(distDirPath)
                End If
            ElseIf file.Exists(srcDirPath) Then
                'ファイルの場合
                files = {srcDirPath}
            Else
                Console.WriteLine(String.Format("srcDirPath is Not Exists [{0}]", srcDirPath))
                Exit Sub
            End If
            'ファイル除外処理
            _filteIgnore._filterPatternList = ignorePatternList
            Dim filesListB = _filteIgnore.FilterdValueListByMatchPattern(files.ToList())
            'すべてコピー
            For Each filepath As String In filesListB
                CopyFile(filepath, distDirPath, includeDir)
            Next
        End Sub

    End Class
End Module
