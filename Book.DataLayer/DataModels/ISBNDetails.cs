using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book.DataLayer.DataModels
{
    [Table("ISBNDetails")]
    public class ISBNDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ISBNId { get; set; }
        public string ISBN10 { get; set; }
        public string ISBN13 { get; set; }
    }
}