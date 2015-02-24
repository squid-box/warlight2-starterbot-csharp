@ECHO OFF

:: Collects all .cs files in a .zip archive, ready for submission.
:: Uses 7-zip, which is cool.

SET zip="C:\Program Files\7-Zip\7z.exe"
SET dest=bot.zip

%zip% a %dest% .\Bot\*.cs
TIMEOUT 1
%zip% a %dest% .\Map\*.cs
TIMEOUT 1
%zip% a %dest% .\Move\*.cs
