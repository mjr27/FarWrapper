# FarWrapper
mc_wrapper.sh analogue for Far Manager. Provides a way to easily use far manager for folder navigation in powershell.

Requires [FarNet](https://github.com/nightroman/FarNet)

## Disclaimer

This plugin is made for my own needs. It serves them well. So i suppose, this is the only and final version.

## How it works

When invoked, plugin writes current directory of active panel to  a file, specified in `FARWRAPPER_PATH` environment variable.

Shell can read this file to navigate to the last known folder.

This plugin is supposed to be called upon exit.

## Usage

Install [FarNet](https://github.com/nightroman/FarNet)

Build this project as administrator and dll file will be property placed in your far plugins folder. Or just download dll from releases and place it in  `C:\Program Files\Far Manager\FarNet\Modules\FarWrapper\FarWrapper.dll`

To utilize folder navigation the following sample powershell function is provided. Feel free to modify it by your needs.

```powershell
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
```

## Quit macro

To be able to quit with F10, place this in `%APPDATA%\Far Manager\Profile\Macros\scripts\FarWrapper.macro.lua`

```lua
Macro {
area="Common"; key="F10"; description="FarWrapper: quit"; action=function()
Keys("F11 w")
Keys("F10")
end;
}
```