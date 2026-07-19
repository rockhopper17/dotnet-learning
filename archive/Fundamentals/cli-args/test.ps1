dotnet run
if ($LastExitCode -eq 0)
{
    Write-Host "execution succeeded"
}
else
{
    Write-Host "execution failed"
}

Write-Host "Return value = " $LastExitCode