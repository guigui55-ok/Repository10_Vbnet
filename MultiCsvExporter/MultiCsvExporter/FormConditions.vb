Public Class FormConditions

    Public _logger As AppLogger
    Public _filterCondition As FilterCondition

    Public Class Ui
        Public Shared ConTable As DataGridView
    End Class

    Public Class ConstColNo
        Public Const ID = 0
        Public Const ENABLE = 1
        Public Const COL_NAME = 2
        Public Const PATTERN = 3
        Public Const NOT_FLAG = 4
    End Class

    Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub

    Sub New(ByRef logger As AppLogger, ByRef filterCondition As FilterCondition)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        _logger = logger
        Ui.ConTable = Me.DataGridView_Condition
        Ui.ConTable.AllowUserToAddRows = False
        _filterCondition = filterCondition
        SetupDataGridViewColumns(Ui.ConTable)

        Ui.ConTable.Rows.Add()
    End Sub

    Private Sub FormCondition_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetDataFromConsitions(_filterCondition)
    End Sub

    Public Sub SetDataFromConsitions(condition As FilterCondition)
        _logger.Info("*SetDataFromConsitions")
        DataGridViewCreateRow(Ui.ConTable, condition.ColNames.Count)
        For i = 0 To condition.ColNames.Count - 1
            Ui.ConTable.Rows(i).Cells(ConstColNo.COL_NAME).Value = condition.ColNames(i)
            Ui.ConTable.Rows(i).Cells(ConstColNo.PATTERN).Value = condition.patterns(i)
            Ui.ConTable.Rows(i).Cells(ConstColNo.NOT_FLAG).Value = condition.NotFlags(i)
        Next
        Me.RadioButton_MatchAll.Checked = condition.matchAll
        SetNoNoDataGridView(Ui.ConTable, ConstColNo.ID)
    End Sub

    Public Sub SetDataToConditions()
        _logger.Info("*SetDataToConditions")

        Dim colNamesList = New List(Of String)
        Dim PatternList = New List(Of String)
        Dim NotFlagList = New List(Of Boolean)
        For Each row As DataGridViewRow In Ui.ConTable.Rows
            Dim id = row.Cells(ConstColNo.ID).Value
            Dim _enable = row.Cells(ConstColNo.ENABLE).Value
            Dim colName = row.Cells(ConstColNo.COL_NAME).Value
            Dim pattern = row.Cells(ConstColNo.PATTERN).Value
            Dim notFlag = row.Cells(ConstColNo.NOT_FLAG).Value

            colNamesList.Add(colName)
            PatternList.Add(pattern)
            NotFlagList.Add(notFlag)
        Next

        _filterCondition.ColNames = colNamesList
        _filterCondition.patterns = PatternList
        _filterCondition.NotFlags = NotFlagList
        _filterCondition.matchAll = RadioButton_MatchAll.Checked
        _logger.Info($"Data.Count = {_filterCondition.ColNames.Count}")
    End Sub

    ''' <summary>
    ''' DataGridView に指定されたインデックスの行を作成（存在しない場合は追加）します。
    ''' </summary>
    ''' <param name="dgv">対象の DataGridView</param>
    ''' <param name="maxIndex">確保したい行インデックス</param>
    Private Sub DataGridViewCreateRow(ByVal dgv As DataGridView, ByVal maxIndex As Integer)
        If dgv.AllowUserToAddRows Then
            ' ユーザー追加行がある場合、最終行は IsNewRow なので除外
            While dgv.Rows.Count - 1 <= maxIndex
                dgv.Rows.Add()
            End While
        Else
            ' 新規行なしの場合、すべて追加
            While dgv.Rows.Count <= maxIndex
                dgv.Rows.Add()
            End While
        End If
    End Sub

    Private Sub FormCondition_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        'AdjustWidthsToDataGridView(Ui.ConTable, 8, 40)
        AdjustWidthsToDataGridView(Ui.ConTable, 97, 120)
        '上記関数について、以下プロパティの設定とすること
        'Form.AutoSize = False
        'Form.MinimumSize = 0, 0
        'groupBox.Dock = DockStyle.None


        _logger.Info("### dgv")
        Dim _list = New List(Of String)
        For Each col As DataGridViewColumn In Ui.ConTable.Columns
            _list.Add($"{col.Name} : {col.Width}")
        Next
        _logger.Info($"col.Width = {String.Join(", ", _list)}")

    End Sub

    Private Sub FormCondition_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SetDataToConditions()
        _filterCondition.LogoutValues()
        e.Cancel = True
        Me.Visible = False
    End Sub

    ''' <summary>
    ''' 指定の DataGridView にカラムを追加して初期化します。
    ''' </summary>
    ''' <param name="dgv">対象の DataGridView</param>
    Public Sub SetupDataGridViewColumns(ByVal dgv As DataGridView)
        dgv.Columns.Clear()

        ' No（Int型）
        Dim colNo As New DataGridViewTextBoxColumn()
        colNo.Name = "No"
        colNo.HeaderText = "No"
        colNo.ValueType = GetType(Integer)
        colNo.Width = 40
        dgv.Columns.Add(colNo)

        ' Enable（チェックボックス）
        Dim colEnable As New DataGridViewCheckBoxColumn()
        colEnable.Name = "Enable"
        colEnable.HeaderText = "Enable"
        colEnable.ValueType = GetType(Boolean)
        colEnable.Width = 50
        dgv.Columns.Add(colEnable)

        ' カラム名（文字列）
        Dim colColumnName As New DataGridViewTextBoxColumn()
        colColumnName.Name = "ColumnName"
        colColumnName.HeaderText = "カラム名"
        colColumnName.ValueType = GetType(String)
        colColumnName.Width = 150
        dgv.Columns.Add(colColumnName)

        ' 条件（正規表現）（文字列）
        Dim colCondition As New DataGridViewTextBoxColumn()
        colCondition.Name = "Condition"
        colCondition.HeaderText = "条件（正規表現）"
        colCondition.ValueType = GetType(String)
        colCondition.Width = 200
        dgv.Columns.Add(colCondition)

        ' Not（チェックボックス）
        Dim colNot As New DataGridViewCheckBoxColumn()
        colNot.Name = "Not"
        colNot.HeaderText = "Not"
        colNot.ValueType = GetType(Boolean)
        colNot.Width = 40
        dgv.Columns.Add(colNot)
    End Sub
    ''' <summary>
    ''' DataGridViewのカラム幅合計に基づき、GroupBoxとFormの幅を調整します。
    ''' </summary>
    ''' <param name="dgv">対象のDataGridView</param>
    ''' <param name="extraMargin">余白として追加する幅（デフォルト: 40）</param>
    Public Sub AdjustWidthsToDataGridView(ByVal dgv As DataGridView, ByVal extraMargin As Integer, ByVal formMargin As Integer)
        If dgv Is Nothing OrElse dgv.Parent Is Nothing OrElse dgv.Parent.Parent Is Nothing Then Return

        ' 1. カラムの合計幅を取得
        Dim totalColumnWidth As Integer = 0
        For Each col As DataGridViewColumn In dgv.Columns
            totalColumnWidth += col.Width
        Next
        _logger.Info($"totalColumnWidth = {totalColumnWidth}")
        _logger.Info($"extraMargin = {extraMargin}")

        ' 2. GroupBoxの参照を取得
        Dim groupBox As Control = dgv.Parent
        Dim form As Form = TryCast(groupBox.Parent, Form)
        If form Is Nothing Then Return

        ' 3. GroupBoxとFormの幅を調整
        Dim desiredWidth As Integer = totalColumnWidth + extraMargin
        _logger.Info($"desiredWidth = {desiredWidth}")

        'groupBox.Width = desiredWidth
        'form.Width = desiredWidth + (form.Width - groupBox.Width) ' フォームの枠やパディングも考慮
        Dim dStyle = groupBox.Dock
        groupBox.Dock = DockStyle.None '一時的に
        form.Width = totalColumnWidth + extraMargin
        '_logger.Info($"formMargin = {formMargin}")
        _logger.Info($"groupBox.Width = {groupBox.Width}")
        _logger.Info($"form.Width = {form.Width}")

        groupBox.Dock = dStyle

        ' 必要に応じて高さも調整可能（オプション）
    End Sub

    Private Sub SetNoNoDataGridView(dgv As DataGridView, colIndex As Integer, Optional startNum As Integer = 1)
        Dim count = 0
        For Each row As DataGridViewRow In dgv.Rows
            row.Cells(colIndex).Value = (startNum + count).ToString
            count += 1
        Next
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button_AddRow.Click
        Ui.ConTable.Rows.Add()

        Dim lastRow = Ui.ConTable.Rows(Ui.ConTable.Rows.Count - 1)
        lastRow.Cells()(0).Value = lastRow.Index.ToString
        SetNoNoDataGridView(Ui.ConTable, ConstColNo.ID)
    End Sub

    Private Sub Button_RemoveRow_Click(sender As Object, e As EventArgs) Handles Button_RemoveRow.Click
        Try
            'Dim rows = Ui.ConTable.SelectedRows
            'Dim _list = New List(Of String)
            'For Each row As DataGridViewRow In rows
            '    _list.Add(row.Index)
            'Next
            '_logger.Info($"selectedRows = {String.Join(", ", _list)}")
            'If rows.Count < 1 Then Exit Sub
            Dim rowIndex As Integer = Ui.ConTable.CurrentCell.RowIndex

            Ui.ConTable.Rows.RemoveAt(rowIndex)
            _logger.Info($"removeRow = {rowIndex}")
        Catch ex As InvalidOperationException
            '型 'System.InvalidOperationException' のハンドルされていない例外が MultiCsvExporter.exe で発生しました
            'コミットされていない新しい行を削除することはできません。
            If ex.Message.Contains("コミットされていない新しい行を削除することはできません") Then
                _logger.Info(ex.GetType.ToString + ":" + ex.Message)
            Else
                Throw ex
            End If
        End Try
        SetNoNoDataGridView(Ui.ConTable, ConstColNo.ID)
    End Sub
    Private Sub SafeRemoveRow(ByVal dgv As DataGridView, ByVal index As Integer)
        If index >= 0 AndAlso index < dgv.Rows.Count Then
            If Not dgv.Rows(index).IsNewRow Then
                dgv.Rows.RemoveAt(index)
                _logger.Info($"removeRow = {index}")
            End If
        End If
    End Sub
End Class