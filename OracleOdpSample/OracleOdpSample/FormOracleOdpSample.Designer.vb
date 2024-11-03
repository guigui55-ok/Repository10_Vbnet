<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormOracleOdpSample
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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
        Me.ButtonExecute = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ButtonExecute
        '
        Me.ButtonExecute.Location = New System.Drawing.Point(131, 67)
        Me.ButtonExecute.Name = "ButtonExecute"
        Me.ButtonExecute.Size = New System.Drawing.Size(75, 23)
        Me.ButtonExecute.TabIndex = 0
        Me.ButtonExecute.Text = "Execute"
        Me.ButtonExecute.UseVisualStyleBackColor = True
        '
        'FormOracleOdpSample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(218, 102)
        Me.Controls.Add(Me.ButtonExecute)
        Me.Name = "FormOracleOdpSample"
        Me.Text = "OracleOdpSample"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ButtonExecute As Button
End Class
