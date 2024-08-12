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
        ' JSON�t�@�C���̃p�X���w��
        'Dim jsonFilePath As String = "path\to\your\jsonfile.json"
        Dim fileName As String = "__test_file.json"
        Dim fileNameB As String = "__test_file_text.json"
        Dim dirPath As String = Directory.GetCurrentDirectory()
        Dim filePath As String = dirPath & "\" & fileName
        Dim filePathB As String = dirPath & "\" & fileNameB

        ' JSON�t�@�C����ǂݍ��݁ADictionary�ɕϊ�
        Dim jsonContent As String = File.ReadAllText(fileName)
        Dim jsonData As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonContent)

        ' Dictionary�̓��e���m�F�i�I�v�V�����j
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


        ' �ۑ�����f�[�^�̒�`
        'Dim person As New Person With {
        '    .Name = "John Doe",
        '    .Age = 30,
        '    .IsEmployed = True
        '}

        '/
        '������̏ꍇ
        Dim writeData = GetJson_B()
        File.WriteAllText(filePathB, writeData)
        Console.WriteLine("JSON�t�@�C�����쐬����܂���: " & filePathB)

        '/
        '�I�u�W�F�N�g�Ȃǂ���json�f�[�^���쐬
        writeData = GetJson_A()
        writeData = GetJson_C()
        writeData = GetJson_D()

        ' �I�u�W�F�N�g��JSON�ɃV���A���C�Y
        Dim json As String = JsonConvert.SerializeObject(writeData, Formatting.Indented)

        ' JSON���t�@�C���ɏ�������
        File.WriteAllText(filePath, json)

        Console.WriteLine("JSON�t�@�C�����쐬����܂���: " & filePath)
    End Sub

    Function GetJson_D()
        Console.WriteLine("GetJson_D: ")
        'Dictionary�^��1�s����Add����
        ' ���C����Dictionary���쐬
        Dim logData As New Dictionary(Of String, Object)

        ' �f�[�^��1�s���ǉ�
        logData.Add("LogPath", "Log\__log_{TimeFormat}.log")
        logData.Add("LogFileTimeFormat", "yyyymmdd_hhmmss")

        ' Item1�p��Dictionary���쐬���ăf�[�^��ǉ�
        Dim item1 As New Dictionary(Of String, Object)
        item1.Add("Time", "1:00:00")
        item1.Add("Remaining Time", "00:00:00")
        item1.Add("Notification", "Notification")

        ' Item1�����C����Dictionary�ɒǉ�
        logData.Add("Item1", item1)
        Return logData
    End Function

    Function GetJson_C()
        Console.WriteLine("GetJson_C: ")
        ' Dictionary��JSON�̃f�[�^���`
        'Dictionary�^����
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
        ' JSON������̒�`
        '�����񂩂�
        Dim json As String = "{""Name"":""John Doe"",""Age"":30,""IsEmployed"":true}"
        Return json
    End Function

    Function GetJson_A()
        Console.WriteLine("GetJson_A: ")
        '�N���X�̃����o�ϐ�����
        Dim person As New Person With {
            .Name = "John Doe",
            .Age = 30,
            .IsEmployed = True
        }
        Return person
    End Function

    ' �T���v���p�̃N���X
    Public Class Person
        Public Property Name As String
        Public Property Age As Integer
        Public Property IsEmployed As Boolean
    End Class
End Module
