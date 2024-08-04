
Imports System.IO
Imports System.Diagnostics

Public Enum LogLevel
    DEF
    CRITICAL
    ERR
    WARN
    NORMAL
    INFO
    DEBUG
    TRACE
End Enum

<Flags>
Public Enum OutputMode
    NONE = &B0         ' 0000
    DEBUG_WINDOW = &B1     ' 0001
    CONSOLE = &B10     ' 0010
    FILE = &B100     ' 0100
End Enum

Public Class SimpleLogger
    Public LoggerLogLevel As Integer = LogLevel.INFO
    Public FilePath As String = ""
    Public LogOutPutMode As OutputMode = OutputMode.DEBUG_WINDOW
    Public AddTime As Boolean = True

    Sub New()

    End Sub

    Sub PrintCritical(Value As String)

        If LogLevel.CRITICAL <= Me.LoggerLogLevel Then
            Me.Print(Value)
        End If
    End Sub
    Sub PrintError(Value As String)
        If LogLevel.ERR <= Me.LoggerLogLevel Then
            Me.Print(Value)
        End If
    End Sub
    Sub PrintWarn(Value As String)
        If LogLevel.WARN <= Me.LoggerLogLevel Then
            Me.Print(Value)
        End If
    End Sub
    Sub PrintInfo(Value As String)
        If LogLevel.INFO <= Me.LoggerLogLevel Then
            Me.Print(Value)
        End If
    End Sub
    Sub PrintDebug(Value As String)
        If LogLevel.DEBUG <= Me.LoggerLogLevel Then
            Me.Print(Value)
        End If
    End Sub
    Sub PrintTrace(Value As String)
        If LogLevel.TRACE <= Me.LoggerLogLevel Then
            Me.Print(Value)
        End If
    End Sub
    '////////////////////////////////////////////////////////////////////////////////
    ' Common
    Private Function AddTimeValue(Value As String)
        If Me.AddTime Then
            Return Me.GetTimeStr() & "    " & Value
        End If
    End Function
    Private Function GetTimeStr()
        Dim now As DateTime = DateTime.Now
        ' 指定された書式で日付と時刻を文字列として取得
        Dim formattedDate As String = now.ToString("yyyy/MM/dd HH:mm:ss ffffff")
        Return formattedDate
    End Function
    Private Sub Print(Value As String)
        Value = Me.AddTimeValue(Value)
        If (Me.LogOutPutMode And OutputMode.DEBUG_WINDOW) = OutputMode.DEBUG_WINDOW Then
            Debug.WriteLine(Value)
        End If
        If (Me.LogOutPutMode And OutputMode.CONSOLE) = OutputMode.CONSOLE Then
            Console.WriteLine(Value)
        End If
        If (Me.LogOutPutMode And OutputMode.FILE) = OutputMode.FILE Then
            Me.PrintToFile(Value)
        End If
    End Sub

    Private Sub PrintToFile(Value As String)
        If Me.FilePath <> "" Then
            Me.WriteToFile(Value)
        End If
    End Sub

    Private Sub WriteToFile(Value As String)
        Try
            Using writer As StreamWriter = New StreamWriter(FilePath, True)
                writer.Write(Value & Environment.NewLine)
            End Using
        Catch ex As Exception
            ' エラーが発生した場合の処理
            Debug.WriteLine("WriteToFile ERROR: " & ex.Message)
        End Try
    End Sub
    '////////////////////////////////////////////////////////////////////////////////
    'Sample
    Private Sub SampleWriteText()
        ' 書き込むファイルのパス
        Dim filePath As String = "C:\example\output.txt"

        ' 書き込むテキスト
        Dim textToWrite As String = "こんにちは、世界！"

        ' テキストを書き込む
        Try
            ' File.WriteAllTextを使用してファイルにテキストを書き込む
            File.WriteAllText(filePath, textToWrite)
            Console.WriteLine("ファイルにテキストを書き込みました。")
        Catch ex As Exception
            ' エラーが発生した場合の処理
            Console.WriteLine("エラーが発生しました: " & ex.Message)
        End Try

        ' プログラムが終了しないように入力を待つ
        Console.ReadLine()
    End Sub

End Class
