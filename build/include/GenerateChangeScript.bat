@echo off

rem %1 = Database, %2 = Server, %3 = Username, %4 = Password

if [%1] == [] goto Help
if [%2] == [] goto Help

set sqlPackageDir="C:\Program Files (x86)\Microsoft SQL Server\110\DAC\bin"

if not [%3] == [] (
    if [%4] == [] goto Help
    call %sqlPackageDir%\SqlPackage.exe /Action:Script /SourceFile:DeploymentCockpit.Database.dacpac /TargetDatabaseName:%1 /TargetServerName:%2 /TargetUser:%3 /TargetPassword:%4 /OutputPath:ChangeScript.sql
) else (
    call %sqlPackageDir%\SqlPackage.exe /Action:Script /SourceFile:DeploymentCockpit.Database.dacpac /TargetDatabaseName:%1 /TargetServerName:%2 /OutputPath:ChangeScript.sql
)

set sqlPackageDir=
goto :eof

:Help
echo Parameters are missing!
echo.
echo Usage:
echo   GenerateChangeScript.bat DatabaseName ServerName [Username Password]
echo.
echo Example (Windows user):
echo   GenerateChangeScript.bat DeploymentCockpit .\SQLEXPRESS
echo.
echo Example (SQL user):
echo   GenerateChangeScript.bat DeploymentCockpit .\SQLEXPRESS myUsername myPassword
