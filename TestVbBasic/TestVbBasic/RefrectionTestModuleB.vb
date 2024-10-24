Public Module RefrectionTestModuleB

    Public Enum ConstTestFlagsEnum
        VALUE_A
        VALUE_B
        VALUE_C
    End Enum

    Public Structure ConstTestFlagsStruct
        Public VALUE_A As String
        Public VALUE_B As String
        Public VALUE_C As Integer
    End Structure

    Public Class ConstTestFlagsClass
        Public VALUE_A As String
        Public VALUE_B As String
        Public VALUE_C As Integer
    End Class
    Public Sub TestCopyObjectB()
        ' Enum 型を取得
        Dim typeValue As Type = GetType(ConstTestFlagsEnum)

        ' Enum のメンバを辞書形式で取得
        Dim dict = GetDictFromObjectMemberEnum(typeValue)

        ' 対象の構造体にコピー
        Dim TestFlagsStruct As New ConstTestFlagsStruct
        CopyValues_SameMemberObjectByDict(dict, TestFlagsStruct)

        ' 結果を表示
        Dim dictResult = GetDictFromObjectMember(TestFlagsStruct)
        PrintDict(dictResult)
    End Sub

    ' Enumのメンバ変数の名前と値を{変数名:値}のDictionary(Of String, Object)型にして取得する
    Public Function GetDictFromObjectMemberEnum(enumType As Type) As Dictionary(Of String, Object)
        Dim retDict As New Dictionary(Of String, Object)

        ' Enumの名前と値を取得して辞書に格納
        For Each enumName In [Enum].GetNames(enumType)
            Dim enumValue = [Enum].Parse(enumType, enumName)
            retDict.Add(enumName, enumValue)
        Next

        Return retDict
    End Function

    ' srcObject から distObject にメンバ変数の値をコピーする
    ' srcDictはEnumの名前:値のDictionary
    Public Sub CopyValues_SameMemberObjectByDict(ByRef srcDict As Dictionary(Of String, Object), ByRef distObject As Object)
        Dim destType = distObject.GetType()

        ' distObject のフィールドに srcDict の値をコピーする
        For Each srcKey In srcDict.Keys
            Dim destField = destType.GetField(srcKey)
            If destField IsNot Nothing Then
                'srcDict(srcKey)を  destField に合った方に変換する
                Dim bufVal = ConvertValueType(srcDict(srcKey), destField)
                ' フィールドに値をセット
                destField.SetValue(distObject, srcDict(srcKey))
            End If
        Next
    End Sub

    'srcValueのTypeをdistValueのTypeに代入できるように、型変換を行う
    '※注意：distValueは destField を渡すか他の方法とするかは検討中
    Public Function ConvertValueType(srcValue As Object, distValue As Object)

    End Function





    Public Sub TestCopyObjectA()
        Dim TestFlagsA As ConstTestFlagsStruct = New ConstTestFlagsStruct With {
            .VALUE_A = "valueA",
            .VALUE_B = "valueB",
            .VALUE_C = 33
        }

        Dim TestFlagsB As New ConstTestFlagsClass()

        ' コピー処理の実行
        CopyValues_SameMemberObject(TestFlagsA, TestFlagsB)

        ' メンバ変数をDictionaryとして取得
        Dim dict = GetDictFromObjectMember(TestFlagsB)

        Console.WriteLine(" ##### ")
        PrintDict(dict)

        Console.WriteLine(" ##### ")
        Dim TestFlagsC As ConstTestFlagsStruct = New ConstTestFlagsStruct
        CopyValues_SameMemberObject(TestFlagsB, TestFlagsC)
        dict = GetDictFromObjectMember(TestFlagsB)
        PrintDict(dict)

        'Console.WriteLine(" ##### ")
        'Dim TestFlagsD As ConstTestFlagsStruct = New ConstTestFlagsStruct
        'CopyValues_SameMemberObject(EnumTestFlags, TestFlagsA)
        'dict = GetDictFromObjectMember(TestFlagsB)
        'PrintDict(dict)

    End Sub

    ' srcObject から distObjectにメンバ変数の値をコピーする
    ' メンバ変数名は同じものであることを確認済み
    ' srcObjectは[Enum, Structure, Class]を想定
    ' distObjectは[Structure, Class]を想定
    Public Sub CopyValues_SameMemberObject(ByRef srcObject As Object, ByRef distObject As Object)
        ' srcObjectのメンバ変数名一覧を取得する
        Dim srcType = srcObject.GetType()
        Dim destType = distObject.GetType()

        ' srcObjectのフィールドを取得し、対応するdestObjectのフィールドに値をコピーする
        For Each srcField In srcType.GetFields()
            Dim destField = destType.GetField(srcField.Name)
            If destField IsNot Nothing AndAlso destField.FieldType = srcField.FieldType Then
                destField.SetValue(distObject, srcField.GetValue(srcObject))
            End If
        Next
    End Sub

    ' argObjectのメンバ変数の値を{変数名:値}のDictionary(Of String, Object)型にして取得する
    Public Function GetDictFromObjectMember(argObject As Object) As Dictionary(Of String, Object)
        Dim retDict As New Dictionary(Of String, Object)
        Dim objType = argObject.GetType()

        ' メンバ変数を取得し、Dictionaryに追加
        For Each field In objType.GetFields()
            retDict.Add(field.Name, field.GetValue(argObject))
        Next

        Return retDict
    End Function

    Public Sub PrintDict(dict As Dictionary(Of String, Object))
        For Each key As String In dict.Keys
            Console.WriteLine(String.Format("{0}: {1}", key, dict(key)))
        Next
    End Sub

End Module