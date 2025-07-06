Imports System.Drawing

Public Class DataTableAnyUtil

    Public Class ConstDirection
        Public Const LEFT = "left"
        Public Const RIGHT = "right"
        Public Const TOP = "top"
        Public Const DOWN = "down"
    End Class

    Public Shared Function FindLastFilledCellInDirection(table As DataTable,
                                              startRow As Integer,
                                              startCol As Integer,
                                              direction As String) As Point
        Dim currentRow As Integer = startRow
        Dim currentCol As Integer = startCol

        ' 境界チェック
        If currentRow < 0 OrElse currentRow >= table.Rows.Count Then Return Point.Empty
        If currentCol < 0 OrElse currentCol >= table.Columns.Count Then Return Point.Empty

        Do While True
            ' 現在のセルが空なら1つ前のセルの位置を返す
            Dim value = table.Rows(currentRow)(currentCol)
            If IsDBNull(value) OrElse value Is Nothing OrElse value.ToString() = "" Then
                Select Case direction.ToLower()
                    Case "right"
                        Return New Point(currentCol - 1, currentRow)
                    Case "left"
                        Return New Point(currentCol + 1, currentRow)
                    Case "up"
                        Return New Point(currentCol, currentRow + 1)
                    Case "down"
                        Return New Point(currentCol, currentRow - 1)
                    Case Else
                        Return Point.Empty
                End Select
            End If

            ' 次に進む
            Select Case direction.ToLower()
                Case "right"
                    currentCol += 1
                    If currentCol >= table.Columns.Count Then Exit Do
                Case "left"
                    currentCol -= 1
                    If currentCol < 0 Then Exit Do
                Case "up"
                    currentRow -= 1
                    If currentRow < 0 Then Exit Do
                Case "down"
                    currentRow += 1
                    If currentRow >= table.Rows.Count Then Exit Do
                Case Else
                    Return Point.Empty
            End Select
        Loop

        ' 境界で抜けた場合、1つ前が最後の入力セル
        Select Case direction.ToLower()
            Case "right"
                Return New Point(currentCol - 1, currentRow)
            Case "left"
                Return New Point(currentCol + 1, currentRow)
            Case "up"
                Return New Point(currentCol, currentRow + 1)
            Case "down"
                Return New Point(currentCol, currentRow - 1)
            Case Else
                Return Point.Empty
        End Select
    End Function

    Public Shared Function GetCellValue(table As DataTable, row As Integer, col As Integer) As Object
        ' 範囲チェック
        If row < 0 OrElse row >= table.Rows.Count Then Return Nothing
        If col < 0 OrElse col >= table.Columns.Count Then Return Nothing

        ' 値を取得
        Return table.Rows(row)(col)
    End Function

    Public Shared Function ExtractDataTableRange(sourceTable As DataTable,
                                      startRow As Integer, startCol As Integer,
                                      endRow As Integer, endCol As Integer) As DataTable
        Dim resultTable As New DataTable()

        ' 範囲の正規化
        If startRow > endRow Then
            Dim temp = startRow
            startRow = endRow
            endRow = temp
        End If
        If startCol > endCol Then
            Dim temp = startCol
            startCol = endCol
            endCol = temp
        End If

        ' 範囲チェック
        If startRow < 0 OrElse endRow >= sourceTable.Rows.Count Then Return Nothing
        If startCol < 0 OrElse endCol >= sourceTable.Columns.Count Then Return Nothing

        ' 抜き出す列の定義
        For col = startCol To endCol
            Dim colName = sourceTable.Columns(col).ColumnName
            resultTable.Columns.Add(colName, sourceTable.Columns(col).DataType)
        Next

        ' 抜き出す行のデータ
        For row = startRow To endRow
            Dim newRow = resultTable.NewRow()
            For col = startCol To endCol
                newRow(col - startCol) = sourceTable.Rows(row)(col)
            Next
            resultTable.Rows.Add(newRow)
        Next

        Return resultTable
    End Function


    ''' <summary>
    ''' DataTableの1行目をカラム名に設定し、その1行目を削除します。
    ''' </summary>
    ''' <param name="dt">対象のDataTable</param>
    Public Shared Sub UseFirstRowAsHeaderAndRemove(ByRef dt As DataTable)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            Throw New ArgumentException("DataTableが空です。")
        End If

        ' 1行目のデータを取得
        Dim firstRow As DataRow = dt.Rows(0)
        Dim columnCount As Integer = dt.Columns.Count

        ' 一時的に全てのカラム名を変更
        For i As Integer = 0 To columnCount - 1
            Dim newColName As String = firstRow(i)?.ToString()
            If String.IsNullOrEmpty(newColName) Then
                newColName = $"Column{i + 1}"
            End If
            dt.Columns(i).ColumnName = newColName
        Next

        ' 1行目を削除
        dt.Rows.RemoveAt(0)
    End Sub


    Public Shared Function ConvertDataTableColumnTypes(schemaTable As DataTable, sourceTable As DataTable) As DataTable
        Dim resultTable As New DataTable()

        ' 1. カラム作成
        For Each col As DataColumn In sourceTable.Columns
            Dim rows = schemaTable.Select($"COLUMN_NAME = '{col.ColumnName}'")
            Dim targetType As Type = GetType(String)

            If rows.Length > 0 Then
                Dim dataType As String = rows(0)("DATA_TYPE").ToString().ToUpperInvariant()

                Select Case dataType
                    Case "CHAR", "VARCHAR2", "NCHAR", "NVARCHAR2"
                        targetType = GetType(String)
                    Case "NUMBER"
                        targetType = GetType(Decimal)
                    Case "INTEGER", "INT"
                        targetType = GetType(Integer)
                    Case "DATE", "TIMESTAMP"
                        targetType = GetType(DateTime)
                    Case "FLOAT"
                        targetType = GetType(Single)
                    Case "DOUBLE"
                        targetType = GetType(Double)
                    Case Else
                        targetType = GetType(String)
                End Select
            End If

            resultTable.Columns.Add(col.ColumnName, targetType)
        Next

        ' 2. データ変換して行を追加
        For Each row As DataRow In sourceTable.Rows
            Dim newRow As DataRow = resultTable.NewRow()

            For Each col As DataColumn In sourceTable.Columns
                Dim value = row(col.ColumnName)
                Dim targetType = resultTable.Columns(col.ColumnName).DataType

                If value Is DBNull.Value Then
                    newRow(col.ColumnName) = DBNull.Value
                Else
                    Try
                        ' 型変換（DateTimeの特別処理含む）
                        If targetType Is GetType(DateTime) Then
                            If TypeOf value Is Double OrElse IsNumeric(value) Then
                                ' Excelシリアル値として処理
                                newRow(col.ColumnName) = DateTime.FromOADate(Convert.ToDouble(value)).ToString("yyyy/M/d H:m:ss")
                            Else
                                newRow(col.ColumnName) = Convert.ToDateTime(value).ToString("yyyy/M/d H:m:ss")
                            End If
                        Else
                            newRow(col.ColumnName) = Convert.ChangeType(value, targetType)
                        End If
                    Catch ex As Exception
                        ' 変換失敗時は安全な文字列として格納
                        newRow(col.ColumnName) = value.ToString()
                    End Try
                End If
            Next

            resultTable.Rows.Add(newRow)
        Next

        Return resultTable
    End Function

End Class
