﻿Module ModuleCsvUtility

    Sub Main()
        MainTestCsv()
    End Sub

    Sub MainTestCsv()
        Dim csvStream As CsvStream = New CsvStream()
        Dim filePath As String
        filePath = "C:\Users\OK\source\repos\Repository10_VBnet\VbUtility\CsvUtility\TestData\Test1.csv"
        Dim dataTable As DataTable
        dataTable = csvStream.ReadCsvFile(filePath)
        Dim rowCount As Integer
        'Dim colCount As Integer
        Dim bufLine As String
        'For Each row As DataRow In dataTable.Rows
        '    colCount = 1
        '    bufLine = ""
        '    For Each cell In row.ItemArray
        '        bufLine += cell.ToString() + ", "
        '        colCount += 1
        '    Next
        '    bufLine = bufLine.Substring(0, bufLine.Length - 2)
        '    Console.WriteLine(String.Format("row={0}, [{1}]", rowCount, bufLine))
        '    rowCount += 1
        'Next
        Dim dataList2d As List(Of List(Of String)) = csvStream.ConvertDataTableToListString(dataTable)
        rowCount = 1
        For Each colList As List(Of String) In dataList2d
            bufLine = String.Format("row={0}, ", rowCount)
            bufLine = String.Format("[{0}]", String.Join(", ", colList))
            Console.WriteLine(bufLine)
        Next

        Console.ReadKey()
    End Sub

End Module
