param (
    [Parameter(Mandatory=$true)]
	[string]$version
 )
$ErrorActionPreference = "Stop"
$WarningPreference = "Stop"

#
# Use Configureoo to decrypt our keys
#
$nugetApiKeyCipherText = "EBydBeVXZ/q7hngUAVKFImYmwRtsVYPX9DfcgmecJ8UlKnAUVsw2TjGF+5628qSdFJVrDkUBhILfueo68NP3Ew=="
$githubApiKeyCipherText = "HBlfUHJDOSI8qIu+2x/EwUamltddyf3kkGlIkYEcdoXvdmLkjCamPPYtqf4NUStbjvdIAYJK1T2FPymwaQ7CNw=="
$nugetApiKey = .\src\Configureoo\bin\Release\net47\Configureoo decryptvalue -k nuget -ct $nugetApiKeyCipherText
$githubToken = .\src\Configureoo\bin\Release\net47\Configureoo decryptvalue -k nuget -ct $githubApiKeyCipherText

#
# Publish NuGet Packages
#
.\lib\nuget\nuget.exe push .\build\artifacts\Overactiveminds.Configureoo.Core.$version.nupkg -ApiKey $nugetApiKey -Source https://api.nuget.org/v3/index.json
.\lib\nuget\nuget.exe push .\build\artifacts\Overactiveminds.Configureoo.JsonConfigurationProvider.$version.nupkg -ApiKey $nugetApiKey -Source https://api.nuget.org/v3/index.json
.\lib\nuget\nuget.exe push .\build\artifacts\Overactiveminds.Configureoo.KeyStore.EnvironmentVariables.$version.nupkg -ApiKey $nugetApiKey -Source https://api.nuget.org/v3/index.json

#
# Create Release on github and upload binaries
#
git tag $version
git push orign $version