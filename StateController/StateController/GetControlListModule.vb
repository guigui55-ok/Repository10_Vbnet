Module GetControlListModule
    Public Class ControlInfo
        Public Property Name As String
        Public Property Location As Point
        Public Property Size As Size
        Public Property Text As String
        Public Property FontName As String
        Public Property FontSize As Single
        Public Property FontStyle As FontStyle
        Public Property IsBold As Boolean

        Public Sub New(ctrl As Control)
            Name = ctrl.Name
            Location = ctrl.Location
            Size = ctrl.Size
            Text = ctrl.Text
            FontName = ctrl.Font.Name
            FontSize = ctrl.Font.Size
            FontStyle = ctrl.Font.Style
            IsBold = ctrl.Font.Bold
        End Sub

        ' Dictionary型に変換するメソッド
        Public Function ToDictionary() As Dictionary(Of String, Object)
            Dim dict As New Dictionary(Of String, Object) From {
                {"Name", Name},
                {"Location", Location},
                {"Size", Size},
                {"Text", Text},
                {"FontName", FontName},
                {"FontSize", FontSize},
                {"FontStyle", FontStyle.ToString()},
                {"IsBold", IsBold}
            }
            Return dict
        End Function
    End Class

    Public Class ControlManager
        Private ReadOnly _controls As New List(Of ControlInfo)
        Public _ReadFilePath As String = ""

        Public Sub New(form As Form)
            RetrieveControls(form)
        End Sub

        ' フォーム上のすべてのコントロールを再帰的に取得
        Private Sub RetrieveControls(ctrl As Control)
            For Each c As Control In ctrl.Controls
                _controls.Add(New ControlInfo(c))
                If c.HasChildren Then
                    RetrieveControls(c)
                End If
            Next
        End Sub

        ' 取得したコントロール情報をテキストファイルに出力
        Public Sub ExportToTextFile(filePath As String)
            Me._ReadFilePath = filePath
            Using writer As New System.IO.StreamWriter(filePath)
                For Each ctrlInfo As ControlInfo In _controls
                    writer.WriteLine("Control Name: " & ctrlInfo.Name)
                    writer.WriteLine("Location: " & ctrlInfo.Location.ToString())
                    writer.WriteLine("Size: " & ctrlInfo.Size.ToString())
                    writer.WriteLine("Text: " & ctrlInfo.Text)
                    writer.WriteLine("Font Name: " & ctrlInfo.FontName)
                    writer.WriteLine("Font Size: " & ctrlInfo.FontSize)
                    writer.WriteLine("Font Style: " & ctrlInfo.FontStyle.ToString())
                    writer.WriteLine("Is Bold: " & ctrlInfo.IsBold)
                    writer.WriteLine(New String("-"c, 40))
                Next
            End Using
        End Sub
    End Class


End Module
