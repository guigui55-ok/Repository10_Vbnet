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


    ''' <summary>
    ''' 指定した DataTable のデータを DataGridView に設定します（DataSourceは使用しません）。
    ''' </summary>
    ''' <param name="dataGridView">データを表示する DataGridView</param>
    ''' <param name="dataTable">表示元の DataTable</param>
    Public Shared Sub SetDataTableToDataGridView(dataGridView As DataGridView, dataTable As DataTable, isSetColumn As Boolean)
        If dataGridView Is Nothing OrElse dataTable Is Nothing Then
            Throw New ArgumentNullException("DataGridView または DataTable が null です。")
        End If

        ' DataSourceの解除
        dataGridView.DataSource = Nothing

        ' 既存のカラムと行をクリア
        'dataGridView.Columns.Clear()
        dataGridView.Rows.Clear()

        ' DataSourceを使うと DataGridViewSyncer でエラーが発生する
        'エラー：System.InvalidOperationException:コントロールがデータバインドされているとき、DataGridView の行コレクションにプログラムで行を追加することはできません。
        'dataGridView.DataSource = dataTable.Copy()

        If isSetColumn Then
            ' DataTable のカラムを DataGridView に追加
            For Each column As DataColumn In dataTable.Columns
                dataGridView.Columns.Add(column.ColumnName, column.ColumnName)
            Next
        End If

        'Error: ハンドルされていない例外: System.InvalidOperationException: 列を含んでいない DataGridView コントロールに行を追加することはできません。列を最初に追加してください。
        ' DataTable の行を DataGridView に追加
        For Each row As DataRow In dataTable.Rows
            dataGridView.Rows.Add(row.ItemArray)
        Next
    End Sub

    ''' <summary>
    ''' DataGridView から DataTable を作成します。
    ''' </summary>
    ''' <param name="dataGridView">元になる DataGridView</param>
    ''' <returns>DataGridView の内容をコピーした DataTable</returns>
    Public Shared Function CreateDataTableFromDataGridView(dataGridView As DataGridView) As DataTable
        Dim dt As New DataTable()

        ' カラムの作成
        For Each col As DataGridViewColumn In dataGridView.Columns
            ' 型は Object にしておく（後で個別に指定も可能）
            dt.Columns.Add(col.Name, GetType(Object))
        Next

        ' データの追加（新規行は除外）
        For Each row As DataGridViewRow In dataGridView.Rows
            If Not row.IsNewRow Then
                Dim values(dataGridView.Columns.Count - 1) As Object
                For i As Integer = 0 To dataGridView.Columns.Count - 1
                    values(i) = row.Cells(i).Value
                Next
                dt.Rows.Add(values)
            End If
        Next

        Return dt
    End Function

    ''' <summary>
    ''' DataGridView のカラム型を Oracle のスキーマ情報に基づいて変更する
    ''' </summary>
    ''' <param name="dataGridView">対象の DataGridView</param>
    ''' <param name="schemaTable">Oracle スキーマ情報の DataTable（COLUMN_NAME, DATA_TYPE を含む）</param>
    Public Shared Sub SetDataGridViewColumnTypesFromSchema(dataGridView As DataGridView, schemaTable As DataTable)
        For Each col As DataGridViewColumn In dataGridView.Columns
            ' 対応するカラム情報を検索（列名が一致するもの）
            Dim rows = schemaTable.Select($"COLUMN_NAME = '{col.Name}'")

            If rows.Length > 0 Then
                Dim dataType As String = rows(0)("DATA_TYPE").ToString().ToUpperInvariant()

                ' Oracleデータ型 → .NETデータ型 変換
                Select Case dataType
                    Case "CHAR", "VARCHAR2", "NCHAR", "NVARCHAR2"
                        col.ValueType = GetType(String)

                    Case "NUMBER"
                        ' 必要に応じて DATA_PRECISION / DATA_SCALE で型分岐可能
                        col.ValueType = GetType(Decimal)

                    Case "DATE", "TIMESTAMP"
                        col.ValueType = GetType(DateTime)
                        col.DefaultCellStyle.Format = "yyyy/M/d H:mm:ss"

                    Case "INTEGER", "INT"
                        col.ValueType = GetType(Integer)

                    Case "FLOAT"
                        col.ValueType = GetType(Single)

                    Case "DOUBLE"
                        col.ValueType = GetType(Double)

                    Case Else
                        ' 未対応の型は文字列として扱う
                        col.ValueType = GetType(String)
                End Select
            End If
        Next
    End Sub

    ''' <summary>
    ''' DataGridView の各セルの値を、対応する DataColumn の型に変換します。
    ''' DataSource が DataTable である必要があります。
    ''' </summary>
    ''' <param name="dataGridView">対象の DataGridView</param>
    Public Shared Sub ConvertCellValuesToColumnTypes(dataGridView As DataGridView)
        ' DataSource が DataTable か確認
        Dim dt As DataTable = TryCast(dataGridView.DataSource, DataTable)
        If dt Is Nothing Then
            Throw New InvalidOperationException("DataGridView の DataSource が DataTable ではありません。")
        End If

        For Each row As DataRow In dt.Rows
            For Each col As DataColumn In dt.Columns
                Dim value = row(col.ColumnName)

                ' DBNull や Nothing、空文字はそのまま
                If value Is DBNull.Value OrElse value Is Nothing OrElse value.ToString().Trim() = "" Then
                    Continue For
                End If

                '' 既に正しい型ならスキップ
                'If value.GetType() Is col.DataType Then
                '    Continue For
                'End If

                Try
                    ' 特別処理: DateTime の場合に OADate 対応
                    If col.DataType Is GetType(DateTime) AndAlso IsNumeric(value) Then
                        row(col.ColumnName) = DateTime.FromOADate(Convert.ToDouble(value))
                    Else
                        ' 通常の型変換
                        row(col.ColumnName) = Convert.ChangeType(value, col.DataType)
                    End If
                Catch ex As Exception
                    ' 変換失敗時は元の値を保持（必要ならログ出力なども）
                    ' row(col.ColumnName) = value.ToString()
                End Try
            Next
        Next
    End Sub

    ''' <summary>
    ''' DataGridView の DateTime カラムに「yyyy/M/d H:m:s」形式の表示を設定する
    ''' </summary>
    ''' <param name="dataGridView">対象の DataGridView</param>
    Public Shared Sub SetDateTimeFormat(dataGridView As DataGridView)
        For Each col As DataGridViewColumn In dataGridView.Columns
            If col.ValueType Is GetType(DateTime) Then
                col.DefaultCellStyle.Format = "yyyy/M/d H:mm:ss"
            End If
        Next
    End Sub

    ''' <summary>
    ''' DataGridView のカラム幅を自動調整し、最大幅を 200 に制限します。
    ''' </summary>
    ''' <param name="dataGridView">対象の DataGridView</param>
    Public Shared Sub AutoResizeColumnsWithMaxWidth(dataGridView As DataGridView)
        If dataGridView Is Nothing Then Return

        ' 内容に基づいて幅を自動調整
        dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

        ' 各カラムの最大幅を制限
        For Each col As DataGridViewColumn In dataGridView.Columns
            If col.Width > 200 Then
                col.Width = 200
            End If
        Next
    End Sub

End Class
