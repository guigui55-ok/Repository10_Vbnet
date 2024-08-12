
Imports System.IO
Module XmlModule

    Function GetDictionaryByReadXml(logger As MainLogger, xmlFilePath As String, Optional defaultDict As Dictionary(Of String, Object) = Nothing)
        Dim xmlDocument As XDocument
        'ファイルパスが存在しない場合は defaultDict を書き込む
        If Not File.Exists(xmlFilePath) Then
            logger.PrintInfo("Not Exists XmlFilePath")
            logger.PrintInfo(String.Format("xmlFilePath = {0}", xmlFilePath))
            ' XMLドキュメントの生成
            xmlDocument = ConvertXmlDocumentFromDictionary(defaultDict)
            ' XMLファイルへの書き込み
            xmlDocument.Save(xmlFilePath)
        End If
        ' XMLファイルの読み込み
        xmlDocument = XDocument.Load(xmlFilePath)
        ' ルート要素からDictionaryへの変換
        Dim data As Dictionary(Of String, Object) = ConvertDictionaryFromXml(xmlDocument.Root)
        logger.PrintInfo(String.Format("ReadXml Path = {0}", xmlFilePath))
        Return data
    End Function

    ' XMLからDictionaryに変換するメソッド
    Function ConvertDictionaryFromXml(element As XElement) As Dictionary(Of String, Object)
        Dim dictionary As New Dictionary(Of String, Object)()

        For Each childElement As XElement In element.Elements()
            If childElement.HasElements Then
                ' 子要素を持つ場合は再帰的に処理
                dictionary.Add(childElement.Name.LocalName, ConvertDictionaryFromXml(childElement))
            Else
                ' 子要素がない場合はそのまま値を格納
                dictionary.Add(childElement.Name.LocalName, childElement.Value)
            End If
        Next

        Return dictionary
    End Function



    ' DictionaryからXMLドキュメントを生成するメソッド
    Function ConvertXmlDocumentFromDictionary(data As Dictionary(Of String, Object)) As XDocument
        Dim rootElement As New XElement("Root")
        AddElementsToXml(rootElement, data)
        Return New XDocument(rootElement)
    End Function

    ' Dictionaryのデータを再帰的にXML要素に変換して追加するメソッド
    Sub AddElementsToXml(parentElement As XElement, data As Dictionary(Of String, Object))
        For Each keyValuePair In data
            Dim key As String = keyValuePair.Key
            Dim value As Object = keyValuePair.Value

            If TypeOf value Is Dictionary(Of String, Object) Then
                ' ネストされたDictionaryを処理
                Dim childElement As New XElement(key)
                AddElementsToXml(childElement, CType(value, Dictionary(Of String, Object)))
                parentElement.Add(childElement)
            Else
                ' シンプルなキーと値を処理
                ' スペースを含むキーはXML要素名として使えないため、スペースを削除
                Dim elementName As String = key.Replace(" ", "")
                parentElement.Add(New XElement(elementName, value.ToString()))
            End If
        Next
    End Sub



End Module
