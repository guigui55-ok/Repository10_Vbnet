﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MessageBase
    Inherits System.Windows.Forms.UserControl

    'UserControl はコンポーネント一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBoxMessageFrame = New System.Windows.Forms.GroupBox()
        Me.SuspendLayout()
        '
        'GroupBoxMessageFrame
        '
        Me.GroupBoxMessageFrame.Location = New System.Drawing.Point(11, 14)
        Me.GroupBoxMessageFrame.Name = "GroupBoxMessageFrame"
        Me.GroupBoxMessageFrame.Size = New System.Drawing.Size(426, 92)
        Me.GroupBoxMessageFrame.TabIndex = 0
        Me.GroupBoxMessageFrame.TabStop = False
        Me.GroupBoxMessageFrame.Text = "Message"
        '
        'MessageBase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBoxMessageFrame)
        Me.Name = "MessageBase"
        Me.Size = New System.Drawing.Size(450, 120)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBoxMessageFrame As GroupBox
End Class
