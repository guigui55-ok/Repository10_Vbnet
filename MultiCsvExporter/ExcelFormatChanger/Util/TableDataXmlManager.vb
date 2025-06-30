Imports System.IO
Imports System.Data

''' <summary>
''' DataTable の XML 保存・読み込みを管理するクラス
''' </summary>
Public Class TableDataXmlManager
    Private _filePath As String

    ''' <summary>
    ''' XML保存先ファイルのパスを指定して初期化
    ''' </summary>
    ''' <param name="filePath">XMLファイルのフルパス</param>
    Public Sub New(filePath As String)
        _filePath = filePath
    End Sub

    ''' <summary>
    ''' DataTable を XMLファイルに保存
    ''' </summary>
    ''' <param name="dt">保存対象の DataTable</param>
    Public Sub SaveDataTableToXml(dt As DataTable)
        Try
            dt.WriteXml(_filePath, XmlWriteMode.WriteSchema)
        Catch ex As Exception
            ' 必要に応じてログや例外処理を追加
            Throw New ApplicationException("XML保存に失敗しました。", ex)
        End Try
    End Sub

    ''' <summary>
    ''' XMLファイルから DataTable を読み込む
    ''' </summary>
    ''' <returns>読み込まれた DataTable。ファイルが存在しない場合は空の DataTable。</returns>
    Public Function LoadDataTableFromXml() As DataTable
        Dim dt As New DataTable()

        If File.Exists(_filePath) Then
            Try
                dt.ReadXml(_filePath)
            Catch ex As Exception
                Throw New ApplicationException("XML読み込みに失敗しました。", ex)
            End Try
        End If

        Return dt
    End Function

    ''' <summary>
    ''' XML保存ファイルの存在確認
    ''' </summary>
    Public Function ExistsXmlFile() As Boolean
        Return File.Exists(_filePath)
    End Function

    ''' <summary>
    ''' XMLファイルを削除（初期化したい場合など）
    ''' </summary>
    Public Sub DeleteXmlFile()
        If File.Exists(_filePath) Then
            File.Delete(_filePath)
        End If
    End Sub

    Public Sub UsageSample_Save()
        Dim _xmlManager = New TableDataXmlManager("filePath.xml")
        Dim DataGridView1 = New DataGridView

        _xmlManager = New TableDataXmlManager("data.xml")

        Dim dt As DataTable = _xmlManager.LoadDataTableFromXml()
        DataGridView1.DataSource = dt
    End Sub

    Public Sub UsageSample_Load()
        Dim _xmlManager = New TableDataXmlManager("filePath.xml")
        Dim DataGridView1 = New DataGridView

        Dim dt As DataTable = TryCast(DataGridView1.DataSource, DataTable)
        If dt IsNot Nothing Then
            _xmlManager.SaveDataTableToXml(dt)
        End If
    End Sub

End Class
