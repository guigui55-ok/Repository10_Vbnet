Imports System.IO

Public Class IniReaderWriter
    Private iniFilePath As String
    Public ReadOnly Property DataStore As IniDataStore

    Public Sub New()
        DataStore = New IniDataStore()
    End Sub

    Public Sub Init(filePath As String)
        iniFilePath = filePath
    End Sub

    Public Sub Load()
        If Not File.Exists(iniFilePath) Then Return

        Dim currentSection As IniDataStore.IniSection = Nothing
        Dim pendingComments As New List(Of String)

        For Each rawLine In File.ReadAllLines(iniFilePath)
            Dim line As String = rawLine.Trim()

            If String.IsNullOrWhiteSpace(line) Then
                pendingComments.Add("")
                Continue For
            End If

            If line.StartsWith(";") OrElse line.StartsWith("#") Then
                pendingComments.Add(rawLine)
                Continue For
            End If

            If line.StartsWith("[") AndAlso line.EndsWith("]") Then
                Dim sectionName As String = line.Substring(1, line.Length - 2).Trim()
                DataStore.AddSection(sectionName)
                currentSection = DataStore.GetAllSections()(sectionName)
                currentSection.SectionComment.AddRange(pendingComments)
                pendingComments.Clear()
            ElseIf line.Contains("=") Then
                If currentSection IsNot Nothing Then
                    Dim parts = line.Split(New Char() {"="c}, 2)
                    Dim key = parts(0).Trim()
                    Dim value = parts(1).Trim()
                    currentSection.KeyValues(key) = value
                    If pendingComments.Count > 0 Then
                        currentSection.KeyComments(key) = New List(Of String)(pendingComments)
                        pendingComments.Clear()
                    End If
                End If
            End If
        Next

        ' 最後に残ったコメントは、ファイル末尾の全体コメントとして保存
        If pendingComments.Count > 0 Then
            DataStore.CommentLines.AddRange(pendingComments)
        End If
    End Sub

    Public Sub Save()
        Using writer As New StreamWriter(iniFilePath, False)

            ' ファイル冒頭コメント
            For Each comment In DataStore.CommentLines
                writer.WriteLine(comment)
            Next

            For Each section In DataStore.GetAllSections().Values
                For Each commentLine In section.SectionComment
                    writer.WriteLine(commentLine)
                Next
                writer.WriteLine("[" & section.SectionName & "]")

                For Each kv In section.KeyValues
                    If section.KeyComments.ContainsKey(kv.Key) Then
                        For Each commentLine In section.KeyComments(kv.Key)
                            writer.WriteLine(commentLine)
                        Next
                    End If
                    writer.WriteLine(kv.Key & "=" & kv.Value)
                Next

                writer.WriteLine()
            Next
        End Using
    End Sub

    ' 基本的な操作メソッド（前回同様）
    Public Sub AddSection(section As String)
        DataStore.AddSection(section)
    End Sub

    Public Sub RemoveSection(section As String)
        DataStore.RemoveSection(section)
    End Sub

    Public Sub SetValue(section As String, key As String, value As String)
        DataStore.SetValue(section, key, value)
    End Sub

    Public Sub RemoveKey(section As String, key As String)
        DataStore.RemoveKey(section, key)
    End Sub

    Public Function GetValue(section As String, key As String) As String
        Return DataStore.GetValue(section, key)
    End Function
End Class
