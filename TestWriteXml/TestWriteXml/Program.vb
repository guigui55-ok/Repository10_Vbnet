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

        ' XML�t�@�C���̓ǂݍ���
        Dim xmlDocument As XDocument = XDocument.Load(filePath)

        ' ���[�g�v�f����Dictionary�ւ̕ϊ�
        Dim data As Dictionary(Of String, Object) = ConvertDictionaryFromXml(xmlDocument.Root)

        ' ���ʂ̊m�F
        PrintDictionary(data)
    End Sub


    ' Dictionary�̓��e���R���\�[���ɕ\�����邽�߂̃��\�b�h
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

        ' Dictionary�̃f�[�^������
        Dim logData As New Dictionary(Of String, String) From {
            {"LogPath", "Log\__log_{TimeFormat}.log"},
            {"LogFileTimeFormat", "yyyymmdd_hhmmss"}
        }

        Dim item1Data As New Dictionary(Of String, String) From {
            {"Time", "1:00:00"},
            {"RemainingTime", "00:00:00"},
            {"Notification", "Notification"}
        }

        ' XML�̍\�z
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

        ' XML�t�@�C���ւ̏�������
        xmlDocument.Save(filePath)
        Console.WriteLine("XML�t�@�C���Ƀf�[�^���������݂܂���: " & filePath)
    End Sub

    '////////////////////////////////////////////////////////////////////////////////

    Sub TestWriteXml_B()

        Dim fileName As String = "__test_file.xml"
        Dim fileNameB As String = "__test_file_text.xml"
        Dim dirPath As String = Directory.GetCurrentDirectory()
        Dim filePath As String = dirPath & "\" & fileName
        Dim filePathB As String = dirPath & "\" & fileNameB

        ' GetJson_D���\�b�h�Ńf�[�^���擾
        Dim data As Dictionary(Of String, Object) = GetJson_D()

        ' XML�h�L�������g�̐���
        Dim xmlDocument As XDocument = ConvertXmlDocumentFromDictionary(data)

        ' XML�t�@�C���ւ̏�������
        xmlDocument.Save(filePath)
        Console.WriteLine("XML�t�@�C���Ƀf�[�^���������݂܂���: " & filePath)
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

End Module
