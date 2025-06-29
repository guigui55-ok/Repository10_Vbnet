Public Class DataTableManager

    Public _ds As DataSet

    Public Function ConvertDataTableToListList(dt As DataTable) As List(Of List(Of String))
        Dim retListList = New List(Of List(Of String))
        Dim _list = New List(Of String)
        For Each col As DataColumn In dt.Columns
            _list.Add(col.ColumnName)
        Next
        retListList.Add(_list)
        For Each row As DataRow In dt.Rows
            _list = New List(Of String)
            For Each col As DataColumn In dt.Columns
                Dim _value = ConvertNollToString(row(col.ColumnName))
                _list.Add(_value)
            Next
            retListList.Add(_list)
        Next
        Return retListList
    End Function

    Public Function ConvertDataTableToDictionaryList(dt As DataTable) As List(Of Dictionary(Of String, Object))
        Dim retDictList = New List(Of Dictionary(Of String, Object))
        For Each row As DataRow In dt.Rows
            Dim _dict = New Dictionary(Of String, Object)
            For Each col As DataColumn In dt.Columns
                Dim _value = ConvertNollToString(row(col.ColumnName))
                _dict(col.ColumnName) = _value
            Next
            retDictList.Add(_dict)
        Next
        Return retDictList
    End Function


    Private Function ConvertNollToString(_value As Object)
        If _value Is Nothing Or IsDBNull(_value) Then
            Return ""
        Else
            Return _value.ToString()
        End If
    End Function


    ''' <summary>
    ''' コピー元のDataTableを、コピー先のDataTableの任意の位置に挿入します。
    ''' コピー先に十分な行や列がない場合は自動で追加されます。
    ''' </summary>
    ''' <param name="destTable">コピー先のDataTable</param>
    ''' <param name="sourceTable">コピー元のDataTable</param>
    ''' <param name="startRow">コピー先の開始行インデックス</param>
    ''' <param name="startCol">コピー先の開始列インデックス</param>
    Public Sub CopyDataTableToPosition(destTable As DataTable, sourceTable As DataTable, startRow As Integer, startCol As Integer)
        ' 列を自動追加（列名は"ColumnX"の形式）
        Dim requiredCols As Integer = startCol + sourceTable.Columns.Count
        While destTable.Columns.Count < requiredCols
            destTable.Columns.Add("Column" & destTable.Columns.Count.ToString())
        End While

        ' 行を自動追加
        Dim requiredRows As Integer = startRow + sourceTable.Rows.Count
        While destTable.Rows.Count < requiredRows
            destTable.Rows.Add(destTable.NewRow())
        End While

        ' データをコピー
        For r As Integer = 0 To sourceTable.Rows.Count - 1
            For c As Integer = 0 To sourceTable.Columns.Count - 1
                destTable.Rows(startRow + r)(startCol + c) = sourceTable.Rows(r)(c)
            Next
        Next
    End Sub


    ''' <summary>
    ''' 指定されたDataTableの中で、値が入力されている最終行のインデックスを取得します。
    ''' 値が空文字、Nothing、DBNull.Valueのみの行は無視されます。
    ''' </summary>
    ''' <param name="table">対象のDataTable</param>
    ''' <returns>最終的に値が入力されている行のインデックス、該当なしは -1</returns>
    Public Function GetLastRowIndex(table As DataTable) As Integer
        If table Is Nothing OrElse table.Rows.Count = 0 Then
            Return -1
        End If

        For i As Integer = table.Rows.Count - 1 To 0 Step -1
            Dim row As DataRow = table.Rows(i)
            Dim hasValue As Boolean = False
            For Each item In row.ItemArray
                If item IsNot Nothing AndAlso
               Not (TypeOf item Is DBNull) AndAlso
               Not (TypeOf item Is String AndAlso String.IsNullOrWhiteSpace(DirectCast(item, String))) Then
                    hasValue = True
                    Exit For
                End If
            Next

            If hasValue Then
                Return i
            End If
        Next

        Return -1
    End Function



    ''' <summary>
    ''' 指定されたDataTableの中で、値が入力されている最終行を取得します。
    ''' 値が空文字、Nothing、DBNull.Valueのみの行は無視されます。
    ''' </summary>
    ''' <param name="table">対象のDataTable</param>
    ''' <returns>最終的に値が入力されているDataRow、またはNothing</returns>
    Public Function GetLastRow(table As DataTable) As DataRow
        If table Is Nothing OrElse table.Rows.Count = 0 Then
            Return Nothing
        End If

        For i As Integer = table.Rows.Count - 1 To 0 Step -1
            Dim row As DataRow = table.Rows(i)
            Dim hasValue As Boolean = False
            For Each item In row.ItemArray
                If item IsNot Nothing AndAlso
               Not (TypeOf item Is DBNull) AndAlso
               Not (TypeOf item Is String AndAlso String.IsNullOrWhiteSpace(DirectCast(item, String))) Then
                    hasValue = True
                    Exit For
                End If
            Next

            If hasValue Then
                Return row
            End If
        Next

        Return Nothing
    End Function



    ''' <summary>
    ''' DataSetにDataTableを追加する。
    ''' （同じTableNameだったら後ろに「_数字」を付けて、追加する）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="dt"></param>
    Public Sub AddDataTableToDataSet(ByRef ds As DataSet, dt As DataTable)
        If ds.Tables.Contains(dt.TableName) Then

            Dim matches As Text.RegularExpressions.MatchCollection
            matches = System.Text.RegularExpressions.Regex.Matches(dt.TableName, "_\d{1,2}$", System.Text.RegularExpressions.RegexOptions.None)
            Dim num = 1
            Dim matchStr = ""
            For Each match As System.Text.RegularExpressions.Match In matches
                matchStr = match.Value
                If IsNumeric(matchStr) Then
                    num = CInt(matchStr) + 1
                End If
                Exit For
            Next
            If num > 1 Then
                dt.TableName = dt.TableName.Substring(0, dt.TableName - Len(matchStr)) + "_" + num.ToString
            Else
                dt.TableName = dt.TableName + "_1"
            End If
            AddDataTableToDataSet(ds, dt)
        Else
            ds.Tables.Add(dt)
        End If
    End Sub

    ''' <summary>
    ''' DataTableの指定座標に文字列をセットします。行や列が不足している場合は自動的に追加します。
    ''' </summary>
    ''' <param name="table">対象のDataTable</param>
    ''' <param name="row">行インデックス</param>
    ''' <param name="col">列インデックス</param>
    ''' <param name="value">セットする文字列</param>
    Public Sub SetValueAt(table As DataTable, row As Integer, col As Integer, value As String)
        If table Is Nothing Then Return

        ' 列追加
        While table.Columns.Count <= col
            table.Columns.Add("Column" & table.Columns.Count.ToString())
        End While

        ' 行追加
        While table.Rows.Count <= row
            table.Rows.Add(table.NewRow())
        End While

        table.Rows(row)(col) = value
    End Sub

    ''' <summary>
    ''' DataTableの指定座標から、右方向に文字列配列の値をセットします。
    ''' </summary>
    ''' <param name="table">対象のDataTable</param>
    ''' <param name="startRow">開始行</param>
    ''' <param name="startCol">開始列</param>
    ''' <param name="values">書き込む文字列配列</param>
    Public Sub SetValuesRight(table As DataTable, startRow As Integer, startCol As Integer, values As String())
        If values Is Nothing OrElse values.Length = 0 Then Return
        For i As Integer = 0 To values.Length - 1
            SetValueAt(table, startRow, startCol + i, values(i))
        Next
    End Sub

    ''' <summary>
    ''' DataTableの指定座標から、下方向に文字列配列の値をセットします。
    ''' </summary>
    ''' <param name="table">対象のDataTable</param>
    ''' <param name="startRow">開始行</param>
    ''' <param name="startCol">開始列</param>
    ''' <param name="values">書き込む文字列配列</param>
    Public Sub SetValuesDown(table As DataTable, startRow As Integer, startCol As Integer, values As String())
        If values Is Nothing OrElse values.Length = 0 Then Return
        For i As Integer = 0 To values.Length - 1
            SetValueAt(table, startRow + i, startCol, values(i))
        Next
    End Sub



    Public Shared Sub UsageSample()
        Dim manager As New DataTableManager()
        Dim table As New DataTable()

        ' 単一値のセット
        manager.SetValueAt(table, 2, 3, "Hello")

        ' 配列（右方向）
        manager.SetValuesRight(table, 0, 0, New String() {"A", "B", "C"})

        ' 配列（下方向）
        manager.SetValuesDown(table, 0, 5, New String() {"1", "2", "3"})
    End Sub

End Class
