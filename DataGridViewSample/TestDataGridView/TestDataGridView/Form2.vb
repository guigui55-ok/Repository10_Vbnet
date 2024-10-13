Public Class Form2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Me.Visible = False
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' MenuStripの作成
        'Dim menuStrip As New MenuStrip()
        MenuStrip1 = Me.MenuStrip1

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

        ' FormにMenuStripを追加
        Me.MainMenuStrip = MenuStrip1
        Me.Controls.Add(MenuStrip1)
    End Sub
End Class