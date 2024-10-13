Imports System.IO
Imports CommonUtilityPrj

Public Module ModuleMakeTestFile
    Public Class MakeTestFile
        Public Sub New()
        End Sub

        ''' <summary>
        ''' （テストデータ作成用）特定のディレクトリのファイルすべての拡張子を置き換える（extListを順番に置き換える）
        ''' </summary>
        ''' <param name="dirPath"></param>
        ''' <param name="extList"></param>
        Public Sub ChangeRotationExtFilesInDir(dirPath As String, extList As List(Of String))
            Dim files = Directory.GetFiles(dirPath)
            If extList.Count < 1 Then Exit Sub
            Dim extCount = 0
            Dim extCountMax = extList.Count - 1
            For Each filepath In files
                Dim fileNameOnly = Path.GetFileNameWithoutExtension(filepath)
                Dim newFileName = fileNameOnly + extList(extCount)
                Dim newPath = Path.Combine(dirPath, newFileName)
                FileSystem.Rename(filepath, newPath)
                Console.WriteLine(String.Format("oldPath = {0}, newFileName = {1}", filepath, newFileName))
                extCount += 1
                If extCountMax < extCount Then extCount = 0
            Next

        End Sub

        ''' <summary>
        ''' （テストデータ作成用）dirPathに ファイル名 fileNameBase_amount.[fileNameBase ext] の空ファイルをamountの個数分作成する（メイン関数）
        ''' </summary>
        ''' <param name="dirPath"></param>
        ''' <param name="amount"></param>
        ''' <param name="fileNameBase"></param>
        Public Sub CreateFiles(dirPath As String, amount As Integer, fileNameBase As String)
            Dim ext = Path.GetExtension(fileNameBase)
            Dim fileNameOnly = Path.GetFileNameWithoutExtension(fileNameBase)
            Dim number As Integer = 1
            For i As Integer = 1 To amount
                Dim newPath = CreateNewFilePath(dirPath, fileNameBase, number)
                Dim newNumStr = GetLastNumberInFile(newPath)
                If newNumStr <> "" Then
                    number = Integer.Parse(newNumStr)
                End If
                CreateFile(newPath)
                Console.WriteLine(String.Format("CreateFile = {0}", newPath))
            Next
        End Sub

        Public Sub CreateFile(createPath As String)
            'contents , encoding は指定なし、からファイルを作成
            Using write = New StreamWriter(createPath, True)

            End Using
        End Sub

        ''' <summary>
        ''' 新しいファイルパスを取得する（存在する場合は番号をスキップして次の数字を採番）
        ''' </summary>
        ''' <param name="dirPath"></param>
        ''' <param name="fileNameBase"></param>
        ''' <param name="index"></param>
        ''' <returns></returns>
        Public Function CreateNewFilePath(dirPath As String, fileNameBase As String, index As String)
            Dim ext = Path.GetExtension(fileNameBase)
            Dim fileNameOnly = Path.GetFileNameWithoutExtension(fileNameBase)
            Dim number As Integer = 1
            Dim newFileName As String = fileNameBase + String.Format("_{0}", index)
            If ext <> "" Then
                newFileName += ext
            End If
            Dim newPath = Path.Combine(dirPath, newFileName)
            If File.Exists(newPath) Then
                If (1000 < index) Then
                    Throw New Exception("Index Over 1000") '無限ループ防止用
                End If
                newPath = CreateNewFilePath(dirPath, fileNameBase, index + 1)
            End If
            Return newPath
        End Function

        ''' <summary>
        ''' ファイル名（拡張子除く）の最後の数字を取得する（1-10桁）
        ''' </summary>
        ''' <param name="filePath"></param>
        ''' <returns></returns>
        Public Function GetLastNumberInFile(filePath As String)
            Dim value = Path.GetFileNameWithoutExtension(filePath)
            Dim ret = CommonGeneralModule.GetMatchString("\d{1,10}$", value)
            Return ret
        End Function



        'Public Function CreateDir(dirPath As String)
        '    Directory.CreateDirectory(dirPath)
        'End Function

    End Class
End Module
