@echo off
git.exe pull %2

choice /c YN /n /m "Quit [Y|N] "
IF "%errorlevel%" == "1" (exit)

git.exe add * %2

set /p location="commit message:"

git.exe commit -m "%location%" %2

git.exe push %2
echo saved changes
pause
EXIT