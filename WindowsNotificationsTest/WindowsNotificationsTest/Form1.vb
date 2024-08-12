Imports Windows.UI.Notifications
Imports Windows.Data.Xml.Dom

Module Module1
    Sub Main()
        ' トースト通知のXMLコンテンツを定義
        Dim toastXml As XmlDocument = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02)

        ' テキスト要素にメッセージを設定
        Dim stringElements As XmlNodeList = toastXml.GetElementsByTagName("text")
        stringElements.Item(0).AppendChild(toastXml.CreateTextNode("こんにちは"))
        stringElements.Item(1).AppendChild(toastXml.CreateTextNode("これはVB.NETからの通知です"))

        ' トースト通知オブジェクトを作成
        Dim toast As New ToastNotification(toastXml)

        ' 通知を送信
        ToastNotificationManager.CreateToastNotifier("あなたのアプリ名").Show(toast)
    End Sub
End Module

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub



End Class
