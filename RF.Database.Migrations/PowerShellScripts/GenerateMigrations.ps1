    function GenerateMigration{
    [CmdletBinding()]
    param(
    [Parameter(Mandatory=$true)] [string] $Name
    )

   if([string]::IsNullOrWhiteSpace($Name)){
        Write-Host -ForegroundColor Red "Migration name cannot be empty!";
        exit -1;
    }

    Write-Host "Generating ef migrations";
    $OutputVariable = dotnet ef migrations add $Name -s .\..\  -o .\Migrations\Output -c EFCoreContext | Out-String ;
    Write-Host -Foreground Yellow $OutputVariable

 if($LASTEXITCODE -eq 0){
    Write-Host -Foreground Green "Migrations generated successfully";
    return $Name
}

    Write-Host -Foreground Red "Couldnt generate migrations";
    exit -1;
}

GenerateMigration;