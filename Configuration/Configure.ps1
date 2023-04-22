#####################################################################
#
#   This powershell file will read in each line of .config file,
#   and then set the setting so that it can be accessed by the program.
#
#
#
#####################################################################

param(
    [string] $pathToFile = "/Users/matthewpotts/Projects/dotnet-alpaca/Configuration"
)

[xml] $ConfigFile = Get-Content -Path "$pathToFile/app.config" 
Write-Host "$ConfigFile"

foreach ($token in $ConfigFile.Configuration.DeviceSettings.MajorCommands) {
    
    Write-Host "$($token.key)"
}
