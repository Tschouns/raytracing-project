@echo off

rem argument: %{sourceDir}/%{ActiveProject:FileBaseName}.html

taskkill ^
    /IM firefox.exe ^
    /F ^
    >nul ^
    2> nul

"C:\Program Files\Mozilla Firefox\firefox.exe" ^
    -save-mode ^
    -new-window ^
    -devtools ^
    file://%1
