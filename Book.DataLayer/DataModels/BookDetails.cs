using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book.DataLayer.DataModels
{
    [Table("Book")]
    public class BookDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BookId { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public PhysicalBookDetails PhysicalBookDetails { get; set; }
        public ISBNDetails ISBNDetails { get; set; }
    }
}