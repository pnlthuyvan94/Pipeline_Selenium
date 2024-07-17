param(
    [string]$projectDir
)
$processNames = @("chromedriver.exe", "geckodriver.exe", "IEDriverServer.exe", "MicrosoftWebDriver.exe")
$executablePath = $projectDir + "Pipeline.Testing\bin\x64\Debug\"

# Get a list of all running processes with the specified names
$processes = Get-WmiObject -Class Win32_Process
$processes = $processes | Where-Object { $_.Name -in $processNames }

# Check if any processes are found with the specified names
if ($processes) {
    foreach ($process in $processes) {
        $processPath = $process.ExecutablePath
        $id = $process.ProcessId
        
        if ($processPath -like "$executablePath*") {
            Write-Host "Executable path of $processName (ID: $id): $processPath"
            # Kill the process by its PID
            taskkill /PID $id /F
            Write-Host "Process $processName (ID: $id) has been terminated."
        }
    }
} 