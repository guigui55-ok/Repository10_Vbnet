Imports System.IO
Imports System.Text.RegularExpressions
Imports DataTableUtility
Public Class FormDataTableReportMaker

    Class ConstErrorFlag
        Public Shared RESULT_OK = 1
        Public Shared SYSTEM_ERROR = -2
        Public Shared RESULT_NG = -3
        Public Shared FILE_NAME_ERROR = 11
    End Class

    Class Constants
        Public Shared ReadOnly CSV_WRITE_BEGIN_ROW = 1
        Public Shared ReadOnly CSV_WRITE_BEGIN_COL = 1
        Public Shared ReadOnly TABLE_OFFSET_ROW = 1
        Public Shared ReadOnly TABLE_OFFSET_COL = 1
        Public Shared ReadOnly NEXT_TABLE_OFFSET = 2
        '
        Public Shared RESULT_RIGHT = 11
        Public Shared RESULT_DOWN = 12
    End Class

    Private Sub FormDataTableReportMaker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 初期化処理（必要に応じて記述）

        Me.KeyPreview = True

    End Sub

    Private Sub FormDataTableReportMaker_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Oem5 AndAlso e.Control Then
            OutputConsole("KeyDown \ + Ctrl")
            'Dim dirPath = Path.GetDirectoryName(Application.ExecutablePath)
            'Dim dirPath = Path.GetDirectoryName(Application.StartupPath)
            'Dim dirPath = Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location)
            Dim dirPath = Path.GetDirectoryName(GetExePath())
            OutputConsole("dirPath=" + dirPath)
            OpenPath(dirPath)
        End If
    End Sub

    Private Sub OpenPath(targetPath As String)
        ' ファイルが存在するか確認
        If File.Exists(targetPath) Or Directory.Exists(targetPath) Then
            ' エクスプローラーでファイルを選択した状態で開く
            Process.Start("explorer.exe", "/select,""" & targetPath & """")
        Else
            MessageBox.Show("指定されたファイルが存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub


    Private Function GetExePath() As String
        ' 実行ファイルの完全なパス（ファイル名を含む）
        Dim exePath As String = Reflection.Assembly.GetExecutingAssembly().Location
        ' フォルダーパス（bin\Debug または bin\Release）
        Dim exeDirectory As String = Path.GetDirectoryName(exePath)

        ' ビルド構成を確認
        Dim buildConfig As String = If(exeDirectory.ToLower().Contains("debug"), "Debug",
                                       If(exeDirectory.ToLower().Contains("release"), "Release", "Unknown"))

        '' 結果を表示
        'MessageBox.Show("EXEファイルのパス: " & exePath & vbCrLf &
        '                "フォルダーパス: " & exeDirectory & vbCrLf &
        '                "ビルド構成: " & buildConfig, "実行ファイル情報")

        Dim retPath = Path.Combine(exePath, buildConfig)
        Return retPath
    End Function


    Private Sub OutputConsole(value As Object)
        Dim buf = String.Format("{0}", value)
        Debug.WriteLine(buf)
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
        Dim destDir = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Result")
        Dim fileManager = New FileManager()
        Dim csvManager = New CsvManager()
        Dim excelManager = New ExcelManager()

        fileManager.CopyCsvFiles(srcDir, destDir)
        Dim csvFiles = fileManager.GetCsvFiles(destDir)
        Dim filterList = RichTextBox1.Text.Split(vbCrLf).ToList()

        Dim nowRow = 1
        Dim nowCol = 1
        Dim beginRow = 1
        Dim beginCol = 1
        Dim resultDict As New Dictionary(Of String, String)

        For Each filterValue In filterList
            Dim targetFiles = csvFiles.Where(Function(f) f.Contains(filterValue)).ToList()
            Dim dataTable As DataTable

            nowRow = 1
            nowCol = 1
            ' データ書き込み用の空のDataTableを用意する
            Dim tableName As String = ""
            Dim resultDataTableUtil As TableDataManager = New TableDataManager
            Dim FileCount = 0
            Dim writeCol As Integer = 1
            For Each file In targetFiles
                FileCount += 1
                Dim comment = IO.Path.GetFileNameWithoutExtension(file).Split("_").Last()
                tableName = IO.Path.GetFileNameWithoutExtension(file).Split("_").First()
                'tableName = tableName + FileCount.ToString()
                OutputConsole(" ########## ")
                OutputConsole(String.Format("tableName = {0}", tableName))
                OutputConsole(String.Format("comment = {0}", comment))
                resultDataTableUtil.AddTable(tableName)
                dataTable = csvManager.ReadCsv(file)
                OutputConsole(String.Format("datatable.Rows.Count = {0}", dataTable.Rows.Count))
                OutputConsole(String.Format("datatable.Columns.Count = {0}", dataTable.Columns.Count))

                '############ BEGIN
                Dim isRightOrDown As Integer = DetermineWriteDirection(file)

                ' データテーブルにヘッダーとデータを追加
                AddHeaderAndData(dataTable)
                TableDataManager.PrintDataTableContentsOther(dataTable)
                OutputConsole(String.Format("datatable.Rows.Count = {0}", dataTable.Rows.Count))
                OutputConsole(String.Format("datatable.Columns.Count = {0}", dataTable.Columns.Count))

                If isRightOrDown = Constants.RESULT_RIGHT Then
                    nowRow = beginRow
                    ' 行と列を入れ替える
                    dataTable = DataTableConverter.Transpose(dataTable)
                    TableDataManager.PrintDataTableContentsOther(dataTable)
                    OutputConsole(String.Format("datatable.Rows.Count = {0}", dataTable.Rows.Count))
                    OutputConsole(String.Format("datatable.Columns.Count = {0}", dataTable.Columns.Count))

                    ' テーブル名とコメントを resultDataTable に書き込む
                    'WriteMetaData(dataTable, resultDataTable, nowRow, nowCol, tableName, comment)
                    resultDataTableUtil.InsertPrimitiveData(tableName, tableName, nowRow, nowCol)
                    nowRow += 1
                    resultDataTableUtil.InsertPrimitiveData(tableName, comment, nowRow, nowCol)
                    nowRow += 1
                    nowRow += 1
                    nowCol += 1
                    resultDataTableUtil.InsertData(tableName, dataTable, nowRow, nowCol, writeCol)

                    ' 入れ替え済みのデータを resultDataTable に追加
                    'WriteDataTable(resultDataTable, dataTable, nowRow, nowCol)
                    TableDataManager.PrintDataTableContentsOther(resultDataTableUtil.GetDataTable(tableName))

                    nowCol += Constants.NEXT_TABLE_OFFSET
                ElseIf isRightOrDown = Constants.RESULT_DOWN Then
                    ' テーブル名とコメントを resultDataTable に書き込む
                    resultDataTableUtil.InsertPrimitiveData(tableName, tableName, nowRow, nowCol)
                    resultDataTableUtil.InsertPrimitiveData(tableName, comment, nowRow + 1, nowCol)

                    ' データを resultDataTable に追加
                    nowRow += 2
                    resultDataTableUtil.InsertData(tableName, dataTable, nowRow, nowCol + 1, writeCol)

                    nowRow += Constants.NEXT_TABLE_OFFSET
                Else
                    Dim msg = "isRightOrDown is Invalid Value"
                    msg = "ファイル名に '_R_' または '_D_' が含まれていません: " & file
                    MessageBox.Show(msg)
                    Exit Sub
                End If
                '############ END
            Next

            For Each bufTable As DataTable In resultDataTableUtil.GetDataSet().Tables
                Dim bufTableName = bufTable.TableName
                Dim outputCsv = IO.Path.Combine(destDir, $"{bufTableName}_Result.csv")
                csvManager.WriteCsv(outputCsv, bufTable)
                OutputConsole("outputCsv = " + outputCsv)
                resultDict(tableName) = outputCsv
            Next
        Next

        ' エクセルファイルにまとめて書き込む
        Dim excelPath = IO.Path.Combine(destDir, "FinalResult.xlsx")
        excelManager.WriteToExcel(excelPath, resultDict)
        OutputConsole("excelPath = " + excelPath)
        OutputConsole("処理が完了しました。")
    End Sub

    ' 書き込み方向を判定する関数
    Private Function DetermineWriteDirection(fileName As String) As Integer
        Dim regexRight As New Regex("_R_")
        Dim regexDown As New Regex("_D_")
        If regexRight.IsMatch(fileName) Then
            Return Constants.RESULT_RIGHT ' Right
        ElseIf regexDown.IsMatch(fileName) Then
            Return Constants.RESULT_DOWN ' Down
        Else
            Dim msg = "ファイル名に '_R_' または '_D_' が含まれていません: " & fileName
            'Throw New Exception(msg)
            Return ConstErrorFlag.FILE_NAME_ERROR
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
    Private Sub WriteMetaData(nowTable As DataTable, resultDataTable As DataTable, nowRow As Integer, nowCol As Integer, tableName As String, comment As String)
        'Dim metaRow As DataRow = resultDataTable.NewRow()
        Dim metaRow = resultDataTable.Rows(nowRow)
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

    Private Sub TextBox_SrcDirPath_TextChanged(sender As Object, e As EventArgs) Handles TextBox_SrcDirPath.TextChanged

    End Sub
End Class
