Module Module1

    Sub Main()
        TestMain()
    End Sub

    Sub TestMain()
        ' DataTableUtility クラスのインスタンスを作成
        Dim utility As New TableDataManager()

        ' テーブルを追加
        utility.AddTable("TestTable")

        ' SourceTableA を準備してデータ挿入
        Dim sourceTableA As New DataTable()
        sourceTableA.Columns.Add("Col1")
        sourceTableA.Columns.Add("Col2")
        sourceTableA.Rows.Add("Data1", "Data2")
        sourceTableA.Rows.Add("Data3", "Data4")
        utility.InsertData("TestTable", sourceTableA, 0, 0, False)

        ' SourceTableB を準備してデータ挿入
        Dim sourceTableB As New DataTable()
        sourceTableB.Columns.Add("ColA")
        sourceTableB.Columns.Add("ColB")
        sourceTableB.Columns.Add("ColC")
        sourceTableB.Columns.Add("ColD")
        sourceTableB.Rows.Add("BData1", "BData2", "BData3", "BData4")
        sourceTableB.Rows.Add("BData5", "BData6", "BData7", "BData8")
        utility.InsertData("TestTable", sourceTableB, 7, 2, False)

        ' DataSet 情報を出力
        utility.PrintDataSetInfo()

        ' 指定行を出力
        utility.PrintRows("TestTable", 0)
        utility.PrintRows("TestTable", 7)

        ' DataSet 全体を出力
        utility.PrintDataSetContents()

        ' DataSet を取得
        Dim currentDataSet As DataSet = utility.GetDataSet()
    End Sub


    'Sub TestMain()
    '    ' DataTableUtility クラスのインスタンスを作成
    '    Dim utility As New DataTableUtility()

    '    ' テーブルを追加
    '    utility.AddTable("TestTable")

    '    ' データを準備
    '    Dim sourceTable As New DataTable()
    '    sourceTable.Columns.Add("Col1")
    '    sourceTable.Columns.Add("Col2")
    '    sourceTable.Rows.Add("Data1", "Data2")
    '    sourceTable.Rows.Add("Data3", "Data4")

    '    ' データを挿入
    '    'utility.InsertData(sourceTable, "TestTable", 0, 0)
    '    utility.InsertData(sourceTable, "TestTable", 3, 3)

    '    ' DataSet 情報を出力
    '    utility.PrintDataSetInfo()

    '    ' 指定行を出力
    '    utility.PrintRows("TestTable", 0)

    '    ' DataSet 全体を出力
    '    utility.PrintDataSetContents()

    '    ' DataSet を取得
    '    Dim currentDataSet As DataSet = utility.GetDataSet()
    'End Sub


End Module
