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

    '################################################################################
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

        Public Function DataIsValid() As Boolean
            If FindSheetName = "" And FindRangeString = "" And FindValue = "" Then
                Return False
            Else
                Return True
            End If
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

        Public Function SetData(row As DataRow)
            'No  Sheet	Range	Value	Mode	OffsetRow	OffsetCol	EntireRow	EntireCol	RowCount	ColCount
            Dim colOrder = {
                "Sheet",
                "Range",
                "Value",
                "Mode",
                "OffsetRow",
                "OffsetCol",
                "EntireRow",
                "EntireCol",
                "RowCount",
                "ColCount"
            }
            Dim _list = New List(Of String)
            For Each targetName In colOrder
                For Each col As DataColumn In row.Table.Columns
                    If targetName = col.ColumnName Then
                        Dim buf = row(col)
                        If buf Is Nothing Or IsDBNull(buf) Then
                            buf = ""
                        End If
                        _list.Add(buf)
                    End If
                Next
            Next
            SetData(_list.ToArray)
            Return Nothing
        End Function

        Public Function SetData(_dataAry() As Object) As Object()

            Dim count = 0
            FindSheetName = _dataAry(count)
            count += 1
            FindRangeString = _dataAry(count)
            count += 1
            FindValue = _dataAry(count)
            count += 1
            FindMode = _dataAry(count)
            count += 1
            OffsetRow = _dataAry(count)
            count += 1
            OffsetCol = _dataAry(count)
            count += 1
            EntireRow = _dataAry(count)
            count += 1
            EntireCol = _dataAry(count)
            count += 1
            TargetCountRow = _dataAry(count)
            count += 1
            TargetCountCol = _dataAry(count)
            Return Nothing
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

    '################################################################################
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

        ''' <summary>
        ''' DestItemのメインのデータ（FindStringなど）が、空の時、SrcItemからコピーする
        ''' </summary>
        Public Sub InputBlankDataSrcToDest()
            Dim NothingMainDataFlagStr = DestItem.FindRangeString + DestItem.FindValue + DestItem.FindSheetName 'ここの入力が無ければ他の項目をコピーする
            If DestItem.FindRangeString = "" Then DestItem.FindRangeString = SrcItem.FindRangeString
            If DestItem.FindValue = "" Then DestItem.FindValue = SrcItem.FindValue
            If DestItem.FindSheetName = "" Then DestItem.FindSheetName = SrcItem.FindSheetName

            If NothingMainDataFlagStr = "" Then
                If DestItem.FindMode = 0 Then DestItem.FindMode = SrcItem.FindMode
                If DestItem.OffsetRow = 0 Then DestItem.OffsetRow = SrcItem.OffsetRow
                If DestItem.OffsetCol = 0 Then DestItem.OffsetCol = SrcItem.OffsetCol
                If DestItem.EntireRow = 0 Then DestItem.EntireRow = SrcItem.EntireRow
                If DestItem.EntireCol = 0 Then DestItem.EntireCol = SrcItem.EntireCol
                If DestItem.TargetCountRow = 0 Then DestItem.TargetCountRow = SrcItem.TargetCountRow
                If DestItem.TargetCountCol = 0 Then DestItem.TargetCountCol = SrcItem.TargetCountCol

            End If
        End Sub
    End Class
#End Region

    '################################################################################

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


    Public Sub SetSrcDataTable(dtSrc As Data.DataTable, srcFilePath As String, dtDest As Data.DataTable, destFilePath As String)

        _srcFilePath = srcFilePath

        Dim count = 0
        For Each row As DataRow In dtSrc.Rows
            Dim _pairData = New ChangeFormatDataPairManager.DataPair()

            ' filter src
            Dim filSrc = New ChangeFormatDataPairManager.ChangeFormatData()
            filSrc.SetData(row)
            filSrc.FilePath = srcFilePath

            If dtDest.Rows.Count <= count Then
                Continue For
            End If

            '※Destは無いこともある
            'filter dest
            Dim filDest = New ChangeFormatDataPairManager.ChangeFormatData()

            count += 1
        Next

        'Dim fildest = New ChangeFormatDataPairManager.ChangeFormatData()
        'fildest.FindSheetName = "" '無いときはシートインデックス1
        'fildest.FindRangeString = "A:A"
        'fildest.FindValue = "■TableA"
        'fildest.FindMode = ChangeFormatDataPairManager.ConstfilterMode.CONTAINS
        'fildest.EntireRow = True
        'fildest.FilePath = destFilePath

        '_pairData.ResetValue(filSrc, fildest)
        '_dataPairList.Add(_pairData)

        ''Data2行目
        '_pairData = New ChangeFormatDataPairManager.DataPair()
        'filSrc = New ChangeFormatDataPairManager.ChangeFormatData()
        'filSrc.FindSheetName = "" '無いときはシートインデックス1
        'filSrc.FindRangeString = "A:A"
        'filSrc.FindValue = "■TableA_02"
        'filSrc.FindMode = ChangeFormatDataPairManager.ConstfilterMode.CONTAINS
        'filSrc.EntireRow = True
        'filSrc.FilePath = srcFilePath

        'fildest = New ChangeFormatDataPairManager.ChangeFormatData()

        '_pairData.ResetValue(filSrc, fildest)
        '_ChangeDataManager._dataPairList.Add(_pairData)

        ''Data3行目
        '_pairData = New ChangeFormatDataPairManager.DataPair()
        'filSrc = New ChangeFormatDataPairManager.ChangeFormatData()
        'filSrc.FindSheetName = "" '無いときはシートインデックス1
        'filSrc.FindRangeString = "A:A"
        'filSrc.FindValue = "■TableB_01"
        'filSrc.FindMode = ChangeFormatDataPairManager.ConstfilterMode.CONTAINS
        'filSrc.EntireRow = True
        'filSrc.FilePath = srcFilePath

        'fildest = New ChangeFormatDataPairManager.ChangeFormatData()

        '_pairData.ResetValue(filSrc, fildest)
        '_ChangeDataManager._dataPairList.Add(_pairData)

        ''Data4行目
        '_pairData = New ChangeFormatDataPairManager.DataPair()
        'filSrc = New ChangeFormatDataPairManager.ChangeFormatData()
        'filSrc.FindSheetName = "" '無いときはシートインデックス1
        'filSrc.FindRangeString = "A:A"
        'filSrc.FindValue = "■TableB_02"
        'filSrc.FindMode = ChangeFormatDataPairManager.ConstfilterMode.CONTAINS
        'filSrc.EntireRow = True
        'filSrc.FilePath = srcFilePath

        'fildest = New ChangeFormatDataPairManager.ChangeFormatData()

        '_pairData.ResetValue(filSrc, fildest)
        '_ChangeDataManager._dataPairList.Add(_pairData)
    End Sub
End Class
