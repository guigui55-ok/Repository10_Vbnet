Imports System.Data

Public Class MultiConditionFilter

    Public Class ConstMatchFlag
        Public Const MATCH_COMPLETE = 0
        Public Const MATCH_CONTAIN = 1
        Public Const MATCH_STARTS_WITH = 2
        Public Const MATCH_ENDS_WITH = 3
    End Class


    Public Class FilterCondition
        Public Key As String
        Public FilterValue As String
        Public IsNotFlag As Boolean
        Public KeyMatchFlag As Integer
    End Class

    ''' <summary>
    ''' DataTable から、複数条件のいずれかに一致する行を抽出
    ''' </summary>
    Public Function FilterMatchingRows(table As DataTable, conditions As Dictionary(Of String, Object)) As List(Of DataRow)
        Return table.AsEnumerable().Where(Function(row) IsRowMatchAnyCondition(row, conditions)).ToList()
    End Function

    ''' <summary>
    ''' DataTable から、複数条件のいずれかに一致する行を除外
    ''' </summary>
    Public Function FilterExcludeMatchingRows(table As DataTable, conditions As Dictionary(Of String, Object)) As List(Of DataRow)
        Return table.AsEnumerable().Where(Function(row) Not IsRowMatchAnyCondition(row, conditions)).ToList()
    End Function

    ''' <summary>
    ''' Dictionaryデータ群を条件でフィルタ（抽出）
    ''' </summary>
    Public Function FilterMatchingDictList(dataList As List(Of Dictionary(Of String, Object)), conditions As Dictionary(Of String, Object)) As List(Of Dictionary(Of String, Object))
        Return dataList.Where(Function(d) IsDictMatchAnyCondition(d, conditions)).ToList()
    End Function

    ''' <summary>
    ''' Dictionaryデータ群を条件で除外
    ''' </summary>
    Public Function FilterExcludeMatchingDictList(dataList As List(Of Dictionary(Of String, Object)), conditions As Dictionary(Of String, Object)) As List(Of Dictionary(Of String, Object))
        Return dataList.Where(Function(d) Not IsDictMatchAnyCondition(d, conditions)).ToList()
    End Function

    ''' <summary>
    ''' DataRow がいずれかの条件に一致するか判定
    ''' </summary>
    Private Function IsRowMatchAnyCondition(row As DataRow, conditions As Dictionary(Of String, Object)) As Boolean
        For Each kvp In conditions
            If row.Table.Columns.Contains(kvp.Key) AndAlso Not row.IsNull(kvp.Key) AndAlso row(kvp.Key).ToString() = kvp.Value.ToString() Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Dictionary がいずれかの条件に一致するか判定
    ''' </summary>
    Private Function IsDictMatchAnyCondition(dict As Dictionary(Of String, Object), conditions As Dictionary(Of String, Object)) As Boolean
        For Each kvp In conditions
            If dict.ContainsKey(kvp.Key) AndAlso dict(kvp.Key)?.ToString() = kvp.Value.ToString() Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' DataTable から、条件に一致する行を除外した新しい DataTable を返す
    ''' </summary>
    Public Function FilterExcludeMatchingDataTable(table As DataTable, conditions As Dictionary(Of String, Object)) As DataTable
        Dim resultTable As DataTable = table.Clone() ' スキーマだけコピー

        For Each row As DataRow In table.Rows
            If Not IsRowMatchAnyCondition(row, conditions) Then
                resultTable.ImportRow(row)
            End If
        Next

        Return resultTable
    End Function


    Public Shared Sub UsageSample()
        Dim myDictList = New List(Of Dictionary(Of String, Object))
        Dim myTable = New DataTable()

        Dim conditions As New Dictionary(Of String, Object) From {
            {"Status", "Active"},
            {"Type", "Admin"}
        }

        Dim filter As New MultiConditionFilter()

        ' DataTableから抽出
        Dim matchedRows = filter.FilterMatchingRows(myTable, conditions)

        ' 除外する場合
        Dim remainingRows = filter.FilterExcludeMatchingRows(myTable, conditions)

        ' Dictionaryのリストで抽出
        Dim matchedDicts = filter.FilterMatchingDictList(myDictList, conditions)
    End Sub

    Public Shared Sub UsageSampleB()
        Dim myTable = New DataTable()
        Dim conditions As New Dictionary(Of String, Object) From {
            {"Status", "Inactive"},
            {"Category", "Obsolete"}
        }

        Dim filter As New MultiConditionFilter()
        Dim filteredTable As DataTable = filter.FilterExcludeMatchingDataTable(myTable, conditions)
    End Sub

End Class
