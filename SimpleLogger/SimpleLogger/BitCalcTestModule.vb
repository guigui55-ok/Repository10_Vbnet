

Module BitCalcTestModule

        ' 設定値をビット値で定義する
        <Flags>
        Enum Settings
            None = 0         ' 0000
            Setting1 = 1     ' 0001
            Setting2 = 2     ' 0010
            Setting3 = 4     ' 0100
            Setting4 = 8     ' 1000
        End Enum

    Sub BitCalcTestMain()
        ' 複数の設定値を組み合わせる
        Dim combinedSettings As Settings = Settings.Setting1 Or Settings.Setting3

        ' 設定値を表示する
        Console.WriteLine("Combined Settings: " & combinedSettings.ToString())

        ' 特定の設定値が含まれているかを確認する
        If (combinedSettings And Settings.Setting1) = Settings.Setting1 Then
            Console.WriteLine("Setting1 is included.")
        End If

        If (combinedSettings And Settings.Setting2) = Settings.Setting2 Then
            Console.WriteLine("Setting2 is included.")
        Else
            Console.WriteLine("Setting2 is not included.")
        End If

        ' 設定値の追加
        combinedSettings = combinedSettings Or Settings.Setting2
        Console.WriteLine("Updated Combined Settings: " & combinedSettings.ToString())

        ' 設定値の削除
        combinedSettings = combinedSettings And Not Settings.Setting1
        Console.WriteLine("Updated Combined Settings after removing Setting1: " & combinedSettings.ToString())
    End Sub

End Module
