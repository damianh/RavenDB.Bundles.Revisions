param(
	[int]$buildNumber = 0,
	[bool]$skipRestore = $false
	)
$nugetexe_path = ".\build\nuget.exe"

& .\build\nuget.exe install ".\src\.nuget\packages.config" -OutputDirectory ".\src\packages"

Import-Module (Get-ChildItem ".\src\packages\psake.*\tools\psake.psm1" | Select-Object -First 1)
Invoke-Psake .\build\default.ps1 default -framework "4.5.1x64" -properties @{ buildNumber=$buildNumber }
Remove-Module psake