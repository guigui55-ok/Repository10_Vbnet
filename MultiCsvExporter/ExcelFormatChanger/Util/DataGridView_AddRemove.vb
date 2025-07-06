

''' <summary>
''' DataGridViewの行の追加・削除を、UIのボタンから行うクラス
''' </summary>
Public Class DataGridView_AddRemove

    Private _logger As Object
    Private _dataGridView As DataGridView

    Public Event AddedRowEvent As EventHandler

    Sub New(logger As Object, dgv As DataGridView)
        _logger = logger
        _dataGridView = dgv

        AddHandler _dataGridView.RowValidated, AddressOf DataGridView1_RowValidated
    End Sub

    Private Sub DataGridView1_RowValidated(sender As Object, e As DataGridViewCellEventArgs)
        _dataGridView.EndEdit()
    End Sub

    Public Sub EndEditAll()
        For Each row As DataGridViewRow In _dataGridView.Rows
            If Not row.IsNewRow Then
                _dataGridView.CurrentCell = row.Cells(0)
                _dataGridView.EndEdit()
            End If
        Next
    End Sub

    Public Sub SetButton(addButton As Button, removeButton As Button)
        AddHandler addButton.Click, AddressOf Button_AddRow_Click
        AddHandler removeButton.Click, AddressOf Button_RemoveRow_Click
    End Sub
    Private Sub Button_AddRow_Click(sender As Object, e As EventArgs)
        _dataGridView.Rows.Add()

        Dim lastRow = _dataGridView.Rows(_dataGridView.Rows.Count - 1)
        lastRow.Cells()(0).Value = lastRow.Index.ToString
        _dataGridView.CurrentCell = lastRow.Cells(0)
        _dataGridView.EndEdit()
        'Dim ColumunPosition_ItemName_Number = 0
        'SetNoNoDataGridView(_dataGridView, ColumunPosition_ItemName_Number)
        RaiseEvent AddedRowEvent(Me, EventArgs.Empty)
    End Sub

    Private Sub Button_RemoveRow_Click(sender As Object, e As EventArgs)
        Try
            Dim rowIndex As Integer = _dataGridView.CurrentCell.RowIndex

            _dataGridView.Rows.RemoveAt(rowIndex)
            _logger.Info($"removeRow = {rowIndex}")
        Catch ex As InvalidOperationException
            '型 'System.InvalidOperationException' のハンドルされていない例外が MultiCsvExporter.exe で発生しました
            'コミットされていない新しい行を削除することはできません。
            If ex.Message.Contains("コミットされていない新しい行を削除することはできません") Then
                _logger.Info(ex.GetType.ToString + ":" + ex.Message)
            Else
                Throw ex
            End If
        End Try
        Dim ColumunPosition_ItemName_Number = 0
        SetNoNoDataGridView(_dataGridView, ColumunPosition_ItemName_Number)
    End Sub

    Private Sub SetNoNoDataGridView(dgv As DataGridView, colIndex As Integer, Optional startNum As Integer = 1)
        Dim count = 0
        For Each row As DataGridViewRow In _dataGridView.Rows
            row.Cells(colIndex).Value = (startNum + count).ToString
            count += 1
        Next
    End Sub
End Class
