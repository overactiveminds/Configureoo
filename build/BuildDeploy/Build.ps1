﻿param (
    [Parameter(Mandatory=$true)]
	[string]$version
)
param (
	[Parameter(Mandatory=$true)]
	[string]$vsixVersion
)
$ErrorActionPreference = "Stop"
$WarningPreference = "Stop"

#
# Set Version Number for VSIX
#
$path = Resolve-Path -Path ".\src\Configureoo.VisualStudioTools\source.extension.vsixmanifest"
[xml]$xml = (Get-Content $path)
$node = $xml.PackageManifest.Metadata.Identity
$node.SetAttribute("Version", $vsixVersion)
$xml.Save($path)

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

Copy-Item .\src\Configureoo.VisualStudioTools\bin\Release\Configureoo.VisualStudioTools.vsix .\build\artifacts\Configureoo.VisualStudioTools-$version.vsix

Remove-Item .\build\artifacts\Configureoo-$version-netcore2.0.tar
