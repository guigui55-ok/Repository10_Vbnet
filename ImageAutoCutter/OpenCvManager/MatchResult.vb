Imports Emgu.CV
Imports Emgu.CV.Structure
Imports Emgu.CV.CvEnum
Imports System.Drawing

''' <summary>
''' 画像マッチングの結果を格納するクラス
''' </summary>
Public Class MatchResult
    Public Property IsMatch As Boolean
    Public Property MatchLocation As Point
    Public Property MatchValue As Double
End Class