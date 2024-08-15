Public Class FormSystemVolumeController

    Dim _volumeControl As VolumeControl
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._volumeControl = New VolumeControl()

        Dim valSingle As Single
        valSingle = Me._volumeControl.GetVolume()
        Debug.Print(String.Format("NowVolume = {0}", valSingle))
        Me.TextBox1.Text = CStr(valSingle)
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Dim IsMuted As Boolean = Me._volumeControl.IsMuted()
        If Not (CheckBox1.Checked = IsMuted) Then
            Me._volumeControl.ToggleMute()
        End If
        Debug.Print(String.Format("Muted = {0}", Not IsMuted))
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim valStr As String = Me.TextBox1.Text
        If Not IsNumeric(valStr) Then
            Dim msg As String = String.Format("数値を入力してください", valStr)
            'MessageBox.Show(msg)
            Debug.WriteLine(msg)
            Exit Sub
        End If
        Dim valSingle As Single = CSng(valStr)

        If Me._volumeControl.IsInvalidVolume(valSingle) Then
            Debug.WriteLine(String.Format("Input is Invalid {0}", valSingle))
            Exit Sub
        End If
        Me._volumeControl.SetVolume(valSingle)
        Debug.Print(String.Format("SetVolume = {0}", valSingle))
        valSingle = Me._volumeControl.GetVolume()
        Debug.Print(String.Format("NowVolume = {0}", valSingle))
    End Sub
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1_Click(sender, DirectCast(e, EventArgs))
        End If
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        'TextBoxでEnter押下時、システム音吹鳴を抑制するため
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
        End If
    End Sub
End Class
