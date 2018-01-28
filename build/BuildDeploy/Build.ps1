param (
    [Parameter(Mandatory=$true)]
	[string]$version
 )
$ErrorActionPreference = "Stop"
$WarningPreference = "Stop"
#
# Build
#
msbuild .\src\Configureoo.sln /p:Configureation=Release /t:Restore /t:Clean /t:Build /verbosity:m

#
# Run Tests
#
dotnet test --no-build .\src\Configureoo.UnitTests\Configureoo.UnitTests.csproj --configuration=Release

#
# Create Artifacts
#
dotnet pack --no-build .\src\Configureoo.Core\Configureoo.Core.csproj --configuration=Release --output ..\..\build\artifacts /p:Version=$version
dotnet pack --no-build .\src\Configureoo.JsonConfigurationProvider\Configureoo.JsonConfigurationProvider.csproj --configuration=Release --output ..\..\build\artifacts /p:Version=$version
dotnet pack --no-build .\src\Configureoo.KeyStore.EnvironmentVariables\Configureoo.KeyStore.EnvironmentVariables.csproj --configuration=Release --output ..\..\build\artifacts /p:Version=$version

.\lib\7zip\7z a .\build\artifacts\Configureoo-$version-net47.zip .\src\Configureoo\bin\Release\net47\*.*
.\lib\7zip\7z a .\build\artifacts\Configureoo-$version-netcore2.0.zip .\src\Configureoo\bin\Release\netcoreapp2.0\*.* 
.\lib\7zip\7z a .\build\artifacts\Configureoo-$version-netcore2.0.tar .\src\Configureoo\bin\Release\netcoreapp2.0\*.* 
.\lib\7zip\7z a .\build\artifacts\Configureoo-$version-netcore2.0.tgz .\build\artifacts\Configureoo-$version-netcore2.0.tar

Remove-Item .\build\artifacts\Configureoo-$version-netcore2.0.tar
