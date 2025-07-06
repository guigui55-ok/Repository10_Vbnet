Public Class DataGridViewAnyUtil
    ''' <summary>
    ''' 指定した DataTable のデータを DataGridView に設定します。
    ''' </summary>
    ''' <param name="dataGridView">データを表示する DataGridView</param>
    ''' <param name="dataTable">表示元の DataTable</param>
    Public Shared Sub SetDataTableToDataGridView(dataGridView As DataGridView, dataTable As DataTable)
        If dataGridView Is Nothing OrElse dataTable Is Nothing Then
            Throw New ArgumentNullException("DataGridView または DataTable が null です。")
        End If
        ' DataSourceの解除
        dataGridView.DataSource = Nothing

        ' データグリッドビューの既存データとカラムをクリア
        dataGridView.Columns.Clear()
        dataGridView.Rows.Clear()

        ' DataSourceを使う方法（推奨）
        dataGridView.DataSource = dataTable.Copy()
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
