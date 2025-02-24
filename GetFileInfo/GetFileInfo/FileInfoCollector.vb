Imports System.IO
Imports System.Text.RegularExpressions


Public Class FileInfoCollector
    Public Class FileInfoKeys
        Public Const FileName As String = "FileName"
        Public Const CreatedDate As String = "CreatedDate"
        Public Const ModifiedDate As String = "ModifiedDate"
        Public Const FileSize As String = "FileSize"
        Public Const ProductVersion As String = "ProductVersion"
    End Class

    Private ReadOnly _targetDirectory As String
    Private ReadOnly _filePatterns As List(Of String)

    ''' <summary>
    ''' コンストラクタで対象ディレクトリと検索するファイル名の正規表現リストを設定
    ''' </summary>
    ''' <param name="targetDirectory">検索対象ディレクトリ</param>
    ''' <param name="filePatterns">検索対象のファイル名パターン（正規表現）</param>
    Public Sub New(targetDirectory As String, filePatterns As List(Of String))
        _targetDirectory = targetDirectory
        _filePatterns = filePatterns
    End Sub

    ''' <summary>
    ''' 指定したディレクトリからファイル情報を取得する
    ''' </summary>
    ''' <returns>ファイル情報を格納したDictionaryのリスト</returns>
    Public Function CollectFileInfo() As List(Of Dictionary(Of String, String))
        Dim fileInfoList As New List(Of Dictionary(Of String, String))

        ' ディレクトリ内の全ファイルを取得
        Dim allFiles As String() = Directory.GetFiles(_targetDirectory)

        ' 各ファイルに対して処理を行う
        For Each filePath In allFiles
            Dim fileName As String = Path.GetFileName(filePath)

            ' ファイル名がいずれかの正規表現にマッチするかチェック
            If Not _filePatterns.Any(Function(pattern) Regex.IsMatch(fileName, pattern)) Then
                Continue For
            End If

            ' ファイル情報を取得
            Dim fileInfo As New FileInfo(filePath)
            Dim versionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(filePath)

            ' Dictionaryに情報を格納
            Dim fileData As New Dictionary(Of String, String) From {
                {FileInfoKeys.FileName, fileName},
                {FileInfoKeys.CreatedDate, fileInfo.CreationTime.ToString("yyyy-MM-dd HH:mm:ss")},
                {FileInfoKeys.ModifiedDate, fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss")},
                {FileInfoKeys.FileSize, fileInfo.Length.ToString()},
                {FileInfoKeys.ProductVersion, If(String.IsNullOrEmpty(versionInfo.ProductVersion), "N/A", versionInfo.ProductVersion)}
            }

            ' リストに追加
            fileInfoList.Add(fileData)
        Next

        Return fileInfoList
    End Function
End Class

