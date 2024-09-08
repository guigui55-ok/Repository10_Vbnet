

Imports StateController

Public Class Form1
    Public m_FormStateController As FormStateContoller
    Public m_AppStatus As AppStatus
    Public m_StateList As List(Of AppStatus)
    Public m_isLoadingForm1 As Boolean
    Public m_MessageControlList As List(Of Control)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.m_isLoadingForm1 = True
        Me.m_StateList = New List(Of AppStatus)()
        Me.m_AppStatus = New AppStatus()
        Me.m_FormStateController = New FormStateContoller()
        '/
        Me.Initialize_UserControls()
        '/
        Dim stateAmount As Integer = 2
        Dim bufState As AppStatus
        For Each mode_num As Integer In [Enum].GetValues(GetType(ExecuteMode))
            bufState = New AppStatus()
            ' ChangeStatusEvent に対するハンドラーを登録
            AddHandler bufState.ChangeStatusEvent, AddressOf Me.ChangeStatusEvent
            bufState.ChangeMode(mode_num)
            Me.m_StateList.Add(bufState)
        Next
        m_FormStateController._mainStatus = Me.m_StateList(0)
        '/
        Me.m_FormStateController.Show()
        Me.m_FormStateController.Activate()
        Dim x As Integer = Me.m_FormStateController.Location.X + Me.m_FormStateController.Width
        Dim y As Integer = Me.m_FormStateController.Location.Y
        x = Me.m_FormStateController.Location.X
        y = Me.m_FormStateController.Location.Y + Me.m_FormStateController.Height
        Me.Location = New Point(x, y)
        Me.m_isLoadingForm1 = False
        Me.GetMainStatus().ChangeStatus(StateFlagsModeA.A_INIT)
    End Sub

    Private Function GetMainStatus() As AppStatus
        Return Me.m_FormStateController._mainStatus

    End Function

    WithEvents MessageA1 As MessageA
    WithEvents MessageB1 As MessageB
    WithEvents MessageNone1 As MessageNone
    Private Sub Initialize_UserControls()
        m_MessageControlList = New List(Of Control)
        Me.m_MessageControlList.Add(Me.MessageBase1)

        Me.MessageA1 = New ChangeFormTest.MessageA()
        Me.MessageA1.Location = New System.Drawing.Point(16, 12)
        Me.MessageA1.Name = "MessageA1"
        Me.MessageA1.Size = New System.Drawing.Size(450, 120)
        Me.MessageA1.TabIndex = 0
        Me.Controls.Add(Me.MessageA1)
        Me.m_MessageControlList.Add(MessageA1)
        uConSubInit(MessageA1)

        Me.MessageB1 = New ChangeFormTest.MessageB()
        Me.MessageB1.Location = New System.Drawing.Point(16, 12)
        Me.MessageB1.Name = "MessageB1"
        Me.MessageB1.Size = New System.Drawing.Size(450, 120)
        Me.MessageB1.TabIndex = 0
        Me.Controls.Add(Me.MessageB1)
        Me.m_MessageControlList.Add(MessageB1)
        uConSubInit(MessageB1)

        Me.MessageNone1 = New ChangeFormTest.MessageNone()
        Me.MessageNone1.Location = New System.Drawing.Point(16, 12)
        Me.MessageNone1.Name = "MessageNone1"
        Me.MessageNone1.Size = New System.Drawing.Size(450, 120)
        Me.MessageNone1.TabIndex = 0
        Me.Controls.Add(Me.MessageNone1)
        Me.m_MessageControlList.Add(MessageNone1)
        uConSubInit(MessageNone1)

        Me.MessageA1.attr.Initialize_Values(ExecuteMode.MODE_A, StateFlagsModeA.A_INIT)
        Me.MessageB1.attr.Initialize_Values(ExecuteMode.MODE_A, StateFlagsModeA.A_PREPARE)
    End Sub

    Private Sub uConSubInit(uCon As UserControl)
        VisibleFalseUserControlAll()
        uCon.Show()
        uCon.Visible = True
        uCon.Visible = False
    End Sub

    Private Sub VisibleFalseUserControlAll()
        For Each uCon As Object In Me.m_MessageControlList
            uCon.Visible = False
        Next
    End Sub

    Private Sub ChangeUserControl()
        Dim pAppState As AppStatus = Me.m_FormStateController._mainStatus
        DebugPrint(String.Format(
                   "ChangeUserControl mode,state = [{0}, {1}]",
                   pAppState._exeMode, pAppState._nowState))
        If Me.m_isLoadingForm1 Then
            Exit Sub
        End If
        Dim nowState As Integer = pAppState._nowState
        Dim isMatch As Boolean = False
        For Each uCon As Object In Me.m_MessageControlList
            Dim attr As MessageAttribute = uCon.attr
            If attr.IsMatchAppState(pAppState) Then
                DebugPrint(String.Format("show : {0}", uCon.Name))
                ''DebugPrint(String.Format("parent : {0}", uCon.Parent.Name)) ' Error
                'DebugPrint(String.Format("Location : {0}", uCon.Location))
                'DebugPrint(String.Format("Size : {0}", uCon.Size))
                'DebugPrint(String.Format("Visible : {0}", uCon.Visible))
                'uCon.BringToFront() '1
                uCon.Visible = True
                isMatch = True
            Else
                uCon.Visible = False
            End If
        Next
        If Not isMatch Then
            Me.MessageNone1.Show()
            Me.MessageNone1.Visible = True
        End If
    End Sub

    Private Sub ChangeStatusEvent()
        Me.ChangeUserControl()
        Select Case Me.m_AppStatus._nowState
            Case StateFlagsModeA.A_INIT

            Case Else
                DebugPrint("ChangeStatusEvent m_AppStatus - Case Else")
        End Select
    End Sub

    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If Me.m_isLoadingForm1 Then
            If Me.m_FormStateController Is Nothing Then
                Exit Sub
            End If
            Me.m_FormStateController.Activate()
        End If
    End Sub
End Class
