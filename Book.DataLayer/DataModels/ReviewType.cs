using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book.DataLayer.DataModels
{
    [Table("ReviewType")]
    public class ReviewType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewTypeId { get; set; }
        public string TypeName { get; set; }
    }
}