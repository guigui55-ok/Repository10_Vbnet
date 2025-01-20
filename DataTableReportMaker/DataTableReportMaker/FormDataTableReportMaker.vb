Imports System.IO
Imports System.Text.RegularExpressions

Public Class FormDataTableReportMaker

    Class Constants
        Public Shared ReadOnly CSV_WRITE_BEGIN_ROW = 1
        Public Shared ReadOnly CSV_WRITE_BEGIN_COL = 1
        Public Shared ReadOnly TABLE_OFFSET_ROW = 1
        Public Shared ReadOnly TABLE_OFFSET_COL = 1
        Public Shared ReadOnly NEXT_TABLE_OFFSET = 2
    End Class

    Private Sub FormDataTableReportMaker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 初期化処理（必要に応じて記述）
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim srcDir = TextBox_SrcDirPath.Text
        If Not Directory.Exists(srcDir) Then
            Dim msg = "Directory Path Not Exists = " + srcDir
            MessageBox.Show(msg)
            Exit Sub
        End If
        If Directory.GetFiles(srcDir).Length < 1 Then
            Dim msg = "Directory.GetFiles(srcDir).Length < 1 = " + srcDir
            MessageBox.Show(msg)
            Exit Sub
        End If

        Dim sourceDir = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "csv")
        Dim destDir = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReadData")
        Dim fileManager = New FileManager()
        Dim csvManager = New CsvManager()
        Dim excelManager = New ExcelManager()

        fileManager.CopyCsvFiles(srcDir, destDir)
        Dim csvFiles = fileManager.GetCsvFiles(destDir)
        Dim filterList = RichTextBox1.Text.Split(vbCrLf).ToList()

        Dim nowRow = 1
        Dim nowCol = 1
        Dim resultDict As New Dictionary(Of String, String)

        For Each filterValue In filterList
            Dim targetFiles = csvFiles.Where(Function(f) f.Contains(filterValue)).ToList()
            Dim dataTable As DataTable

            nowRow = 1
            nowCol = 1
            ' データ書き込み用の空のDataTableを用意する
            Dim tableName As String = ""
            Dim resultDataTable As DataTable = New DataTable

            For Each file In targetFiles
                Dim comment = IO.Path.GetFileNameWithoutExtension(file).Split("_").Last()
                tableName = IO.Path.GetFileNameWithoutExtension(file).Split("_").First()
                dataTable = csvManager.ReadCsv(file)

                '############ BEGIN
                Dim isRight As Boolean = DetermineWriteDirection(file)

                ' データテーブルにヘッダーとデータを追加
                AddHeaderAndData(dataTable)

                If isRight Then
                    ' 行と列を入れ替える
                    dataTable = TransposeDataTable(dataTable)

                    ' テーブル名とコメントを resultDataTable に書き込む
                    WriteMetaData(resultDataTable, nowRow, nowCol, tableName, comment)

                    ' 入れ替え済みのデータを resultDataTable に追加
                    WriteDataTable(resultDataTable, dataTable, nowRow, nowCol)

                    nowCol += Constants.NEXT_TABLE_OFFSET
                Else
                    ' テーブル名とコメントを resultDataTable に書き込む
                    WriteMetaData(resultDataTable, nowRow, nowCol, tableName, comment)

                    ' データを resultDataTable に追加
                    WriteDataTable(resultDataTable, dataTable, nowRow, nowCol)

                    nowRow += Constants.NEXT_TABLE_OFFSET
                End If
                '############ END
            Next

            Dim outputCsv = IO.Path.Combine(destDir, $"{tableName}_Result.csv")
            csvManager.WriteCsv(outputCsv, resultDataTable)
            Debug.WriteLine("outputCsv = " + outputCsv)
            resultDict(tableName) = outputCsv
        Next

        ' エクセルファイルにまとめて書き込む
        Dim excelPath = IO.Path.Combine(destDir, "FinalResult.xlsx")
        excelManager.WriteToExcel(excelPath, resultDict)
        MessageBox.Show("処理が完了しました。")
    End Sub

    ' 書き込み方向を判定する関数
    Private Function DetermineWriteDirection(fileName As String) As Boolean
        Dim regexRight As New Regex("_R_")
        Dim regexDown As New Regex("_D_")
        If regexRight.IsMatch(fileName) Then
            Return True ' Right
        ElseIf regexDown.IsMatch(fileName) Then
            Return False ' Down
        Else
            Throw New Exception("ファイル名に '_R_' または '_D_' が含まれていません: " & fileName)
        End If
    End Function

    ' ヘッダーとデータを追加する関数
    Private Sub AddHeaderAndData(dataTable As DataTable)
        Dim newRow As DataRow = dataTable.NewRow()
        newRow(0) = "カラム名"
        For col As Integer = 1 To dataTable.Columns.Count - 1
            newRow(col) = "データ" & col
        Next
        dataTable.Rows.InsertAt(newRow, 0)
    End Sub

    ' 行と列を入れ替える関数
    Private Function TransposeDataTable(dataTable As DataTable) As DataTable
        Dim transposedTable As New DataTable()

        ' 列を作成
        For i As Integer = 0 To dataTable.Rows.Count - 1
            transposedTable.Columns.Add("Column" & i)
        Next

        ' 行と列を入れ替え
        For col As Integer = 0 To dataTable.Columns.Count - 1
            Dim newRow As DataRow = transposedTable.NewRow()
            For row As Integer = 0 To dataTable.Rows.Count - 1
                newRow(row) = dataTable.Rows(row)(col)
            Next
            transposedTable.Rows.Add(newRow)
        Next

        Return transposedTable
    End Function

    ' メタデータを書き込む関数
    Private Sub WriteMetaData(resultDataTable As DataTable, nowRow As Integer, nowCol As Integer, tableName As String, comment As String)
        Dim metaRow As DataRow = resultDataTable.NewRow()
        metaRow(nowCol) = tableName
        resultDataTable.Rows.Add(metaRow)

        metaRow = resultDataTable.NewRow()
        metaRow(nowCol) = comment
        resultDataTable.Rows.Add(metaRow)
    End Sub

    ' データテーブルを書き込む関数
    Private Sub WriteDataTable(resultDataTable As DataTable, dataTable As DataTable, ByRef nowRow As Integer, ByRef nowCol As Integer)
        For Each row As DataRow In dataTable.Rows
            Dim newRow As DataRow = resultDataTable.NewRow()
            For col As Integer = 0 To dataTable.Columns.Count - 1
                newRow(nowCol + col) = row(col)
            Next
            resultDataTable.Rows.Add(newRow)
            nowRow += 1
        Next
    End Sub
End Class
