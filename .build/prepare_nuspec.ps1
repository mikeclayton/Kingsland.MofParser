$ErrorActionPreference = "Stop"
Set-StrictMode -Version "Latest"

. "$PSScriptRoot/Install-DotNetTool.ps1"

# install the gitversion tool if it isn't already installed
# and get the version numbers for this commit
Install-DotNetTool `
    -Name    "gitversion.tool" `
    -Version "5.8.2"
$versions = dotnet-gitversion | convertfrom-json
write-host "versions = "
write-host ($versions | format-list | out-string)

# get the target framework from the csproj
$csproj = [xml] (get-content "src/Kingsland.MofParser/Kingsland.MofParser.csproj" -raw)
$targetFramework = $csproj.SelectSingleNode("/Project/PropertyGroup/TargetFramework").InnerText
write-host "target framework = '$targetFramework'"

# insert values into the nuspec file
$nuspecPath = "$($env:GITHUB_WORKSPACE)/.build/Kingsland.MofParser.nuspec"
$nuspecText = get-content $nuspecPath -raw
write-host "nuspec before = "
write-host "------------"
$nuspecText
write-host "------------"
$nuspecText = $nuspecText.Replace("{{ commit }}", $versions.Sha)
$nuspecText = $nuspecText.Replace("{{ version }}", $versions.NuGetVersionV2)
$nuspecText = $nuspecText.Replace("{{ targetFramework }}", $targetFramework)
write-host "nuspec after = "
write-host "------------"
$nuspecText
write-host "------------"

# write the versioned nuspec
set-content -Path $nuspecPath -Value $nuspecText
