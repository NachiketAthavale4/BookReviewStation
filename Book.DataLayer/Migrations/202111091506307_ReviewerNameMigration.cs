namespace Book.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewerNameMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Review", "ReviewerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Review", "ReviewerName");
        }
    }
}
