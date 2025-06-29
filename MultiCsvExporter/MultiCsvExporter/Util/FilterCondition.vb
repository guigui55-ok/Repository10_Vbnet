Imports System.Text.RegularExpressions
Imports System.Linq
Imports System.Data

Public Class FilterCondition
    Public ColNames As List(Of String)
    Public patterns As List(Of String)
    Public regexList As List(Of Regex)
    Public matchAll As Boolean = True
    Public matchAny As Boolean
    Public NotFlags As List(Of Boolean)

    Public Event LogoutEvent As EventHandler

    Public Sub New(colNames As List(Of String), patterns As List(Of String), matchAll As Boolean, Optional MatchAny As Boolean = False)
        SetSetting(colNames, patterns, matchAll, MatchAny)
        SetTestData()
    End Sub

    Public Sub SetTestData()
        Dim _colNames = New List(Of String) From {"ID", "Code"}
        Dim _patterns = New List(Of String) From {"74", "Item0"}
        Dim _notFlags = New List(Of Boolean) From {False, False}
        Me.ColNames = _colNames
        Me.patterns = _patterns
        Me.NotFlags = _notFlags
    End Sub
    Public Sub Logout(value As String)
        RaiseEvent LogoutEvent(value, EventArgs.Empty)
    End Sub

    Public Sub LogoutValues()
        Dim log = GetValuesStr()
        Logout(log)
    End Sub

    Public Function GetValuesStr()
        Dim valuesStr = ""
        Dim _list = New List(Of String)
        For i = 0 To ColNames.Count - 1
            Dim buf = ""
            buf += $"[{i}]" + ColNames(i) + ", " + patterns(i) + ", " + $"Not={NotFlags(i)}"
            _list.Add(buf)
        Next
        valuesStr = String.Join(vbCrLf, _list)
        Return valuesStr
    End Function


    Public Sub SetSetting(colNames As List(Of String), patterns As List(Of String), matchAll As Boolean, Optional MatchAny As Boolean = False)
        Me.ColNames = colNames
        Me.patterns = patterns
        Me.matchAll = matchAll
        Me.matchAny = MatchAny
        Me.regexList = patterns.Select(Function(p) New Regex(p)).ToList()
    End Sub


    Public Function GetFilterConditions() As Dictionary(Of String, Object)
        Logout("*GetFilterConditions")
        '作成中
        Logout("作成中")
        'サブフォームDataGridViewから読み込み
        'いずれかに一致orすべて一致　チェックボックスを
        Dim filterDict = New Dictionary(Of String, Object)
        Dim dictStr = DictionaryAnyUtil.ConcatDictionaryToString(filterDict)
        Logout($"filterDict.Keys.Count = {filterDict.Keys.Count}")
        Logout($"filter dictStr = {dictStr}")
        Return filterDict
    End Function

    ''' <summary>
    ''' 文字列が条件に一致（または一致しない）かを判定
    ''' </summary>
    Private Function IsMatch(target As String) As Boolean
        Dim matchCount = regexList.AsEnumerable().Count(Function(r) r.IsMatch(target))
        Dim matchResult = If(matchAll, matchCount = regexList.Count, matchCount > 0)
        Return If(NotFlags(matchCount), Not matchResult, matchResult)
    End Function

    ''' <summary>
    ''' Dictionary をフィルタ
    ''' </summary>
    Public Function FilterDictionary(inputDict As Dictionary(Of String, String)) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)

        For Each kvp In inputDict
            If IsMatch(kvp.Value) Then
                result.Add(kvp.Key, kvp.Value)
            End If
        Next

        Return result
    End Function

    ''' <summary>
    ''' 単一カラムの DataRow を評価
    ''' </summary>
    Public Function IsDataRowMatch(row As DataRow, columnName As String) As Boolean
        If row.IsNull(columnName) Then Return False
        Return IsMatch(row(columnName).ToString())
    End Function

    ''' <summary>
    ''' 複数カラムを対象に DataRow を評価
    ''' </summary>
    Public Function IsDataRowMatchMultipleColumns(row As DataRow, columnNames As List(Of String)) As Boolean
        Dim matchCount As Integer = 0

        For Each colName In columnNames
            If Not row.IsNull(colName) AndAlso IsMatch(row(colName).ToString()) Then
                matchCount += 1
            End If
        Next

        ' カラム数に対して一致した数で判定
        Return If(matchAll, matchCount = columnNames.Count, matchCount > 0)
    End Function
End Class
