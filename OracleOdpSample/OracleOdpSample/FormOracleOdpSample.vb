Public Class FormOracleOdpSample

    Public _oracleSample As OracleSampleClass

    Private Sub FormOracleOdpSample_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _oracleSample = New OracleSampleClass()
    End Sub

    Private Sub ButtonExecute_Click(sender As Object, e As EventArgs) Handles ButtonExecute.Click
        _oracleSample.Test1()
    End Sub
End Class
