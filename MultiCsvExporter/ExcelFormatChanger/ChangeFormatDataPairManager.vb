Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop

''' <summary>
''' ExcelFormatChangeで使用するデータクラス
''' </summary>
Public Class ChangeFormatDataPairManager

#Region "Classes"
    Public Class ConstfilterMode
        Public Const CONTAINS = 0
        Public Const MATCH_ALL = 1
        Public Const STARTS_WITH = 2
        Public Const ENDS_WITH = 3
    End Class

    Public Class ChangeFormatData
        Public FilePath As String
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

        Public Function GetDataArray(index As Integer) As Object()
            Dim ret = {
                index,
                FindSheetName,
                FindRangeString,
                FindValue,
                FindMode,
                OffsetRow,
                OffsetCol,
                EntireRow,
                EntireCol,
                TargetCountRow,
                TargetCountCol
            }
            Return ret
        End Function

        Public Function GetDeepCopyObject() As ChangeFormatData
            Dim ret = New ChangeFormatData
            ret.FilePath = Me.FilePath
            ret.FindSheetName = Me.FindSheetName
            ret.FindRangeString = Me.FindRangeString
            ret.FindValue = Me.FindValue
            ret.FindMode = Me.FindMode

            ret.OffsetRow = Me.OffsetRow
            ret.OffsetCol = Me.OffsetCol
            ret.EntireRow = Me.EntireRow
            ret.EntireCol = Me.EntireCol
            ret.TargetCountRow = Me.TargetCountRow
            ret.TargetCountCol = Me.TargetCountCol
            ret.TargetSheetName = Me.TargetSheetName
            ret.TargetRangeString = Me.TargetRangeString

            Return ret
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

        Public Sub SetFilePath(srcFilePath As String, destFilePath As String)
            SrcItem.FilePath = srcFilePath
            DestItem.FilePath = destFilePath
        End Sub

        Public Overrides Function ToString() As String
            Return $"Pair: {SrcItem.ToString()} and {DestItem.ToString()}"
        End Function
    End Class
#End Region

    '// ================================================================================

    Public _dataPairList As List(Of DataPair)
    Public _srcFilePath = ""
    Public _destFilePath = ""

    Public Event LogoutEvent As EventHandler

    Sub New()
        _dataPairList = New List(Of DataPair)
    End Sub

    Private Sub Logout(value As String)
        RaiseEvent LogoutEvent(value, EventArgs.Empty)
    End Sub

End Class
