namespace BookApi.Controllers
{
    using Book.Abstract.Interfaces;
    using Book.Domain.Models;
    using BookApi.Extensions;
    using BookApi.Infrastructure;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http;

    [RoutePrefix("api/review")]
    public class ReviewController : ApiController
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        [HttpPost]
        [Route("add/critical")]
        [ApiExceptionFilter(errorCode: "Review300")]
        public IHttpActionResult AddCriticsReview(Review review, ReviewType reviewType = ReviewType.Critical)
        {
            if (review == null)
            {
                return BadRequest("Book Id cannot be 0 or less than 0");
            }

            review.Type = reviewType;

            var insertedReview = this.reviewRepository.AddReview(review);

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(insertedReview.JsonSerialize(), Encoding.UTF8, "application/json")
            };

            return ResponseMessage(httpResponse);
        }

        [HttpPost]
        [Route("add/user")]
        [ApiExceptionFilter(errorCode: "Review300")]
        public IHttpActionResult AddUserReview(Review review, ReviewType reviewType = ReviewType.UserGenerated)
        {
            if (review == null)
            {
                return BadRequest("Book Id cannot be 0 or less than 0");
            }

            review.Type = reviewType;

            var insertedReview = this.reviewRepository.AddReview(review);

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(insertedReview.JsonSerialize(), Encoding.UTF8, "application/json")
            };

            return ResponseMessage(httpResponse);
        }

        [HttpGet]
        [Route("{reviewId}")]
        [ApiExceptionFilter(errorCode: "Review300")]
        public IHttpActionResult GetReview(long reviewId)
        {
            if (reviewId <= 0)
            {
                return BadRequest("Review Id smaller or equal to 0");
            }

            var review = this.reviewRepository.GetReview(reviewId);

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(review.JsonSerialize(), Encoding.UTF8, "application/json")
            };

            return ResponseMessage(httpResponse);
        }

        [HttpGet]
        [Route("critical/book/{bookid}")]
        [ApiExceptionFilter(errorCode: "Review300")]
        public IHttpActionResult GetCriticalReviewsForBook(long bookId, int pageSize = 5, int pageNumber = 1)
        {
            if (bookId <= 0)
            {
                return BadRequest("Book Id smaller or equal to 0");
            }

            var criticalReviews = this.reviewRepository.GetCriticsReviewsForBook(bookId, pageSize, pageNumber);

            if (criticalReviews == null)
            {
                return NotFound();
            }

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(criticalReviews.JsonSerialize(), Encoding.UTF8, "application/json")
            };

            return ResponseMessage(httpResponse);
        }

        [HttpGet]
        [Route("user/book/{bookid}")]
        [ApiExceptionFilter(errorCode: "Review300")]
        public IHttpActionResult GetUserReviewsForBook(long bookId, int pageSize = 5, int pageNumber = 1)
        {
            if (bookId <= 0)
            {
                return BadRequest("Book Id smaller or equal to 0");
            }

            var userReviews = this.reviewRepository.GetUserReviewsForBook(bookId, pageSize, pageNumber);

            if (userReviews == null)
            {
                return NotFound();
            }

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(userReviews.JsonSerialize(), Encoding.UTF8, "application/json")
            };

            return ResponseMessage(httpResponse);
        }

        [HttpPut]
        [Route("update")]
        [ApiExceptionFilter(errorCode: "Review300")]
        public IHttpActionResult UpdateReview(Review review)
        {
            if (review == null)
            {
                return BadRequest("Review is empty");
            }

            var updatedReview = this.reviewRepository.UpdateReview(review);

            if (updatedReview == null)
            {
                return NotFound();
            }

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(updatedReview.JsonSerialize(), Encoding.UTF8, "application/json")
            };

            return ResponseMessage(httpResponse);
        }

        [HttpDelete]
        [Route("delete/{reviewId}")]
        [ApiExceptionFilter(errorCode: "Review300")]
        public IHttpActionResult DeleteReview(long reviewId)
        {
            if (reviewId <= 0)
            {
                return BadRequest("Review Id is smaller than 0");
            }

            var deletedReview = this.reviewRepository.DeleteReview(reviewId);

            if (deletedReview == null)
            {
                return NotFound();
            }

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(deletedReview.JsonSerialize(), Encoding.UTF8, "application/json")
            };

            return ResponseMessage(httpResponse);
        }
    }
}