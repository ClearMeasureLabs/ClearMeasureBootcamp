#
# PostDeploy.ps1
#
$DatabaseServer = $OctopusParameters["DatabaseServer"]
$DatabaseName = $OctopusParameters["DatabaseName"]
$DatabaseAction = $OctopusParameters["DatabaseAction"]
& .\scripts\AliaSQL.exe $DatabaseAction $DatabaseServer $DatabaseName .\scripts