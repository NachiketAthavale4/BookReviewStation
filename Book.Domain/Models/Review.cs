using System.ComponentModel.DataAnnotations;

namespace Book.Domain.Models
{
    public class Review : BaseError
    {
        public long ReviewId { get; set; }
        public long BookId { get; set; }
        public ReviewType Type { get; set; }
        public string ReviewDescription { get; set; }
        public string ReviewTitle { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }
        public string ReviewingOrganization { get; set; }
        public string ReviewerName { get; set; }

        public Review()
        {

        }

        public Review(string errorCodeDescription, string errorCode) : base(errorCodeDescription, errorCode)
        {

        }
    }
}