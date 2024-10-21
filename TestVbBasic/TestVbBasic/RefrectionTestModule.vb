
Imports System.Reflection

Public Module RefrectionTestModule

    Public Class Constants
        Public Const MaxValue As Integer = 100
        Public Const MinValue As Integer = 1
        Public Const DefaultName As String = "TestName"
    End Class

    Sub MainRefrection()
        ' 定数クラスの型を取得
        Dim constantType As Type = GetType(Constants)

        ' 定数フィールドを取得
        Dim fields As FieldInfo() = constantType.GetFields(
            BindingFlags.Public Or BindingFlags.Static Or BindingFlags.FlattenHierarchy)

        ' 変数名と値を出力
        For Each field As FieldInfo In fields
            If field.IsLiteral AndAlso Not field.IsInitOnly Then
                ' フィールド名と値を表示
                Console.WriteLine($"{field.Name}: {field.GetValue(Nothing)}")
            End If
        Next
    End Sub
End Module
