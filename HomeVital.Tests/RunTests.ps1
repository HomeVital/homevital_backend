# Script to run test files in the HomeVital.Tests project one by one

Write-Host "=======================================================" -ForegroundColor Cyan
Write-Host "         Running HomeVital Test Suite                  " -ForegroundColor Cyan
Write-Host "         (Sequential File Execution)                   " -ForegroundColor Cyan
Write-Host "=======================================================" -ForegroundColor Cyan
Write-Host "`nInitializing test data..." -ForegroundColor Yellow

# Run the data initialization logic
try {
    dotnet run --project ..\HomeVital.API\HomeVital.API.csproj -- data -- test
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Data initialization failed with exit code $LASTEXITCODE" -ForegroundColor Red
        exit $LASTEXITCODE
    }
    Write-Host "Data initialization completed successfully." -ForegroundColor Green
}
catch {
    Write-Host "Error during data initialization: $_" -ForegroundColor Red
    exit 1
}

# Determine the current directory and project path
$currentDir = Get-Location
$currentDirName = Split-Path -Leaf $currentDir
$testProjectPath = "."

# If we're in the parent directory, use the HomeVital.Tests subdirectory
if ($currentDirName -ne "HomeVital.Tests") {
    $testProjectPath = ".\HomeVital.Tests"
    
    if (-not (Test-Path $testProjectPath)) {
        Write-Host "Test project not found at: $testProjectPath" -ForegroundColor Red
        Write-Host "Please run this script from the solution root or the HomeVital.Tests directory." -ForegroundColor Yellow
        exit 1
    }
}

try {
    # Build the test project first to ensure everything is compiled
    Write-Host "`nBuilding test project..." -ForegroundColor Yellow
    dotnet build $testProjectPath
    
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Build failed with exit code $LASTEXITCODE" -ForegroundColor Red
        exit $LASTEXITCODE
    }

    # Define the list of test classes to run in sequence
    $testClasses = @(
        "PatientTest", 
        "TeamTest",
        "WorkerTest",
        "VitalTest",
        "PatientPlanTests",
        "OxygenSatTests",
        "BloodSugarTests",
        "BloodPressureTests",
        "BodyWeightTests",
        "BodyTempTests",
        "MeasurementsTests"
    )

    $overallSuccess = $true
    $summary = @()

    # Run each test class in sequence
    foreach ($testClass in $testClasses) {
        $separator = "=" * 50
        Write-Host "`n$separator" -ForegroundColor Blue
        Write-Host "  Running Test Class: $testClass" -ForegroundColor Blue
        Write-Host "$separator" -ForegroundColor Blue
        
        # Create filter for the specific test class - use the fully qualified name pattern
        $filter = "FullyQualifiedName~HomeVital.Tests.$testClass"
        
        # Run tests for this file with detailed diagnostics
        Write-Host "Starting tests for $testClass..." -ForegroundColor Yellow
        dotnet test $testProjectPath --filter "$filter" --verbosity minimal
        
        # Track results
        if ($LASTEXITCODE -eq 0) {
            Write-Host "`n✅ $testClass tests completed successfully!" -ForegroundColor Green
            $summary += "✅ $testClass"
        } else {
            Write-Host "`n❌ $testClass tests failed with exit code $LASTEXITCODE" -ForegroundColor Red
            $summary += "❌ $testClass"
            $overallSuccess = $false
        }
        
        Write-Host "`nCompleted test class: $testClass" -ForegroundColor Yellow
        Write-Host "$separator`n" -ForegroundColor Blue
        
        # Optional: Add a small pause between test files to make output more readable
        # Start-Sleep -Seconds 2
    }

    # Show overall summary
    Write-Host "`nTest Execution Summary:" -ForegroundColor Yellow
    foreach ($result in $summary) {
        Write-Host "- $result" -ForegroundColor White
    }
    
    if ($overallSuccess) {
        Write-Host "`nAll test classes executed successfully!" -ForegroundColor Green
    } else {
        Write-Host "`nSome test classes failed. Check the output above for details." -ForegroundColor Red
    }
}
catch {
    Write-Host "Error executing tests: $_" -ForegroundColor Red
    exit 1
}

Write-Host "`n=======================================================" -ForegroundColor Cyan
Write-Host "         Test Run Completed                           " -ForegroundColor Cyan
Write-Host "=======================================================" -ForegroundColor Cyan