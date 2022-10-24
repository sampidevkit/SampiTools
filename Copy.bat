@echo off

xcopy Release\NBTest Z:\NBTest /c /i /e /h /y
xcopy Release\NBTest C:\Portable\NBTest /c /i /e /h /y

echo Done.