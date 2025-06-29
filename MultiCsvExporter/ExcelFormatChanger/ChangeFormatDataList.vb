Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop

''' <summary>
''' ExcelFormatChangeで使用するデータクラス
''' </summary>
Public Class ChangeFormatDataPairList

    Public Class ConstfilterMode
        Public Const CONTAINS = 0
        Public Const MATCH_ALL = 1
        Public Const STARTS_WITH = 2
        Public Const ENDS_WITH = 3
    End Class

    Public Class ChangeFormatData
        Public FindSheetName As String
        Public FindRangeString As String '値を発見するための範囲
        Public FindValue As String
        Public FindMode As Integer

        '値を発見してからの位置・範囲調整用
        Public OffsetRow As Integer = 0
        Public OffsetCol As Integer = 0
        Public EntireRow As Boolean = False
        Public EntireCol As Boolean = False
        Public TargetCountRow As Integer = 1
        Public TargetCountCol As Integer = 1

        Public TargetSheetName As String
        Public TargetRangeString As String '値を発見してOffsetと TaragetCount適用してからの範囲
        Public Overrides Function ToString() As String
            'Return $"[sheetname={FilterSheetName}, range={FilterRangeString}], value={FilterValue}, mode={FilterMode}"
            'Return $"[#{FilterSheetName}, {FilterRangeString}], {FilterValue}, {FilterMode}"
            Return $"FindValue={FindValue}"
        End Function
    End Class

    ''' <summary>
    ''' ChangeFormatDataのペアで扱うクラス（コピー元 src とコピー先 dest の検索条件を別条件にするため）
    ''' </summary>
    Public Class DataPair
        Public Property SrcItem As ChangeFormatData
        Public Property DestItem As ChangeFormatData

        Public Sub New()

        End Sub

        Public Sub New(src As ChangeFormatData, dest As ChangeFormatData)
            ResetValue(src, dest)
        End Sub
        Public Sub ResetValue(src As ChangeFormatData, dest As ChangeFormatData)
            Me.SrcItem = src
            Me.DestItem = dest
        End Sub

        Public Overrides Function ToString() As String
            Return $"Pair: {SrcItem.ToString()} and {DestItem.ToString()}"
        End Function
    End Class

    '// ================================================================================

    Public _filterList As List(Of ChangeFormatData)
    Public _srcFilePath = ""
    Public _destFilePath = ""

    Sub New()
        _filterList = New List(Of ChangeFormatData)
    End Sub

    Public Sub SetTestData()
        Dim srcFilePath = "C:\Users\OK\source\repos\Repository10_VBnet\MultiCsvExporter\MultiCsvExporter\bin\Debug\Output\FormatSrc.xlsx"
        Dim destFilePath = "C:\Users\OK\source\repos\Repository10_VBnet\MultiCsvExporter\MultiCsvExporter\bin\Debug\Output\Export.xlsx"

        Dim _pairData = New DataPair()
        Dim filSrc = New ChangeFormatData()
        filSrc.FindSheetName = "" '無いときはシートインデックス1
        filSrc.FindRangeString = "A:A"
        filSrc.FindValue = "■TableA"
        filSrc.FindMode = ConstfilterMode.CONTAINS
        filSrc.EntireRow = True
        'filSrc.TargetCountRow '読み取りfileから動的に設定

        _pairData.ResetValue(filSrc, filSrc)

    End Sub

    Public Sub ExecuteChangeFormat()
        '変更先ファイル名を読み込む
        '変更先ファイル名の変更範囲を読み込み、変更範囲を動的に設定
        '-- 実行
        '※繰り返し
        '書式コピー元、フォーマット範囲を検索・設定
        '書式コピー先範囲を設定
        'コピーペースト

    End Sub

End Class
