Public Class FormMain
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim password As String = "secret123"
        Dim form As New FormVerifyPassword(password)
        If form.ShowDialog() = DialogResult.OK Then
            Output("パスワード確認成功！")
        Else
            Output("キャンセルまたはパスワード不一致")
        End If
    End Sub

    Private Sub Output(value As String)
        Debug.WriteLine($"{value}")
    End Sub
End Class