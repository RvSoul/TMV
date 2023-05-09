color B

del  .PublishFiles\*.*   /s /q

dotnet restore

dotnet build

cd TMV.Web

dotnet publish -o ..\TMV.Web\bin\Debug\net7.0\

md ..\.PublishFiles

xcopy ..\TMV.Web\bin\Debug\net7.0\*.* ..\.PublishFiles\ /s /e 

echo "Successfully!!!! ^ please see the file .PublishFiles"

cmd