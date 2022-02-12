REM codesign program
pushd CertificateFile_pfx

rem %1 : path for exe file and dll file

rem code sign start *.dll
signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 ..\..\bin\Release\NGettext.dll

rem code sign start *.exe
signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 ..\..\bin\Release\Br3D.exe

rem call Codesign_all.bat ..\..\bin\Release
popd
