namespace Book.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        AuthorName = c.String(),
                        Publisher = c.String(),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        ISBNDetails_ISBNId = c.Long(),
                        PhysicalBookDetails_PhysicalBookId = c.Long(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.ISBNDetails", t => t.ISBNDetails_ISBNId)
                .ForeignKey("dbo.PhysicalBookDetails", t => t.PhysicalBookDetails_PhysicalBookId)
                .Index(t => t.ISBNDetails_ISBNId)
                .Index(t => t.PhysicalBookDetails_PhysicalBookId);
            
            CreateTable(
                "dbo.ISBNDetails",
                c => new
                    {
                        ISBNId = c.Long(nullable: false, identity: true),
                        ISBN10 = c.String(),
                        ISBN13 = c.String(),
                    })
                .PrimaryKey(t => t.ISBNId);
            
            CreateTable(
                "dbo.PhysicalBookDetails",
                c => new
                    {
                        PhysicalBookId = c.Long(nullable: false, identity: true),
                        BookWeight = c.Int(nullable: false),
                        NumberOfPages = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Length = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhysicalBookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Book", "PhysicalBookDetails_PhysicalBookId", "dbo.PhysicalBookDetails");
            DropForeignKey("dbo.Book", "ISBNDetails_ISBNId", "dbo.ISBNDetails");
            DropIndex("dbo.Book", new[] { "PhysicalBookDetails_PhysicalBookId" });
            DropIndex("dbo.Book", new[] { "ISBNDetails_ISBNId" });
            DropTable("dbo.PhysicalBookDetails");
            DropTable("dbo.ISBNDetails");
            DropTable("dbo.Book");
        }
    }
}
