$ErrorActionPreference = "Stop"
Set-StrictMode -Version "Latest"

. "$PSScriptRoot/Install-DotNetTool.ps1"

# install jetbrains resharper tool
Install-DotNetTool `
    -Name    "jetbrains.resharper.globaltools"

dotnet tool run jb inspectcode "src/Kingsland.MofParser.sln" -output=".build/out/jb-inspectcode.sarif" --no-build
