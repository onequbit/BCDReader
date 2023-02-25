@echo off
set outfile=BCDReader.exe
set buildfiles=ExtensionMethods.cs TempFile.cs AdminProcess.cs Lib.cs BCDUtils.cs BCDReader.cs
set runelevated=-win32manifest:%outfile%.manifest
set strongname=/keyfile:keyfile.snk
set appicon=-win32icon:onequbit.ico
set winexeoptions=-target:winexe %appicon% %strongname% %runelevated%
set consoleoptions=-target:exe %appicon% %strongname%
set compileoptions=-out:%outfile% %consoleoptions%
