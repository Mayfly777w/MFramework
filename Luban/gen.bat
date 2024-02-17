set WORKSPACE=..
set GEN_CLIENT=LubanLibrary\Luban.dll

dotnet %GEN_CLIENT% ^
    -t client ^
    -c cs-simple-json ^
    -d json  ^
    --conf luban.conf ^
    -x outputCodeDir=%WORKSPACE%\Assets\GenCode ^
    -x outputDataDir=%WORKSPACE%\Assets\GenJson

pause