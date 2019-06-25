# This should be put in user powershell profile
function far {
    Param(
        [string] $Location,
        [string] $OtherLocation
    )
    $tempFile = New-TemporaryFile
    $Env:FARWRAPPER_PATH = $tempFile.FullName;

    $far_root = 'C:\Program Files\Far Manager\Far.exe'
    if (! $Location) {
        $Location = Get-Location
    }
    if (! $OtherLocation) {
        $OtherLocation = $Location
    }

    &  $far_root $Location  $OtherLocation

    $path = Get-Content -Path $Env:FARWRAPPER_PATH
    Remove-Item -ErrorAction Ignore Env:FARWRAPPER_PATH  

    if ("" -eq $path -or $null -eq $path) {
        return
    }
    Set-Location $path
}