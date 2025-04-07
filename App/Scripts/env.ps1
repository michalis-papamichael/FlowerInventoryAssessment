
$conn = [Environment]::GetEnvironmentVariable("FlowerInventoryAssessment:connection", "Machine")
$newConn="Server={server};Database=FlowerInventoryAssessment;Trusted_Connection=False;User ID={user};Password={psw};TrustServerCertificate=True"

if ( $conn -ne $newConn )
{
    [Environment]::SetEnvironmentVariable("FlowerInventoryAssessment:connection",$newConn, "Machine")
}

$connLogs = [Environment]::GetEnvironmentVariable("FlowerInventoryAssessmentLogs:connection", "Machine")
$newConnLogs="Server={server};Database=FlowerInventoryAssessmentLogs;Trusted_Connection=False;User ID={user};Password={psw};TrustServerCertificate=True"
if ( $connLogs -ne $newConnLogs )
{
    [Environment]::SetEnvironmentVariable("FlowerInventoryAssessmentLogs:connection",$newConnLogs , "Machine")
}