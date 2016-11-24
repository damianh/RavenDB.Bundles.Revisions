properties {
	$baseDir  = resolve-path ..\
	$artifactDir = "$baseDir\artifacts"
	$srcDir = "$baseDir\src"
	$sln = "$baseDir\src\RavenDB.Bundles.Revisions.sln"
	$xunitPath = FindTool("xunit.runner.console.*\tools\xunit.console.exe")
	$version = "2.0.0"
	$buildNumber = 0
}

task default -depends Release

task Clean {
	remove-item -force -recurse $artifactDir -ErrorAction SilentlyContinue
	exec { msbuild $sln /t:Clean }
}

task Compile -depends Clean {
	exec { msbuild $sln /p:Configuration=Release }
}

task RunTests -depends Compile {
	New-Item $artifactDir -Type directory

	exec { & $xunitPath "$srcDir\Raven.Bundles.Revisions.Tests\bin\Release\Raven.Bundles.Revisions.Tests.dll" -html "$artifactDir\index.html" }
}

task BuildPackages -depends Compile{
	New-Item $artifactDir\RavenDB.Bundles.Revisions\lib\net45 -Type directory | Out-Null
	Copy-Item $srcDir\Raven.Bundles.Revisions\RavenDB.Bundles.Revisions.nuspec $artifactDir\RavenDB.Bundles.Revisions\RavenDB.Bundles.Revisions.nuspec
	@("Raven.Bundles.Revisions.???") |% { Copy-Item "$srcDir\Raven.Bundles.Revisions\bin\Release\$_" $artifactDir\RavenDB.Bundles.Revisions\lib\net45 }
	&"$baseDir\build\nuget.exe" pack $artifactDir\RavenDB.Bundles.Revisions\RavenDB.Bundles.Revisions.nuspec -OutputDirectory $artifactDir -Version $version

	New-Item $artifactDir\RavenDB.Client.Revisions\lib\net45 -Type directory | Out-Null
	Copy-Item $srcDir\Raven.Client.Revisions\RavenDB.Client.Revisions.nuspec $artifactDir\RavenDB.Client.Revisions\RavenDB.Client.Revisions.nuspec
	@("Raven.Client.Revisions.???") |% { Copy-Item "$srcDir\Raven.Client.Revisions\bin\Release\$_" $artifactDir\RavenDB.Client.Revisions\lib\net45 }
	&"$baseDir\build\nuget.exe" pack $artifactDir\RavenDB.Client.Revisions\RavenDB.Client.Revisions.nuspec -OutputDirectory $artifactDir -Version $version
}

task Release -depends RunTests, BuildPackages

function FindTool {
    param(
        [string] $name
    )
    $result = Get-ChildItem "$srcDir\packages\$name" | Select-Object -First 1
    return $result.FullName
}
