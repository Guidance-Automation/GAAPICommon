$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$grpcToolsVersion = '2.83.0'
$commonProtosVersion = '2.17.0'
$openApiGeneratorVersion = '2.29.0'

$repoRoot = Split-Path -Parent $PSScriptRoot
$projectPath = Join-Path $repoRoot 'src\GAAPICommon.csproj'
$protoPath = Join-Path $repoRoot 'src\Protos'
$outputPath = Join-Path $repoRoot 'src\OpenApi'
$toolPath = Join-Path $repoRoot "artifacts\tools\openapi\$openApiGeneratorVersion"

$nugetPackagesPath = $env:NUGET_PACKAGES
if ([string]::IsNullOrWhiteSpace($nugetPackagesPath)) {
    $nugetPackagesPath = Join-Path ([Environment]::GetFolderPath('UserProfile')) '.nuget\packages'
}

$grpcToolsPath = Join-Path $nugetPackagesPath "grpc.tools\$grpcToolsVersion"
$commonProtosPath = Join-Path $nugetPackagesPath "google.api.commonprotos\$commonProtosVersion\content\protos"

if ((-not (Test-Path -LiteralPath $grpcToolsPath)) -or (-not (Test-Path -LiteralPath $commonProtosPath))) {
    & dotnet restore $projectPath
    if ($LASTEXITCODE -ne 0) {
        throw "dotnet restore failed with exit code $LASTEXITCODE."
    }
}

if ($IsWindows) {
    $protocPath = Join-Path $grpcToolsPath 'tools\windows_x64\protoc.exe'
    $pluginName = "protoc-gen-openapiv2-v$openApiGeneratorVersion-windows-x86_64.exe"
    $pluginHash = 'cce39f2a9adf922b132fca2dc11d0ce2bda0b2956e42090605cd9dbe1c2a42bc'
}
elseif ($IsLinux) {
    $protocPath = Join-Path $grpcToolsPath 'tools\linux_x64\protoc'
    $pluginName = "protoc-gen-openapiv2-v$openApiGeneratorVersion-linux-x86_64"
    $pluginHash = '804794a445ae57914b58df059cb9cf96a9f2baf25501f0039b6dd5fbca260b0a'
}
else {
    throw 'OpenAPI generation currently supports x64 Windows and x64 Linux.'
}

if (-not (Test-Path -LiteralPath $protocPath)) {
    throw "protoc was not found at $protocPath."
}

New-Item -ItemType Directory -Path $toolPath -Force | Out-Null
$pluginPath = Join-Path $toolPath $pluginName
$pluginUrl = "https://github.com/grpc-ecosystem/grpc-gateway/releases/download/v$openApiGeneratorVersion/$pluginName"

$downloadPlugin = -not (Test-Path -LiteralPath $pluginPath)
if (-not $downloadPlugin) {
    $actualHash = (Get-FileHash -Algorithm SHA256 -LiteralPath $pluginPath).Hash.ToLowerInvariant()
    $downloadPlugin = $actualHash -ne $pluginHash
}

if ($downloadPlugin) {
    Invoke-WebRequest -Uri $pluginUrl -OutFile $pluginPath
    $actualHash = (Get-FileHash -Algorithm SHA256 -LiteralPath $pluginPath).Hash.ToLowerInvariant()
    if ($actualHash -ne $pluginHash) {
        Remove-Item -LiteralPath $pluginPath
        throw "OpenAPI generator hash mismatch. Expected $pluginHash; received $actualHash."
    }
}

if ($IsLinux) {
    & chmod '+x' $pluginPath
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to make $pluginPath executable."
    }
}

$documents = @(
    'Services\Agents\AgentService.proto'
    'Services\FleetManager\FleetManagerService.proto'
    'Services\Jobs\JobBuilderService.proto'
    'Services\Jobs\JobsStateService.proto'
    'Services\Jobs\JobStateService.proto'
    'Services\Jobs\TaskStateService.proto'
    'Services\Maps\MapService.proto'
    'Services\Scheduling\SchedulingService.proto'
    'Services\Servicing\ServicingService.proto'
)

$goPackageMappings = Get-ChildItem -LiteralPath $protoPath -Recurse -Filter '*.proto' |
    ForEach-Object {
        $relativePath = [System.IO.Path]::GetRelativePath($protoPath, $_.FullName).Replace('\', '/')
        $relativeDirectory = [System.IO.Path]::GetDirectoryName($relativePath).Replace('\', '/').ToLowerInvariant()
        "M$relativePath=github.com/Guidance-Automation/GAAPICommon/gen/$relativeDirectory"
    }
$generatorOptions = @('output_format=json') + $goPackageMappings
$generatorOptionArgument = "--openapiv2_opt=$($generatorOptions -join ',')"

New-Item -ItemType Directory -Path $outputPath -Force | Out-Null

foreach ($document in $documents) {
    $protoFile = $document.Replace('\', '/')
    $documentRelativePath = [System.IO.Path]::ChangeExtension($document, 'swagger.json')
    $documentPath = Join-Path $outputPath $documentRelativePath

    & $protocPath `
        "--proto_path=$protoPath" `
        "--proto_path=$commonProtosPath" `
        "--proto_path=$(Join-Path $grpcToolsPath 'build\native\include')" `
        "--plugin=protoc-gen-openapiv2=$pluginPath" `
        "--openapiv2_out=$outputPath" `
        $generatorOptionArgument `
        $protoFile

    if ($LASTEXITCODE -ne 0) {
        throw "OpenAPI generation failed for $protoFile with exit code $LASTEXITCODE."
    }

    if (-not (Test-Path -LiteralPath $documentPath)) {
        throw "Expected generated OpenAPI document was not found: $documentPath"
    }

}

Write-Host "Generated $($documents.Count) OpenAPI documents in $outputPath."
