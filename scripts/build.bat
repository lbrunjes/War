:: Remove e
del ..\StructureWatch\bin\Release\net5.0\win-x64\publish
dotnet publish ../War.csproj -r win-x64 -c Release /p:PublishSingleFile=true /p:Version=%1 --version-suffix %1