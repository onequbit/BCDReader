@echo off
set outfile=CmdRequire.exe
set buildfiles=ExtensionMethods.cs Lib.cs CmdRequire.cs
set runelevated=-win32manifest:%outfile%.manifest
set strongname=/keyfile:keyfile.snk
set appicon=-win32icon:onequbit.ico
set winexeoptions=-target:winexe %appicon% %strongname% %runelevated%
set consoleoptions=-target:exe %appicon% %strongname%
REM set consoleoptions=-target:winexe %appicon% %strongname%
set compileoptions=-out:%outfile% %consoleoptions%
