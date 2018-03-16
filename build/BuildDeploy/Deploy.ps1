param (
    [Parameter(Mandatory=$true)]
	[string]$version
 )
$ErrorActionPreference = "Stop"
$WarningPreference = "Stop"

#
# Use Configureoo to decrypt our keys
#
$nugetApiKeyCipherText = "niwFkPjMPvg6kZO5n0A7qhBXHgJgOBMxxe6dRJhAfPiRDEWGNCgbhNyFg61uBKTPsDg/7hgx2Eb44prrhL4+fA=="
$githubApiKeyCipherText = "kjInlwsn7AJKdeEr5n7HizDyujg40x74LMakL0qpvrCaZQa3QhryGIaZUIVcoPPh+JVMa5/9lhuQGeImMXUR4A=="
$nugetApiKey = .\src\Configureoo\bin\Release\net47\Configureoo decryptvalue -k nuget -ct $nugetApiKeyCipherText
$githubToken = .\src\Configureoo\bin\Release\net47\Configureoo decryptvalue -k github -ct $githubApiKeyCipherText

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
