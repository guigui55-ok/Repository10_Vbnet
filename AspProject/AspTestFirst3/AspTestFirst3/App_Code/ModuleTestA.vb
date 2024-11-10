


Public Class Calc
    Implements ICalc

    Public Sub New()
        Console.WriteLine("Calc New")
    End Sub

    Public Function Add(i As Integer, j As Integer) As Integer Implements ICalc.Add
        Return i + j
    End Function

End Class




'#############################
'Namespace MyMath

'    Public Class Calc
'        Implements ICalc

'        Public Function Add(i As Integer, j As Integer) As Integer Implements ICalc.Add
'            Return i + j
'        End Function

'    End Class

'End Namespace

'#############################

'Module ModuleTestA

'    Public Class CalcModule
'        Implements ICalcModule

'        Public Function Add(i As Integer, j As Integer) As Integer Implements ICalcModule.Add
'            Return i + j
'        End Function

'    End Class

'End Module

' メモ ここでインターフェイス名 "IService" を変更する場合は、Web.config で "IService" への参照も更新する必要があります。
