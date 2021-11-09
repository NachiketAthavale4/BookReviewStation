using Book.DataLayer.DataModels;
using Book.DataLayer.Migrations;
using System.Data.Entity;

namespace Book.DataLayer.Context
{
    public class BookContext : DbContext
    {
        public BookContext() : base("BookDbCon")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BookContext, Configuration>());
        }

        public DbSet<BookDetails> Books { get; set; }
        public DbSet<ISBNDetails> ISBNDetails { get; set; }
        public DbSet<PhysicalBookDetails> PhysicalBookDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewType> ReviewTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}