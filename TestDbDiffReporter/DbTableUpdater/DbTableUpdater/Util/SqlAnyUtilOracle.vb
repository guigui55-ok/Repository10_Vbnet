Public Class SqlAnyUtilOracle
    ''' <summary>
    ''' DataRowのデータをもとにOracle用のINSERT文を作成する
    ''' </summary>
    ''' <param name="tableName">INSERT先のテーブル名</param>
    ''' <param name="row">データソースのDataRow</param>
    ''' <returns>INSERT文文字列</returns>
    Public Shared Function CreateOracleInsertStatement(tableName As String, row As DataRow) As String
        Dim columnNames As New List(Of String)()
        Dim values As New List(Of String)()

        For Each column As DataColumn In row.Table.Columns
            columnNames.Add(column.ColumnName)

            Dim value As Object = row(column)

            If value Is DBNull.Value OrElse value Is Nothing Then
                values.Add("NULL")
            ElseIf TypeOf value Is String Then
                ' 文字列はシングルクォートをエスケープして囲む
                Dim strVal As String = value.ToString().Replace("'", "''")
                values.Add("'" & strVal & "'")
            ElseIf TypeOf value Is Date Then
                ' OracleのTO_DATE形式に変換
                Dim dateVal As Date = CType(value, Date)
                values.Add("TO_DATE('" & dateVal.ToString("yyyy-MM-dd HH:mm:ss") & "', 'YYYY-MM-DD HH24:MI:SS')")
            ElseIf TypeOf value Is Boolean Then
                values.Add(If(CType(value, Boolean), "1", "0"))
            Else
                ' 数値などその他の型はそのまま
                values.Add(value.ToString())
            End If
        Next

        Dim sql As String = String.Format("INSERT INTO {0} ({1}) VALUES ({2});",
                                          tableName,
                                          String.Join(", ", columnNames),
                                          String.Join(", ", values))
        Return sql
    End Function


    ''' <summary>
    ''' DataRowの内容からOracle用のUPDATE文を作成します（複数主キー対応）
    ''' </summary>
    ''' <param name="tableName">テーブル名</param>
    ''' <param name="row">更新対象のデータ</param>
    ''' <param name="primaryKeyList">主キー列名のリスト（更新対象から除外、WHERE句に使用）</param>
    ''' <returns>UPDATE文文字列</returns>
    Public Shared Function CreateOracleUpdateStatement(tableName As String,
                                            row As DataRow,
                                            primaryKeyList As List(Of String)) As String
        Dim setClauses As New List(Of String)()
        Dim whereClauses As New List(Of String)()

        For Each column As DataColumn In row.Table.Columns
            Dim colName As String = column.ColumnName
            Dim value As Object = row(colName)

            ' 値の整形
            Dim formattedValue As String
            If value Is DBNull.Value OrElse value Is Nothing Then
                formattedValue = "NULL"
            ElseIf TypeOf value Is String Then
                formattedValue = "'" & value.ToString().Replace("'", "''") & "'"
            ElseIf TypeOf value Is Date Then
                formattedValue = "TO_DATE('" & CType(value, Date).ToString("yyyy-MM-dd HH:mm:ss") & "', 'YYYY-MM-DD HH24:MI:SS')"
            ElseIf TypeOf value Is Boolean Then
                formattedValue = If(CType(value, Boolean), "1", "0")
            Else
                formattedValue = value.ToString()
            End If

            ' 主キーはWHERE句へ、それ以外はSET句へ
            If primaryKeyList.Any(Function(pk) String.Equals(pk, colName, StringComparison.OrdinalIgnoreCase)) Then
                whereClauses.Add($"{colName} = {formattedValue}")
            Else
                setClauses.Add($"{colName} = {formattedValue}")
            End If
        Next

        If setClauses.Count = 0 OrElse whereClauses.Count = 0 Then
            Return $"-- 無効なUPDATE文: SETまたはWHEREが空です"
        End If

        Dim sql As String = $"UPDATE {tableName} SET {String.Join(", ", setClauses)} WHERE {String.Join(" AND ", whereClauses)};"
        Return sql
    End Function


    ''' <summary>
    ''' DataRowの内容からOracle用のMERGE（UPSERT）文を作成する（複合主キー対応）
    ''' </summary>
    ''' <param name="tableName">対象テーブル名</param>
    ''' <param name="row">DataRowのデータ</param>
    ''' <param name="primaryKeyList">主キー列名のリスト</param>
    ''' <returns>MERGE文文字列</returns>
    Public Shared Function CreateOracleMergeStatement(tableName As String,
                                           row As DataRow,
                                           primaryKeyList As List(Of String)) As String
        Dim allColumns As New List(Of String)()
        Dim usingSelectParts As New List(Of String)()
        Dim onConditions As New List(Of String)()
        Dim updateSetParts As New List(Of String)()
        Dim insertColumns As New List(Of String)()
        Dim insertValues As New List(Of String)()

        For Each column As DataColumn In row.Table.Columns
            Dim colName As String = column.ColumnName
            Dim value As Object = row(colName)

            allColumns.Add(colName)

            ' 値をOracle SQL用にフォーマット
            Dim formattedValue As String
            If value Is DBNull.Value OrElse value Is Nothing Then
                formattedValue = "NULL"
            ElseIf TypeOf value Is String Then
                formattedValue = "'" & value.ToString().Replace("'", "''") & "'"
            ElseIf TypeOf value Is Date Then
                formattedValue = "TO_DATE('" & CType(value, Date).ToString("yyyy-MM-dd HH:mm:ss") & "', 'YYYY-MM-DD HH24:MI:SS')"
            ElseIf TypeOf value Is Boolean Then
                formattedValue = If(CType(value, Boolean), "1", "0")
            Else
                formattedValue = value.ToString()
            End If

            usingSelectParts.Add($"{formattedValue} AS {colName}")

            If primaryKeyList.Any(Function(pk) String.Equals(pk, colName, StringComparison.OrdinalIgnoreCase)) Then
                onConditions.Add($"T.{colName} = S.{colName}")
            Else
                updateSetParts.Add($"T.{colName} = S.{colName}")
            End If

            insertColumns.Add(colName)
            insertValues.Add($"S.{colName}")
        Next

        If primaryKeyList.Count = 0 Then
            Return "-- エラー: 主キーが指定されていません"
        End If

        Dim sql As New System.Text.StringBuilder()
        sql.AppendLine($"MERGE INTO {tableName} T")
        sql.AppendLine($"USING (SELECT {String.Join(", ", usingSelectParts)} FROM DUAL) S")
        sql.AppendLine($"ON ({String.Join(" AND ", onConditions)})")
        sql.AppendLine("WHEN MATCHED THEN")
        sql.AppendLine($"  UPDATE SET {String.Join(", ", updateSetParts)}")
        sql.AppendLine("WHEN NOT MATCHED THEN")
        sql.AppendLine($"  INSERT ({String.Join(", ", insertColumns)})")
        sql.AppendLine($"  VALUES ({String.Join(", ", insertValues)})")

        Return sql.ToString()
    End Function


    ''' <summary>
    ''' Oracleの主キー列取得SQLを生成する関数（ALL_CONSTRAINTS を使用）
    ''' </summary>
    ''' <param name="tableName">対象テーブル名</param>
    ''' <param name="row">データが格納されているDataRow</param>
    ''' <returns>主キー取得用のSQL文字列</returns>
    Public Shared Function GeneratePrimaryKeySelectSql(userName As String, tableName As String, row As DataRow) As String
        ' スキーマ名を取得（DataTable.Namespace または拡張プロパティから取得可能）
        Dim schemaName As String = Nothing

        If Not String.IsNullOrEmpty(row.Table.Namespace) Then
            schemaName = row.Table.Namespace
        ElseIf row.Table.ExtendedProperties.ContainsKey("SchemaName") Then
            schemaName = row.Table.ExtendedProperties("SchemaName").ToString()
        Else
            'schemaName = "USER" ' または CURRENT_SCHEMA でもOK
            schemaName = userName ' または CURRENT_SCHEMA でもOK
        End If

        ' スキーマ名・テーブル名はOracleでは通常大文字で扱われる
        schemaName = schemaName.ToUpper()
        tableName = tableName.ToUpper()

        ' SQL生成
        Dim sql As String = $"
SELECT acc.COLUMN_NAME
FROM ALL_CONSTRAINTS ac
JOIN ALL_CONS_COLUMNS acc
  ON ac.OWNER = acc.OWNER
 AND ac.CONSTRAINT_NAME = acc.CONSTRAINT_NAME
WHERE ac.CONSTRAINT_TYPE = 'P'
  AND ac.TABLE_NAME = '{tableName}'
  AND ac.OWNER = '{schemaName}'
ORDER BY acc.POSITION"
        Return sql.Trim()
    End Function


    ''' <summary>
    ''' 主キー条件に合致するROWIDを取得するSQL文を生成する
    ''' </summary>
    ''' <param name="tableName">テーブル名</param>
    ''' <param name="primaryKeyValues">主キー列名とその値のペア</param>
    ''' <returns>ROWIDを取得するSQL文</returns>
    Public Shared Function GenerateSelectRowIdSql(tableName As String,
                                       primaryKeyValues As Dictionary(Of String, Object)) As String
        If String.IsNullOrWhiteSpace(tableName) Then
            Throw New ArgumentException("テーブル名が指定されていません。")
        End If
        If primaryKeyValues Is Nothing OrElse primaryKeyValues.Count = 0 Then
            Throw New ArgumentException("主キーが指定されていません。")
        End If

        Dim conditions As New List(Of String)()

        For Each kvp In primaryKeyValues
            Dim colName As String = kvp.Key
            Dim value As Object = kvp.Value

            Dim formattedValue As String
            If value Is Nothing OrElse value Is DBNull.Value Then
                formattedValue = "IS NULL"
                conditions.Add($"{colName} {formattedValue}")
                Continue For
            End If

            If TypeOf value Is String Then
                formattedValue = $"'{value.ToString().Replace("'", "''")}'"
            ElseIf TypeOf value Is Date Then
                formattedValue = $"TO_DATE('{CDate(value).ToString("yyyy-MM-dd HH:mm:ss")}', 'YYYY-MM-DD HH24:MI:SS')"
            ElseIf TypeOf value Is Boolean Then
                formattedValue = If(CBool(value), "1", "0")
            Else
                formattedValue = value.ToString()
            End If

            conditions.Add($"{colName} = {formattedValue}")
        Next

        Dim whereClause As String = String.Join(" AND ", conditions)
        Dim sql As String = $"SELECT ROWID FROM {tableName} WHERE {whereClause};"

        Return sql
    End Function


    ''' <summary>
    ''' OracleのALL_TAB_COLUMNSを使って、指定テーブルのカラム名とデータ型を取得するSQLを生成する
    ''' </summary>
    ''' <param name="tableName">テーブル名</param>
    ''' <param name="schemaName">スキーマ名（省略時はUSERを使用）</param>
    ''' <returns>カラム情報取得SQL文字列</returns>
    Public Shared Function GenerateColumnInfoSql(tableName As String, Optional schemaName As String = Nothing) As String
        If String.IsNullOrWhiteSpace(tableName) Then
            Throw New ArgumentException("テーブル名が指定されていません。")
        End If

        Dim ret = ""
        ' スキーマ未指定の場合はUSER視点で取得（ALL_TAB_COLUMNSではOWNERが必要）
        If String.IsNullOrWhiteSpace(schemaName) Then
            ret = $"
SELECT COLUMN_NAME, DATA_TYPE, DATA_LENGTH, DATA_PRECISION, DATA_SCALE
FROM USER_TAB_COLUMNS
WHERE TABLE_NAME = '{tableName.ToUpper()}'
ORDER BY COLUMN_ID"
        Else
            ret = $"
SELECT COLUMN_NAME, DATA_TYPE, DATA_LENGTH, DATA_PRECISION, DATA_SCALE
FROM ALL_TAB_COLUMNS
WHERE TABLE_NAME = '{tableName.ToUpper()}'
  AND OWNER = '{schemaName.ToUpper()}'
ORDER BY COLUMN_ID"
        End If
        Return ret.Trim
    End Function

End Class
