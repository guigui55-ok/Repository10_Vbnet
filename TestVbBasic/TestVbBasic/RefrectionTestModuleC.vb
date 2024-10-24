Module RefrectionTestModuleC

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

    Public Sub TestCopyObjectC_1()
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
            Dim bufVal = Convert.ToInt32(enumValue) '数値に変換
            retDict.Add(enumName, bufVal)
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
                ' srcDict(srcKey)を  destField に合った型に変換する
                Dim bufVal = ConvertValueType(srcDict(srcKey), destField.FieldType)
                ' フィールドに値をセット
                destField.SetValue(distObject, bufVal)

                '確認のため出力
                If (srcKey = "VALUE_A") Then
                    Console.WriteLine("After setting VALUE_A: " & CType(distObject, ConstTestFlagsStruct).VALUE_A)
                End If
                If (srcKey = "VALUE_B") Then
                    Console.WriteLine("After setting VALUE_B: " & CType(distObject, ConstTestFlagsStruct).VALUE_B)
                End If
                If (srcKey = "VALUE_C") Then
                    Console.WriteLine("After setting VALUE_C: " & CType(distObject, ConstTestFlagsStruct).VALUE_C)
                End If
            End If
        Next
    End Sub

    ' srcValueのTypeをdestTypeのTypeに代入できるように、型変換を行う
    Public Function ConvertValueType(srcValue As Object, destType As Type) As Object
        ' すでに同じ型であれば、そのまま返す
        If srcValue.GetType() Is destType Then
            Return srcValue
        End If

        ' 型変換を試みる
        Try
            ' 文字列型への変換
            If destType Is GetType(String) Then
                Return srcValue.ToString()
            End If

            ' 整数型への変換
            If destType Is GetType(Integer) Then
                Return Convert.ToInt32(srcValue)
            End If

            ' その他の型への変換
            Return Convert.ChangeType(srcValue, destType)

        Catch ex As Exception
            Throw New InvalidCastException(String.Format("Cannot convert value '{0}' to type '{1}'", srcValue, destType.Name), ex)
        End Try
    End Function

    ' メンバ変数を {変数名:値} の Dictionary(Of String, Object) 型にして取得する
    Public Function GetDictFromObjectMember(argObject As Object) As Dictionary(Of String, Object)
        Dim retDict As New Dictionary(Of String, Object)
        Dim objType = argObject.GetType()

        ' メンバ変数を取得し、Dictionary に追加
        For Each field In objType.GetFields()
            retDict.Add(field.Name, field.GetValue(argObject))
        Next

        Return retDict
    End Function

    ' Dictionaryの内容を表示するメソッド
    Public Sub PrintDict(dict As Dictionary(Of String, Object))
        For Each key As String In dict.Keys
            Console.WriteLine(String.Format("{0}: {1}", key, dict(key)))
        Next
    End Sub
End Module
