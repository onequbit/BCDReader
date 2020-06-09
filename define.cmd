@echo off
set buildfiles=TempFile.cs AdminProcess.cs ExtensionMethods.cs bcdreader.cs
set runelevated=-win32manifest:app.manifest
set strongname=/keyfile:keyfile.snk
set appicon=-win32icon:onequbit.ico
set winexeoptions=-target:winexe %appicon% %strongname% %runelevated%
set consoleoptions=-target:exe %appicon% %strongname%
set compileoptions=%consoleoptions%
