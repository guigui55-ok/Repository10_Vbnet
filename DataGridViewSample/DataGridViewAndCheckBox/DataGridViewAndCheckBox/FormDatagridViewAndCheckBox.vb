Public Class FormDatagridViewAndCheckBox
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ToggleColumnVisibility("Check")
    End Sub
    Private Sub ToggleColumnVisibility(columnName As String)
        ' 指定された列名の列が存在するかを確認
        If DataGridView1.Columns.Contains(columnName) Then
            ' 列の表示/非表示を切り替え
            DataGridView1.Columns(columnName).Visible = Not DataGridView1.Columns(columnName).Visible
        Else
            MessageBox.Show("指定された列が見つかりません。")
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeDataGridView(Me.DataGridView1)
        Initialize()
    End Sub

    Private Sub InitializeDataGridView(dgv As DataGridView)
        Try
            ' 列名と型のリストを作成
            'このリストによって、Columnの作成を自動化する
            Dim columns As New List(Of (Name As String, Type As String)) From {
                ("No", "Text"),
                ("Check", "CheckBox"),
                ("Value1", "Text"),
                ("Value2", "Text"),
                ("Value3", "Text")
            }

            ' 列の追加をループで処理
            For Each col In columns
                If Not dgv.Columns.Contains(col.Name) Then
                    Select Case col.Type
                        Case "Text"
                            dgv.Columns.Add(col.Name, col.Name) ' テキスト列の追加
                        Case "CheckBox"
                            Dim checkBoxColumn As New DataGridViewCheckBoxColumn With {
                                .Name = col.Name,
                                .HeaderText = col.Name
                            }
                            dgv.Columns.Add(checkBoxColumn) ' チェックボックス列の追加
                    End Select
                End If
            Next

            ' DataGridViewに行データを追加
            Dim rand As New Random()
            For i As Integer = 1 To 5
                Dim value1 As Double = Math.Round(0.1 + (rand.NextDouble() * (0.3 - 0.1)), 3)
                Dim value2 As Double = Math.Round(0.1 + (rand.NextDouble() * (0.3 - 0.1)), 3)
                Dim value3 As Double = Math.Round(0.1 + (rand.NextDouble() * (0.3 - 0.1)), 3)

                ' 行を追加
                dgv.Rows.Add(i, False, value1.ToString("F3"), value2.ToString("F3"), value3.ToString("F3"))
            Next
        Catch ex As Exception
            Console.WriteLine("InitializeDataGridView Error")
            Console.WriteLine(ex.GetType().ToString() + ":" + ex.Message)
            Console.WriteLine(ex.StackTrace)
        End Try
    End Sub

    Private Sub Initialize()
        Dim dataGridView As DataGridView = DataGridView1
        Try
            ' ランダム値を生成するための変数
            Dim rnd As New Random()
            Dim buf As String = ""
            Dim rowMax As Integer = 2
            Dim colMax As Integer = 3

            ' 行と列にデータをセット
            For row As Integer = 0 To rowMax
                If (dataGridView.RowCount <= row) Then
                    Console.WriteLine("Add Row " + row.ToString())
                    dataGridView.Rows.Add() ' 行を追加
                End If

                ' 各列に値を設定
                For col As Integer = 0 To colMax
                    If col = 0 Then

                    ElseIf col = 1 Then
                    buf = dataGridView.Rows(row).Cells(col).Value.ToString()
                    Else
                        If (dataGridView.Columns.Count <= col) Then
                            Console.WriteLine("Add Col " + col.ToString())
                            dataGridView.Columns.Add("Column" + col.ToString(), "Column_" + col.ToString())
                        End If
                        buf = CDbl(rnd.Next(6000, 7000) / 1000).ToString()
                        ' 値を設定
                        dataGridView.Rows(row).Cells(col).Value = buf
                    End If
                Next
            Next

            ' 設定されたデータを確認
            For Each rowObj As DataGridViewRow In dataGridView.Rows
                buf = ""
                For Each cellObj As DataGridViewCell In rowObj.Cells
                    If cellObj.Value IsNot Nothing Then
                        Dim bufVal As String = cellObj.Value.ToString()
                        buf = buf + String.Format("[{0}, {1}] {2}, ", rowObj.Index, cellObj.ColumnIndex, bufVal)
                    Else
                        buf = buf + String.Format("[{0}, {1}] , ", rowObj.Index, cellObj.ColumnIndex)
                    End If
                Next
                Console.WriteLine(buf)
            Next
        Catch ex As Exception
            Console.WriteLine("Initialize Error")
            Console.WriteLine(ex.GetType().ToString() + ":" + ex.Message)
            Console.WriteLine(ex.StackTrace)
        End Try
    End Sub

End Class
