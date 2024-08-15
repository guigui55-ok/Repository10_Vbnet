Imports System
Imports System.Windows.Forms

Module Program
    <STAThread()>
    Sub Main()
        Dim args As String() = Environment.GetCommandLineArgs()
        Dim segment As New ArraySegment(Of String)(args, 1, args.Length - 1)
        DebugPrint(String.Format("args = {0}", String.Join(" ", segment)))

        Dim useConsole As Boolean = args.Contains("-v") OrElse args.Contains("-m")
        Dim useWindow As Boolean = args.Contains("-w")
        'Consoleを優先する、WindowフラグがなければConsoleにする
        useConsole = Not useWindow


        If useConsole Then
            RunConsoleMode(args)
        End If

        If Not useConsole OrElse useWindow Then
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New FormSystemVolumeController()) ' 既存のWinFormsメインフォームを起動
        End If
    End Sub

    Private Sub RunConsoleMode(args As String())
        Dim volumeControl As New VolumeControl()

        ' ボリューム設定
        Dim volumeIndex As Integer = Array.IndexOf(args, "-v")
        If volumeIndex > -1 AndAlso volumeIndex + 1 < args.Length Then
            Dim volumeStr As String = args(volumeIndex + 1)
            Dim volume As Single
            If Single.TryParse(volumeStr, volume) AndAlso Not volumeControl.IsInvalidVolume(volume) Then
                volumeControl.SetVolume(volume)
                'Console.WriteLine($"ボリュームを {volume * 100}% に設定しました。")
                Console.WriteLine($"The volume has been set to {volume * 100}%.")
            Else
                'DebugPrint("無効なボリューム値です。0.0から1.0の範囲で指定してください。")
                Console.WriteLine("Invalid volume value. Please specify a value in the range of 0.0 to 1.0.")
            End If
        End If

        ' ミュート設定
        Dim muteIndex As Integer = Array.IndexOf(args, "-m")
        If muteIndex > -1 AndAlso muteIndex + 1 < args.Length Then
            'If muteIndex > -1 AndAlso muteIndex + 1 <= args.Length Then
            Dim muteStr As String = args(muteIndex + 1)
            If muteStr = "1" Then
                If Not volumeControl.IsMuted() Then
                    volumeControl.ToggleMute()
                End If
                'Console.WriteLine("System Volume Set To Mute.")
                Console.WriteLine("The system volume has been set to mute.")
            ElseIf muteStr = "0" Then
                If volumeControl.IsMuted() Then
                    volumeControl.ToggleMute()
                End If
                'DebugPrint("システムのミュートを解除しました。")
                Console.WriteLine("The system mute has been turned off.")
            Else
                'Console.WriteLine("無効なミュート値です。1=ミュート、0=ミュート解除で指定してください。")
                Console.WriteLine("Invalid mute value. Please specify 1 for mute and 0 for unmute.")
            End If
        End If

        ' ウィンドウ表示フラグが立っていない場合はここで終了
        If Not args.Contains("-w") Then
            'DebugPrint("処理が完了しました。Enterキーを押して終了します。")
            'Console.ReadLine()
            Console.WriteLine("Success")
            Exit Sub
        End If
    End Sub

    Public Sub DebugPrint(value As String)
        Debug.WriteLine(value)
    End Sub
End Module
