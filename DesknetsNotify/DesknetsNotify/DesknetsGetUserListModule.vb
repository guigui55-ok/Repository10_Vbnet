
Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Module DesknetsGetUserListModule

    Public Class DesknetAPIExample
        Private Const API_URL As String = "https://<desknet's-url>/cgi-bin/dneo/zrconst.cgi"
        Private Const ACCESS_KEY As String = "your_access_key"

        Public Async Function GetUserListAsync() As Task
            Using client As New HttpClient()
                ' リクエストデータを設定
                Dim requestData As New With {
                    Key .access_key = ACCESS_KEY,
                    Key .parameters = New With {
                        Key .target = "user_list",
                        Key .offset = 0,
                        Key .limit = 50
                    }
                }

                ' JSONデータをシリアライズ
                Dim json As String = JsonConvert.SerializeObject(requestData)
                Dim content As New StringContent(json, Encoding.UTF8, "application/json")

                ' POSTリクエストを送信
                Dim response As HttpResponseMessage = Await client.PostAsync(API_URL, content)

                If response.IsSuccessStatusCode Then
                    ' レスポンスを表示
                    Dim responseBody As String = Await response.Content.ReadAsStringAsync()
                    Console.WriteLine("レスポンス: " & responseBody)
                Else
                    Console.WriteLine("エラー: " & response.StatusCode)
                End If
            End Using
        End Function
    End Class
End Module
