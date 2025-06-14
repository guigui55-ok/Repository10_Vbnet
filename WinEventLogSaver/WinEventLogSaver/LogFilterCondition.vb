Public Class EventLogFilterCondition
    Public Property StartTime As DateTime?
    Public Property EndTime As DateTime?

    Public Property EventIdContains As String = ""
    Public Property LevelContains As String = ""
    Public Property SourceContains As String = ""
    Public Property UserContains As String = ""
    Public Property MachineContains As String = ""
    Public Property MessageContains As String = ""

    ' 指定された EventRecord が条件を満たすかどうかを判定
    Public Function IsMatch(record As System.Diagnostics.Eventing.Reader.EventRecord) As Boolean
        If Me.StartTime.HasValue AndAlso record.TimeCreated < Me.StartTime Then Return False
        If Me.EndTime.HasValue AndAlso record.TimeCreated > Me.EndTime Then Return False

        If Not String.IsNullOrEmpty(EventIdContains) AndAlso Not record.Id.ToString().Contains(EventIdContains) Then Return False
        If Not String.IsNullOrEmpty(LevelContains) AndAlso Not record.LevelDisplayName?.ToString().Contains(LevelContains) Then Return False
        If Not String.IsNullOrEmpty(SourceContains) AndAlso Not record.ProviderName?.ToString().Contains(SourceContains) Then Return False
        If Not String.IsNullOrEmpty(UserContains) AndAlso Not record.UserId?.ToString().Contains(UserContains) Then Return False
        If Not String.IsNullOrEmpty(MachineContains) AndAlso Not record.MachineName?.Contains(MachineContains) Then Return False
        If Not String.IsNullOrEmpty(MessageContains) AndAlso Not record.FormatDescription()?.Contains(MessageContains) Then Return False

        Return True
    End Function
End Class