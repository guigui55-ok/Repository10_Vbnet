Imports System.Diagnostics

'<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1_old
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(34, 22)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(142, 38)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(135, 86)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(164, 44)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(339, 152)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
End Class


Public Class Form1_old
    Private WithEvents testEvent As New TestEvent

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
        Debug.WriteLine("RecieveEventメソッドが実行されました。")
    End Sub

    Private Sub AnotherEvent()
        ' AnotherMethod内から呼び出される
        Debug.WriteLine("AnotherEventメソッドが実行されました。")
    End Sub

    ' TestEventのイベントをハンドルする
    Private Sub testEvent_RaiseEvent() Handles testEvent.MyEvent
        RecieveEvent()
    End Sub

    ' TestEventの別のイベントをハンドルする
    Private Sub testEvent_AnotherEvent() Handles testEvent.AnotherEvent
        AnotherEvent()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class

Public Class TestEvent_old
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
