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
        Dim util As New ExcelAnyUtil()
        Dim srcWb As Workbook = Nothing
        Dim destWb As Workbook = Nothing
        Try
            'open
            '// Excelオブジェクトを取得
            srcWb = util.GetWorkBook(_dataPairManager._srcFilePath)
            destWb = util.GetWorkBook(_dataPairManager._destFilePath)

            For Each dataPairValue In _dataPairManager._dataPairList
                _logger.Info($"execute [{count}]")
                dataPairValue.SetFilePath(_dataPairManager._srcFilePath, _dataPairManager._destFilePath)
                dataPairValue.InputBlankDataSrcToDest()

                ExecuteChangeFormat_Single(util, dataPairValue, srcWb, destWb)
                count += 1
            Next

            destWb.Save()
        Catch ex As Exception
            _logger.Info(vbCrLf + "**********" + vbCrLf)
            _logger.Info(ex.GetType.ToString + ":" + ex.Message)
            _logger.Info(ex.StackTrace)
            _logger.Info(vbCrLf + "**********" + vbCrLf)

            Throw
        Finally
            util.CloseWb(srcWb, Nothing)
            util.CloseWb(destWb, Nothing)
            _logger.Info("Close ExcelObject")
        End Try
        _logger.Info("ExecuteChangeFormat Done.")
    End Sub
    Public Sub ExecuteChangeFormat_Single(
            util As ExcelAnyUtil,
            _dataPair As ChangeFormatDataPairManager.DataPair,
            srcWb As Workbook,
            destWb As Workbook
            )
        'init
        Dim conSrc As ChangeFormatDataPairManager.ChangeFormatData 'Condition Source
        Dim conDest As ChangeFormatDataPairManager.ChangeFormatData 'Condition Destination
        conSrc = _dataPair.SrcItem
        conDest = _dataPair.DestItem

        Dim srcWs As Worksheet = Nothing
        Dim destWs As Worksheet = Nothing
        Try
            srcWs = util.GetWorkSheet(srcWb, conSrc.FindSheetName)
            destWs = util.GetWorkSheet(destWb, conDest.FindSheetName)

            _logger.Info($"srcFilePath = {conSrc.FilePath}")
            _logger.Info($"destFilePath = {conDest.FilePath}")

            '※ src,Dest同じファイルだと以下のエラーが発生する
            '「'System.Runtime.InteropServices.COMException'：起動されたオブジェクトはクライアントから切断されました。 (HRESULT からの例外:0x80010108 (RPC_E_DISCONNECTED))」
            _logger.Info($"srcWb = {srcWb.Name}")
            _logger.Info($"srcWs = {srcWs.Name}")
            _logger.Info($"destWb = {destWb.Name}")
            _logger.Info($"destWs = {destWs.Name}")

            '//まず、コピペ先の長さを読み込み、範囲を決定する

            '範囲決定時に、xlDownとするかのオプションは追加する？

            '// 書式コピー先範囲を設定
            'dest
            Dim foundAddrDest = util.FindCellAddress(
                    destWs,
                    conDest.FindRangeString,
                    conDest.FindValue,
                    True)
            _logger.Info("dest 検索結果: " & foundAddrDest)

            'endXl
            Dim endAddr = util.GetEndAddress(
                srcWs,
                foundAddrDest, Microsoft.Office.Interop.Excel.XlDirection.xlDown)
            _logger.Info("src endAddr: " & endAddr)
            Dim newRangeAddr = ExcelAddressUtil.GetUnionRangeAddress(endAddr, foundAddrDest)
            _logger.Info("dest newRangeAddr: " & newRangeAddr)

            'set src Address
            conDest.TargetCountRow = conSrc.TargetCountRow
            _logger.Info($"dest TargetCountRow: {conDest.TargetCountRow}")

            conDest.TargetRangeString = ExcelAddressUtil.ExpandExcelAddress(
                foundAddrDest, conDest.TargetCountRow, 0, entireRow:=True, entireCol:=False)
            _logger.Info($"dest TargetRangeString: {conDest.TargetRangeString}")



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

            ''endXl
            'Dim endAddr = util.GetEndAddress(
            '    srcWs,
            '    foundAddrSrc, Microsoft.Office.Interop.Excel.XlDirection.xlDown)
            '_logger.Info("src endAddr: " & endAddr)
            'Dim newRangeAddr = ExcelAddressUtil.GetUnionRangeAddress(endAddr, foundAddrSrc)
            '_logger.Info("src newRangeAddr: " & newRangeAddr)

            'set src Address
            conSrc.TargetCountRow = ExcelAddressUtil.GetRowCount(newRangeAddr)
            _logger.Info($"src TargetCountRow: {conSrc.TargetCountRow}")

            'Dim countCol = ExcelAddressUtil.GetColCount(foundAddrSrc, newRangeAddr)
            conSrc.TargetRangeString = ExcelAddressUtil.ExpandExcelAddress(
                newRangeAddr, conSrc.TargetCountRow, 0, entireRow:=True, entireCol:=False)
            _logger.Info($"src TargetRangeString: {conSrc.TargetRangeString}")

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
            Throw

        Finally
            _logger.Info("Close ExcelObject")
            util.CloseWs(srcWs)
            util.CloseWs(destWs)
        End Try
    End Sub




End Class
