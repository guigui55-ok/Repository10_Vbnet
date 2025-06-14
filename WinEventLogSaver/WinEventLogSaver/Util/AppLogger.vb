

Imports System.IO


' 250614更新
Public Class AppLogger
    Public Enum LogLevel
        [DEF]
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
        NONE = 0
        DEBUG_WINDOW = 1
        CONSOLE = 2
        FILE = 4
    End Enum

    Public Property LoggerLogLevel As LogLevel = LogLevel.INFO
    Public Property FilePath As String = ""
    Public Property LogFileTimeFormat As String = "_yyyyMMdd_HHmmss"
    Public Property LogOutPutMode As OutputMode = OutputMode.DEBUG_WINDOW
    Public Property AddTime As Boolean = True

    Public Event AddLogEvent As EventHandler
    Public LastLog As String = ""

    Public Sub New()
    End Sub

    Public Sub MakeLogDir()
        Dim dirPath As String = Path.GetDirectoryName(Me.FilePath)
        If Not Directory.Exists(dirPath) Then
            Directory.CreateDirectory(dirPath)
            Debug.Print("Log CreateDirectory Path= " & dirPath)
        End If
    End Sub


    Public Function GetDefaultLogFilePath() As String
        Dim exeDirPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Dim logDirPath = Path.Combine(exeDirPath, "Log")
        Dim logFileName = "AppLog_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".log"
        Dim logFilePath = Path.Combine(logDirPath, logFileName)
        Return logFilePath
    End Function

    ''' <summary>
    ''' ログのファイルパスを設定する（日付自動付与）
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="logFileTimeFormat"></param>
    Public Sub SetFilePathWithDate(filePath As String, Optional logFileTimeFormat As String = "")
        If String.IsNullOrEmpty(logFileTimeFormat) Then
            logFileTimeFormat = Me.LogFileTimeFormat
        End If

        If String.IsNullOrEmpty(logFileTimeFormat) Then
            Me.FilePath = filePath
        Else
            Dim dirPath As String = Path.GetDirectoryName(filePath)
            Dim fileNameOnly As String = Path.GetFileNameWithoutExtension(filePath)
            Dim datetimeStr As String = DateTime.Now.ToString(logFileTimeFormat)
            Dim ext As String = Path.GetExtension(filePath)
            Me.FilePath = $"{dirPath}\{fileNameOnly}{datetimeStr}{ext}"
        End If
        Me.MakeLogDir()
    End Sub

    Public Sub SetFilePath(filePath As String)
        LogFileTimeFormat = ""
        If String.IsNullOrEmpty(LogFileTimeFormat) Then
            LogFileTimeFormat = Me.LogFileTimeFormat
        End If

        If String.IsNullOrEmpty(LogFileTimeFormat) Then
            Me.FilePath = filePath
        Else
            Dim dirPath As String = Path.GetDirectoryName(filePath)
            Dim fileNameOnly As String = Path.GetFileNameWithoutExtension(filePath)
            Dim datetimeStr As String = DateTime.Now.ToString(LogFileTimeFormat)
            Dim ext As String = Path.GetExtension(filePath)
            Me.FilePath = $"{dirPath}\{fileNameOnly}{datetimeStr}{ext}"
        End If
        Me.MakeLogDir()
    End Sub


    Public Sub Critical(value As String)
        If LogLevel.CRITICAL <= Me.LoggerLogLevel Then
            Me.Print(value)
        End If
    End Sub

    Public Sub Err(ex As Exception, value As String)
        If LogLevel.ERR <= Me.LoggerLogLevel Then
            Me.Print(value)
            Me.Print(ex.GetType().ToString + ":" + ex.Message)
            Me.Print(ex.StackTrace)
        End If
    End Sub

    Public Sub Err(value As String)
        If LogLevel.ERR <= Me.LoggerLogLevel Then
            Me.Print(value)
        End If
    End Sub

    Public Sub Warn(value As String)
        If LogLevel.WARN <= Me.LoggerLogLevel Then
            Me.Print(value)
        End If
    End Sub

    Public Sub Info(value As String)
        If LogLevel.INFO <= Me.LoggerLogLevel Then
            Me.Print(value)
        End If
    End Sub

    Public Sub DebugInfo(value As String)
        If LogLevel.DEBUG <= Me.LoggerLogLevel Then
            Me.Print(value)
        End If
    End Sub

    Public Sub TraceInfo(value As String)
        If LogLevel.TRACE <= Me.LoggerLogLevel Then
            Me.Print(value)
        End If
    End Sub

    Private Function AddTimeValue(value As String) As String
        If Me.AddTime Then
            Return Me.GetTimeStr() & "    " & value
        End If
        Return value
    End Function

    Private Function GetTimeStr() As String
        Dim now As DateTime = DateTime.Now
        Return now.ToString("[yyyy/MM/dd HH:mm:ss.ffffff]")
    End Function

    Private Sub Print(value As String)
        value = Me.AddTimeValue(value)
        If (Me.LogOutPutMode And OutputMode.DEBUG_WINDOW) = OutputMode.DEBUG_WINDOW Then
            Debug.WriteLine(value)
        End If
        If (Me.LogOutPutMode And OutputMode.CONSOLE) = OutputMode.CONSOLE Then
            Console.WriteLine(value)
        End If
        If (Me.LogOutPutMode And OutputMode.FILE) = OutputMode.FILE Then
            Me.PrintToFile(value)
        End If
        LastLog = value
        RaiseEvent AddLogEvent(Me, EventArgs.Empty)
    End Sub

    Private Sub PrintToFile(value As String)
        If Not String.IsNullOrEmpty(Me.FilePath) Then
            Me.WriteToFile(value)
        End If
    End Sub

    Private Sub WriteToFile(value As String)
        Try
            Using writer As New StreamWriter(Me.FilePath, True)
                writer.WriteLine(value)
            End Using
        Catch ex As Exception
            Debug.WriteLine("WriteToFile ERROR: " & ex.Message)
        End Try
    End Sub

End Class
