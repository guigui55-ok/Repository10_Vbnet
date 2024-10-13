Imports System.Threading

Public Class FormDataGridViewAuto

    Public m_nowProcessing As Boolean = False
    Public m_taskA As Task
    Public m_taskB As Task
    ' クラスレベルでCancellationTokenSourceを宣言
    Private cts As CancellationTokenSource

    Private Sub FormDataGridViewAuto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeDataGridView(DataGridView1)
    End Sub

    'エラー	BC37267	定義済みの型 'ValueTuple(Of ,)' は、定義、またはインポートされていません。
    '細心の検索とインストール（参照追加、System.ValueTuple）

    Public COL_NAME_CH As String = "CH"
    Public DataDict As Dictionary(Of String, Object)

    Public m_recieveValue As String

    Private Sub InitializeDataGridView(dataGridView As DataGridView)

        ' 列名と型のリストを作成
        'このリストによって、Columnの作成を自動化する
        Dim columnValues = New List(Of (Name As String, Type As String)) From {
                ("No", "Text"),
                (COL_NAME_CH, "CheckBox"),
                ("Value1", "Text"),
                ("Value2", "Text"),
                ("Value3", "Text")
            }

        ' 列の追加をループで処理
        For Each col In columnValues
            If Not dataGridView.Columns.Contains(col.Name) Then
                Select Case col.Type
                    Case "Text"
                        dataGridView.Columns.Add(col.Name, col.Name) ' テキスト列の追加
                    Case "CheckBox"
                        Dim checkBoxColumn As New DataGridViewCheckBoxColumn With {
                                .Name = col.Name,
                                .HeaderText = col.Name
                            }
                        dataGridView.Columns.Add(checkBoxColumn) ' チェックボックス列の追加
                End Select
            End If
        Next

        Dim colWidthSum As Integer = 0
        '幅調整
        For Each col As DataGridViewColumn In dataGridView.Columns
            If (col.Name.Contains("No")) Then
                col.Width = 30
            End If
            If (col.Name.Contains(COL_NAME_CH)) Then
                col.Width = 30
            End If
            If (col.Name.Contains("Value")) Then
                col.Width = 80
            End If
            colWidthSum += col.Width
        Next
        Console.WriteLine($"colWidthSum = {colWidthSum}")


        ' DataGridViewに行データを追加
        'Dim rand As New Random()
        For i As Integer = 1 To 5
            'Dim value1 As Double = Math.Round(0.1 + (rand.NextDouble() * (0.3 - 0.1)), 3)
            'Dim value2 As Double = Math.Round(0.1 + (rand.NextDouble() * (0.3 - 0.1)), 3)
            'Dim value3 As Double = Math.Round(0.1 + (rand.NextDouble() * (0.3 - 0.1)), 3)
            ' 行を追加
            dataGridView.Rows.Add(i, True, "", "", "")
        Next

        'DataGridViewReadOnly
        dataGridView.AllowUserToAddRows = False
        dataGridView.AllowUserToDeleteRows = False

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dataGridView As DataGridView = DataGridView1
        Dim checkBoxColumn As DataGridViewCheckBoxColumn = dataGridView.Columns(COL_NAME_CH)
        Dim aaa As DataGridViewColumn

        Dim retDict As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
        Dim bufKey As String
        Dim bufValue As String
        For i As Integer = 0 To dataGridView.Rows.Count - 1
            Dim row As DataGridViewRow = dataGridView.Rows(i)
            bufKey = row.Cells("No").Value
            Dim bufFlag As Boolean = row.Cells(COL_NAME_CH).Value
            bufValue = bufFlag
            If bufFlag Then
                retDict(bufKey) = bufValue
            End If
        Next

        Dim buf As String = ""
        For Each key In retDict.Keys
            buf += String.Format("{0}:{1}, ", key, retDict(key))
        Next
        Console.WriteLine("dict = " + buf)
    End Sub

    Private Function GetCheckedRowsList() As List(Of Integer)
        Dim bufDtaGridView As DataGridView = DataGridView1
        Dim retDict As Dictionary(Of String, Object) = New Dictionary(Of String, Object)
        Dim bufKey As String
        Dim bufValue As String
        For i As Integer = 0 To bufDtaGridView.Rows.Count - 1
            Dim row As DataGridViewRow = bufDtaGridView.Rows(i)
            bufKey = row.Cells("No").Value
            Dim bufFlag As Boolean = row.Cells(COL_NAME_CH).Value
            bufValue = bufFlag
            If bufFlag Then
                retDict(bufKey) = bufValue
            End If
        Next

        'Dim buf As String = ""
        Dim retList As List(Of Integer) = New List(Of Integer)
        For Each key In retDict.Keys
            Dim bufInt As Integer = Integer.Parse(key)
            retList.Add(bufInt)
        Next
        'Console.WriteLine("dict = " + buf)
        Return retList
    End Function

    'Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    '    Dim dataGridView As DataGridView = DataGridView1
    '    Dim maxRow = dataGridView.Rows.Count
    '    Dim maxCol = dataGridView.Columns.Count - 2

    '    Dim checkedNoList As List(Of Integer) = GetCheckedRowsList()

    '    Dim task As Task = task.Run(
    '        Sub()
    '            処理
    '            Console.WriteLine("Button2_Click TaskRun")
    '            For Each checkedNo As Integer In checkedNoList
    'For rowNum As Integer = 1 To maxRow
    '                Dim rowNum As Integer = checkedNo
    '                For colNum As Integer = 1 To maxCol
    '                    Console.WriteLine(String.Format("row={0}, col={1}", rowNum, colNum))
    '                    ModuleGetDataSampleAsync.GetDataSampleAsync(DataGridView1, Me)
    '                    If (m_recieveValue <> "") Then
    '                        ModuleGetDataSampleAsync.UpdateValueInTable(DataGridView1, Me, rowNum, colNum)
    '                    End If
    '                Next
    '            Next
    '        End Sub
    '    )
    '    //
    '    GetDataSampleAsync(DataGridView1, Me)
    '    ModuleGetDataSampleAsync.UpdateValueInTable(DataGridView1, Me, 1, 2)
    'End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dataGridView As DataGridView = DataGridView1
        Dim maxRow = dataGridView.Rows.Count
        Dim maxCol = dataGridView.Columns.Count - 2

        Dim checkedNoList As List(Of Integer) = GetCheckedRowsList()
        cts = New CancellationTokenSource()
        Dim token = cts.Token

        Me.Button2.Enabled = False
        m_nowProcessing = True
        m_taskA = Task.Run(
            Function()
                Try
                    '処理
                    'Me.Button2.Enabled = False 'System.InvalidOperationException:有効ではないスレッド間の操作: コントロールが作成されたスレッド以外のスレッドからコントロール 'Button2' がアクセスされました。
                    Console.WriteLine("Button2_Click TaskA Run")
                    For Each checkedNo As Integer In checkedNoList
                        ' タスクのキャンセルがリクエストされているかチェック
                        If token.IsCancellationRequested Then
                            Console.WriteLine("TaskA 中止されました")
                            Return False
                        End If
                        Dim rowNum As Integer = checkedNo
                        For colNum As Integer = 1 To maxCol
                            Console.WriteLine(String.Format("row={0}, col={1}", rowNum, colNum))
                            ModuleGetDataSampleAsync.GetDataSampleAsync(DataGridView1, Me)
                            If (m_recieveValue <> "") Then
                                ModuleGetDataSampleAsync.UpdateValueInTable(DataGridView1, Me, rowNum, colNum)
                            End If
                        Next
                    Next
                    Return True
                Catch ex As Exception
                    Console.WriteLine(" ----- ")
                    Console.WriteLine(ex.GetType().ToString() + ":" + ex.Message)
                    Console.WriteLine(ex.StackTrace)
                    Console.WriteLine(" ----- ")
                    Throw ex
                    Return False
                Finally
                    'Me.Button2.Enabled = True
                    ' 処理終了後に Button2 を有効化する処理を UI スレッドで行う
                    Me.Invoke(Sub() Me.Button2.Enabled = True)
                End Try
            End Function
        )

        'm_taskB = Task.Run(
        '    Function()
        '        Try
        '            Console.WriteLine("TaskB Run")
        '            '処理
        '            While True
        '                ' タスクのキャンセルがリクエストされているかチェック
        '                If token.IsCancellationRequested Then
        '                    Console.WriteLine("TaskB 中止されました")
        '                    Return False
        '                End If
        '                If m_taskA.IsCompleted Then
        '                    Button2.Enabled = True
        '                    Exit While
        '                End If
        '            End While
        '            Return True
        '        Catch ex As Exception
        '            Console.WriteLine(" ----- ")
        '            Console.WriteLine(ex.GetType().ToString() + ":" + ex.Message)
        '            Console.WriteLine(ex.StackTrace)
        '            Console.WriteLine(" ----- ")
        '            Throw ex
        '            Return False
        '        Finally
        '            'Me.Button2.Enabled = True
        '            ' ここでも同様にUIスレッドで操作する必要がある
        '            Me.Invoke(Sub() Me.Button2.Enabled = True)
        '        End Try
        '    End Function
        ')

        '//
        'GetDataSampleAsync(DataGridView1, Me)
        'ModuleGetDataSampleAsync.UpdateValueInTable(DataGridView1, Me, 1, 2)
    End Sub


    Private Function GetDataDict(argDict As Dictionary(Of String, Object), length As Integer)
        'チェックボックスONのみのDict
        Dim retDict As Dictionary(Of String, Object) = argDict
        'Dictに値を入れていく
        For Each key In retDict.Keys
            Dim valueList As List(Of String) = New List(Of String)
            For i = 0 To length - 1
                Dim bufVal = GetRandomDoubleStr()
                valueList.Add(bufVal)
            Next
            retDict(key) = valueList
        Next
        Return retDict
    End Function

    Private Function GetRandomDouble()
        Dim rand As Random = New Random()
        Dim valInt = rand.Next(0, 7000)
        Return valInt / 1000
    End Function

    Public Function GetRandomDoubleStr() As String
        Dim ValDbl = GetRandomDouble()
        'Console.WriteLine(ValDbl.GetType().ToString())
        'Return ValDbl.ToString("F3").ToString()
        'Return ValDbl.ToString("G4").ToString()
        Return ValDbl.ToString()
    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim bufDataGridView As DataGridView = DataGridView1
        For Each row As DataGridViewRow In bufDataGridView.Rows
            For Each col As DataGridViewColumn In bufDataGridView.Columns
                If col.HeaderText.Contains("Value") Then
                    row.Cells(col.HeaderText).Value = ""
                End If
            Next
        Next
    End Sub

    Private Sub ButtonAbort_Click(sender As Object, e As EventArgs) Handles ButtonAbort.Click
        ' タスクの中止をリクエスト
        If cts IsNot Nothing Then
            cts.Cancel() ' タスクを中止するシグナルを送る
            Console.WriteLine("中止要求が送信されました")
        End If
    End Sub
End Class
