Param(
    [Parameter(Mandatory=$True)]
    [string]$server
)

$logoutUrl = "$server/Account/Logout"
$uri = "$server/Account/Login?ReturnUrl=/"

Invoke-WebRequest $uri -UseBasicParsing | Foreach {
    $_.Content.Contains("Expense Report System") | Foreach {
        if(-Not($_)) {
            Throw "Web smoke test failed"
        }
        else {
            Write-Host "Web smoke test passed."
        }
    }
}
