Imports System.Diagnostics

Public Class Form1
    Private WithEvents testEvent As New TestEvent
    Private form2 As Form2

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        form2 = New Form2(testEvent)
        form2.Show()
        Me.KeyPreview = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' TestEventのStartMethodを呼び出す
        testEvent.StartMethod()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' TestEventのAnotherMethodを呼び出す
        testEvent.AnotherMethod()
    End Sub

    Private Sub RecieveEvent()
        ' StartMethod内から呼び出される
        Debug.WriteLine("Form1のRecieveEventメソッドが実行されました。")
    End Sub

    Private Sub AnotherEvent()
        ' AnotherMethod内から呼び出される
        Debug.WriteLine("Form1のAnotherEventメソッドが実行されました。")
    End Sub

    ' TestEventのイベントをハンドルする
    Private Sub testEvent_RaiseEvent() Handles testEvent.MyEvent
        RecieveEvent()
    End Sub

    ' TestEventの別のイベントをハンドルする
    Private Sub testEvent_AnotherEvent() Handles testEvent.AnotherEvent
        AnotherEvent()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
    'Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
    '    If e.KeyCode = Keys.Escape Then
    '        ' Escapeキーが押された場合の処理
    '        Debug.Print("Escapeキーが押されました")
    '        If ActiveControl IsNot Nothing AndAlso TypeOf ActiveControl Is TextBox Then
    '            ActiveControl = Nothing
    '        End If
    '        'Me.Focus()
    '        form2.Focus()
    '    End If
    'End Sub
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            ' Escapeキーが押された場合の処理
            Debug.Print("Escapeキーが押されました")
            If ActiveControl IsNot Nothing AndAlso TypeOf ActiveControl Is Form2 Then
                ActiveControl = Nothing
            End If
            'Me.Focus()
            form2.Focus()
        End If
    End Sub

End Class

Public Class Form2
    Private WithEvents testEvent As TestEvent

    Public Sub New(te As TestEvent)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        testEvent = te
    End Sub

    Private Sub RecieveEvent() Handles testEvent.MyEvent
        ' StartMethod内から呼び出される
        Debug.WriteLine("Form2のRecieveEventメソッドが実行されました。")
    End Sub
End Class

Public Class TestEvent
    ' イベントの定義
    Public Event MyEvent()
    Public Event AnotherEvent()

    Public Sub StartMethod()
        ' イベントの発生
        RaiseEvent MyEvent()
    End Sub

    Public Sub AnotherMethod()
        ' 別のイベントの発生
        RaiseEvent AnotherEvent()
    End Sub
End Class
