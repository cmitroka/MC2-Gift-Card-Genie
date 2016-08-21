ROBOCOPY.EXE "Source\bin\x86\Debug" "\\webserver-vm\C\gcg\MerchantEXEs" *.EXE /XF *.VSHOST.EXE /R:1 /W:1 /NS
ROBOCOPY.EXE "Source\bin\x86\Debug" "\\webserver-pc\C\Websites\Gift Card Genie SSL\MerchantEXEs" *.EXE /XF *.VSHOST.EXE /R:1 /W:1 /NS
pause