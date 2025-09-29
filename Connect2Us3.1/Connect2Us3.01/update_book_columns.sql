-- SQL script to update Book table columns to match the current model

-- Step 1: Add the missing columns to the Books table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = 'RentalPrice' AND object_id = OBJECT_ID('dbo.Books'))
BEGIN
    ALTER TABLE dbo.Books ADD RentalPrice DECIMAL(18,2) NULL;
    PRINT 'Added RentalPrice column';
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = 'IsRentable' AND object_id = OBJECT_ID('dbo.Books'))
BEGIN
    ALTER TABLE dbo.Books ADD IsRentable BIT NOT NULL DEFAULT 0;
    PRINT 'Added IsRentable column';
END

-- Step 2: Handle the Stock to StockLevel conversion
IF EXISTS (SELECT * FROM sys.columns WHERE name = 'Stock' AND object_id = OBJECT_ID('dbo.Books'))
AND NOT EXISTS (SELECT * FROM sys.columns WHERE name = 'StockLevel' AND object_id = OBJECT_ID('dbo.Books'))
BEGIN
    -- Add StockLevel column first
    ALTER TABLE dbo.Books ADD StockLevel INT NOT NULL DEFAULT 0;
    PRINT 'Added StockLevel column';
    
    -- Copy data from Stock to StockLevel
    UPDATE dbo.Books SET StockLevel = Stock;
    PRINT 'Migrated data from Stock to StockLevel';
    
    -- Drop the old Stock column
    ALTER TABLE dbo.Books DROP COLUMN Stock;
    PRINT 'Dropped old Stock column';
END
ELSE IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = 'StockLevel' AND object_id = OBJECT_ID('dbo.Books'))
BEGIN
    -- If Stock doesn't exist and StockLevel doesn't exist, just add StockLevel
    ALTER TABLE dbo.Books ADD StockLevel INT NOT NULL DEFAULT 0;
    PRINT 'Added StockLevel column (no Stock to migrate)';
END

-- Step 3: Update existing records to set reasonable defaults
UPDATE dbo.Books SET IsRentable = 0 WHERE IsRentable IS NULL;

PRINT 'Database schema update completed successfully!';