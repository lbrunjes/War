if [[ -z "$1" ]]; then 
    echo "Version required. last version was:"
    cat  "last_version_published"
    exit 1
fi
echo $1 > "last_version_published"
#Clean up existing data
rm -rf ../bin/Release/net5.0/win-x64/publish
#run tests
#dotnet test

#publish app
dotnet publish ../StructureWatch.csproj -r win-x64 -c Release  /p:PublishSingleFile=true /p:PublishTrimmed=true /p:Version=$1 --version-suffix $1

if [ $? -eq 0 ]; then

    #zip for transfer
    tar -C ../bin/Release/net5.0/win-x64/publish -zcf  War-$1.tar.gz .
    #transfer
   # scp StructureWatch-$1.tar.gz ubuntu@wilhe1m:/home/ubuntu
    #tar -xzvf -C /var/www/StructureWatch

else 
    echo "build failed ... :("
fi