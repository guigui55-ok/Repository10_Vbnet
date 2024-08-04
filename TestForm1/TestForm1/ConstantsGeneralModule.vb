

'https://qiita.com/wifrstfasnriov/questions/84898783c41b7b4be25d


'ファイル名とモジュール名は別にする（ファイル名は影響がないが、混同するため）
'定数を入れ子にする場合は NameSpace で入れ子にする
'基本的に Enum > Module > Class を優先して使用する
'ReadOnlyは付けておく
'Shared 修飾子は状況に応じて
Namespace ConstGeneral
    Public Enum ConstProcessStatus
        NOT_SET_DATA
        WAIT_PROCESS
        PROCESSING
        END_PROCESS
    End Enum

    Public Module ConstGeneralModule
        Public ReadOnly ProessStatusName As New Dictionary(Of ConstProcessStatus, String) From {
            {ConstProcessStatus.NOT_SET_DATA, "データ未セット"},
            {ConstProcessStatus.WAIT_PROCESS, "測定前待機"},
            {ConstProcessStatus.PROCESSING, "測定中"},
            {ConstProcessStatus.END_PROCESS, "測定終了"}
        }
    End Module
End Namespace


Module ConstantsGeneralModule
    Public Const DaysInYear = 365
    Private Const WorkDays = 250

    Sub TestMethod()
        'Dim Buf As String
        If False Then
            Console.WriteLine(WorkDays)
        End If
        'Buf = WorkDays
        '重大度レベル  コード	説明	プロジェクト	ファイル	行	抑制状態
        'メッセージ   IDE0059	値の 'Buf' への不必要な代入	TestForm1	C:\Users\OK\source\repos\Repository10_VBnet\TestForm1\TestForm1\ConstantsGeneralModule.vb	16	アクティブ

    End Sub
End Module

'Imports WindowsApplication1.Form1.Days

Public Enum WorkDays
    Saturday
    Sunday = 0
    Monday
    Tuesday
    Wednesday
    Thursday
    Friday
    Invalid = -1
End Enum
'重大度レベル	コード	説明	プロジェクト	ファイル	行	抑制状態
'エラー	BC30001	名前空間では有効でないステートメントです。	TestForm1	C:\Users\OK\source\repos\Repository10_VBnet\TestForm1\TestForm1\ConstantsGeneralModule.vb	5	アクティブ

'Public Const DaysInYear = 365
'Private Const WorkDays = 250

'https://atmarkit.itmedia.co.jp/ait/articles/1712/20/news024.html


'列挙値に関連付けられている文字列を確認する
Public Enum FlavorEnum
    salty
    sweet
    sour
    bitter
End Enum
Class TestConstGeneralModuleTestClass
    Public Sub TestB()
        If False Then
            Me.TestMethod()
            Me.TestForProcess()
        End If
    End Sub
    Private Sub TestMethod()
        MessageBox.Show("The strings in the flavorEnum are:")
        Dim i As String
        For Each i In [Enum].GetNames(GetType(FlavorEnum))
            MessageBox.Show(i)
        Next
    End Sub

    '列挙型を反復処理するには
    Private Sub TestForProcess()
        Dim items As Array
        items = System.Enum.GetValues(GetType(FirstDayOfWeek))
        Dim item As String
        For Each item In items
            MessageBox.Show(item)
        Next
    End Sub
End Class


'https://atmarkit.itmedia.co.jp/ait/articles/1712/20/news024.html
Class ConstTest
    Const NumberA As Integer = 1
    Private ReadOnly NumberB As Integer = 2
    Public ReadOnly Property NumberC As Integer = 3

    Sub New()
        If False Then
            Me.DummyMethod()
        End If
    End Sub
    Private Sub DummyMethod()
        Console.WriteLine(NumberA)
        Console.WriteLine(NumberB)
    End Sub
    '    重大度レベル	コード	説明	プロジェクト	ファイル	行	抑制状態
    'メッセージ	IDE0051	プライベート メンバー 'ConstTest.DummyMethod' は使用されていません。	TestForm1	C:\Users\OK\source\repos\Repository10_VBnet\TestForm1\TestForm1\ConstantsGeneralModule.vb	87	アクティブ

End Class