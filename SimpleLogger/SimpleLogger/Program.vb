Imports System
Imports System.IO

Module Program
    Sub Main()
        Console.WriteLine("Hello World!")
        Dim Logger As SimpleLogger = New SimpleLogger()
        Dim currentDirectory As String = Directory.GetCurrentDirectory()
        Dim loggerPath As String = currentDirectory & "\__test_log.log"
        Console.WriteLine("loggerPath: " & loggerPath)
        Logger.FilePath = loggerPath
        Logger.LogOutPutMode = Logger.LogOutPutMode + OutputMode.FILE
        Logger.LogOutPutMode = Logger.LogOutPutMode + OutputMode.CONSOLE
        ShowBinaryRepresentation(Logger.LogOutPutMode, 4)
        Logger.PrintInfo("Hello Logger!")
    End Sub

    Sub ShowBinaryRepresentation(ByVal number As Integer, ByVal digits As Integer)
        Dim binaryString As String = Convert.ToString(number, 2).PadLeft(digits, "0"c)
        Console.WriteLine("The binary representation of " & number & " is " & binaryString)
    End Sub
End Module
