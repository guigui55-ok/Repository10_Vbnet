Imports System.IO

Module ModuleCsvUtility

    Sub OutputConsole(value)
        Dim wVal = String.Format("{0}", value)
        Debug.WriteLine(wVal)
    End Sub

    Sub Main()
        MainTestWriteCsvFromDict()
        'MainTestCsv()
    End Sub


    Sub MainTestWriteCsvFromDict()
        Dim dictList = GetTestDictData()
        OutputConsole(String.Format("dictList.Count = {0}", dictList.Count))
        Dim writeDir = GetProjectDirctory()
        Dim writeFileName = "__test_Dict.csv"
        Dim writePath = Path.Combine(writeDir, writeFileName)
        OutputConsole(String.Format("writePatht = {0}", writePath))
        Dim csvStream As CsvStream = New CsvStream()
        csvStream.WriteCsvDictList(writePath, dictList)
    End Sub

    Function GetProjectDirctory() ' 現在の実行ディレクトリを取得
        Dim currentDirectory As String = AppDomain.CurrentDomain.BaseDirectory

        ' プロジェクトフォルダを取得（2階層上に移動）
        Dim projectDirectory As String = IO.Path.GetFullPath(IO.Path.Combine(currentDirectory, "..", ".."))

        Return projectDirectory
    End Function
    Function GetProjectDirctoryRefrect()
        ' 実行中のアセンブリの場所を取得
        Dim assemblyLocation As String = Reflection.Assembly.GetExecutingAssembly().Location

        ' bin/Debug または bin/Releaseの親フォルダを取得
        Dim projectDirectory As String = IO.Path.GetFullPath(IO.Path.Combine(IO.Path.GetDirectoryName(assemblyLocation), "..", ".."))

        Return projectDirectory
    End Function

    Public Function GetTestDictData()
        ' テストデータの作成
        Dim testData As New List(Of Dictionary(Of String, Object)) From {
            New Dictionary(Of String, Object) From {
                {"ID", 1},
                {"Name", "Alice"},
                {"Age", 25}
            },
            New Dictionary(Of String, Object) From {
                {"ID", 2},
                {"Name", "Bob"},
                {"Age", 30}
            },
            New Dictionary(Of String, Object) From {
                {"ID", 3},
                {"Name", "Charlie"},
                {"Age", 35}
            },
            New Dictionary(Of String, Object) From {
                {"ID", 4},
                {"Name", "Diana"},
                {"Age", 28}
            },
            New Dictionary(Of String, Object) From {
                {"ID", 5},
                {"Name", "Eve"},
                {"Age", 22}
            }
        }

        ' データの表示
        For Each item As Dictionary(Of String, Object) In testData
            Console.WriteLine("-------------------")
            For Each kvp As KeyValuePair(Of String, Object) In item
                Console.WriteLine($"{kvp.Key}: {kvp.Value}")
            Next
        Next
        Return testData
    End Function

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
