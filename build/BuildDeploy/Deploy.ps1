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
# Create Release on github and upload binaries
#
$env:GITHUB_TOKEN=$githubToken

git tag $version
git push origin $version

.\lib\github-release\github-release.exe release --user overactiveminds --repo Configureoo --tag $version --name $version --description "Automated release of version $version from Deploy.ps1" --pre-release

# Upload our binaries
.\lib\github-release\github-release.exe upload --user overactiveminds --repo Configureoo --tag $version --name "Configureoo-$version-net47.zip" --file .\build\artifacts\Configureoo-$version-net47.zip
.\lib\github-release\github-release.exe upload --user overactiveminds --repo Configureoo --tag $version --name "Configureoo-$version-netcore2.0.tgz" --file .\build\artifacts\Configureoo-$version-netcore2.0.tgz
.\lib\github-release\github-release.exe upload --user overactiveminds --repo Configureoo --tag $version --name "Configureoo-$version-netcore2.0.zip" --file .\build\artifacts\Configureoo-$version-netcore2.0.zip
.\lib\github-release\github-release.exe upload --user overactiveminds --repo Configureoo --tag $version --name "Configureoo.VisualStudioTools-$version.vsix" --file .\build\artifacts\Configureoo.VisualStudioTools-$version.vsix

#
# Publish NuGet Packages
#
.\lib\nuget\nuget.exe push .\build\artifacts\Overactiveminds.Configureoo.Core.$version.nupkg -ApiKey $nugetApiKey -Source https://api.nuget.org/v3/index.json
.\lib\nuget\nuget.exe push .\build\artifacts\Overactiveminds.Configureoo.JsonConfigurationProvider.$version.nupkg -ApiKey $nugetApiKey -Source https://api.nuget.org/v3/index.json
.\lib\nuget\nuget.exe push .\build\artifacts\Overactiveminds.Configureoo.KeyStore.EnvironmentVariables.$version.nupkg -ApiKey $nugetApiKey -Source https://api.nuget.org/v3/index.json
