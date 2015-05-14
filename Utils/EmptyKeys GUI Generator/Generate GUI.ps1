$path = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent
cd $path
& $path\ekUiGen.exe --input="$path\..\..\Final Game\Shared Code\GUI\Generated GUI" --output="$path\..\..\Final Game\Shared Code\GUI\Generated GUI" --oa="$path\..\..\Final Game\Content" --rm=MonoGame