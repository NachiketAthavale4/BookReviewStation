namespace Book.DataLayer
{
    using Book.Abstract.Interfaces;
    using Book.Configuration.ApiIntegration;
    using Book.DataLayer.Context;
    using Book.DataLayer.Mapper;
    using Book.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class ReviewRepository : IReviewRepository
    {
        private readonly BookRepository bookRepository = null;

        public ReviewRepository()
        {
            this.bookRepository = new BookRepository();
        }

        [ServiceException(errorCodeValue: "Review400")]
        public Review AddReview(Review review)
        {
            using (var context = new BookContext())
            {
                var reviewDataModel = review.MapDomainReviewToDataReview();
                reviewDataModel.Book = this.bookRepository.GetBookDetailsIncludingISBNAndPhysicalByBookId(context, review.BookId);

                var reviewTypeExists =
                    context.ReviewTypes.Any(reviewType => reviewType.TypeName == reviewDataModel.ReviewType.TypeName);

                if (reviewTypeExists)
                {
                    var reviewTypeDto = 
                        context.ReviewTypes.FirstOrDefault(reviewType => reviewType.TypeName == reviewDataModel.ReviewType.TypeName);
                    reviewDataModel.ReviewType = reviewTypeDto;
                }

                context.Reviews.Add(reviewDataModel);
                context.Entry(reviewDataModel).State = EntityState.Added;
                context.SaveChanges();

                return reviewDataModel.MapDataReviewToDomainReview();
            }
        }

        [ServiceException(errorCodeValue: "Review400")]
        public IEnumerable<Review> GetCriticsReviewsForBook(long bookId, int pageSize, int pageNumber)
        {
            using (var context = new BookContext())
            {
                var criticalReviewsDto = context.Reviews.Include(x => x.ReviewType).Include(x => x.Book)
                                                .Where(review => review.Book.BookId == bookId
                                                                 && review.ReviewType.TypeName == ReviewType.Critical.ToString())
                                                .OrderBy(r => r.ReviewId)
                                                .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                                                .ToList();

                if (criticalReviewsDto == null)
                {
                    return null;
                }

                var criticalReviewList = new List<Review>();

                foreach (var criticalReview in criticalReviewsDto)
                {
                    criticalReviewList.Add(criticalReview.MapDataReviewToDomainReview());
                }

                return criticalReviewList;
            }
        }

        [ServiceException(errorCodeValue: "Review400")]
        public Review GetReview(long reviewId)
        {
            using (var context = new BookContext())
            {
                return context.Reviews.Include(x => x.ReviewType).Include(x => x.Book)
                                      .FirstOrDefault(review => review.ReviewId == reviewId).MapDataReviewToDomainReview();
            }
        }

        [ServiceException(errorCodeValue: "Review400")]
        public IEnumerable<Review> GetUserReviewsForBook(long bookId, int pageSize, int pageNumber)
        {
            using (var context = new BookContext())
            {
                var userReviewsDto = context.Reviews.Include(x => x.ReviewType).Include(x => x.Book)
                                                .Where(review => review.Book.BookId == bookId
                                                                 && review.ReviewType.TypeName == ReviewType.UserGenerated.ToString())
                                                .OrderBy(r => r.ReviewId)
                                                .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                                                .ToList();

                if (userReviewsDto == null || userReviewsDto.Count == 0)
                {
                    return null;
                }

                var criticalReviewList = new List<Review>();

                foreach (var userReview in userReviewsDto)
                {
                    criticalReviewList.Add(userReview.MapDataReviewToDomainReview());
                }

                return criticalReviewList;
            }
        }

        [ServiceException(errorCodeValue: "Review400")]
        public Review UpdateReview(Review review)
        {
            using (var context = new BookContext())
            {
                var reviewDto = context.Reviews.Include(x => x.ReviewType).Include(x => x.Book)
                                               .FirstOrDefault(r => r.ReviewId == review.ReviewId);

                if (reviewDto == null)
                {
                    return null;
                }

                reviewDto.ReviewingOrganization = review.ReviewingOrganization;
                reviewDto.ReviewTitle = review.ReviewTitle;
                reviewDto.ReviewerName = review.ReviewerName;
                reviewDto.ReviewDescription = review.ReviewDescription;
                reviewDto.Rating = review.Rating;

                context.Entry(reviewDto).State = EntityState.Modified;
                context.SaveChanges();

                return reviewDto.MapDataReviewToDomainReview();
            }
        }

        [ServiceException(errorCodeValue: "Review400")]
        public Review DeleteReview(long reviewId)
        {
            using (var context = new BookContext())
            {
                var reviewDto = context.Reviews.Include(x => x.ReviewType).Include(x => x.Book)
                                               .FirstOrDefault(r => r.ReviewId == reviewId);

                if (reviewDto == null)
                {
                    return null;
                }

                context.Reviews.Remove(reviewDto);
                context.Entry(reviewDto).State = EntityState.Deleted;
                context.SaveChanges();

                return reviewDto.MapDataReviewToDomainReview();
            }
        }
    }
}