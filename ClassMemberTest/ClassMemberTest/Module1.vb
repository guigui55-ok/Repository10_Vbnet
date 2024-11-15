
Imports System.Reflection

Module ModuleObjectInspector

    Public Class ObjectInspector
        ' 指定したオブジェクトのメンバー変数名を取得する
        Public Shared Function GetMemberVariableNames(ByVal obj As Object) As List(Of String)
            Dim memberNames As New List(Of String)
            Dim type As Type = obj.GetType()
            Dim fields As FieldInfo() = type.GetFields(BindingFlags.Public Or BindingFlags.Instance)

            For Each field As FieldInfo In fields
                memberNames.Add(field.Name)
            Next

            Return memberNames
        End Function

        ' 指定したオブジェクトのメンバー変数名とその値を取得する
        Public Shared Function GetMemberVariableNamesAndValues(ByVal obj As Object) As Dictionary(Of String, Object)
            Dim memberData As New Dictionary(Of String, Object)
            Dim type As Type = obj.GetType()
            Dim fields As FieldInfo() = type.GetFields(BindingFlags.Public Or BindingFlags.Instance)

            For Each field As FieldInfo In fields
                Dim name As String = field.Name
                Dim value As Object = field.GetValue(obj)
                memberData.Add(name, value)
            Next

            Return memberData
        End Function

        ' 指定したオブジェクトのメンバー変数の値のみを取得する
        Public Shared Function GetMemberVariableValues(ByVal obj As Object) As List(Of String)
            Dim values As New List(Of String)
            Dim type As Type = obj.GetType()
            Dim fields As FieldInfo() = type.GetFields(BindingFlags.Public Or BindingFlags.Instance)

            For Each field As FieldInfo In fields
                Dim value As Object = field.GetValue(obj)
                values.Add(If(value?.ToString(), "Nothing"))
            Next

            Return values
        End Function
    End Class
End Module
