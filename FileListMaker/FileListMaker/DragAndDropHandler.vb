Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Public Class DragAndDropHandler
    Private WithEvents _control As Control

    Public Event FileDropped(filePath As String)

    Public Sub New(targetControl As Control)
        _control = targetControl
    End Sub

    Public Sub Initialize()
        _control.AllowDrop = True
        AddHandler _control.DragEnter, AddressOf Control_DragEnter
        AddHandler _control.DragDrop, AddressOf Control_DragDrop
    End Sub

    Private Sub Control_DragEnter(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub Control_DragDrop(sender As Object, e As DragEventArgs)
        Dim files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
        If files.Length > 0 Then
            RaiseEvent FileDropped(files(0))
        End If
    End Sub
End Class

