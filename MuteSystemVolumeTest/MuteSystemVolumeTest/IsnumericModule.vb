Module IsnumericModule
    '/* <数値文字列チェック> */'
    '/* 　val…文字列 */'
    '/* 　len…最大整数部長さ。0の場合はチェックなし */'
    '/*   inMinus…マイナスが含まれるか？ */'
    '/*   decimal_places…小数点以下の最大桁数 */'
    Public Function CheckNum(val As String,
                           Optional len As UInteger = 0,
                           Optional inMinus As Boolean = False,
                           Optional decimal_places As UInteger = 0) As Boolean
        Dim wk_check_moji As String = "0123456789" '/* 許可する文字 */'

        '/* 普通の数値チェック(ここで大体除去) */'
        If IsNumeric(val) = False Then
            Return False
        End If

        '/* マイナス付き */'
        If inMinus = True Then
            wk_check_moji &= "-" '/* マイナス許可 */'
        End If

        '/* 桁数チェック */'
        If decimal_places > 0 Then '/* 小数桁があるか？ */'
            Dim s() As String = val.Split("."c)
            If s.Length = 1 Then
                '/* 整数部分の長さチェック */'
                If len > 0 AndAlso s(0).Replace("-", "").Length > len Then
                    Return False
                End If
            ElseIf s.Length = 2 Then '/* 小数点あり */'
                '/* 整数部分の長さチェック */'
                If len > 0 AndAlso s(0).Replace("-", "").Length > len Then
                    Return False
                End If
                '/* 小数部分の長さチェック */'
                If decimal_places > 0 AndAlso s(1).Length > decimal_places Then
                    Return False
                End If
                wk_check_moji &= "." '/* カンマ許可 */'
            Else
                '/* 小数点が複数の場合はエラー */'
                Return False
            End If
        Else
            '/* 整数部分の長さチェック */'
            If len > 0 AndAlso val.Replace("-", "").Length > len Then
                Return False
            End If
        End If

        '/* 禁則文字チェック */'
        For Each moji As Char In val
            If wk_check_moji.IndexOf(moji) < 0 Then
                '/* 文字が見つからない場合はエラー */'
                Return False
            End If
        Next
        Return True
    End Function
End Module
