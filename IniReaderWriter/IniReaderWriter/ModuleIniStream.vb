Imports System.Runtime.InteropServices
Imports System.Text

Public Class IniStream
    Private _path As String

    ' Iniファイルのパスを設定
    Public Sub New(path As String)
        If String.IsNullOrEmpty(path) Then
            Throw New ArgumentException("パスは有効な値を指定してください。")
        End If
        _path = path
    End Sub

    ' INIファイルから値を読み込む (特定のセクションとキー)
    Public Function ReadValue(section As String, key As String, Optional defaultValue As String = "") As String
        Dim sb As New StringBuilder(255)
        Dim charsRead As Integer = GetPrivateProfileString(section, key, defaultValue, sb, sb.Capacity, _path)
        Return sb.ToString()
    End Function

    ' INIファイルに値を書き込む (特定のセクションとキー)
    Public Sub WriteValue(section As String, key As String, value As String)
        If Not WritePrivateProfileString(section, key, value, _path) Then
            Throw New InvalidOperationException("INIファイルへの書き込みに失敗しました。")
        End If
    End Sub

    ' INIファイルからセクション全体を削除
    Public Sub DeleteSection(section As String)
        If Not WritePrivateProfileString(section, Nothing, Nothing, _path) Then
            Throw New InvalidOperationException("セクションの削除に失敗しました。")
        End If
    End Sub

    ' INIファイルから特定のキーを削除
    Public Sub DeleteKey(section As String, key As String)
        If Not WritePrivateProfileString(section, key, Nothing, _path) Then
            Throw New InvalidOperationException("キーの削除に失敗しました。")
        End If
    End Sub

    ' P/InvokeでGetPrivateProfileStringを呼び出す
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function GetPrivateProfileString(
        lpAppName As String,
        lpKeyName As String,
        lpDefault As String,
        lpReturnedString As StringBuilder,
        nSize As Integer,
        lpFileName As String) As Integer
    End Function

    ' P/InvokeでWritePrivateProfileStringを呼び出す
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function WritePrivateProfileString(
        lpAppName As String,
        lpKeyName As String,
        lpString As String,
        lpFileName As String) As Boolean
    End Function
End Class
