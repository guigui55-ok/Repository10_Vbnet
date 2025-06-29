Imports System.IO

Public Class FileFInder
    Public _logger As AppLogger

    Public _dirPaths() As String
    Public _patterns() As String
    Public _filePathList As List(Of String)

    Public _splitStr = vbLf
    Sub New(logger As AppLogger)
        _logger = logger
        _filePathList = New List(Of String)
    End Sub

    Sub SetValues(dirPaths As String, findPatterns As String)
        _dirPaths = dirPaths.Split(_splitStr)
        _patterns = findPatterns.Split(_splitStr)

        RemoveBlank(_dirPaths)
        RemoveBlank(_patterns)

        _logger.Info($"_dirPaths.Length = {_dirPaths.Length}")
        _logger.Info($"_patterns.Length = {_patterns.Length}")
    End Sub

    Private Sub RemoveBlank(ByRef _ary() As String)
        Dim newList = New List(Of String)
        For Each _val In _ary
            If _val.Trim <> "" Then
                newList.Add(_val)
            End If
        Next
        _ary = newList.ToArray()
    End Sub


    Public Function IsExistsPathAll() As Boolean
        Dim isExists = True
        Dim count = 0
        For Each _path In _dirPaths
            If Not IO.Directory.Exists(_path) Then
                isExists = False
                _logger.Info($"NotFound [{count}][{_path}]")
            End If
            count += 1
        Next
        Return isExists
    End Function

    Sub SetFilePaths()
        'パターン優先
        'パターン１＋フォルダ１、フォルダ２　　パターン２のフォルダ１、フォルダ２... という順番になる
        For Each pattern In _patterns
            For Each dirPath In _dirPaths
                FindPathToList(dirPath, pattern)
            Next
        Next
        _logger.Info($"_filePathList.Count = {_filePathList.Count}")
    End Sub

    Sub FindPathToList(dirPath As String, pattern As String)
        Dim files = Directory.GetFiles(dirPath, pattern, searchOption:=SearchOption.AllDirectories)
        _filePathList.AddRange(files)
    End Sub

End Class
