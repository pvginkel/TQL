@echo off

pushd "%~dp0"

powershell -file .\Support\Build.ps1

pause

popd
