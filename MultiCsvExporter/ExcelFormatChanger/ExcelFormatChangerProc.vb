Imports Microsoft.Office.Interop.Excel

Public Class ExcelFormatChangerProc
    Public _logger As AppLogger
    Public _formMain As FormExcelFormatChanger
    Public _dataPairManager As ChangeFormatDataPairManager


    Sub New(
           ByRef logger As AppLogger,
           ByRef formMain As FormExcelFormatChanger,
           ByRef dataPairManager As ChangeFormatDataPairManager)
        _logger = logger
        _formMain = formMain
        _dataPairManager = dataPairManager
    End Sub

    Public Sub ExecuteChangeFormat()
        _logger.Info("*ExecuteChangeFormat")
        _logger.Info($"_srcFilePath = {_dataPairManager._srcFilePath}")
        _logger.Info($"_destFilePath = {_dataPairManager._destFilePath}")
        _logger.Info($"_dataPairList.Count = {_dataPairManager._dataPairList.Count}")
        '-- 実行
        '※繰り返し
        Dim count = 0
        For Each dataPairValue In _dataPairManager._dataPairList
            _logger.Info($"execute [{count}]")
            dataPairValue.SetFilePath(_dataPairManager._srcFilePath, _dataPairManager._destFilePath)
            ExecuteChangeFormat_Single(dataPairValue)
            count += 1
        Next
        _logger.Info("ExecuteChangeFormat Done.")
    End Sub
    Public Sub ExecuteChangeFormat_Single(_dataPair As ChangeFormatDataPairManager.DataPair)
        'init
        Dim util As New ExcelAnyUtil()
        Dim conSrc As ChangeFormatDataPairManager.ChangeFormatData 'Condition Source
        Dim conDest As ChangeFormatDataPairManager.ChangeFormatData 'Condition Destination
        conSrc = _dataPair.SrcItem
        conDest = _dataPair.DestItem

        Dim srcWb As Workbook = Nothing
        Dim srcWs As Worksheet = Nothing
        Dim destWb As Workbook = Nothing
        Dim destWs As Worksheet = Nothing
        Try
            _logger.Info($"srcFilePath = {conSrc.FilePath}")
            _logger.Info($"destFilePath = {conDest.FilePath}")
            '// Excelオブジェクトを取得
            srcWb = util.GetWorkBook(conSrc.FilePath)
            srcWs = util.GetWorkSheet(srcWb, conSrc.FindSheetName)
            destWb = util.GetWorkBook(conDest.FilePath)
            destWs = util.GetWorkSheet(destWb, conDest.FindSheetName)

            '※ src,Dest同じファイルだと以下のエラーが発生する
            '「'System.Runtime.InteropServices.COMException'：起動されたオブジェクトはクライアントから切断されました。 (HRESULT からの例外:0x80010108 (RPC_E_DISCONNECTED))」
            '_logger.Info($"srcWb = {srcWb.Name}")
            '_logger.Info($"srcWs = {srcWs.Name}")
            '_logger.Info($"destWb = {destWb.Name}")
            '_logger.Info($"destWs = {destWs.Name}")
            _logger.Info($"srcWb = {srcWb.Name}")
            _logger.Info($"srcWs = {srcWs.Name}")
            _logger.Info($"destWb = {destWb.Name}")
            _logger.Info($"destWs = {destWs.Name}")

            '// 変更先ファイル名を読み込む
            '// 変更先ファイル名の変更範囲を読み込み、変更範囲を動的に設定
            '書式コピー元、フォーマット範囲を検索・設定
            'src
            Dim foundAddrSrc = util.FindCellAddress(
                    srcWs,
                    conSrc.FindRangeString,
                    conSrc.FindValue,
                    True)
            _logger.Info("src 検索結果: " & foundAddrSrc)

            'endXl
            Dim endAddr = util.GetEndAddress(
                srcWs,
                foundAddrSrc, Microsoft.Office.Interop.Excel.XlDirection.xlDown)
            _logger.Info("src endAddr: " & endAddr)
            Dim newRangeAddr = ExcelAddressUtil.GetUnionRangeAddress(endAddr, foundAddrSrc)
            _logger.Info("src newRangeAddr: " & newRangeAddr)

            'set src Address
            conSrc.TargetCountRow = ExcelAddressUtil.GetRowCount(newRangeAddr)
            _logger.Info($"src TargetCountRow: {conSrc.TargetCountRow}")

            'Dim countCol = ExcelAddressUtil.GetColCount(foundAddrSrc, newRangeAddr)
            conSrc.TargetRangeString = ExcelAddressUtil.ExpandExcelAddress(
                newRangeAddr, conSrc.TargetCountRow, 0, entireRow:=True, entireCol:=False)
            _logger.Info($"src TargetRangeString: {conSrc.TargetRangeString}")

            '// 書式コピー先範囲を設定
            'dest
            Dim foundAddrDest = util.FindCellAddress(
                    destWs,
                    conDest.FindRangeString,
                    conDest.FindValue,
                    True)
            _logger.Info("dest 検索結果: " & foundAddrDest)

            'set src Address
            conDest.TargetCountRow = conSrc.TargetCountRow
            _logger.Info($"dest TargetCountRow: {conDest.TargetCountRow}")

            conDest.TargetRangeString = ExcelAddressUtil.ExpandExcelAddress(
                foundAddrDest, conDest.TargetCountRow, 0, entireRow:=True, entireCol:=False)
            _logger.Info($"dest TargetRangeString: {conDest.TargetRangeString}")

            '// コピーペースト
            util.CopyFormatBetweenBooks(
                        srcWb,
                        srcWs,
                        conSrc.TargetRangeString,
                        destWb,
                        destWs,
                        conDest.TargetRangeString
                    )
        Catch ex As Exception
            _logger.Info(vbCrLf + "**********" + vbCrLf)
            _logger.Info(ex.GetType.ToString + ":" + ex.Message)
            _logger.Info(ex.StackTrace)
            _logger.Info(vbCrLf + "**********" + vbCrLf)

            Throw
        Finally
            util.CloseWb(srcWb, srcWs)
            util.CloseWb(destWb, destWs)

            _logger.Info("Close ExcelObject")
        End Try
    End Sub




End Class
