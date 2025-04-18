﻿Imports System.Data.OracleClient

Public Class FormOracleSample

    Dim _oracleServerInfo As OracleServerInfo
    Dim _oracleManager As OracleManager
    Dim _ini As IniStream


    Private Sub FormOracleSample_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _oracleServerInfo = New OracleServerInfo()
        _oracleManager = New OracleManager()
        _ini = New IniStream("")
    End Sub



    Private Sub InitializeOracleAccessInfo()

        Dim iniPath = "C:\Users\OK\source\repos\test_media_files\vbnet_oracle\oracle_setting.ini"
        _ini = New IniStream(iniPath)

        ' 値を書き込む
        _ini.WriteValue("Settings", "UserName", "test_user")

        ' 値を読み込む
        Dim UserName As String = _ini.ReadValue("Settings", "UserName")
        Console.WriteLine($"UserName: {UserName}")
        Dim Password As String = _ini.ReadValue("Settings", "Password")
        Console.WriteLine($"Password: {Password}")
        Dim Host As String = _ini.ReadValue("Settings", "Host")
        Console.WriteLine($"Host: {Host}")
        Dim Port As String = _ini.ReadValue("Settings", "Port")
        Console.WriteLine($"Port: {Port}")
        Dim ServiceName As String = _ini.ReadValue("Settings", "ServiceName")
        Console.WriteLine($"ServiceName: {ServiceName}")

        Try
            _oracleServerInfo.UserName = UserName
            _oracleServerInfo.Password = Password
            _oracleServerInfo.Host = Host
            Dim bufint = Integer.Parse(Port)
            _oracleServerInfo.Port = bufint
            _oracleServerInfo.ServiceName = ServiceName
        Catch ex As Exception
            ConsoleOutputError(ex)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        InitializeOracleAccessInfo()
        'Dim sqlStr = "SELECT * FROM CUSTOMER_INFO"
        '_oracleManager.ConnectTest(_oracleServerInfo, sqlStr)
        Dim columnNameList = _oracleManager.GetColumnNameList(_oracleServerInfo, "CUSTOMER_INFO")
        'Dim buf = String.Join(", ", columnNameList)
        Dim buf As String
        For Each value In columnNameList
            buf += value.ToString() + ", "
        Next
        buf = buf.Substring(0, buf.Length - 2)
        Console.WriteLine("columnNames = " + buf)
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        InitializeOracleAccessInfo()
        '_oracleManager.ConnectOracleDataAccess(_oracleServerInfo, "CUSTOMER_INFO")

        Dim sqlStr = "SELECT * FROM CUSTOMER_INFO"
        Dim colNames = _oracleManager.GetColumnNames(_oracleServerInfo, sqlStr)
        Dim defStr = _oracleManager.GetColumnDefString(colNames)
        Console.WriteLine("defStr=" + defStr)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        InitializeOracleAccessInfo()
        'Dim defStrArray = {"ID", "NAME", "AGE", "CITY", "MEMBERSHIP_TYPE_"}
        'Dim ret = _oracleManager.CheckValidColumnName(_oracleServerInfo, "CUSTOMER_INFO", defStrArray.ToList())
        'Console.WriteLine(String.Format("ret = {0}", ret))

        Dim sql = "
BEGIN
    INSERT INTO CUSTOMER_INFO (ID, NAME)
    VALUES ('', 'Test Name');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;
"
        '    sql = "INSERT INTO CUSTOMER_INFO (ID, NAME)
        'VALUES ('', 'Test Name')"

        sql = "BEGIN
    INSERT INTO CUSTOMER_INFO (ID, NAME)
    VALUES ('', 'Test Name');
EXCEPTION
    WHEN OTHERS THEN
        RAISE; -- エラーを再スローする
END;"

        sql = "BEGIN
    SELECT ID, NAME_ FROM CUSTOMER_INFO WHERE ROWNUM <= 1; -- 行数を制限;
EXCEPTION
    WHEN OTHERS THEN
        RAISE; -- エラーを再スローする
END;"

        sql = "BEGIN
    INSERT INTO CUSTOMER_INFO (ID, NAME) VALUES ('', '');
EXCEPTION
    WHEN OTHERS THEN
        RAISE; -- エラーを再スローする
END;"


        sql = "
BEGIN
    INSERT INTO CUSTOMER_INFO (ID, NAME) VALUES ('', '');
EXCEPTION
    WHEN OTHERS THEN
        -- 独自メッセージを付与して再スロー
        RAISE_APPLICATION_ERROR(-20002, 'CustomErrorが発生しました。');
END;"

        '        sql = "
        'BEGIN
        '    DBMS_OUTPUT.PUT_LINE('Hello, World!');
        'EXCEPTION
        '    WHEN OTHERS THEN
        '        -- 独自メッセージを付与して再スロー
        '        RAISE_APPLICATION_ERROR(-20002, 'CustomErrorが発生しました。');
        'END;"

        sql = "
BEGIN
    INSERT INTO CUSTOMER_INFO (ID, NAME) VALUES ('', '');
EXCEPTION
    WHEN OTHERS THEN
        -- エラー情報をログまたは出力に記録
        DBMS_OUTPUT.PUT_LINE('エラーコード: ' || SQLCODE);
        DBMS_OUTPUT.PUT_LINE('エラーメッセージ: ' || SQLERRM);

        -- 独自メッセージを付与して再スロー
        RAISE_APPLICATION_ERROR(-20002, 'CustomErrorが発生しました。元のエラー: ' || SQLERRM);
END;"

        sql = "
BEGIN
    INSERT INTO CUSTOMER_INFO (ID, NAME) VALUES ('', '');
EXCEPTION
    WHEN OTHERS THEN
        -- トレース情報の記録
        INSERT INTO ERROR_LOG (ERROR_CODE, ERROR_MESSAGE, TIMESTAMP)
        VALUES (SQLCODE, SQLERRM, SYSDATE);
END;"


        '最初にRAISEを追加
        sql = "
BEGIN
    INSERT INTO CUSTOMER_INFO (ID, NAME)
    VALUES ('', 'Test Name');
EXCEPTION
    WHEN OTHERS THEN
        RAISE; -- エラーを再スローする
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;
"

        'SQLERRMでエラーメッセージを出力し、原因を追求します。
        Dim result As Boolean = _oracleManager.ExecutePlSqlWithError(_oracleServerInfo, sql)
        Console.WriteLine("result=" + result.ToString())

    End Sub

    Private Sub Button_WriteCsv_Click(sender As Object, e As EventArgs) Handles Button_WriteCsv.Click

        InitializeOracleAccessInfo()
        Dim sqlStr = "SELECT * FROM CUSTOMER_INFO"
        Dim dataSetValue = New DataSet
        Dim resultCode = _oracleManager.GetDataByExecuteSqlServerInfo(_oracleServerInfo, sqlStr, dataSetValue)
        Debug.WriteLine(String.Format("resultCode = {0}", resultCode))
        Dim buf As String
        For Each tableData As DataTable In dataSetValue.Tables
            Dim count = 0
            For Each row In tableData.Rows
                Dim dict = DataTableRowToDict(row)
                Dim bufStr = DictToString(dict)
                bufStr = String.Format("[{0:D2}] ", count) + bufStr
                Debug.WriteLine(bufStr)
                count += 1
            Next
        Next
        Debug.WriteLine("-----")
    End Sub

    Public Function DataTableRowToDict(dataRowValue As DataRow) As Dictionary(Of String, Object)
        Dim ret = New Dictionary(Of String, Object)()
        For Each col In dataRowValue.Table.Columns
            ret(col.ToString()) = dataRowValue(col)
        Next
        Return ret
    End Function

    Public Function DictToString(dict As Dictionary(Of String, Object), Optional formatStr As String = "[{0}:{1}]", Optional delimita As String = ", ") As String
        Dim bufList = New List(Of String)()
        For Each key In dict.Keys
            Dim buf = String.Format(formatStr, key, dict(key).ToString())
            bufList.Add(buf)
        Next
        Dim ret = ""
        ret = String.Join(delimita, bufList)
        Return ret
    End Function

End Class
