param($installPath, $toolsPath, $package, $project)

$file = Join-Path (Join-Path $toolsPath 'phantomjs') 'phantomjs.exe' | Get-ChildItem

$project.ProjectItems.Item($file.Name).Delete()