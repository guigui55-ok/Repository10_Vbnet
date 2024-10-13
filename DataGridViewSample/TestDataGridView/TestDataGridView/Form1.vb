Imports System.IO
Imports System.Data
Imports Microsoft.Office.Interop.Excel
Imports System.Drawing
Imports System.Windows.Forms

Public Class Form1
    Dim formChild As Form2
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.InitializeDataGridView()
        'Me.InitializeDataGridView_B()
        'Me.InitializeDataGridView_C()
        Me.InitializeDataGridView_D()
        Me.CustomizeDataGridView()
        Me.formChild = New Form2()
    End Sub
    Private Sub InitializeDataGridView_D()
        Dim filePath As String = GetExcelPath()
        Dim bufDataTable As Data.DataTable = ReadExcelFile(filePath)
        DataGridView1.DataSource = bufDataTable
    End Sub

    Private Sub InitializeDataGridView_C()
        Dim filePath As String = GetCsvPath()
        Dim bufDataTable As Data.DataTable = ReadCsvFile(filePath)
        DataGridView1.DataSource = bufDataTable

    End Sub
    Private Sub InitializeDataGridView_B()
        ' データを追加
        Dim testlist As New TestList()
        DataGridView1.DataSource = testlist.Data

        ' カラム名を指定
        DataGridView1.Columns(0).HeaderText = "教科"
        DataGridView1.Columns(1).HeaderText = "点数"
        DataGridView1.Columns(2).HeaderText = "氏名"
        DataGridView1.Columns(3).HeaderText = "クラス名"
    End Sub
    Private Sub InitializeDataGridView()

        'カラム数を指定
        DataGridView1.ColumnCount = 4

        'カラム名を指定
        DataGridView1.Columns(0).HeaderText = "教科"
        DataGridView1.Columns(1).HeaderText = "点数"
        DataGridView1.Columns(2).HeaderText = "氏名"
        DataGridView1.Columns(3).HeaderText = "クラス名"

        'データを追加
        DataGridView1.Rows.Add("国語", 90, "田中　一郎", "A")
        DataGridView1.Rows.Add("数学", 50, "鈴木　二郎", "A")
        DataGridView1.Rows.Add("英語", 90, "佐藤　三郎", "B")
    End Sub

    'Dim headerColor1 As Color = Color.FromArgb(255, 165, 0) ' 濃い目のオレンジと茶色の間の色
    'Dim headerColor2 As Color = Color.FromArgb(255, 200, 120) ' 薄い目のオレンジと茶色の間の色
    'Dim headerColor1 As Color = Color.FromArgb(139, 69, 19) ' 暗い目の茶色
    'Dim headerColor2 As Color = Color.FromArgb(210, 105, 30) ' 暗い目のオレンジ
    'Dim headerColor1 As Color = Color.FromArgb(180, 90, 30) ' 微妙なパステルっぽさと暗さを調整
    'Dim headerColor2 As Color = Color.FromArgb(200, 110, 50) ' 微妙なパステルっぽさと暗さを調整
    Dim headerColor1 As Color = Color.FromArgb(220, 150, 100) ' 濃いめのオレンジっぽいパステルカラー
    Dim headerColor2 As Color = Color.FromArgb(240, 180, 130) ' 薄めのオレンジっぽいパステルカラー
    'Dim textColor As Color = Color.White
    Private Sub CustomizeDataGridView()
        ' カラムヘッダーのスタイル設定
        Dim headerColor1 As Color = Me.headerColor1
        Dim headerColor2 As Color = Me.headerColor2
        Dim textColor As Color = Color.White

        ' カラムヘッダーのスタイル設定
        For i As Integer = 0 To DataGridView1.Columns.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Columns(i).HeaderCell.Style.BackColor = headerColor1
            Else
                DataGridView1.Columns(i).HeaderCell.Style.BackColor = headerColor2
            End If
            DataGridView1.Columns(i).HeaderCell.Style.ForeColor = textColor
        Next

        '' 行ヘッダーのスタイル設定
        'For i As Integer = 0 To DataGridView1.Rows.Count - 1
        '    If i Mod 2 = 0 Then
        '        DataGridView1.Rows(i).HeaderCell.Style.BackColor = headerColor1
        '    Else
        '        DataGridView1.Rows(i).HeaderCell.Style.BackColor = headerColor2
        '    End If
        '    DataGridView1.Rows(i).HeaderCell.Style.ForeColor = textColor
        'Next
        ' 行ヘッダーのスタイル設定
        AddHandler DataGridView1.RowPostPaint, AddressOf DataGridView1_RowPostPaint
        'AddHandler DataGridView1.ColumnHeaderCellChanged, AddressOf DataGridView1_ColumnHeaderCellChanged
        'AddHandler DataGridView1.ColumnAdded, AddressOf DataGridView1_ColumnAdded
        'AddHandler DataGridView1.DataBindingComplete, AddressOf DataGridView1_DataBindingComplete
        AddHandler DataGridView1.CellPainting, AddressOf DataGridView1_CellPainting

        ' 他のセルのスタイル設定
        DataGridView1.DefaultCellStyle.BackColor = Color.White
        DataGridView1.DefaultCellStyle.ForeColor = Color.Black

        ' メモリ線の色設定
        Dim gridColor As Color = Color.FromArgb(240, 128, 128) ' 親和性のある色（ライトコーラル）
        DataGridView1.GridColor = gridColor
    End Sub
    'Private Sub DataGridView1_ColumnHeaderCellChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles DataGridView1.ColumnHeaderCellChanged
    '    ' カラムヘッダーのスタイル設定
    '    Dim headerColor1 As Color = Me.headerColor1
    '    Dim headerColor2 As Color = Me.headerColor2
    '    Dim textColor As Color = Color.White

    '    For i As Integer = 0 To DataGridView1.Columns.Count - 1
    '        If i Mod 2 = 0 Then
    '            DataGridView1.Columns(i).HeaderCell.Style.BackColor = headerColor1
    '        Else
    '            DataGridView1.Columns(i).HeaderCell.Style.BackColor = headerColor2
    '        End If
    '        DataGridView1.Columns(i).HeaderCell.Style.ForeColor = textColor
    '    Next
    'End Sub
    'Private Sub DataGridView1_ColumnAdded(sender As Object, e As DataGridViewColumnEventArgs)
    '    ' カラムヘッダーのスタイル設定
    '    Dim headerColor1 As Color = Me.headerColor1
    '    Dim headerColor2 As Color = Me.headerColor2
    '    Dim textColor As Color = Color.White

    '    For i As Integer = 0 To DataGridView1.Columns.Count - 1
    '        If i Mod 2 = 0 Then
    '            DataGridView1.Columns(i).HeaderCell.Style.BackColor = headerColor1
    '        Else
    '            DataGridView1.Columns(i).HeaderCell.Style.BackColor = headerColor2
    '        End If
    '        DataGridView1.Columns(i).HeaderCell.Style.ForeColor = textColor
    '    Next
    'End Sub
    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs)
        ' カラムヘッダーのスタイル設定を再適用
        Dim headerColor1 As Color = Me.headerColor1
        Dim headerColor2 As Color = Me.headerColor2
        Dim textColor As Color = Color.White

        For i As Integer = 0 To DataGridView1.Columns.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Columns(i).HeaderCell.Style.BackColor = headerColor1
            Else
                DataGridView1.Columns(i).HeaderCell.Style.BackColor = headerColor2
            End If
            DataGridView1.Columns(i).HeaderCell.Style.ForeColor = textColor
        Next
    End Sub
    'Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
    '    If e.RowIndex = -1 Then
    '        ' カラムヘッダーのスタイル設定
    '        Dim headerColor1 As Color = Me.headerColor1
    '        Dim headerColor2 As Color = Me.headerColor2
    '        Dim textColor As Color = Color.White

    '        If e.ColumnIndex Mod 2 = 0 Then
    '            e.CellStyle.BackColor = headerColor1
    '        Else
    '            e.CellStyle.BackColor = headerColor2
    '        End If
    '        e.CellStyle.ForeColor = textColor
    '        e.Handled = True
    '    End If
    'End Sub
    Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs)
        ' カラムヘッダーのスタイル設定
        If e.RowIndex = -1 AndAlso e.ColumnIndex >= 0 Then
            e.PaintBackground(e.ClipBounds, True)

            Dim headerColor1 As Color = Me.headerColor1
            Dim headerColor2 As Color = Me.headerColor2
            Dim textColor As Color = Color.White

            If e.ColumnIndex Mod 2 = 0 Then
                e.Graphics.FillRectangle(New SolidBrush(headerColor1), e.CellBounds)
            Else
                e.Graphics.FillRectangle(New SolidBrush(headerColor2), e.CellBounds)
            End If

            e.PaintContent(e.ClipBounds)
            e.Handled = True
        End If
    End Sub
    Private Sub DataGridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs)
        ' 行ヘッダーのスタイル設定
        Dim headerColor1 As Color = Me.headerColor1
        Dim headerColor2 As Color = Me.headerColor2
        Dim textColor As Color = Color.White

        If e.RowIndex Mod 2 = 0 Then
            e.Graphics.FillRectangle(New SolidBrush(headerColor1), e.RowBounds.Left, e.RowBounds.Top, DataGridView1.RowHeadersWidth, e.RowBounds.Height)
        Else
            e.Graphics.FillRectangle(New SolidBrush(headerColor2), e.RowBounds.Left, e.RowBounds.Top, DataGridView1.RowHeadersWidth, e.RowBounds.Height)
        End If

        Dim rowHeaderBounds As System.Drawing.Rectangle = New System.Drawing.Rectangle(e.RowBounds.Left, e.RowBounds.Top, DataGridView1.RowHeadersWidth, e.RowBounds.Height)
        System.Windows.Forms.TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), DataGridView1.RowHeadersDefaultCellStyle.Font, rowHeaderBounds, textColor)
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub ShowChildButton_Click(sender As Object, e As EventArgs) Handles ShowChildButton.Click

        formChild.Visible = True
    End Sub
End Class

Module CommonModule
    Function GetCsvPath()
        Dim fileName As String = "test_data.csv"
        Dim currentDirectory As String = Directory.GetCurrentDirectory()
        Dim filePath As String = Path.Combine(currentDirectory, fileName)
        Return filePath
    End Function
    Function ReadCsvFile(filePath As String) As Data.DataTable
        Dim dt As New Data.DataTable()

        ' CSVファイルを読み込み
        Using sr As New StreamReader(filePath)
            Dim headers As String() = sr.ReadLine().Split(","c)
            For Each header As String In headers
                dt.Columns.Add(header)
            Next

            While Not sr.EndOfStream
                Dim rows As String() = sr.ReadLine().Split(","c)
                dt.Rows.Add(rows)
            End While
        End Using

        Return dt
    End Function
End Module


Module CommonExcelModule
    Function GetExcelPath()
        Dim fileName As String = "test_data.xlsx"
        Dim currentDirectory As String = Directory.GetCurrentDirectory()
        Dim filePath As String = Path.Combine(currentDirectory, fileName)
        Return filePath
    End Function
    Function ReadExcelFile(filePath As String) As Data.DataTable

        Dim dt As New Data.DataTable()

        ' Excelアプリケーションを作成
        Dim excelApp As New Microsoft.Office.Interop.Excel.Application()
        Dim excelWorkbook As Workbook = excelApp.Workbooks.Open(filePath)
        'Dim sheetName As String = "YourSheetName"
        'Dim excelWorksheet As Worksheet = CType(excelWorkbook.Sheets(sheetName), Worksheet)
        Dim excelWorksheet As Worksheet = CType(excelWorkbook.Sheets(1), Worksheet)

        ' 使用範囲の取得
        Dim usedRange As Range = excelWorksheet.UsedRange
        Dim colCount As Integer = usedRange.Columns.Count
        Dim rowCount As Integer = usedRange.Rows.Count

        ' DataTableの列を追加
        For i As Integer = 1 To colCount
            dt.Columns.Add("Column " & i)
        Next

        ' DataTableに行を追加
        For i As Integer = 1 To rowCount
            Dim row As DataRow = dt.NewRow()
            For j As Integer = 1 To colCount
                Dim cellValue As Object = CType(usedRange.Cells(i, j), Range).Value2
                If cellValue Is Nothing Then
                    row(j - 1) = DBNull.Value
                Else
                    row(j - 1) = cellValue
                End If
            Next
            dt.Rows.Add(row)
        Next

        ' リソースの解放
        excelWorkbook.Close(False)
        excelApp.Quit()
        ReleaseObject(excelWorksheet)
        ReleaseObject(excelWorkbook)
        ReleaseObject(excelApp)

        Return dt
    End Function

    Sub ReleaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub
End Module


Public Class Test
    Public Property Subj As String
    Public Property Points As Integer
    Public Property Name As String
    Public Property ClassName As String
End Class

Public Class TestList
    Public ReadOnly Property Data As List(Of Test)

    ' コンストラクタ(データ入力)
    Public Sub New()
        Data = New List(Of Test) From {
            New Test With {.Subj = "国語", .Points = 90, .Name = "田中　一郎", .ClassName = "A"},
            New Test With {.Subj = "数学", .Points = 50, .Name = "鈴木　二郎", .ClassName = "A"},
            New Test With {.Subj = "英語", .Points = 90, .Name = "佐藤　三郎", .ClassName = "B"}
        }
    End Sub
End Class