Public Class UserControl1
    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)

    End Sub

    Private Sub UserControl1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' ファイルメニューの作成
        Dim fileMenu As New ToolStripMenuItem("ファイル")
        Dim openMenuItem As New ToolStripMenuItem("開く")
        Dim closeMenuItem As New ToolStripMenuItem("閉じる")
        fileMenu.DropDownItems.Add(openMenuItem)
        fileMenu.DropDownItems.Add(closeMenuItem)

        ' 編集メニューの作成
        Dim editMenu As New ToolStripMenuItem("編集")

        ' オプションメニューの作成
        Dim optionsMenu As New ToolStripMenuItem("オプション")

        ' ヘルプメニューの作成
        Dim helpMenu As New ToolStripMenuItem("ヘルプ")

        ' MenuStripにメニュー項目を追加
        MenuStrip1.Items.Add(fileMenu)
        MenuStrip1.Items.Add(editMenu)
        MenuStrip1.Items.Add(optionsMenu)
        MenuStrip1.Items.Add(helpMenu)

        ' UserControlにMenuStripを追加
        Me.Controls.Add(MenuStrip1)
    End Sub
End Class
