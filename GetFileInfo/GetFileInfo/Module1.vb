
Module Module1
    Sub Main()
        ' 設定: 対象ディレクトリ
        Dim targetDirectory As String = ""

        ' 設定: ファイル名パターン（正規表現）
        Dim filePatterns As List(Of String)

        'filePatterns = New List(Of String) From {
        '    "DoubleParseTest.exe",
        '    "^example.*\.exe$",   ' 例: exampleで始まる.exeファイル
        '    "^myapp_v\d+\.\d+\.dll$" ' 例: myapp_vX.X.dll形式
        '}

        targetDirectory = "C:\Users\OK\source\repos\Repository10_VBnet\DoubleParseTest\DoubleParseTest\bin\Debug"
        filePatterns = New List(Of String) From {
            "DoubleParseTest.exe"
        }

        ' ファイル情報収集クラスを作成
        Dim collector As New FileInfoCollector(targetDirectory, filePatterns)

        ' ファイル情報を取得
        Dim fileInfoList = collector.CollectFileInfo()

        Output("fileInfoList.Cont = " + fileInfoList.Count.ToString())
        ' 結果を出力
        For Each fileInfo In fileInfoList
            Output("========== ファイル情報 ==========")
            For Each kvp In fileInfo
                Output($"{kvp.Key}: {kvp.Value}")
            Next
            Output("")
        Next
    End Sub

    Public Class TargetInfo
        Dim _mode As Integer
        Dim _targetDir As String
        Dim _filePatterns As List(Of String)
        Public Class TargetInfoMode
            Public Const MODE_A = 1
            Public Const MODE_B = 2
        End Class
        Sub New(mode As Integer)
            _mode = mode
        End Sub

        Sub SetValue(Optional mode As Integer = -1)
            If mode < 0 Then
                mode = _mode
            End If
            Select Case mode
                Case TargetInfoMode.MODE_A
                    _targetDir = "C:\Users\OK\source\repos\Repository10_VBnet\DoubleParseTest\DoubleParseTest\bin\Debug"
                    _filePatterns = New List(Of String) From {
                        "DoubleParseTest.exe"
                    }
                Case Else
                    _targetDir = "-"
                    _filePatterns = New List(Of String) From {
                        "-"
                    }
            End Select
        End Sub

    End Class

    Public Sub Output(value As String)
        Console.WriteLine(value)
        Debug.WriteLine(value)
    End Sub
End Module