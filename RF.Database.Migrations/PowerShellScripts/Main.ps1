$migrationName = & ".\GenerateMigrations.ps1"
if($migrationName){
	& .\GenerateSQL.ps1 -MigrationName $migrationName
}