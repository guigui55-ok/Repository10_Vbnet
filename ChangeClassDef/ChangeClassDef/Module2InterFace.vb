Module Module2InterFace


    Public Class IConstTest
        Inherits ConstTestA
        'Inheris ConstTestB
    End Class

    Public Class ConstTestA
        Public Const CONST_VALUE_A = 1
        Public Const CONST_VALUE_B = 2
    End Class

    Public Class ConstTestB
        Public Const CONST_VALUE_A = 1
        Public Const CONST_VALUE_B = 2
    End Class

End Module
