Public Class FormDataSetTest
    Dim _DataSet As DataSet
    Dim mode As Integer = 0

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If mode = 0 Then
            mode = 1
            SetDataSetToDataGridView(Me.DataGridView1, _DataSet, ConstDataSetName.NameA)
        Else
            mode = 0
            SetDataSetToDataGridView(Me.DataGridView1, _DataSet, ConstDataSetName.NameB)
        End If
    End Sub

    Private Sub FormDataSetTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _DataSet = CreateDataSetTestMethodA()
        Dim bufDict = GetRowAsDictionary(_DataSet, ConstDataSetName.NameA, 1)
        PrettyPrintDictionary(bufDict)
    End Sub


End Class
