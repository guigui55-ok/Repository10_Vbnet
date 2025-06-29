Imports System.Text.RegularExpressions

Public Class ExcelAddressUtil
    Public Structure CellAddress
        Public Col As Integer
        Public Row As Integer
        Public Sub New(col As Integer, row As Integer)
            Me.Col = col
            Me.Row = row
        End Sub
    End Structure


    ''' <summary>
    ''' A1形式または範囲形式（例: "A1", "A1:B2"）から行数を取得
    ''' </summary>
    Public Shared Function GetRowCount(address1 As String, Optional address2 As String = Nothing) As Integer
        Dim row1 As Integer
        Dim row2 As Integer

        ' "A1:B2" のような範囲が1つの引数で渡された場合に対応
        If address2 Is Nothing AndAlso address1.Contains(":") Then
            Dim parts = address1.Split(":"c)
            row1 = ParseRow(parts(0))
            row2 = ParseRow(parts(1))
        Else
            row1 = ParseRow(address1)
            row2 = ParseRow(If(address2, address1))
        End If

        Dim minRow = Math.Min(row1, row2)
        Dim maxRow = Math.Max(row1, row2)
        Return maxRow - minRow + 1
    End Function


    ''' <summary>
    ''' A1形式または範囲形式（例: "A1", "A1:B2"）から列数を取得します
    ''' </summary>
    Public Shared Function GetColCount(address1 As String, Optional address2 As String = Nothing) As Integer
        Dim col1 As Integer
        Dim col2 As Integer

        ' 1つの引数が範囲形式（例: "A1:B2"）のとき
        If address2 Is Nothing AndAlso address1.Contains(":") Then
            Dim parts = address1.Split(":"c)
            col1 = ParseCol(parts(0))
            col2 = ParseCol(parts(1))
        Else
            col1 = ParseCol(address1)
            col2 = ParseCol(If(address2, address1))
        End If

        Dim minCol = Math.Min(col1, col2)
        Dim maxCol = Math.Max(col1, col2)
        Return maxCol - minCol + 1
    End Function

    ''' <summary>
    ''' A1形式から行番号を取得（例: "B2" → 2）
    ''' </summary>
    Private Shared Function ParseRow(address As String) As Integer
        Dim match = Regex.Match(address.ToUpper().Trim(), "^[A-Z]+(\d+)$")
        If match.Success Then
            Return Integer.Parse(match.Groups(1).Value)
        End If

        ' 範囲（例: "A1:B2"）が直接来た場合にも対応
        If address.Contains(":") Then
            Dim parts = address.Split(":"c)
            Return ParseRow(parts(0)) ' 始点を返す（必要に応じて変更可能）
        End If

        Throw New ArgumentException("Invalid address: " & address)
    End Function

    ''' <summary>
    ''' A1形式から列番号を取得（例: "B2" → 2）
    ''' </summary>
    Private Shared Function ParseCol(address As String) As Integer
        address = address.ToUpper().Trim()

        ' 通常のA1形式: A1, AZ100, など
        Dim match = Regex.Match(address, "^([A-Z]+)\d+$")
        If match.Success Then
            Return ColumnLetterToNumber(match.Groups(1).Value)
        End If

        ' 範囲（例: "A1:B2"）が直接来た場合にも対応
        If address.Contains(":") Then
            Dim parts = address.Split(":"c)
            Return ParseCol(parts(0)) ' 始点列を使う（必要に応じて変更可能）
        End If

        ' 列名のみ（例: "A", "AZ"）
        Dim match2 = Regex.Match(address, "^([A-Z]+)$")
        If match2.Success Then
            Return ColumnLetterToNumber(match2.Groups(1).Value)
        End If

        Throw New ArgumentException("Invalid address: " & address)
    End Function

    '' A → 1, B → 2, ..., Z → 26, AA → 27, AB → 28 ...
    'Private Shared Function ColumnLetterToNumber(colStr As String) As Integer
    '    Dim col = 0
    '    For i = 0 To colStr.Length - 1
    '        col = col * 26 + (Asc(colStr(i)) - Asc("A"c) + 1)
    '    Next
    '    Return col
    'End Function

    ''' <summary>
    ''' 複数のセル範囲をまとめて最小の矩形アドレスに変換します。
    ''' </summary>
    ''' <param name="addresses">任意個のA1形式アドレス（例: "A1", "B2:C3", "A1:B2:C3"）</param>
    ''' <returns>最小の矩形アドレス（例: "A1:C3"）</returns>
    Public Shared Function GetUnionRangeAddress(ParamArray addresses As String()) As String
        Dim minRow As Integer = Integer.MaxValue
        Dim maxRow As Integer = Integer.MinValue
        Dim minCol As Integer = Integer.MaxValue
        Dim maxCol As Integer = Integer.MinValue

        For Each addr In addresses
            ' アドレスを分割（複数の":"を含んでもOK）
            Dim parts = addr.Split(":"c)

            For Each part In parts
                If String.IsNullOrWhiteSpace(part) Then Continue For

                Dim parsed = ParseAddress(part.Trim())
                minRow = Math.Min(minRow, parsed.Row)
                maxRow = Math.Max(maxRow, parsed.Row)
                minCol = Math.Min(minCol, parsed.Col)
                maxCol = Math.Max(maxCol, parsed.Col)
            Next
        Next

        Dim startAddress = MakeAddress(minCol, minRow)
        Dim endAddress = MakeAddress(maxCol, maxRow)

        Return $"{startAddress}:{endAddress}"
    End Function

    '' "B2" → (2, 2)
    'Private Shared Function ParseAddress(address As String) As CellAddress
    '    Dim match = Regex.Match(address.ToUpper(), "^([A-Z]+)(\d+)$")
    '    If Not match.Success Then
    '        Throw New ArgumentException("Invalid address: " & address)
    '    End If

    '    Dim colStr = match.Groups(1).Value
    '    Dim row = Integer.Parse(match.Groups(2).Value)
    '    Dim col = ColumnLetterToNumber(colStr)
    '    Return New CellAddress(col, row)
    'End Function
    Private Shared Function ParseAddress(address As String) As CellAddress
        address = address.Trim().ToUpper()

        ' パターン1: 通常のセル (例: "A1")
        Dim match1 = Regex.Match(address, "^([A-Z]+)(\d+)$")
        If match1.Success Then
            Dim col = ColumnLetterToNumber(match1.Groups(1).Value)
            Dim row = Integer.Parse(match1.Groups(2).Value)
            Return New CellAddress(col, row)
        End If

        ' パターン2: 列全体 (例: "A:A")
        Dim match2 = Regex.Match(address, "^([A-Z]+):\1$")
        If match2.Success Then
            Dim col = ColumnLetterToNumber(match2.Groups(1).Value)
            Return New CellAddress(col, 1)
        End If

        ' パターン3: 行全体 (例: "1:1")
        Dim match3 = Regex.Match(address, "^(\d+):\1$")
        If match3.Success Then
            Dim row = Integer.Parse(match3.Groups(1).Value)
            Return New CellAddress(1, row)
        End If

        ' パターン4: 列だけ指定 (例: "A", "AZ")
        Dim match4 = Regex.Match(address, "^([A-Z]+)$")
        If match4.Success Then
            Dim col = ColumnLetterToNumber(match4.Groups(1).Value)
            Return New CellAddress(col, 1)
        End If

        Throw New ArgumentException("Invalid address: " & address)
    End Function

    ''' <summary>
    ''' 列名を番号に変換（例: A → 1, B → 2, Z → 26, AA → 27）
    ''' </summary>
    Private Shared Function ColumnLetterToNumber(colStr As String) As Integer
        Dim col = 0
        For i = 0 To colStr.Length - 1
            col = col * 26 + (Asc(colStr(i)) - Asc("A"c) + 1)
        Next
        Return col
    End Function

    ' (2, 2) → "B2"
    Private Shared Function MakeAddress(col As Integer, row As Integer) As String
        Return ColumnNumberToLetter(col) & row.ToString()
    End Function

    Private Shared Function ColumnNumberToLetter(col As Integer) As String
        Dim result As String = ""
        While col > 0
            col -= 1
            result = ChrW(col Mod 26 + Asc("A"c)) & result
            col \= 26
        End While
        Return result
    End Function


    ''' <summary>
    ''' A1形式のアドレスを、行列方向に拡張または全体行・全体列に変換します。
    ''' </summary>
    Public Shared Function ExpandExcelAddress(
        startAddress As String,
        extendRow As Integer,
        extendCol As Integer,
        entireRow As Boolean,
        entireCol As Boolean) As String

        ' アドレスを範囲で取得（単一セルも "A1:A1" に揃える）
        Dim parts = startAddress.Split(":"c)
        Dim startPart = parts(0)
        Dim endPart = If(parts.Length > 1, parts(1), parts(0))

        'Dim startColStr = ParseCol(startPart)
        Dim startCol = ParseCol(startPart)
        Dim startRow = ParseRow(startPart)
        'Dim endColStr = ParseCol(endPart)
        Dim endCol = ParseCol(endPart)
        Dim endRow = ParseRow(endPart)

        'Dim startCol = ColumnLetterToNumber(startColStr)
        'Dim endCol = ColumnLetterToNumber(endColStr)

        ' 拡張
        If extendRow > 0 Then endRow += extendRow
        If extendCol > 0 Then endCol += extendCol

        If entireCol Then
            Dim colStart = ColumnNumberToLetter(startCol)
            Dim colEnd = ColumnNumberToLetter(endCol)
            Return $"{colStart}:{colEnd}"
        ElseIf entireRow Then
            Return $"{startRow}:{endRow}"
        Else
            Dim newStart = ColumnNumberToLetter(startCol) & startRow
            Dim newEnd = ColumnNumberToLetter(endCol) & endRow
            Return $"{newStart}:{newEnd}"
        End If
    End Function

    Sub UsageSample()
        ' A1 → A1:A2（1行拡張）
        Dim r1 = ExcelAddressUtil.ExpandExcelAddress("A1", 1, 0, False, False)

        ' A1:B2 → A1:B3（1行拡張）
        Dim r2 = ExcelAddressUtil.ExpandExcelAddress("A1:B2", 1, 0, False, False)

        ' A1 → A:B（行全体）
        Dim r3 = ExcelAddressUtil.ExpandExcelAddress("A1", 0, 0, True, False)

        ' A1:B2 → 1:2（列全体）
        Dim r4 = ExcelAddressUtil.ExpandExcelAddress("A1:B2", 0, 0, False, True)
    End Sub

End Class
