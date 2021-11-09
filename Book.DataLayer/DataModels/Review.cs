using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataLayer.DataModels
{
    [Table("Review")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ReviewId { get; set; }
        public BookDetails Book { get; set; }
        public ReviewType ReviewType { get; set; }
        public string ReviewDescription { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewerName { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
        public string ReviewingOrganization { get; set; }
    }
}