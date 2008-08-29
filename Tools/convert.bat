SET XSL_EXE=C:\usr\install\dev\dotnet\msxsl.exe
rmdir /s /q import
mkdir import
for %%f in (*.xml) do %XSL_EXE% "%%f" dce2n2.xslt -o "import\%%f" -u 3.0
SET POST_PROCESSING_SET=import\*.xml
for %%f in (%POST_PROCESSING_SET%) do %XSL_EXE% "%%f" fixAnswers.xslt -o "import\%%~nf.fixed.xml" -u 3.0
