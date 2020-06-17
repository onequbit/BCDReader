@echo off
set outfile=BCDReader.exe
REM set buildfiles=Win32Dll.cs TempFile.cs AdminProcess.cs ExtensionMethods.cs Notifier.cs ConsoleAttached.cs bcdreader.cs
set buildfiles=ExtensionMethods.cs Lib.cs bcdreader.cs
set runelevated=-win32manifest:BCDReader.exe.manifest
set strongname=/keyfile:keyfile.snk
set appicon=-win32icon:onequbit.ico
set winexeoptions=-target:winexe %appicon% %strongname% %runelevated%
set consoleoptions=-target:exe %appicon% %strongname%
REM set consoleoptions=-target:winexe %appicon% %strongname%
set compileoptions=-out:%outfile% %consoleoptions%
