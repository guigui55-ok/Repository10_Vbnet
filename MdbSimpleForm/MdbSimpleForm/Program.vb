﻿Imports System
Imports System.Windows.Forms

Module Program
    <STAThread()>
    Sub Main()
        ' アプリケーションのビジュアルスタイルを有効にします。
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        ' Form1を起動します。
        Application.Run(New FormMainMdbSimple())
    End Sub
End Module