# Database Schema Fix Instructions

## Problem
Your application is failing with the error:
- Invalid column name 'RentalPrice'
- Invalid column name 'StockLevel' 
- Invalid column name 'IsRentable'

This happens because your `Book` model has these properties but they don't exist in the database.

## Solution

### Option 1: Automatic Fix (Recommended)

1. **Run the PowerShell script:**
   ```powershell
   .\fix_database_schema.ps1
   ```

2. **Or run the Batch script:**
   ```cmd
   fix_database_schema.bat
   ```

### Option 2: Manual Fix in SQL Server Management Studio

1. **Open SQL Server Management Studio (SSMS)**

2. **Connect to your LocalDB instance:**
   - Server name: `(LocalDb)\MSSQLLocalDB`
   - Authentication: Windows Authentication

3. **Select the Connect2Us3_01 database**

4. **Execute the SQL script:**
   - Open `update_book_columns.sql`
   - Click Execute (or press F5)

### Option 3: Command Line (if sqlcmd is available)

```cmd
sqlcmd -S (LocalDb)\MSSQLLocalDB -d Connect2Us3_01 -i update_book_columns.sql
```

## What the Script Does

The SQL script will:
1. ✅ Add `RentalPrice` column (nullable decimal)
2. ✅ Add `IsRentable` column (boolean, defaults to false)
3. ✅ Rename `Stock` column to `StockLevel`
4. ✅ Preserve existing data in the Stock column
5. ✅ Set appropriate default values

## Verification

After running the script, you can verify the changes:

```sql
-- Check the Books table structure
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Books'
ORDER BY ORDINAL_POSITION;
```

You should see:
- `RentalPrice` (decimal, nullable)
- `StockLevel` (int, not nullable)
- `IsRentable` (bit, not nullable)

## Troubleshooting

### If sqlcmd is not found:
- Install SQL Server Command Line Utilities
- Or use SQL Server Management Studio (Option 2)

### If LocalDB is not running:
```cmd
sqllocaldb start MSSQLLocalDB
```

### If the database doesn't exist:
- Check your Web.config connection string
- Ensure the initial migration was applied

## After Fix

Once the script runs successfully, restart your application. The `BookBLL.GetAllBooks()` method should work without errors.