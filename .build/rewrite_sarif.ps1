$ErrorActionPreference = "Stop"
Set-StrictMode -Version "Latest"

# "dotnet tool run jb inspectcode" generates a SARIF file with paths relative to the solution file
# but github expects them to be relative to the root of the repository so we need to rewrite them

$sarifFile = ".build/out/jb-inspectcode.sarif"

# read the sarif file from disk
$sarif = Get-Content $sarifFile | ConvertFrom-Json

# update the paths in the SARIF file
foreach( $run in $sarif.runs )
{
    foreach( $result in $run.results )
    {
        # rewrite the file path to be relative to the root of the repository
        $newPath = $result.locations[0].physicalLocation.artifactLocation.uri
        # add a "src/" to the path if it doesn't already have it
        if( -not $newPath.StartsWith("src/") )
        {
            $newPath = "src/$newPath"
        }
        # update the sarif file
        $result.locations[0].physicalLocation.artifactLocation.uri = $newPath
    }
}

# write the modified SARIF file back to disk
$sarif | ConvertTo-Json -Depth 99 | Set-Content $sarifFile