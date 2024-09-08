Imports StateController
Public Class MessageA
    'Inherits MessageAbstruct
    Public attr As MessageAttribute = New MessageAttribute(Me.GetType())
    Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()
        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.attr.m_ControlType = GetType(MessageA)
        Me.attr.m_ControlType = Me.GetType()
    End Sub

    Private Sub MessageA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.attr.Initialize_Values(ExecuteMode.MODE_A, StateFlagsModeA.A_INIT)
        DebugPrint("MessageA_Load")
    End Sub
End Class
