namespace TPshop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContentToMax : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Content", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Content", c => c.String(maxLength: 500));
        }
    }
}
