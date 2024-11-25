
Imports System.Reflection
Module ModuleClassMemberTest

    Class ConstTest
        Public Const NUMBER_01 = "Message 01"
        Public Const NUMBER_02 = "Message 02"
        Public Const NUMBER_03 = "Message 03"
        Public Const NUMBER_04 = "Message 04"
    End Class


    Sub Main()
        '想定する使い方
        Dim num = GetNumber(ConstTest.NUMBER_01) '戻り値は”01”
        Debug.Print(String.Format("NUMBER_01 = {0}", num))
    End Sub



    Function GetNumber(value As Object) As String
        ' ConstTestクラスの型情報を取得
        Dim constType As Type = GetType(ConstTest)

        ' 定数フィールドを取得
        For Each field As FieldInfo In constType.GetFields(BindingFlags.Public Or BindingFlags.Static)
            ' フィールドが定数かつ型が一致する場合
            If field.IsLiteral AndAlso field.GetRawConstantValue().GetType().Equals(value.GetType()) Then
                ' フィールドの値が引数と一致する場合
                If field.GetRawConstantValue().Equals(value) Then
                    ' フィールド名から数値部分を抽出して返す
                    Dim match = System.Text.RegularExpressions.Regex.Match(field.Name, "\d+")
                    If match.Success Then
                        Return match.Value
                    End If
                End If
            End If
        Next

        ' 一致しない場合は空文字を返す
        Return String.Empty
    End Function


    ' #########

    Public Const NUMBER_001 = 1

    Sub MainGetVarName()
        GetVarNameConst()
        GetVarName(NUMBER_001)
    End Sub

    Sub GetVarNameConst()
        ' 定数の名前を取得
        Dim type As Type = GetType(ModuleClassMemberTest)
        Dim fields = type.GetFields(Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Static)
        For Each field In fields
            If field.IsLiteral AndAlso Not field.IsInitOnly Then ' 定数フィールドを確認
                If field.GetValue(Nothing).Equals(NUMBER_001) Then
                    Debug.Print(String.Format("valueName = {0}", field.Name))
                End If
            End If
        Next
    End Sub


    Sub GetVarName(value As Integer)
        '引数で渡されたときのの変数名を取得するこの場合「NUMBER_001」
        Debug.Print(String.Format("valueName = {0}", ""))
    End Sub


    Sub GetClassMemberVarMain()
        Dim obj As New ClassMember With {
            .ValueStr = "Hello",
            .ValueInt = 42,
            .VlaueDate = Date.Now,
            .ValueBool = True,
            .ValueDouble = 3.14
        }

        Dim names = ObjectInspector.GetMemberVariableNames(obj)
        Dim namesAndValues = ObjectInspector.GetMemberVariableNamesAndValues(obj)
        Dim values = ObjectInspector.GetMemberVariableValues(obj)
        Output("***")
        For Each name In names
            Output(name)
        Next
        Output("***")
        For Each kvp In namesAndValues
            Output($"Name: {kvp.Key}, Value: {kvp.Value}")
        Next
        Output("***")
        For Each value In values
            Output(value)
        Next

    End Sub


    Sub MainB()
        ' ClassMember のインスタンスを作成
        Dim obj As New ClassMember()

        ' メンバー変数名を動的に取得して表示
        Dim memberNames = GetMemberVariableNames(obj)

        Output("ClassMemberのメンバー変数名:")
        For Each name In memberNames
            Output(name)
        Next

        '##########
        'メンバー変数名とその値を動的に取得して表示
        Dim objB As New ClassMember With {
        .ValueStr = "Hello",
        .ValueInt = 42,
        .VlaueDate = Date.Now,
        .ValueBool = True,
        .ValueDouble = 3.14
        }

        Dim memberData = GetMemberVariableNamesAndValues(objB)

        Output("ClassMemberのメンバー変数名とその値:")
        For Each kvp In memberData
            Output($"Name: {kvp.Key}, Value: {kvp.Value}")
        Next

        Dim values = GetMemberVariableValues(obj)

        Console.WriteLine("ClassMemberのメンバー変数の値:")
        For Each value In values
            Console.WriteLine(value)
        Next

    End Sub

    '##################################################

    ' 特定のクラスのメンバー変数名を取得する関数
    Function GetMemberVariableNames(ByVal obj As Object) As List(Of String)
        Dim memberNames As New List(Of String)

        ' オブジェクトの型情報を取得
        Dim type As Type = obj.GetType()

        ' メンバー変数（フィールド）を取得
        Dim fields As FieldInfo() = type.GetFields(BindingFlags.Public Or BindingFlags.Instance)

        ' メンバー変数名をリストに追加
        For Each field As FieldInfo In fields
            memberNames.Add(field.Name)
        Next


        Return memberNames
    End Function

    Function GetMemberVariableNamesAndValues(ByVal obj As Object) As Dictionary(Of String, Object)
        Dim memberData As New Dictionary(Of String, Object)

        ' オブジェクトの型情報を取得
        Dim type As Type = obj.GetType()

        ' メンバー変数（フィールド）を取得
        Dim fields As FieldInfo() = type.GetFields(BindingFlags.Public Or BindingFlags.Instance)

        ' メンバー変数名と値を辞書に追加
        For Each field As FieldInfo In fields
            Dim name As String = field.Name
            Dim value As Object = field.GetValue(obj)
            memberData.Add(name, value)
        Next

        Return memberData
    End Function


    Function GetMemberVariableValues(ByVal obj As Object) As List(Of String)
        Dim values As New List(Of String)

        ' オブジェクトの型情報を取得
        Dim type As Type = obj.GetType()

        ' メンバー変数（フィールド）を取得
        Dim fields As FieldInfo() = type.GetFields(BindingFlags.Public Or BindingFlags.Instance)

        ' メンバー変数の値をリストに追加
        For Each field As FieldInfo In fields
            Dim value As Object = field.GetValue(obj)
            ' 値を文字列としてリストに追加
            values.Add(If(value?.ToString(), "Nothing"))
        Next

        Return values
    End Function


    Sub Output(value As Object)
        Dim buf = String.Format("{0}", value)
        Debug.WriteLine(value)
    End Sub

    ' 指定されたクラス
    Class ClassMember
        Public ValueStr As String
        Public ValueInt As Integer
        Public VlaueDate As Date
        Public ValueBool As Boolean
        Public ValueDouble As Double
    End Class
End Module

