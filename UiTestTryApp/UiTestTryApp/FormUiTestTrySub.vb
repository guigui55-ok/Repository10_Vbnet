Public Class FormUiTestTrySub
    Public _logger As AppLogger

    Private Sub Button_Close_Click(sender As Object, e As EventArgs) Handles Button_Close.Click
        _logger.PrintInfo("Button_Close_Click")
        Me.Visible = False
    End Sub

    Private Sub FormUiTestTrySub_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Visible = False
    End Sub

    Private Sub FormUiTestTrySub_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class