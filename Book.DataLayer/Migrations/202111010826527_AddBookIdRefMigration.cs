namespace Book.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookIdRefMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "Description");
        }
    }
}
