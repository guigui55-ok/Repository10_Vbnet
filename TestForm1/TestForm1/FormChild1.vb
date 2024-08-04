Public Class FormChild1
    Private Sub FormChild1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.FormBorderStyle = FormBorderStyle.None
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Me.Close()
        Me.Visible = False
    End Sub

End Class