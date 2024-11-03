Public Module DataSetTestModule

    Public Class ConstDataSetName
        ' 定数クラスとして名前を管理
        Public Const NameA As String = "DataSetA"
        Public Const NameB As String = "DataSetB"
    End Class

    Public Function CreateDataSetTestMethodA() As DataSet
        ' データセットを生成
        Dim bufDataSet As DataSet = New DataSet()

        ' DataSetAのデータテーブルを作成し、データを格納
        Dim tableA As DataTable = New DataTable(ConstDataSetName.NameA)
        tableA.Columns.Add("Col1", GetType(String))
        tableA.Columns.Add("Col2", GetType(Integer))
        tableA.Columns.Add("Col3", GetType(Boolean))

        ' 任意のデータを追加
        tableA.Rows.Add("A1", 1, True)
        tableA.Rows.Add("A2", 2, False)
        tableA.Rows.Add("A3", 3, True)

        ' DataSetにテーブルを追加
        bufDataSet.Tables.Add(tableA)

        ' DataSetBのデータテーブルを作成し、データを格納
        Dim tableB As DataTable = New DataTable(ConstDataSetName.NameB)
        tableB.Columns.Add("Col1", GetType(String))
        tableB.Columns.Add("Col2", GetType(Integer))
        tableB.Columns.Add("Col3", GetType(Boolean))

        ' 任意のデータを追加
        tableB.Rows.Add("B1", 10, True)
        tableB.Rows.Add("B2", 20, False)
        tableB.Rows.Add("B3", 30, True)

        ' DataSetにテーブルを追加
        bufDataSet.Tables.Add(tableB)

        ' データセットを返す
        Return bufDataSet
    End Function

    Public Sub SetDataSetToDataGridView(argDataGridView As DataGridView, argDataSet As DataSet, tableName As String)
        ' 指定されたテーブル名に対応するDataTableを取得
        Dim targetTable As DataTable = argDataSet.Tables(tableName)

        ' DataGridViewにDataTableをバインド
        argDataGridView.DataSource = targetTable
    End Sub


    Public Function GetRowAsDictionary(ds As DataSet, tableName As String, rowIndex As Integer) As Dictionary(Of String, Object)
        Dim result As New Dictionary(Of String, Object)()

        ' Check if the DataTable exists in the DataSet
        If ds.Tables.Contains(tableName) Then
            Dim dt As DataTable = ds.Tables(tableName)

            ' Check if the specified rowIndex is within range
            If rowIndex >= 0 AndAlso rowIndex < dt.Rows.Count Then
                Dim row As DataRow = dt.Rows(rowIndex)

                ' Iterate through each column in the DataRow and add it to the dictionary
                For Each col As DataColumn In dt.Columns
                    result.Add(col.ColumnName, row(col))
                Next
            Else
                Throw New IndexOutOfRangeException("The specified rowIndex is out of range.")
            End If
        Else
            Throw New ArgumentException("The specified table name does not exist in the DataSet.")
        End If

        Return result
    End Function

    Public Sub PrettyPrintDictionary(dict As Dictionary(Of String, Object), Optional indent As Integer = 0)
        Dim indentString As String = New String(" "c, indent)

        For Each kvp As KeyValuePair(Of String, Object) In dict
            If TypeOf kvp.Value Is Dictionary(Of String, Object) Then
                ' Handle nested dictionaries
                Print($"{indentString}{kvp.Key}:")
                PrettyPrintDictionary(CType(kvp.Value, Dictionary(Of String, Object)), indent + 4)
            Else
                Print($"{indentString}{kvp.Key}: {kvp.Value}")
            End If
        Next
    End Sub

    Public Sub Print(value As Object)
        Dim buf = String.Format("{0}", value)
        Console.WriteLine(buf)
        'Debug.Write(buf)
    End Sub
End Module