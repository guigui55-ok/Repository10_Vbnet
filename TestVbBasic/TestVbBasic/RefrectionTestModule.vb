
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


    Sub MainRefrectionB()
        ' 取得したいメンバ変数名のリスト
        Dim MemberList As List(Of String) = New List(Of String)({"MaxValue", "MinValue", "DefaultName"})

        ' 定数クラスの型を取得
        Dim constantType As Type = GetType(Constants)

        ' 各メンバ名に対して値を取得し、出力する
        For Each memberName As String In MemberList
            ' メンバ名に対応するフィールドを取得
            Dim field As FieldInfo = constantType.GetField(memberName, BindingFlags.Public Or BindingFlags.Static Or BindingFlags.FlattenHierarchy)

            ' フィールドが存在するか確認し、値を取得
            If field IsNot Nothing AndAlso field.IsLiteral AndAlso Not field.IsInitOnly Then
                Dim value As Object = field.GetValue(Nothing) ' 定数の値を取得
                Console.WriteLine($"Constants({memberName}): {value}")
            Else
                Console.WriteLine($"メンバ '{memberName}' が見つかりません。")
            End If
        Next
    End Sub

    Public Structure ConstantsC
        Public Const MaxValue As Integer = 100
        Public Const MinValue As Integer = 1
        Public Const DefaultName As String = "TestName"
    End Structure

    Public Class TestData
        Public MaxValue As Integer
        Public MinValue As Integer
        Public DefaultName As String = ""
    End Class

    Sub MainRefrectionC()


        ' 取得したいメンバ変数名のリスト
        Dim MemberList As List(Of String) = New List(Of String)({"MaxValue", "MinValue", "DefaultName"})

        ' Structure の型を取得
        Dim constantType As Type = GetType(ConstantsC)

        ' 各メンバ名に対して値を取得し、出力する
        For Each memberName As String In MemberList
            ' メンバ名に対応するフィールドを取得
            Dim field As FieldInfo = constantType.GetField(
                memberName, BindingFlags.Public Or BindingFlags.Static Or BindingFlags.FlattenHierarchy)

            ' フィールドが存在するか確認し、値を取得
            If field IsNot Nothing AndAlso field.IsLiteral AndAlso Not field.IsInitOnly Then
                Dim value As Object = field.GetValue(Nothing) ' 定数の値を取得
                Console.WriteLine($"Constants({memberName}): {value}")
            Else
                Console.WriteLine($"メンバ '{memberName}' が見つかりません。")
            End If
        Next
    End Sub

    Sub MainRefrectD()
        ' 取得したいメンバ変数名のリスト
        Dim MemberList As List(Of String) = New List(Of String)({"MaxValue", "MinValue", "DefaultName"})

        ' 定数クラス（Structure）の型を取得
        Dim constantType As Type = GetType(Constants)

        ' TestData クラスのインスタンスを作成
        Dim testDataInstance As New TestData()

        ' 各メンバ名に対して定数から値を取得し、TestDataの対応するフィールドに代入
        For Each memberName As String In MemberList
            ' Constants（Structure）のフィールドを取得
            Dim constantField As FieldInfo = constantType.GetField(memberName, BindingFlags.Public Or BindingFlags.Static Or BindingFlags.FlattenHierarchy)

            ' TestDataクラスのフィールドを取得
            Dim testDataField As FieldInfo = testDataInstance.GetType().GetField(memberName, BindingFlags.Public Or BindingFlags.Instance)

            ' 両方のフィールドが存在し、定数の値をTestDataに代入する
            If constantField IsNot Nothing AndAlso testDataField IsNot Nothing AndAlso constantField.IsLiteral AndAlso Not constantField.IsInitOnly Then
                Dim value As Object = constantField.GetValue(Nothing) ' Constants の定数値を取得
                testDataField.SetValue(testDataInstance, value) ' TestData のフィールドに値を設定
                Console.WriteLine($"{memberName}: {value} を TestData に代入しました。")
            Else
                Console.WriteLine($"メンバ '{memberName}' が見つかりません。")
            End If
        Next

        ' TestData の結果を出力して確認
        Console.WriteLine("TestData の内容:")
        Console.WriteLine($"MaxValue: {testDataInstance.MaxValue}")
        Console.WriteLine($"MinValue: {testDataInstance.MinValue}")
        Console.WriteLine($"DefaultName: {testDataInstance.DefaultName}")
    End Sub
End Module
