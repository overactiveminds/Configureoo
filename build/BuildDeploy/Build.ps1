param (
    [Parameter(Mandatory=$true)]
	[string]$version
 )
$ErrorActionPreference = "Stop"
$WarningPreference = "Stop"
#
# Restore NuGet packages
#
.\lib\nuget\nuget.exe restore .\src\Configureoo.sln

#
# Build
#

msbuild .\src\Configureoo.sln /p:Configuration=Release /t:Rebuild

#
# Run Tests
#

##teamcity[testSuiteStarted name='UnitTests']
dotnet test --no-build .\src\Configureoo.UnitTests\Configureoo.UnitTests.csproj --configuration=Release
##teamcity[testSuiteFinished name='UnitTests']

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
