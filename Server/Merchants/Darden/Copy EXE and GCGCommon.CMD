ROBOCOPY.EXE "%cd%\Source\bin\x86\Debug" "\\webserver-vm\c\gcg\MerchantEXEs" *.exe GCGCommon.dll /S /XF *.vshost.exe /R:1 /W:1 /NS
pause