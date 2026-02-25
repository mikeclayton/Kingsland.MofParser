function Install-DotNetTool
{

    param
    (
        [string] $Name,
        [string] $Version
    )

    # calling "dotnet tool install" when already installed will give an error
    # see https://github.com/dotnet/sdk/issues/9500

    $tools = dotnet tool list --format json | convertfrom-json
    $tool  = $tools.data | where-object { $_.packageId -eq $Name }

    if ($null -eq $tool)
    {

        $cmdArgs = @("tool", "install", $Name)

        if( -not [string]::IsNullOrEmpty($Version) )
        {
            $cmdArgs += @("--version", $Version)
        }

	    dotnet $cmdArgs
    }

}