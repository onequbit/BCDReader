@echo off
REM where - found -> errorlevel 0
REM where - not found -> errorlevel 1
IF EXIST pseudo.exe (
echo Found Pseudo
) ELSE (
echo Pseudo not found - errorlevel 1
)