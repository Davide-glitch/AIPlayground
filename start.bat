@echo off
start "Backend" cmd /k "cd c:\Users\doubl\source\backend\AIPlayground && dotnet run --launch-profile https"
start "Frontend" cmd /k "cd c:\Users\doubl\source\frontend\aiplayground-fe && npm start"
