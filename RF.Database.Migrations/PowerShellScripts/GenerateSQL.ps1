Function GenerateScripts{
[CmdletBinding()]
param(
[Parameter(Mandatory=$true)] [AllowEmptyString()][string] $From,
[Parameter(Mandatory=$true)] [AllowEmptyString()][string] $To
)

$scriptName = $migrationName +".sql";

Write-Host -Foreground Green "Generating sql scripts";

if([string]::IsNullOrEmpty($From) -and [string]::IsNullOrEmpty($To))
{
    dotnet ef migrations script $migrationName -s .\..\ -p .\..\..\RF.Infrastructure.Data -o .\..\SQLScripts\$scriptName -c EFCoreContext
}
else
{
    dotnet ef migrations script $From $To -s .\..\ -p .\..\..\RF.Infrastructure.Data -o .\..\SQLScripts\$scriptName -c EFCoreContext
}

if($? -eq $false){
    Write-Host -Foreground Red "Couldnt generate scripts";
    exit 1;
}
else{
    Write-Host -Foreground Green "Scripts generated successfully";
}
}

GenerateScripts;