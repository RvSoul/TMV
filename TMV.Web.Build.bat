git pull


@echo off
for /f "tokens=5" %%i in ('netstat -aon ^| findstr ":5000"') do (
    set n=%%i
)
taskkill /f /pid %n%




dotnet build

cd TMV.Web



dotnet run

cmd