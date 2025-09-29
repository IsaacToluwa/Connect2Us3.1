namespace Connect2Us3._01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookRentalFields : DbMigration
    {
        public override void Up()
        {
            // Add RentalPrice column (nullable decimal)
            AddColumn("dbo.Books", "RentalPrice", c => c.Decimal(precision: 18, scale: 2));
            
            // Add IsRentable column (boolean with default false)
            AddColumn("dbo.Books", "IsRentable", c => c.Boolean(nullable: false, defaultValue: false));
            
            // Note: StockLevel is already handled by renaming Stock column
            // This is done via SQL script for better control
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "IsRentable");
            DropColumn("dbo.Books", "RentalPrice");
            // Note: StockLevel would be renamed back to Stock in Down migration
        }
    }
}