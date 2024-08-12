Imports System
Imports Newtonsoft.Json
Imports System.IO

Module Program
    Sub Main(args As String())
        Console.WriteLine("Hello World!")
        TestWriteJson()
        TestReadMain()
    End Sub



    Sub TestReadMain()
        Console.WriteLine("#### TestReadMain")
        ' JSONファイルのパスを指定
        'Dim jsonFilePath As String = "path\to\your\jsonfile.json"
        Dim fileName As String = "__test_file.json"
        Dim fileNameB As String = "__test_file_text.json"
        Dim dirPath As String = Directory.GetCurrentDirectory()
        Dim filePath As String = dirPath & "\" & fileName
        Dim filePathB As String = dirPath & "\" & fileNameB

        ' JSONファイルを読み込み、Dictionaryに変換
        Dim jsonContent As String = File.ReadAllText(fileName)
        Dim jsonData As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonContent)

        ' Dictionaryの内容を確認（オプション）
        For Each kvp As KeyValuePair(Of String, Object) In jsonData
            Console.WriteLine($"{kvp.Key}: {kvp.Value}")
        Next
    End Sub

    '//////////
    Sub TestWriteJson()
        Dim fileName As String = "__test_file.json"
        Dim fileNameB As String = "__test_file_text.json"
        Dim dirPath As String = Directory.GetCurrentDirectory()
        Dim filePath As String = dirPath & "\" & fileName
        Dim filePathB As String = dirPath & "\" & fileNameB


        ' 保存するデータの定義
        'Dim person As New Person With {
        '    .Name = "John Doe",
        '    .Age = 30,
        '    .IsEmployed = True
        '}

        '/
        '文字列の場合
        Dim writeData = GetJson_B()
        File.WriteAllText(filePathB, writeData)
        Console.WriteLine("JSONファイルが作成されました: " & filePathB)

        '/
        'オブジェクトなどからjsonデータを作成
        writeData = GetJson_A()
        writeData = GetJson_C()
        writeData = GetJson_D()

        ' オブジェクトをJSONにシリアライズ
        Dim json As String = JsonConvert.SerializeObject(writeData, Formatting.Indented)

        ' JSONをファイルに書き込む
        File.WriteAllText(filePath, json)

        Console.WriteLine("JSONファイルが作成されました: " & filePath)
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

    Function GetJson_C()
        Console.WriteLine("GetJson_C: ")
        ' DictionaryでJSONのデータを定義
        'Dictionary型から
        Dim logData As New Dictionary(Of String, Object) From {
            {"LogPath", "Log\__log_{TimeFormat}.log"},
            {"LogFileTimeFormat", "yyyymmdd_hhmmss"},
            {"Item1", New Dictionary(Of String, Object) From {
                {"Time", "1:00:00"},
                {"Remaining Time", "00:00:00"},
                {"Notification", "Notification"}
            }}
        }
        Return logData
    End Function

    Function GetJson_B()
        Console.WriteLine("GetJson_B: ")
        ' JSON文字列の定義
        '文字列から
        Dim json As String = "{""Name"":""John Doe"",""Age"":30,""IsEmployed"":true}"
        Return json
    End Function

    Function GetJson_A()
        Console.WriteLine("GetJson_A: ")
        'クラスのメンバ変数から
        Dim person As New Person With {
            .Name = "John Doe",
            .Age = 30,
            .IsEmployed = True
        }
        Return person
    End Function

    ' サンプル用のクラス
    Public Class Person
        Public Property Name As String
        Public Property Age As Integer
        Public Property IsEmployed As Boolean
    End Class
End Module
