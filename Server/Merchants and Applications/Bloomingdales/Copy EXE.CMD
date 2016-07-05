ROBOCOPY.EXE "Source\bin\x86\Debug" "\\WEBSERVER-VM\gcg\MerchantEXEs" *.EXE /XF *.VSHOST.EXE /R:1 /W:1 /NS
pause