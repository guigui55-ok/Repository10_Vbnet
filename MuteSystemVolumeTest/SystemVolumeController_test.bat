@echo off
@setlocal enabledelayedexpansion

REM ##以下が自動でadmin実行してくれるバッチの実行コード
set PATH1="%0"
whoami /priv | find "SeDebugPrivilege" > nul
if %errorlevel% neq 0 (
  @powershell start-process "%PATH1%" -verb runas
  exit
)

REM ##以上が自動でadmin実行してくれるバッチの実行コード
REM 以下に実行したいコードを記載する
cd C:\Users\OK\source\repos\Repository10_VBnet\MuteSystemVolumeTest\SystemVolumeController\bin\Release\netcoreapp3.1
SystemVolumeController.exe -m 1
