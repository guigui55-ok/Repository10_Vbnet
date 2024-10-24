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

End Module