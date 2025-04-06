Imports System.IO
Imports DataTableUtility

Public Class FormWriteDataTableReadCsv
    Dim _logger As AppLogger
    Dim _dragAndDropControl As DragAndDropControl
    Dim _dragAndDropFile As DragAndDropFile
    Dim _DataIntervalCount As Integer = 2
    Dim _isToDown As Boolean = True

    Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。


        _logger = New AppLogger()
        Me.AllowDrop = True
        Me.RichTextBox_Paths.AllowDrop = True
        '//
        _dragAndDropControl = New DragAndDropControl(_logger, Me.RichTextBox_Paths) '//RecieveするControl
        _dragAndDropControl.AddRecieveControl(Me) '//別コントロールにもイベントを追加
        '_dragAndDropControl.AddRecieveControl(Me.RichTextBox_Main) '//別コントロールにもイベントを追加
        '//
        _dragAndDropFile = New DragAndDropFile(_logger, _dragAndDropControl)
        _dragAndDropControl._dragAndDropForFile = _dragAndDropFile
        AddHandler _dragAndDropFile.DragAndDropEventAfterEventForFile, AddressOf DragAndDropEvent

    End Sub


    Sub DragAndDropEvent(sender As Object, e As EventArgs)
        Try
            _logger.AddLog(Me, " > DragAndDropFileEvent")

            Dim _dragAndDropFile As DragAndDropFile = _dragAndDropControl._dragAndDropForFile
            If (_dragAndDropFile.FileList Is Nothing) Then
                _logger.PrintInfo("Files == null")
                Return
            End If
            If (_dragAndDropFile.FileList.Count < 1) Then
                _logger.PrintInfo("Files.Length < 1")
                Return
            End If
            Dim targetPath As String = _dragAndDropFile.FileList(0)
            _logger.PrintInfo(String.Format("targetPath={0}", targetPath))
            If (Directory.Exists(targetPath)) Then
                _logger.PrintInfo("Path Is Directory")
                Return
            End If
            Dim newLine = vbCrLf
            If RichTextBox_Paths.Text.Length < 1 Then
                newLine = ""
            End If
            RichTextBox_Paths.Text += newLine + targetPath

        Catch ex As Exception
            _logger.AddException(ex, Me, "DragAndDropFileEvent")
        End Try
    End Sub
    Private Sub FormWriteDataTableReadCsv_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button_Write_Click(sender As Object, e As EventArgs) Handles Button_Write.Click
        Dim appPath As String = System.Reflection.Assembly.GetExecutingAssembly().Location
        Dim destDir = Path.Combine(Path.GetDirectoryName(appPath), "result")
        Directory.CreateDirectory(destDir)
        Try
            Dim csvManager = New CsvManager()
            Dim excelManager = New ExcelManager()
            ''Dim pathList = RichTextBox_Paths.Text.Split(vbCrLf).ToList()
            'Dim pathList = Strings.Split(RichTextBox_Paths.Text, vbCrLf).ToList()
            'Dim delimiter() As String = {vbCrLf}

            'Dim tests() As String = RichTextBox_Paths.Text.Split(delimiter, StringSplitOptions.None)
            'pathList = tests.ToList()
            'Dim pathList() As String = RichTextBox_Paths.Text.Split(Environment.NewLine)
            'Dim pathList() As String = RichTextBox_Paths.Text.Split("\n")
            Dim lines() As String = RichTextBox_Paths.Lines
            Dim pathList = lines.ToList()
            'Dim i As Integer = 0
            'For Each bufPath In pathList
            '    _logger.PrintInfo($"[{i}] {bufPath}")
            '    i += 1
            'Next
            If pathList.Count < 1 Then
                _logger.PrintInfo("paths.Count < 1")
            End If
            Dim resultDataTableUtil As TableDataManager = New TableDataManager()
            Dim tableName = "Result"
            Dim nowRow As Integer = 0
            Dim nowCol As Integer = 0
            Dim OFFSET_COL As Integer = 0
            Dim resultDict As New Dictionary(Of String, Object)
            Dim count As Integer = 0
            Dim WRITE_COL As Boolean = True
            resultDataTableUtil.AddTable(tableName)
            For Each targetPath In pathList
                _logger.PrintInfo($"[{count}] {targetPath}")
                Dim dt = csvManager.ReadCsv(targetPath)
                Dim comment = IO.Path.GetFileNameWithoutExtension(targetPath)
                If _isToDown Then
                    ' テーブル名とコメントを resultDataTable に書き込む
                    resultDataTableUtil.InsertPrimitiveData(tableName, comment, nowRow, nowCol)
                    nowRow += 1
                    'resultDataTableUtil.InsertPrimitiveData(tableName, dt.Columns, nowRow, nowCol + OFFSET_COL)
                    'nowRow += 1
                    'resultDataTableUtil.InsertPrimitiveData(tableName, comment, nowRow + 1, nowCol)
                    ' データを resultDataTable に追加
                    resultDataTableUtil.InsertData(tableName, dt, nowRow, nowCol + OFFSET_COL, writeCol:=WRITE_COL)
                    nowRow += dt.Rows.Count
                    If WRITE_COL Then
                        nowRow += 1
                    End If
                    nowRow += 2

                Else
                    'ToRight
                    dt = DataTableConverter.Transpose(dt)

                End If

                'For Each bufTable As DataTable In resultDataTableUtil.GetDataSet().Tables
                '    Dim bufTableName = bufTable.TableName
                '    Dim outputCsv = IO.Path.Combine(destDir, $"{bufTableName}_Result.csv")
                '    csvManager.WriteCsv(outputCsv, bufTable)
                '    _logger.PrintInfo("outputCsv = " + outputCsv)
                '    resultDict(tableName) = outputCsv
                'Next
                count += 1
            Next
            resultDataTableUtil.PrintDataSetContents()
            '一旦Csvに書き込み
            Dim outputCsv = IO.Path.Combine(destDir, $"Result.csv")
            _logger.PrintInfo("outputCsv = " + outputCsv)
            _logger.PrintInfo("tableName = " + tableName)
            csvManager.WriteCsv(outputCsv, resultDataTableUtil.GetDataTable(tableName))
            resultDict("Result") = outputCsv
            ' エクセルファイルにまとめて書き込む
            Dim excelPath = IO.Path.Combine(destDir, "Result.xlsx")
            _logger.PrintInfo("excelPath = " + vbCrLf + excelPath)
            excelManager.WriteToExcel(excelPath, resultDict)
            _logger.PrintInfo("Excel Write Success.")
        Catch ex As Exception
            _logger.AddException(ex, Me, "Button_Write_Click Error")
        End Try
    End Sub

    Private Sub ReadFile(path As String)

    End Sub
End Class
