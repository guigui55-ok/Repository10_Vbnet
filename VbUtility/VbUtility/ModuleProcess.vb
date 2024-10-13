Imports System.IO

Module ModuleProcess
    Public Class ExeFile
        Public Function ExecuteFile(filePath As String, param As String)
            Try
                If (File.Exists(filePath)) Then
                    'Explorerで指定したファイルを選択状態で開く
                    System.Diagnostics.Process.Start(String.Format("""{0}""", filePath), String.Format("""{0}""", param))
                    Return True
                Else
                    'ファイルが存在しない場合のエラーメッセージ
                    'Console.Writeline("ファイルが存在しません。")
                    Return False
                End If
            Catch ex As Exception
                Console.WriteLine("ExecuteFile Error")
                Console.WriteLine(ex.GetType().ToString() + ":" + ex.Message)
                Console.WriteLine(ex.StackTrace)
                Return False
            End Try
        End Function
    End Class

    Public Function ExecuteProcess(filePath As String, param As String)
        Try
            If (File.Exists(filePath)) Then
                'Explorerで指定したファイルを選択状態で開く
                System.Diagnostics.Process.Start(String.Format("""{0}""", filePath), String.Format("""{0}""", param))
                Return True
            Else
                'ファイルが存在しない場合のエラーメッセージ
                'Console.Writeline("ファイルが存在しません。")
                Return False
            End If
        Catch ex As Exception
            Console.WriteLine("ExecuteFile Error")
            Console.WriteLine(ex.GetType().ToString() + ":" + ex.Message)
            Console.WriteLine(ex.StackTrace)
            Return False
        End Try
    End Function
End Module
