Imports System.Diagnostics

Public Class TableDataManager

    Private MainDataSet As DataSet

    ' コンストラクタ
    Public Sub New()
        ' MainDataSet の初期化
        MainDataSet = New DataSet()
    End Sub

    ' OutputConsole 関数 (Debug.WriteLine で出力)
    Private Sub pOutputConsole(message As String)
        Debug.WriteLine(message)
    End Sub

    ' OutputConsole 関数 (Debug.WriteLine で出力)
    Public Shared Sub OutputConsole(message As String)
        Debug.WriteLine(message)
    End Sub

    ' DataTable を追加
    Public Sub AddTable(tableName As String)
        If Not MainDataSet.Tables.Contains(tableName) Then
            MainDataSet.Tables.Add(New DataTable(tableName))
        Else
            'Throw New ArgumentException($"テーブル '{tableName}' はすでに DataSet に存在します。")
            '存在する場合は何もしない
        End If
    End Sub


    ' プリミティブな型からデータをターゲット DataTable に挿入
    Public Sub InsertPrimitiveData(targetTableName As String, value As Object, targetRow As Integer, targetCol As Integer)
        If Not MainDataSet.Tables.Contains(targetTableName) Then
            Throw New ArgumentException($"テーブル '{targetTableName}' は DataSet に存在しません。")
        End If

        Dim targetTable As DataTable = MainDataSet.Tables(targetTableName)

        ' 行と列が存在するか確認し、必要なら追加
        While targetTable.Rows.Count <= targetRow
            targetTable.Rows.Add(targetTable.NewRow())
        End While

        While targetTable.Columns.Count <= targetCol
            targetTable.Columns.Add()
        End While

        ' データを挿入
        Dim buf = value.ToString()
        targetTable.Rows(targetRow)(targetCol) = If(value Is Nothing, Nothing, buf)
    End Sub

    Public Function GetRowValuesString(rowValue As DataRow) As String
        Dim retList = New List(Of String)()
        Dim ret = ""
        For Each col In rowValue.Table.Columns
            Dim bufValue As String = rowValue(col).ToString()
            retList.Add(bufValue)
            ret += col.ToString() + ":" + bufValue + ", "
        Next
        If rowValue.Table.Columns.Count > 0 Then
            ret = ret.Substring(0, ret.Length - 2)
        End If
        Return ret
    End Function


    ' ソース DataTable のデータをターゲット DataTable に挿入
    Public Sub InsertData(
        targetTableName As String,
        sourceTable As DataTable,
        beginRow As Integer,
        beginCol As Integer,
        writeCol As Boolean
    )
        If Not MainDataSet.Tables.Contains(targetTableName) Then
            Throw New ArgumentException($"テーブル '{targetTableName}' は DataSet に存在しません。")
        End If

        Dim targetTable As DataTable = MainDataSet.Tables(targetTableName)

        Dim startRow As Integer = 0
        If writeCol Then
            Dim bufRow As Integer
            Dim bufCol As Integer
            Dim targetRowIdx As Integer = beginRow + startRow
            For j As Integer = 0 To sourceTable.Columns.Count - 1
                Dim targetColIdx As Integer = beginCol + j
                While targetTable.Rows.Count <= targetRowIdx
                    targetTable.Rows.Add(targetTable.NewRow())
                End While

                While targetTable.Columns.Count <= targetColIdx
                    targetTable.Columns.Add()
                End While

                targetTable.Rows(targetRowIdx)(targetColIdx) = targetTable.Columns(j).ColumnName
                bufCol = targetColIdx
            Next
            Debug.WriteLine("targetRowIdx0 = " + (beginRow + startRow).ToString())
            Debug.WriteLine($"[j] " + GetRowValuesString(targetTable.Rows(targetRowIdx)))
            startRow = 1
        End If
        For i As Integer = 0 To (sourceTable.Rows.Count - 1)
            Dim bufRow As Integer
            Dim targetRowIdx As Integer = beginRow + i + startRow
            For j As Integer = 0 To sourceTable.Columns.Count - 1
                Dim targetColIdx As Integer = beginCol + j

                ' ターゲットテーブルに十分な行と列があるか確認し、必要なら追加
                While targetTable.Rows.Count <= targetRowIdx
                    targetTable.Rows.Add(targetTable.NewRow())
                End While

                While targetTable.Columns.Count <= targetColIdx
                    targetTable.Columns.Add()
                End While

                ' データを挿入
                targetTable.Rows(targetRowIdx)(targetColIdx) = sourceTable.Rows(i)(j)
                bufRow = targetRowIdx
            Next
            Debug.WriteLine("targetRowIdx = " + bufRow.ToString())
            Debug.WriteLine($"[j] " + GetRowValuesString(targetTable.Rows(targetRowIdx)))
        Next
    End Sub

    ' DataSet の情報を出力
    Public Sub PrintDataSetInfo()
        For Each table As DataTable In MainDataSet.Tables
            OutputConsole($"Table: {table.TableName}")
            OutputConsole($"Rows.Count: {table.Rows.Count}, Columns.Count: {table.Columns.Count}")
            Dim columnNames As String = String.Join(", ", table.Columns.Cast(Of DataColumn)().[Select](Function(c) c.ColumnName))
            OutputConsole($"Columns: {columnNames}")
        Next
    End Sub




    ' DataSet の指定位置の Rows を出力
    Public Sub PrintRows(tableName As String, rowIndex As Integer)
        If Not MainDataSet.Tables.Contains(tableName) Then
            Throw New ArgumentException($"テーブル '{tableName}' は DataSet に存在しません。")
        End If

        Dim table As DataTable = MainDataSet.Tables(tableName)
        If rowIndex < 0 OrElse rowIndex >= table.Rows.Count Then
            Throw New ArgumentOutOfRangeException($"行番号 {rowIndex} は範囲外です。")
        End If

        Dim rowValues As String = String.Join(", ", table.Rows(rowIndex).ItemArray)
        OutputConsole($"Row[{rowIndex}]: {rowValues}")
    End Sub

    ' DataSet 全体を出力
    Public Sub PrintDataSetContents()
        For Each table As DataTable In MainDataSet.Tables
            OutputConsole($"Table: {table.TableName}")
            Dim columnNames As String = String.Join(", ", table.Columns.Cast(Of DataColumn)().[Select](Function(c) c.ColumnName))
            OutputConsole($"Columns: {columnNames}")
            OutputConsole("Rows:")
            For i As Integer = 0 To table.Rows.Count - 1
                Dim rowValues As String = String.Join(", ", table.Rows(i).ItemArray)
                OutputConsole($"[{i}] {rowValues}")
            Next
        Next
    End Sub


    ' DataSet 全体を出力
    Public Shared Sub PrintDataSetContentsOther(otherDataSet As DataSet)
        For Each table As DataTable In otherDataSet.Tables
            OutputConsole($"Table: {table.TableName}")
            Dim columnNames As String = String.Join(", ", table.Columns.Cast(Of DataColumn)().[Select](Function(c) c.ColumnName))
            OutputConsole($"Columns: {columnNames}")
            OutputConsole("Rows:")
            For i As Integer = 0 To table.Rows.Count - 1
                Dim rowValues As String = String.Join(", ", table.Rows(i).ItemArray)
                OutputConsole($"[{i}] {rowValues}")
            Next
        Next
    End Sub


    ' DataSet 全体を出力
    Public Shared Sub PrintDataTableContentsOther(otherDataTable As DataTable)
        Dim table As DataTable = otherDataTable
        OutputConsole($"Table: {table.TableName}")
        Dim columnNames As String = String.Join(", ", table.Columns.Cast(Of DataColumn)().[Select](Function(c) c.ColumnName))
        OutputConsole($"Columns: {columnNames}")
        OutputConsole("Rows:")
        For i As Integer = 0 To table.Rows.Count - 1
            Dim rowValues As String = String.Join(", ", table.Rows(i).ItemArray)
            OutputConsole($"[{i}] {rowValues}")
        Next
    End Sub

    ' 現在の DataSet を取得
    Public Function GetDataSet() As DataSet
        Return MainDataSet
    End Function

    ' 任意の DataTable を取得
    Public Function GetDataTable(tableName As String) As DataTable
        Return MainDataSet.Tables(tableName)
    End Function

End Class
