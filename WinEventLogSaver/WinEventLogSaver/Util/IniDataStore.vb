Public Class IniDataStore
    Public Class IniSection
        Public Property SectionName As String
        Public Property SectionComment As List(Of String) = New List(Of String)
        Public Property KeyValues As New Dictionary(Of String, String)
        Public Property KeyComments As New Dictionary(Of String, List(Of String))
    End Class

    Private sections As New Dictionary(Of String, IniSection)
    Public Property CommentLines As New List(Of String) ' セクション外コメント（全体の先頭や末尾）

    Public Sub AddSection(section As String)
        If Not sections.ContainsKey(section) Then
            sections(section) = New IniSection() With {.SectionName = section}
        End If
    End Sub

    Public Sub RemoveSection(section As String)
        sections.Remove(section)
    End Sub

    Public Sub SetValue(section As String, key As String, value As String)
        AddSection(section)
        sections(section).KeyValues(key) = value
    End Sub

    Public Function GetValue(section As String, key As String) As String
        If sections.ContainsKey(section) AndAlso sections(section).KeyValues.ContainsKey(key) Then
            Return sections(section).KeyValues(key)
        End If
        Return Nothing
    End Function

    Public Sub RemoveKey(section As String, key As String)
        If sections.ContainsKey(section) Then
            sections(section).KeyValues.Remove(key)
            sections(section).KeyComments.Remove(key)
        End If
    End Sub

    Public Function GetAllSections() As Dictionary(Of String, IniSection)
        Return sections
    End Function
End Class