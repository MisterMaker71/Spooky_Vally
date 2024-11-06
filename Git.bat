@echo off
git.exe pull %2

git.exe add * %2

set /p location="commit message:"

git.exe commit -m "%location%" %2

pause
EXIT