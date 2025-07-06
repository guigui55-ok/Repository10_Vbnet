Public Class DataGridViewSyncer
    Private _dgvA As DataGridView
    Private _dgvB As DataGridView
    Private _isSyncing As Boolean = False

    Public Sub New(dgvA As DataGridView, dgvB As DataGridView)
        _dgvA = dgvA
        _dgvB = dgvB

        AddHandler _dgvA.RowsAdded, AddressOf OnRowsAddedA
        AddHandler _dgvA.RowsRemoved, AddressOf OnRowsRemovedA
        AddHandler _dgvB.RowsAdded, AddressOf OnRowsAddedB
        AddHandler _dgvB.RowsRemoved, AddressOf OnRowsRemovedB
    End Sub

    Private Sub OnRowsAddedA(sender As Object, e As DataGridViewRowsAddedEventArgs)
        If _isSyncing Then Return
        _isSyncing = True
        Try
            For i As Integer = 0 To e.RowCount - 1
                _dgvB.Rows.Insert(e.RowIndex + i)
            Next
        Finally
            _isSyncing = False
        End Try
    End Sub

    Private Sub OnRowsRemovedA(sender As Object, e As DataGridViewRowsRemovedEventArgs)
        If _isSyncing Then Return
        _isSyncing = True
        Try
            For i As Integer = 0 To e.RowCount - 1
                If e.RowIndex < _dgvB.Rows.Count Then
                    _dgvB.Rows.RemoveAt(e.RowIndex)
                End If
            Next
        Finally
            _isSyncing = False
        End Try
    End Sub

    Private Sub OnRowsAddedB(sender As Object, e As DataGridViewRowsAddedEventArgs)
        If _isSyncing Then Return
        _isSyncing = True
        Try
            For i As Integer = 0 To e.RowCount - 1
                _dgvA.Rows.Insert(e.RowIndex + i)
            Next
        Finally
            _isSyncing = False
        End Try
    End Sub

    Private Sub OnRowsRemovedB(sender As Object, e As DataGridViewRowsRemovedEventArgs)
        If _isSyncing Then Return
        _isSyncing = True
        Try
            For i As Integer = 0 To e.RowCount - 1
                If e.RowIndex < _dgvA.Rows.Count Then
                    _dgvA.Rows.RemoveAt(e.RowIndex)
                End If
            Next
        Finally
            _isSyncing = False
        End Try
    End Sub

    ' 同期解除メソッド（必要に応じて）
    Public Sub Dispose()
        RemoveHandler _dgvA.RowsAdded, AddressOf OnRowsAddedA
        RemoveHandler _dgvA.RowsRemoved, AddressOf OnRowsRemovedA
        RemoveHandler _dgvB.RowsAdded, AddressOf OnRowsAddedB
        RemoveHandler _dgvB.RowsRemoved, AddressOf OnRowsRemovedB
    End Sub
End Class