Imports System
Imports System.IO

Module Program
    Sub Main(args As String())
        Console.WriteLine("Hello World!")
        TestWriteXml_B()
        TestReadXmlMain()
    End Sub




    Sub TestReadXmlMain()
        Console.WriteLine("### TestReadXmlMain")
        Dim fileName As String = "__test_file.xml"
        Dim fileNameB As String = "__test_file_text.xml"
        Dim dirPath As String = Directory.GetCurrentDirectory()
        Dim filePath As String = dirPath & "\" & fileName
        Dim filePathB As String = dirPath & "\" & fileNameB

        ' XMLファイルの読み込み
        Dim xmlDocument As XDocument = XDocument.Load(filePath)

        ' ルート要素からDictionaryへの変換
        Dim data As Dictionary(Of String, Object) = ConvertDictionaryFromXml(xmlDocument.Root)

        ' 結果の確認
        PrintDictionary(data)
    End Sub


    ' Dictionaryの内容をコンソールに表示するためのメソッド
    Sub PrintDictionary(data As Dictionary(Of String, Object), Optional indent As Integer = 0)
        Dim indentString As String = New String(" "c, indent)

        For Each keyValuePair In data
            If TypeOf keyValuePair.Value Is Dictionary(Of String, Object) Then
                Console.WriteLine($"{indentString}{keyValuePair.Key}:")
                PrintDictionary(CType(keyValuePair.Value, Dictionary(Of String, Object)), indent + 4)
            Else
                Console.WriteLine($"{indentString}{keyValuePair.Key}: {keyValuePair.Value}")
            End If
        Next
    End Sub


    '////////////////////////////////////////////////////////////////////////////////
    Sub TestWriteXml()

        Dim fileName As String = "__test_file.xml"
        Dim fileNameB As String = "__test_file_text.xml"
        Dim dirPath As String = Directory.GetCurrentDirectory()
        Dim filePath As String = dirPath & "\" & fileName
        Dim filePathB As String = dirPath & "\" & fileNameB

        ' Dictionaryのデータを準備
        Dim logData As New Dictionary(Of String, String) From {
            {"LogPath", "Log\__log_{TimeFormat}.log"},
            {"LogFileTimeFormat", "yyyymmdd_hhmmss"}
        }

        Dim item1Data As New Dictionary(Of String, String) From {
            {"Time", "1:00:00"},
            {"RemainingTime", "00:00:00"},
            {"Notification", "Notification"}
        }

        ' XMLの構築
        Dim xmlDocument As New XDocument(
            New XElement("Root",
                New XElement("LogPath", logData("LogPath")),
                New XElement("LogFileTimeFormat", logData("LogFileTimeFormat")),
                New XElement("Item1",
                    New XElement("Time", item1Data("Time")),
                    New XElement("RemainingTime", item1Data("RemainingTime")),
                    New XElement("Notification", item1Data("Notification"))
                )
            )
        )

        ' XMLファイルへの書き込み
        xmlDocument.Save(filePath)
        Console.WriteLine("XMLファイルにデータを書き込みました: " & filePath)
    End Sub

    '////////////////////////////////////////////////////////////////////////////////

    Sub TestWriteXml_B()

        Dim fileName As String = "__test_file.xml"
        Dim fileNameB As String = "__test_file_text.xml"
        Dim dirPath As String = Directory.GetCurrentDirectory()
        Dim filePath As String = dirPath & "\" & fileName
        Dim filePathB As String = dirPath & "\" & fileNameB

        ' GetJson_Dメソッドでデータを取得
        Dim data As Dictionary(Of String, Object) = GetJson_D()

        ' XMLドキュメントの生成
        Dim xmlDocument As XDocument = ConvertXmlDocumentFromDictionary(data)

        ' XMLファイルへの書き込み
        xmlDocument.Save(filePath)
        Console.WriteLine("XMLファイルにデータを書き込みました: " & filePath)
    End Sub



    Function GetJson_D()
        Console.WriteLine("GetJson_D: ")
        'Dictionary型を1行ずつAddする
        ' メインのDictionaryを作成
        Dim logData As New Dictionary(Of String, Object)

        ' データを1行ずつ追加
        logData.Add("LogPath", "Log\__log_{TimeFormat}.log")
        logData.Add("LogFileTimeFormat", "yyyymmdd_hhmmss")

        ' Item1用のDictionaryを作成してデータを追加
        Dim item1 As New Dictionary(Of String, Object)
        item1.Add("Time", "1:00:00")
        item1.Add("Remaining Time", "00:00:00")
        item1.Add("Notification", "Notification")

        ' Item1をメインのDictionaryに追加
        logData.Add("Item1", item1)
        Return logData
    End Function

End Module
