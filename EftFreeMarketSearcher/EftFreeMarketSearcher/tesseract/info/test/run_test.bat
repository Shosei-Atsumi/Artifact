



tesseract --help
IF %ERRORLEVEL% NEQ 0 exit /B 1
tesseract --list-langs
IF %ERRORLEVEL% NEQ 0 exit /B 1
tesseract eurotext.tif outputbase
IF %ERRORLEVEL% NEQ 0 exit /B 1
exit /B 0
