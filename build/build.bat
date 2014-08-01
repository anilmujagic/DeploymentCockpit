cd ..\src

..\build\tools\nuget.exe restore DeploymentCockpit.sln

set msBuildDir=%WINDIR%\Microsoft.NET\Framework\v4.0.30319
call %msBuildDir%\msbuild.exe DeploymentCockpit.sln /p:Configuration=Release /t:Clean,Build /l:FileLogger,Microsoft.Build.Engine;logfile=..\build\build.log
set msBuildDir=

cd ..\build

rd .\BuildOutput /S /Q
md .\BuildOutput

md .\BuildOutput\Database
copy .\include\GenerateChangeScript.bat .\BuildOutput\Database
copy ..\src\DeploymentCockpit.Database\bin\Release\DeploymentCockpit.Database.dacpac .\BuildOutput\Database
copy ..\src\DeploymentCockpit.Database\bin\Release\DeploymentCockpit.Database_Create.sql .\BuildOutput\Database
PowerShell -NoProfile -ExecutionPolicy Bypass -Command "& {(Get-Content .\BuildOutput\Database\DeploymentCockpit.Database_Create.sql) | ForEach-Object { $_ -replace 'DeploymentCockpit.Database', 'DeploymentCockpit' } | Set-Content .\BuildOutput\Database\DeploymentCockpit.Database_Create.sql}"

md .\BuildOutput\Server
xcopy ..\src\DeploymentCockpit.Server\bin\*.dll .\BuildOutput\Server\bin /I
xcopy ..\src\DeploymentCockpit.Server\App_Start\*.cs .\BuildOutput\Server\App_Start /I
copy ..\src\DeploymentCockpit.Server\Web.config .\BuildOutput\Server
copy ..\src\DeploymentCockpit.Server\AppSettings.config .\BuildOutput\Server
copy ..\src\DeploymentCockpit.Server\ConnectionStrings.config .\BuildOutput\Server
copy ..\src\DeploymentCockpit.Server\index.html .\BuildOutput\Server
xcopy ..\src\DeploymentCockpit.Server\app\* .\BuildOutput\Server\app /I /S /EXCLUDE:exclusions.txt
xcopy ..\src\DeploymentCockpit.Server\assets\* .\BuildOutput\Server\assets /I /S /EXCLUDE:exclusions.txt
xcopy ..\src\DeploymentCockpit.Server\ext\* .\BuildOutput\Server\ext /I /S /EXCLUDE:exclusions.txt

md .\BuildOutput\JobRunner
xcopy ..\src\DeploymentCockpit.JobRunner\bin\Release\DeploymentCockpit.JobRunner.exe* .\BuildOutput\JobRunner
xcopy ..\src\DeploymentCockpit.JobRunner\bin\Release\*.dll .\BuildOutput\JobRunner
copy ..\src\DeploymentCockpit.JobRunner\AppSettings.config .\BuildOutput\JobRunner
copy ..\src\DeploymentCockpit.JobRunner\ConnectionStrings.config .\BuildOutput\JobRunner

md .\BuildOutput\Target
xcopy ..\src\DeploymentCockpit.Target\bin\Release\DeploymentCockpit.Target.exe* .\BuildOutput\Target
xcopy ..\src\DeploymentCockpit.Target\bin\Release\*.dll .\BuildOutput\Target
copy ..\src\DeploymentCockpit.Target\AppSettings.config .\BuildOutput\Target

if "%1" == "update" (
    rem Add .sample extension to avoid replacing files during update
    ren .\BuildOutput\Server\AppSettings.config AppSettings.config.sample
    ren .\BuildOutput\Server\ConnectionStrings.config ConnectionStrings.config.sample
    ren .\BuildOutput\JobRunner\AppSettings.config AppSettings.config.sample
    ren .\BuildOutput\JobRunner\ConnectionStrings.config ConnectionStrings.config.sample
    ren .\BuildOutput\Target\AppSettings.config AppSettings.config.sample
)

.\tools\7z.exe a .\BuildOutput\DeploymentCockpit.zip .\BuildOutput\*
rd .\BuildOutput\Database /S /Q
rd .\BuildOutput\Server /S /Q
rd .\BuildOutput\JobRunner /S /Q
rd .\BuildOutput\Target /S /Q
