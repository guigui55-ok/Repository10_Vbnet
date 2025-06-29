Public Class CommandLineParser
    Private _options As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

    ''' <summary>
    ''' コマンドライン引数をパースします。
    ''' </summary>
    ''' <param name="args">引数配列</param>
    Public Sub Parse(args As String())
        Dim i As Integer = 0
        While i < args.Length
            Dim arg As String = args(i)

            If arg.StartsWith("--") OrElse arg.StartsWith("-") Then
                Dim key As String = arg.TrimStart("-"c)
                If i + 1 < args.Length AndAlso Not args(i + 1).StartsWith("-") Then
                    _options(key) = args(i + 1)
                    i += 1
                Else
                    _options(key) = "true"
                End If

            ElseIf arg.StartsWith("/") AndAlso arg.Contains(":") Then
                Dim parts = arg.Substring(1).Split(New Char() {":"c}, 2)
                If parts.Length = 2 Then
                    _options(parts(0)) = parts(1)
                End If

            End If

            i += 1
        End While
    End Sub

    ''' <summary>
    ''' 指定されたオプションが存在するかを確認します。
    ''' </summary>
    Public Function HasOption(key As String) As Boolean
        Return _options.ContainsKey(key)
    End Function

    ''' <summary>
    ''' 指定されたオプションの値を取得します。
    ''' </summary>
    Public Function GetOption(key As String, Optional defaultValue As String = "") As String
        If _options.ContainsKey(key) Then
            Return _options(key)
        Else
            Return defaultValue
        End If
    End Function

    ''' <summary>
    ''' すべてのオプションを取得します。
    ''' </summary>
    Public ReadOnly Property AllOptions As Dictionary(Of String, String)
        Get
            Return _options
        End Get
    End Property

    ''' <summary>
    ''' 使い方例
    ''' </summary>
    Sub UsageSample()
        Dim parser As New CommandLineParser()
        parser.Parse(Environment.GetCommandLineArgs().Skip(1).ToArray())

        If parser.HasOption("input") Then
            Console.WriteLine("Input: " & parser.GetOption("input"))
        End If

        If parser.HasOption("mode") Then
            Console.WriteLine("Mode: " & parser.GetOption("mode"))
        End If
    End Sub
End Class
