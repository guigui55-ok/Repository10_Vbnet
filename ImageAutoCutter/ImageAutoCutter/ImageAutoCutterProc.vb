Imports OpenCvManager

Public Class ImageAutoCutterProc
    Public _logger As AppLogger
    Public _fileNameList = New List(Of String)
    Public _inputDirPath = ""
    Public _searchPattern = ""
    Public _templeteFilePath = ""
    Public _threthold As Double = 0.9

    Sub New(logger As AppLogger)
        _logger = logger
    End Sub

    Public Sub FindPaths(inputDirPath As String, searchPattern As String)
        _inputDirPath = inputDirPath
        _searchPattern = searchPattern

        _logger.Info($"inDirStr = {inputDirPath}")
        _logger.Info($"patStr = {searchPattern}")

        'init
        Dim fileFinder = New FileFInder(_logger)

        'set
        fileFinder.SetValues(inputDirPath, searchPattern)

        'find
        fileFinder.FindFilePaths()
        _logger.Info($"fileListCount = {fileFinder._filePathList.Count}")

        'reomve
        '_cutのファイル名を削除（1回実行すると_cut.pngができるが、2回目実行時はそのファイルも参照してしまう）
        fileFinder.RemovePathRegex({".*_cut\."})
        _logger.Info($"fileListCount = {fileFinder._filePathList.Count}")

        'convert
        _fileNameList = New List(Of String)
        For Each fPath In fileFinder._filePathList
            Dim bufFname = IO.Path.GetFileName(fPath)
            _fileNameList.add(bufFname)
        Next

    End Sub


    Public Sub ExecuteCutAutoMain(tempPath As String, cutSize As Point, threthold As Double, outDirPath As String)
        _logger.Info("*ExecuteCutAutoMain")
        Dim count = 0
        For Each fname In _fileNameList
            _logger.Info($"***** [{count}] *****")
            ExecuteCutAutoSingle(_inputDirPath, fname, tempPath, cutSize, threthold, outDirPath)
            count += 1
        Next
        _logger.Info("ExecuteCutAutoMain Done.")
    End Sub

    Public Sub ExecuteCutAutoSingle(dirPath As String, fileName As String, tempPath As String, cutSize As Point, threthold As Double, outDirPath As String)

        Dim matcher As New ImageMatcher()
        matcher.Threshold = threthold ' 任意でしきい値を調整可能

        Dim basePath = IO.Path.Combine(dirPath, fileName)

        Dim result = matcher.MatchTemplate(basePath, tempPath)

        If result.IsMatch Then
            _logger.Info($"一致しました！座標: {result.MatchLocation}, 類似度: {result.MatchValue:F3}")
        Else
            _logger.Info("一致しませんでした。")
            Exit Sub
        End If

        'log base
        Dim bufSize = matcher.GetImageSize(basePath)
        _logger.Info($"basePath = {IO.Path.GetFileName(basePath)}  [{bufSize}]")

        'log temp
        bufSize = matcher.GetImageSize(tempPath)
        _logger.Info($"tempPath = {IO.Path.GetFileName(tempPath)}  [{bufSize}]")

        'log loc
        Dim beginLoc = result.MatchLocation
        _logger.Info($"beginLoc = {beginLoc}")

        'log cut size input
        _logger.Info($"cutSize = {cutSize}")

        'log endLoc
        Dim endLoc = New Point(cutSize.X + beginLoc.X, cutSize.Y + beginLoc.Y)
        _logger.Info($"endLoc = {endLoc}")

        'write path
        IO.Directory.CreateDirectory(outDirPath)
        'Dim wdir = IO.Path.GetDirectoryName(basePath)
        Dim wdir = outDirPath
        Dim wFilename = IO.Path.GetFileNameWithoutExtension(basePath) + "_cut" + IO.Path.GetExtension(basePath)
        Dim wPath = IO.Path.Combine(wdir, wFilename)
        _logger.Info($"wPath = {wPath}")

        'execute
        matcher.CropAndSaveImage(basePath, beginLoc, endLoc, wPath)

    End Sub

End Class
