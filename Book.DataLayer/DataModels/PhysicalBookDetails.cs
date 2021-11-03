using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book.DataLayer.DataModels
{
    [Table("PhysicalBookDetails")]
    public class PhysicalBookDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PhysicalBookId { get; set; }
        public int BookWeight { get; set; }
        public int NumberOfPages { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
    }
}