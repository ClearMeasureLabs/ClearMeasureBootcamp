#
# PostDeploy.ps1
#
$path = $PSScriptRoot
$hibernateConfig = "$path\hibernate.cfg.xml"
$integratedSecurity = "Integrated Security=true"
$DatabaseServer = $OctopusParameters["DatabaseServer"]
$DatabaseName = $OctopusParameters["DatabaseName"]
$connection_string = "server=$DatabaseServer;database=$DatabaseName;$integratedSecurity;"

$filePath = $hibernateConfig
$xpath = "//e:property[@name = 'connection.connection_string']"
$value = $connection_string
$namespaces = @{"e" = "urn:nhibernate-configuration-2.2"}


[xml] $fileXml = Get-Content $filePath
    
if($namespaces -ne $null -and $namespaces.Count -gt 0) {
    $ns = New-Object Xml.XmlNamespaceManager $fileXml.NameTable
    $namespaces.GetEnumerator() | %{ $ns.AddNamespace($_.Key,$_.Value) }
    $node = $fileXml.SelectSingleNode($xpath,$ns)
} else {
    $node = $fileXml.SelectSingleNode($xpath)
}
    
if($node.NodeType -eq "Element") {
    $node.InnerText = $value
} else {
    $node.Value = $value
}

$fileXml.Save($filePath) 

Write-Host "DatabaseServer: $DatabaseServer"
Write-Host "DatabaseName: $DatabaseName"
Write-Host "-----------------------"

$nunitPath = ".\NUnit.Console.3.0.1\tools"
& $nunitPath\nunit3-console.exe .\ClearMeasure.Bootcamp.IntegrationTests.dll --where "cat == DataLoader" --noheader 