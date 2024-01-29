@echo off

rmdir /s /q "%appdata%\mplab_ipe"
rmdir /s /q "%appdata%\mplab_ide"
del /f /s /q "%localappdata%\Temp\*.*"
del /f /s /q "%localappdata%\ZaloPC\*.*"
del /f /s /q "C:\Windows\Temp\*.*"
del /f /s /q "C:\Windows\prefetch\*.*"
del /f /s /q "C:\Windows\servicing\LCU\*.*"

echo Done.