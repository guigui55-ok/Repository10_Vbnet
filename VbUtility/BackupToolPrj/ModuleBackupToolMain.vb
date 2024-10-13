Imports System.Deployment
Imports System.IO
Imports System.Reflection
Imports CsvUtility

Module ModuleBackupToolMain

    Sub Main()
        MainTestBackupCopyByCsv()
    End Sub


    Sub MainTestBackupCopyByCsv()
        Dim csvStream As CsvStream = New CsvStream()
        Dim filePath As String
        Dim appDirPath = GetApplicationDirectory()
        filePath = Path.Combine(appDirPath, "backup.csv")
        Dim ignoreList = New List(Of String)({"\.bin$"})
        '//
        Dim dataTable As DataTable
        dataTable = csvStream.ReadCsvFile(filePath)
        Dim rowCount As Integer
        Dim bufLine As String
        Dim dataList2d As List(Of List(Of String)) = csvStream.ConvertDataTableToListString(dataTable)
        rowCount = 1
        For Each colList As List(Of String) In dataList2d
            Console.WriteLine(" ########## ")
            bufLine = String.Format("row={0}, ", rowCount)
            bufLine = String.Format("[{0}]", String.Join(", ", colList))
            Console.WriteLine(bufLine)
            Dim srcDirPath As String = colList(0)
            Dim distDirPath As String = colList(2)
            Dim backupDirPath As String = colList(1)
            MainBackupMethod(appDirPath, srcDirPath, distDirPath, backupDirPath, ignoreList)
        Next

        Console.ReadKey()
    End Sub
    Sub MainBackupMethod(appDirPath As String, srcDirPath As String, distDirPath As String, backupDirPath As String, ignoreList As List(Of String))
        'testDir から BackupDirにバックアップ、 CopyDirにコピー（置き換え）
        Dim backupTool = New BackupTool()
        'Dim appDirPath = GetApplicationDirectory()
        'Dim srcDirPath = Path.Combine(appDirPath, "testDir")
        'Dim ignoreList = New List(Of String)({"\.bin$"})
        'Dim distDirPath = Path.Combine(appDirPath, "CopyDir")
        'Dim backupDirPath = Path.Combine(appDirPath, "BackupDir")
        '//
        backupTool.BackupDir(srcDirPath, backupDirPath, True, ignoreList)
        '//
        Console.WriteLine(" ********** ")
        backupTool.CopyDir(srcDirPath, distDirPath, False, ignoreList)
        'Console.ReadKey()
    End Sub




    Sub MainBackupSimple()
        'testDir から BackupDirにバックアップ、 CopyDirにコピー（置き換え）
        Dim backupTool = New BackupTool()
        Dim appDirPath = GetApplicationDirectory()
        Dim srcDirPath = Path.Combine(appDirPath, "testDir")
        Dim ignoreList = New List(Of String)({"\.bin$"})
        Dim distDirPath = Path.Combine(appDirPath, "CopyDir")
        Dim backupDirPath = Path.Combine(appDirPath, "BackupDir")
        '//
        backupTool.BackupDir(srcDirPath, backupDirPath, True, ignoreList)
        '//
        Console.WriteLine(" ********** ")
        backupTool.CopyDir(srcDirPath, distDirPath, False, ignoreList)
        Console.ReadKey()
    End Sub


    Private Function GetApplicationDirectory()
        Dim myAssembly As Assembly = Assembly.GetEntryAssembly()
        Dim appFilePath As String = myAssembly.Location
        Dim appDirPath = CommonUtilityPrj.GetParentPath(appFilePath)
        Return appDirPath
    End Function

    Sub MainCreateFiles()
        ' アプリの実行ファイルディレクトリに、testDirディテク取りを作成、いくつかテストファイルを作成 拡張子を変更
        Dim appDirPath = GetApplicationDirectory()
        Dim dirPath = Path.Combine(appDirPath, "testDir")
        If Not Directory.Exists(dirPath) Then
            Directory.CreateDirectory(dirPath)
            Console.WriteLine("Create Directory = " + dirPath)
        End If

        Dim makeTestFile = New MakeTestFile()
        makeTestFile.CreateFiles(dirPath, 10, "testFileBase")

        Console.WriteLine(" ========== ")
        Dim extList = New List(Of String)({
            ".txt", ".jpg", ".gif", ".bin"})
        makeTestFile.ChangeRotationExtFilesInDir(dirPath, extList)

        Console.ReadKey()
    End Sub

End Module
