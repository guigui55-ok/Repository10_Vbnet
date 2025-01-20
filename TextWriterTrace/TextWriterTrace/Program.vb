Imports System.Diagnostics

Module Program
    Sub Main()
        ' TraceListener を追加してファイルに出力
        Dim logFilePath As String = "debug_output.log"
        Dim fileListener As New TextWriterTraceListener(logFilePath)

        ' デフォルトの Debug 出力にファイルリスナーを追加
        Debug.Listeners.Add(fileListener)

        ' デバッグ出力
        Debug.WriteLine("デバッグメッセージ: プログラムが開始されました。")

        ' 明示的にファイルをフラッシュして閉じる
        Debug.Flush()

        ' 実行中の間のデバッグ出力
        For i As Integer = 0 To 5
            Debug.WriteLine($"デバッグメッセージ: カウント {i}")
            Threading.Thread.Sleep(500)
        Next

        Debug.WriteLine("デバッグメッセージ: プログラムが終了します。")

        ' リスナーを削除して閉じる
        Debug.Flush()
        Debug.Listeners.Remove(fileListener)
        fileListener.Close()

        Console.WriteLine($"デバッグ出力が {logFilePath} に保存されました。")
        Debug.WriteLine($"デバッグ出力が {logFilePath} に保存されました。")
    End Sub
End Module