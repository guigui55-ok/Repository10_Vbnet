
Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json.Linq
Imports System.Windows.Forms


Module DesknetsApiModuleImports
    Public Class DesknetNotifier
        ' Desknet's APIベースURL（モジュール名を含む）
        Private Const API_BASE_URL As String = "https://local.yourdomain/cgi-bin/dneo/zrconst.cgi"
        Private Const API_KEY As String = "your_access_key"

        Public NotifyIcon1 As NotifyIcon

        ' HTTPクライアント
        Private ReadOnly HttpClient As New HttpClient()

        ' ログ出力
        Public Sub LogOutput(value As Object, Optional toMsgBox As Boolean = False, Optional logType As Integer = 0)
            Dim buf = String.Format("{0}", value)
            buf = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "   " + buf
            Debug.WriteLine(buf)
            Console.WriteLine(buf)
            If toMsgBox Then
                If logType = 2 Then
                    MessageBox.Show(value.ToString(), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        End Sub

        ' APIから新着メールを取得
        Private Async Function GetNewEmailsAsync() As Task(Of JArray)
            Dim requestUrl As String = API_BASE_URL
            LogOutput($"APIリクエスト: {requestUrl}")

            ' APIリクエストデータ
            Dim requestData As New JObject(
                New JProperty("access_key", API_KEY),
                New JProperty("parameters", New JObject(
                    New JProperty("target", "mail_new"),
                    New JProperty("offset", 0),
                    New JProperty("limit", 50)
                ))
            )

            Try
                Dim content As New StringContent(requestData.ToString(), Encoding.UTF8, "application/json")
                Dim response As HttpResponseMessage = Await HttpClient.PostAsync(requestUrl, content)

                If response.IsSuccessStatusCode Then
                    Dim contentString As String = Await response.Content.ReadAsStringAsync()
                    Dim jsonResponse As JObject = JObject.Parse(contentString)

                    If jsonResponse.ContainsKey("emails") Then
                        Return jsonResponse("emails").Value(Of JArray)()
                    Else
                        LogOutput("APIレスポンスに 'emails' キーが存在しません。")
                    End If
                Else
                    LogOutput($"APIエラー: {response.StatusCode}")
                End If
            Catch ex As Exception
                LogOutput($"通信エラー: {ex.Message}")
            End Try

            Return Nothing
        End Function

        ' Windows通知を表示
        Private Sub ShowNotification(subject As String, sender As String)
            LogOutput($"通知表示: 件名='{subject}', 送信者='{sender}'")
            NotifyIcon1.BalloonTipTitle = "新着メール"
            NotifyIcon1.BalloonTipText = $"件名: {subject}{vbCrLf}送信者: {sender}"
            NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info
            NotifyIcon1.Visible = True
            NotifyIcon1.ShowBalloonTip(5000)
        End Sub

        ' メール受信チェックを実行
        Public Async Sub CheckEmails()
            LogOutput("メール受信チェック開始")
            Dim newEmails As JArray = Await GetNewEmailsAsync()

            If newEmails IsNot Nothing AndAlso newEmails.Count > 0 Then
                For Each email In newEmails
                    Dim subject As String = email("subject").ToString()
                    Dim sender As String = email("sender").ToString()
                    ShowNotification(subject, sender)
                Next
            Else
                LogOutput("新着メールはありません。")
            End If
        End Sub
    End Class

End Module
