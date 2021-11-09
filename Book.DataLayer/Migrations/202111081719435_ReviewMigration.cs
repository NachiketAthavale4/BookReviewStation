namespace Book.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        ReviewId = c.Long(nullable: false, identity: true),
                        ReviewDescription = c.String(),
                        ReviewTitle = c.String(),
                        Rating = c.Int(nullable: false),
                        ReviewingOrganization = c.String(),
                        Book_BookId = c.Long(),
                        ReviewType_ReviewTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Book", t => t.Book_BookId)
                .ForeignKey("dbo.ReviewType", t => t.ReviewType_ReviewTypeId)
                .Index(t => t.Book_BookId)
                .Index(t => t.ReviewType_ReviewTypeId);
            
            CreateTable(
                "dbo.ReviewType",
                c => new
                    {
                        ReviewTypeId = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.ReviewTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "ReviewType_ReviewTypeId", "dbo.ReviewType");
            DropForeignKey("dbo.Review", "Book_BookId", "dbo.Book");
            DropIndex("dbo.Review", new[] { "ReviewType_ReviewTypeId" });
            DropIndex("dbo.Review", new[] { "Book_BookId" });
            DropTable("dbo.ReviewType");
            DropTable("dbo.Review");
        }
    }
}
