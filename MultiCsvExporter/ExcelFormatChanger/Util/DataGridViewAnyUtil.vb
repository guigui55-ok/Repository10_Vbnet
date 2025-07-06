Public Class DataGridViewAnyUtil
    ''' <summary>
    ''' DataGridViewの指定行のセル値をObject()配列として取得する
    ''' </summary>
    ''' <param name="dgv">対象のDataGridView</param>
    ''' <param name="rowIndex">取得する行のインデックス（例：1）</param>
    ''' <returns>Object()配列（セル値）</returns>
    Public Shared Function GetRowValues(dgv As DataGridView, rowIndex As Integer) As Object()
        If dgv Is Nothing Then Throw New ArgumentNullException(NameOf(dgv))
        If rowIndex < 0 OrElse rowIndex >= dgv.Rows.Count Then
            Throw New ArgumentOutOfRangeException(NameOf(rowIndex), "行インデックスが範囲外です。")
        End If

        Dim row As DataGridViewRow = dgv.Rows(rowIndex)
        Dim values(row.Cells.Count - 1) As Object

        For i As Integer = 0 To row.Cells.Count - 1
            values(i) = row.Cells(i).Value
        Next

        Return values
    End Function
    ' DataGridViewの最後のデータ行を取得する関数
    Public Shared Function GetLastDataRow(dgv As DataGridView) As DataGridViewRow
        Dim lastIndex As Integer = dgv.Rows.Count - 1

        ' 新しい行（追加用行）が存在する場合、それを除外する
        If dgv.AllowUserToAddRows Then
            lastIndex -= 1
        End If

        If lastIndex >= 0 Then
            Return dgv.Rows(lastIndex)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' 指定した行に文字列配列の値を左から順に設定します（開始列指定可）
    ''' </summary>
    ''' <param name="dgv">対象のDataGridView</param>
    ''' <param name="rowIndex">書き込む行インデックス</param>
    ''' <param name="values">書き込む文字列配列</param>
    ''' <param name="startColIndex">開始列インデックス（左端は0）</param>
    Public Shared Sub SetRowValuesFromArray(dgv As DataGridView, rowIndex As Integer, values As Object(), startColIndex As Integer)
        If rowIndex < 0 OrElse rowIndex >= dgv.Rows.Count Then Exit Sub
        If startColIndex < 0 Then startColIndex = 0

        Dim targetRow As DataGridViewRow = dgv.Rows(rowIndex)

        For i As Integer = 0 To values.Length - 1
            Dim colIndex As Integer = startColIndex + i
            If colIndex >= dgv.ColumnCount Then Exit For ' 列数を超えたら終了
            targetRow.Cells(colIndex).Value = values(i)
        Next
    End Sub
End Class
