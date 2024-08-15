Imports System
Imports System.Windows.Forms

Module Program
    <STAThread()>
    Sub Main()
        Dim useConsole As Boolean = False ' ここでフラグを設定
        Dim args As String() = Environment.GetCommandLineArgs()
        If args.Contains("-console") Then
            useConsole = True
        End If

        If useConsole Then
            RunConsoleMode()
        Else
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New Form1()) ' 既存のWinFormsメインフォームを起動
        End If
    End Sub

    Private Sub RunConsoleMode()
        Console.WriteLine("Console Mode")
        ' コンソールモードの処理をここに記述
        Console.ReadLine()
    End Sub

    '' アプリケーションのビジュアルスタイルを有効にします。
    'Application.EnableVisualStyles()
    'Application.SetCompatibleTextRenderingDefault(False)
    '' Form1を起動します。
    'Application.Run(New Form1())
End Module
