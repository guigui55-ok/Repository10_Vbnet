Public Class StatusControllerTestMain
    Public _logger As SimpleLogger
    Public _formStateCon As FormStateContoller
    Public _StateList As List(Of AppStatus) = New List(Of AppStatus)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' キープレビューを有効にすることで、フォームでキーイベントをキャプチャする
        Me.KeyPreview = True
        _logger = New SimpleLogger()

        _formStateCon = New FormStateContoller()
        '/
        Dim stateAmount As Integer = 2
        Dim bufState As AppStatus
        For Each mode_num As Integer In [Enum].GetValues(GetType(ExecuteMode))
            bufState = New AppStatus()
            ' ChangeStatusEvent に対するハンドラーを登録
            AddHandler bufState.ChangeStatusEvent, AddressOf ChangeStatusEvent
            bufState.ChangeMode(mode_num)
            Me._StateList.Add(bufState)
        Next
        _formStateCon._mainStatus = Me._StateList(0)

    End Sub


    ' ChangeStatusEvent メソッドを追加
    Private Sub ChangeStatusEvent()
        ' ここにステータス変更時の処理を追加
        Me._logger.PrintInfo("AppStatusが変更されました。")
        ' 他の処理をここに追加
    End Sub

    ' キーダウンイベントを処理
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        ' エスケープキーが押されたときの処理
        If e.KeyCode = Keys.Escape AndAlso Not e.Control AndAlso Not e.Shift Then
            _logger.PrintInfo("Escapeキーが押されました。")
            e.SuppressKeyPress = True ' キーが他に伝播しないようにする
        End If

        ' エスケープキー＋Ctrlが押されたときの処理
        If e.KeyCode = Keys.Escape AndAlso e.Control Then
            _logger.PrintInfo("Escape + Ctrlキーが押されました。")
            '先にWindowsのスタートメニューが起動してしまい、フォームが非アクティブになるのでこれは実行されない
            e.SuppressKeyPress = True
        End If

        ' Tabキーが押されたときの処理
        If e.KeyCode = Keys.Tab Then
            _logger.PrintInfo("Tabキーが押されました。")
            e.SuppressKeyPress = True
        End If

        ' Shift + Enterキーが押されたときの処理
        If e.KeyCode = Keys.Enter AndAlso e.Shift Then
            _logger.PrintInfo("Shift + Enterキーが押されました。")
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me._formStateCon.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim controlManager As New ControlManager(Me._formStateCon)
        controlManager.ExportToTextFile("controls_info.txt")
    End Sub

    Private Function GetTestPath(fileName As String)
        ' 実行中のアプリケーションの実行パスを取得する
        Dim executionPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Return executionPath
    End Function

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        ' 選択されたタブのインデックスを取得
        Dim selectedIndex As Integer = TabControl1.SelectedIndex

        ' インデックスに基づいて処理を分岐
        Select Case selectedIndex
            Case 0
                Me._logger.PrintInfo("Tab 1が選択されました")
                Me._formStateCon.ChangeMode(Me._StateList(ExecuteMode.MODE_A))
            Case 1
                Me._logger.PrintInfo("Tab 2が選択されました")
                Me._formStateCon.ChangeMode(Me._StateList(ExecuteMode.MODE_B))

            Case 2
                Me._logger.PrintInfo("Tab 3が選択されました")
                ' Tab 3が選択されたときの処理をここに書く

            Case Else
                Me._logger.PrintInfo("不明なタブが選択されました")
                ' その他のタブが選択されたときの処理
        End Select
    End Sub

End Class
