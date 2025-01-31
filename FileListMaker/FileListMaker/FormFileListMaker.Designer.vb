<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormFileListMaker
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxPath = New System.Windows.Forms.TextBox()
        Me.ButtonCreateFile = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxIncludeList = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxIgnoreList = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxOutputPath = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ButtonShowExplorer = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Path:"
        '
        'TextBoxPath
        '
        Me.TextBoxPath.Location = New System.Drawing.Point(74, 36)
        Me.TextBoxPath.Name = "TextBoxPath"
        Me.TextBoxPath.Size = New System.Drawing.Size(568, 19)
        Me.TextBoxPath.TabIndex = 1
        '
        'ButtonCreateFile
        '
        Me.ButtonCreateFile.Location = New System.Drawing.Point(558, 138)
        Me.ButtonCreateFile.Name = "ButtonCreateFile"
        Me.ButtonCreateFile.Size = New System.Drawing.Size(84, 23)
        Me.ButtonCreateFile.TabIndex = 2
        Me.ButtonCreateFile.Text = "Create File"
        Me.ButtonCreateFile.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Include:"
        '
        'TextBoxIncludeList
        '
        Me.TextBoxIncludeList.Location = New System.Drawing.Point(74, 88)
        Me.TextBoxIncludeList.Name = "TextBoxIncludeList"
        Me.TextBoxIncludeList.Size = New System.Drawing.Size(568, 19)
        Me.TextBoxIncludeList.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(483, 12)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "※条件はダブルクォーテーションで囲い、カンマで区切る。　例）""正規表現_条件1"",""正規表現_条件2"""
        '
        'TextBoxIgnoreList
        '
        Me.TextBoxIgnoreList.Location = New System.Drawing.Point(74, 113)
        Me.TextBoxIgnoreList.Name = "TextBoxIgnoreList"
        Me.TextBoxIgnoreList.Size = New System.Drawing.Size(568, 19)
        Me.TextBoxIgnoreList.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 12)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Ignore:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(315, 12)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "フォルダをD＆Dして、そのフォルダのファイル一覧をCsvで出力する。"
        '
        'TextBoxOutputPath
        '
        Me.TextBoxOutputPath.Location = New System.Drawing.Point(82, 168)
        Me.TextBoxOutputPath.Name = "TextBoxOutputPath"
        Me.TextBoxOutputPath.Size = New System.Drawing.Size(461, 19)
        Me.TextBoxOutputPath.TabIndex = 10
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 171)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 12)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "OutputPath:"
        '
        'ButtonShowExplorer
        '
        Me.ButtonShowExplorer.Location = New System.Drawing.Point(558, 171)
        Me.ButtonShowExplorer.Name = "ButtonShowExplorer"
        Me.ButtonShowExplorer.Size = New System.Drawing.Size(84, 23)
        Me.ButtonShowExplorer.TabIndex = 11
        Me.ButtonShowExplorer.Text = "ShowExplorer"
        Me.ButtonShowExplorer.UseVisualStyleBackColor = True
        '
        'FormFileListMaker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(654, 208)
        Me.Controls.Add(Me.ButtonShowExplorer)
        Me.Controls.Add(Me.TextBoxOutputPath)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxIgnoreList)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxIncludeList)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ButtonCreateFile)
        Me.Controls.Add(Me.TextBoxPath)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FormFileListMaker"
        Me.Text = "FileListMaker"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxPath As TextBox
    Friend WithEvents ButtonCreateFile As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxIncludeList As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxIgnoreList As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxOutputPath As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents ButtonShowExplorer As Button
End Class
