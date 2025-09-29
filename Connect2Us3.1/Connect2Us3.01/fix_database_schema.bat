@echo off
echo ========================================
echo Database Schema Fix for Connect2Us3_01
echo ========================================
echo.
echo This script will add the missing columns to your Books table.
echo Database: Connect2Us3_01 on LocalDB
echo.

REM Check if sqlcmd is available
where sqlcmd >nul 2>nul
if %errorlevel% neq 0 (
    echo ERROR: sqlcmd is not available in your PATH.
    echo Please install SQL Server Command Line Utilities or use SQL Server Management Studio.
    echo.
    echo Alternatively, manually run the update_book_columns.sql script in SQL Server Management Studio.
    pause
    exit /b 1
)

echo Executing database schema update...
echo.

REM Execute the SQL script against the LocalDB database
sqlcmd -S (LocalDb)\MSSQLLocalDB -d Connect2Us3_01 -i update_book_columns.sql -o schema_update.log

if %errorlevel% equ 0 (
    echo.
    echo SUCCESS: Database schema has been updated!
    echo The following changes were applied:
    echo - Added RentalPrice column to Books table
    echo - Added IsRentable column to Books table  
    echo - Renamed Stock column to StockLevel
    echo.
    echo You can now run your application.
) else (
    echo.
    echo ERROR: Database update failed!
    echo Check schema_update.log for details.
    echo.
    echo You may need to:
    echo 1. Ensure SQL Server LocalDB is running
    echo 2. Check that the Connect2Us3_01 database exists
    echo 3. Run the update manually in SQL Server Management Studio
)

echo.
pause