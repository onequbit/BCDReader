@echo off
set buildfiles=Kernel32.cs TempFile.cs AdminProcess.cs ExtensionMethods.cs Notifier.cs ConsoleAttached.cs bcdreader.cs
set runelevated=-win32manifest:BCDReader.exe.manifest
set strongname=/keyfile:keyfile.snk
set appicon=-win32icon:onequbit.ico
set winexeoptions=-target:winexe %appicon% %strongname% %runelevated%
REM set consoleoptions=-target:exe %appicon% %strongname%
set consoleoptions=-target:winexe %appicon% %strongname%
set compileoptions=%consoleoptions%
