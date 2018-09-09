namespace TPshop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteProductDetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductDetails", "ProductID", "dbo.Products");
            DropIndex("dbo.ProductDetails", new[] { "ProductID" });
            AddColumn("dbo.Products", "CPU", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "Ram", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "Bus", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "RamMax", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "VGA", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "Storage", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "StorageType", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "Monitor", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "Resolution", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "BatteryCapacity", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "BatteryCell", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "Webcam", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "Size", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "Weight", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "OS", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "Warranty", c => c.String(maxLength: 100));
            DropTable("dbo.ProductDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductDetails",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        CPU = c.String(maxLength: 100),
                        Ram = c.String(maxLength: 100),
                        Bus = c.String(maxLength: 100),
                        RamMax = c.String(maxLength: 100),
                        VGA = c.String(maxLength: 100),
                        Storage = c.String(maxLength: 100),
                        StorageType = c.String(maxLength: 100),
                        Monitor = c.String(maxLength: 100),
                        Resolution = c.String(maxLength: 100),
                        BatteryCapacity = c.String(maxLength: 100),
                        BatteryCell = c.String(maxLength: 100),
                        Webcam = c.String(maxLength: 100),
                        Size = c.String(maxLength: 100),
                        Weight = c.String(maxLength: 100),
                        OS = c.String(maxLength: 100),
                        Warranty = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ProductID);
            
            DropColumn("dbo.Products", "Warranty");
            DropColumn("dbo.Products", "OS");
            DropColumn("dbo.Products", "Weight");
            DropColumn("dbo.Products", "Size");
            DropColumn("dbo.Products", "Webcam");
            DropColumn("dbo.Products", "BatteryCell");
            DropColumn("dbo.Products", "BatteryCapacity");
            DropColumn("dbo.Products", "Resolution");
            DropColumn("dbo.Products", "Monitor");
            DropColumn("dbo.Products", "StorageType");
            DropColumn("dbo.Products", "Storage");
            DropColumn("dbo.Products", "VGA");
            DropColumn("dbo.Products", "RamMax");
            DropColumn("dbo.Products", "Bus");
            DropColumn("dbo.Products", "Ram");
            DropColumn("dbo.Products", "CPU");
            CreateIndex("dbo.ProductDetails", "ProductID");
            AddForeignKey("dbo.ProductDetails", "ProductID", "dbo.Products", "ID");
        }
    }
}
