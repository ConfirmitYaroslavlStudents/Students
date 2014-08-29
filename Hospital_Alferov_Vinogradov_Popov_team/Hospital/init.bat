@echo off
rmdir /s /q Hospital\bin\Debug\Printers
mkdir Hospital\bin\Debug\Printers
copy Printers\TxtPrinter\bin\Debug\TxtPrinter.dll  Hospital\bin\Debug\Printers\TxtPrinter.dll
xcopy Databases Hospital\bin\Debug\Databases /i
pause
