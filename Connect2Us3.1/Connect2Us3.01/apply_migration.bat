@echo off
echo Applying Entity Framework migration to add Book rental fields...
echo.

REM Set up Visual Studio environment
call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsDevCmd.bat"

REM Build the project first
echo Building project...
MSBuild.exe Connect2Us3.01.csproj /verbosity:minimal
if errorlevel 1 (
    echo Build failed! Please check the errors above.
    pause
    exit /b 1
)

echo.
echo Project built successfully.
echo.
echo IMPORTANT: Please run the following SQL script on your database to apply the migration:
echo   update_book_columns.sql
echo.
echo This script will:
echo - Add RentalPrice column to Books table
echo - Add IsRentable column to Books table  
echo - Rename Stock column to StockLevel
echo - Set appropriate default values
echo.
echo After running the SQL script, the application should work correctly.
echo.
pause