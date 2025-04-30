Public Class FormVerifyPassword

    Private _verifyPassword As VerifyPassword
    Private _password As String

    Sub New(verifyPassword As String)
        InitializeComponent()

        _password = verifyPassword
        _verifyPassword = New VerifyPassword(_password)

        ' パスワード入力をマスク
        'TextBox_Password.UseSystemPasswordChar = True
        ' アスタリスクで隠す場合（New内などに記述）
        TextBox_Password.UseSystemPasswordChar = False
        TextBox_Password.PasswordChar = "*"c
        Label_Message.Visible = False
    End Sub

    ' OKボタン押下時
    Private Sub Button_Ok_Click(sender As Object, e As EventArgs) Handles Button_Ok.Click
        ComparePassword()
    End Sub

    ' キャンセルボタン押下時
    Private Sub Button_Cancel_Click(sender As Object, e As EventArgs) Handles Button_Cancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' ★ パスワード比較処理（共通化）
    Private Sub ComparePassword()
        Dim input As String = TextBox_Password.Text
        Output($"入力パスワード: {input}")

        If _verifyPassword.IsMatch(input) Then
            Me.DialogResult = DialogResult.OK
            Output("パスワード一致")
            Me.Close()
        Else
            Output("パスワード不一致")
            Label_Message.Text = "パスワードが一致しません。"
            Label_Message.ForeColor = Color.Red
            Label_Message.Font = New Font(Label_Message.Font, FontStyle.Bold)
            Label_Message.Visible = True
        End If
    End Sub

    ' ★ EnterキーでOKボタン処理と同じ動作をする
    Private Sub TextBox_Password_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox_Password.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' 音などを抑制
            ComparePassword()
        End If
    End Sub

    ' ★ デバッグ出力用メソッド
    Private Sub Output(value As String)
        Debug.WriteLine($"{value}")
    End Sub

    ' パスワード検証クラス
    Class VerifyPassword
        Private _expectedPassword As String

        Public Sub New(expectedPassword As String)
            _expectedPassword = expectedPassword
        End Sub

        Public Function IsMatch(input As String) As Boolean
            Return input = _expectedPassword
        End Function
    End Class

End Class
