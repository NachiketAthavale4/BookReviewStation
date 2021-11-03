using Book.Abstract.Interfaces;
using Book.Domain.Models;
using BookApi.Extensions;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace BookApi.Controllers
{
    [RoutePrefix("api/book")]
    public class BookController : ApiController
    {
        private readonly IBookRepository bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        [HttpGet]
        [Route("getAllBooks")]
        public IHttpActionResult GetBooks(int pageSize, int pageNumber)
        {
            if (pageSize == 0)
            {
                pageSize = 5;
            }

            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            var bookDetailsList = this.bookRepository.GetBooks(pageSize, pageNumber);

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(bookDetailsList.JsonSerialize(), Encoding.UTF8, "application/json")
            };

            return ResponseMessage(httpResponse);
        }

        [HttpGet]
        [Route("{bookId}")]
        public IHttpActionResult GetBookById(long bookId)
        {
            if (bookId == 0)
            {
                return BadRequest("Book Id is incorrect");
            }

            var bookDetails = this.bookRepository.GetBookByBookId(bookId);

            if (bookDetails == null)
            {
                return NotFound();
            }

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(bookDetails.JsonSerialize(), Encoding.UTF8, "application/json")
            };

            return ResponseMessage(httpResponse);
        }

        [HttpPut]
        [Route("update")]
        public IHttpActionResult UpdateBookDetails(BookDetails bookDetails)
        {
            if (bookDetails.BookId == 0)
            {
                return BadRequest("Book Id is incorrect");
            }

            var updatedBookDetails = this.bookRepository.UpdateBookDetails(bookDetails);

            if (updatedBookDetails == null)
            {
                return NotFound();
            }

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(bookDetails.JsonSerialize(), Encoding.UTF8, "application/json")
            };

            return ResponseMessage(httpResponse);
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateBook(BookDetails bookDetails)
        {
            var createBookDetails = this.bookRepository.CreateBookDetails(bookDetails);

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(createBookDetails.JsonSerialize(), Encoding.UTF8, "application/json")
            };

            return ResponseMessage(httpResponse);
        }

        [HttpDelete]
        [Route("remove/{bookId}")]
        public IHttpActionResult RemoveBook(long bookId)
        {
            if (bookId == 0)
            {
                return BadRequest("Book Id is incorrect");
            }

            var removeBookDetail = this.bookRepository.RemoveBookDetails(bookId);

            if (removeBookDetail == null)
            {
                return NotFound();
            }

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(removeBookDetail.JsonSerialize(), Encoding.UTF8, "application/json")
            };

            return ResponseMessage(httpResponse);
        }
    }
}