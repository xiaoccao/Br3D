rem %1 : path for exe file and dll file

rem code sign start *.dll
signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 %1\FolderExplorer.dll
signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 %1\GongShell.dll
signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 %1\Syroot.KnownFolders.dll
signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 %1\DotNetZip.dll
signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 %1\NGettext.dll

rem code sign start *.exe
signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 %1\InsideFolder.exe
