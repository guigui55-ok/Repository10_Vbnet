Imports StateController
Public Class MessageAttribute
    Public m_ControlType As Type = Nothing
    Public m_AppStateMode As Integer
    Public m_AppStateFlag As Integer

    Public Sub New(Optional _Type As Type = Nothing)
        Me.m_ControlType = _Type
        Me.Initialize_Values(-1, -1) 'Defalt Value
    End Sub

    Public Sub Initialize_Values(mode As Integer, flag As Integer)
        Debug.WriteLine(String.Format("Initialize_Values :{0}", Me.GetType().Name))
        Me.m_AppStateMode = mode
        Me.m_AppStateFlag = flag
    End Sub

    Public Function IsMatchAppState(state As AppStatus)
        Dim buf As String = Me.m_ControlType.Name
        If state._nowState = Me.m_AppStateFlag _
            And state._exeMode = Me.m_AppStateMode Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
