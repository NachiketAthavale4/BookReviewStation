namespace Book.Abstract.Interfaces
{
    using Book.Domain.Models;
    using System.Collections.Generic;

    public interface IReviewRepository
    {
        IEnumerable<Review> GetCriticsReviewsForBook(long bookId, int pageSize, int pageNumber);
        IEnumerable<Review> GetUserReviewsForBook(long bookId, int pageSize, int pageNumber);
        Review AddReview(Review review);
        Review GetReview(long reviewId);
        Review UpdateReview(Review review);
        Review DeleteReview(long reviewId);
    }
}