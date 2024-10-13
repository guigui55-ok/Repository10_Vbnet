Imports System.Text.RegularExpressions

Public Module ModuleFilterIgnore
    Public Class FilterIgnore
        Public _filterPatternList As List(Of String) = New List(Of String)
        Public Sub New()

        End Sub

        Public Sub SetFilterList(filterPatternList As List(Of String))
            _filterPatternList = filterPatternList
        End Sub

        Public Function FilterdValueListByMatchPattern(valueList As List(Of String)) As List(Of String)
            Dim retList As List(Of String) = FilterdValueListByMatchPattern(_filterPatternList, valueList)
            Return retList
        End Function

        Public Function FilterdValueListByMatchPattern(patternList As List(Of String), valueList As List(Of String)) As List(Of String)
            If patternList Is Nothing Then
                Return valueList
            End If
            If valueList Is Nothing Then
                Return valueList
            End If
            If patternList.Count < 1 Then
                Return valueList
            End If
            Dim retList As List(Of String) = New List(Of String)
            For Each value As String In valueList
                If Not IsMatchPatternList(patternList, value) Then
                    retList.Add(value)
                End If
            Next
            Return retList
        End Function
        Public Function IsMatchPatternList(patternList As List(Of String), value As String) As Boolean
            For Each pattern As String In patternList
                If IsMatchPattern(pattern, value) Then
                    Return True
                End If
            Next
            Return False
        End Function
        Public Function IsMatchPattern(pattern As String, value As String) As Boolean
            'System.ArgumentException: '解析中 "*.*bin" - 量指定子 {x,y} の前に何もありません。'
            Try
                Dim result As Boolean = Regex.IsMatch(value, pattern)
                Return result
            Catch ex As Exception
                Console.WriteLine("IsMatchPattern Error")
                Console.WriteLine(ex.GetType().ToString() + ":" + ex.Message)
                Console.WriteLine(ex.StackTrace)
            End Try
        End Function


    End Class
End Module
