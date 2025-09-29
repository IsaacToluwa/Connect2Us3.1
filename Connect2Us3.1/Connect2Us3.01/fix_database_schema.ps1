# PowerShell script to fix the database schema for Connect2Us3_01

Write-Host "========================================" -ForegroundColor Green
Write-Host "Database Schema Fix for Connect2Us3_01" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host ""

# Check if sqlcmd is available
$sqlcmdAvailable = Get-Command sqlcmd -ErrorAction SilentlyContinue

if (-not $sqlcmdAvailable) {
    Write-Host "ERROR: sqlcmd is not available in your PATH." -ForegroundColor Red
    Write-Host "Please install SQL Server Command Line Utilities or use SQL Server Management Studio." -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Alternatively, manually run the update_book_columns.sql script in SQL Server Management Studio." -ForegroundColor Yellow
    Write-Host ""
    Write-Host "The SQL script is located at: $(Get-Location)\update_book_columns.sql" -ForegroundColor Cyan
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host "Executing database schema update..." -ForegroundColor Yellow
Write-Host ""

try {
    # Execute the SQL script against the LocalDB database
    sqlcmd -S "(LocalDb)\MSSQLLocalDB" -d "Connect2Us3_01" -i "update_book_columns.sql" -o "schema_update.log"
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host ""
        Write-Host "SUCCESS: Database schema has been updated!" -ForegroundColor Green
        Write-Host "The following changes were applied:" -ForegroundColor Green
        Write-Host "  ✓ Added RentalPrice column to Books table" -ForegroundColor Green
        Write-Host "  ✓ Added IsRentable column to Books table" -ForegroundColor Green
        Write-Host "  ✓ Renamed Stock column to StockLevel" -ForegroundColor Green
        Write-Host ""
        Write-Host "You can now run your application." -ForegroundColor Green
    } else {
        Write-Host ""
        Write-Host "ERROR: Database update failed!" -ForegroundColor Red
        Write-Host "Check schema_update.log for details." -ForegroundColor Yellow
        Write-Host ""
        Write-Host "You may need to:" -ForegroundColor Yellow
        Write-Host "  1. Ensure SQL Server LocalDB is running" -ForegroundColor Yellow
        Write-Host "  2. Check that the Connect2Us3_01 database exists" -ForegroundColor Yellow
        Write-Host "  3. Run the update manually in SQL Server Management Studio" -ForegroundColor Yellow
    }
} catch {
    Write-Host ""
    Write-Host "ERROR: An exception occurred while executing the database update." -ForegroundColor Red
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host ""
    Write-Host "Please run the SQL script manually in SQL Server Management Studio." -ForegroundColor Yellow
}

Write-Host ""
Read-Host "Press Enter to exit"