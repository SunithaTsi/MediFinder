param(
    [string]$SiteName = "MediFinder",
    [int]$Port = 8085,
    [string]$PublishPath = ""
)

$principal = New-Object Security.Principal.WindowsPrincipal([Security.Principal.WindowsIdentity]::GetCurrent())
if (-not $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
    throw "Run this script from an elevated PowerShell window: Run as Administrator."
}

Import-Module WebAdministration

if ([string]::IsNullOrWhiteSpace($PublishPath)) {
    $PublishPath = Join-Path (Split-Path -Parent $PSScriptRoot) "publish\iis"
}

$PublishPath = (Resolve-Path $PublishPath).Path
if (-not (Test-Path (Join-Path $PublishPath "web.config"))) {
    throw "The publish folder must contain web.config. Run: dotnet publish .\MediFinder.csproj -c Release -o .\publish\iis"
}

if (-not (Test-Path "IIS:\AppPools\$SiteName")) {
    New-WebAppPool -Name $SiteName | Out-Null
}

Set-ItemProperty "IIS:\AppPools\$SiteName" -Name managedRuntimeVersion -Value ""
Set-ItemProperty "IIS:\AppPools\$SiteName" -Name startMode -Value AlwaysRunning
Set-ItemProperty "IIS:\AppPools\$SiteName" -Name processModel.identityType -Value ApplicationPoolIdentity
Set-ItemProperty "IIS:\AppPools\$SiteName" -Name processModel.loadUserProfile -Value true

$existingSite = Get-Website -Name $SiteName -ErrorAction SilentlyContinue
if ($existingSite) {
    Set-ItemProperty "IIS:\Sites\$SiteName" -Name physicalPath -Value $PublishPath
    Set-ItemProperty "IIS:\Sites\$SiteName" -Name applicationPool -Value $SiteName
}
else {
    New-Website -Name $SiteName -Port $Port -PhysicalPath $PublishPath -ApplicationPool $SiteName | Out-Null
}

Set-ItemProperty "IIS:\Sites\$SiteName" -Name applicationDefaults.preloadEnabled -Value true

Start-WebAppPool -Name $SiteName
Start-Website -Name $SiteName

Write-Host "IIS site '$SiteName' is configured."
Write-Host "Open: http://localhost:$Port"
Write-Host ""
Write-Host "If the site opens but database access fails, move the connection string from LocalDB to SQL Server Express/Developer, or configure the app pool to run as the Windows user that owns the LocalDB database."
