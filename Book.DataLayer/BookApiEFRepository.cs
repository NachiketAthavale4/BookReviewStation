using Book.Abstract.Interfaces;
using Book.DataLayer.Context;
using Book.DataLayer.Mapper;
using Book.Domain.Models;
using System.Data.Entity;
using System.Linq;

namespace Book.DataLayer
{
    public class BookEFRepository : IBookRepository
    {
        private readonly ISBNRepository iSBNRepository = null;

        private readonly PhysicalBookRepository physicalBookRepository = null;

        private readonly BookRepository bookRepository = null;

        public BookEFRepository()
        {
            this.iSBNRepository = new ISBNRepository();
            this.physicalBookRepository = new PhysicalBookRepository();
            this.bookRepository = new BookRepository();
        }

        public BookDetails GetBookByBookId(long bookId)
        {
            using (var context = new BookContext())
            {
                var bookDetails = context.Books.Include(b => b.ISBNDetails).Include(c => c.PhysicalBookDetails)
                                               .FirstOrDefault(book => book.BookId == bookId);
                return bookDetails.MapDataBookDetails();
            }
        }

        public BookDetails UpdateBookDetails(BookDetails bookDetails)
        {
            using (var context = new BookContext())
            {
                var bookDtoDetails = this.bookRepository.GetBookDetailsIncludingISBNAndPhysicalByBookId(context, bookDetails?.BookId);

                if (bookDtoDetails == null)
                {
                    return null;
                }

                // ISBN Update flow.

                this.UpdateISBNDetailsForABook(context, bookDtoDetails, bookDetails.ISBNDetails);

                // Physical Book Update Flow.

                this.UpdatePhysicalBookDetailsForABook(context, bookDtoDetails, bookDetails.PhysicalBookDetails);

                this.bookRepository.UpdateBookDetails(context, bookDtoDetails, bookDetails);

                bookDtoDetails = this.bookRepository.GetBookDetailsIncludingISBNAndPhysicalByBookId(context, bookDetails.BookId);
                return bookDtoDetails.MapDataBookDetails();
            }
        }

        private void UpdateISBNDetailsForABook(BookContext context, DataModels.BookDetails bookDetailsDto, ISBNDetails isbnDetails)
        {
            var isbnDetailsDto = this.iSBNRepository.GetISBNDetailsByISBNId(context, bookDetailsDto.ISBNDetails?.ISBNId);

            if (isbnDetails == null && isbnDetailsDto != null)
            {
                this.iSBNRepository.RemoveISBNDetails(context, isbnDetailsDto);
            }
            else if (isbnDetails != null && isbnDetailsDto == null)
            {
                var isbnDtoAdd = this.iSBNRepository.AddISBNDetail(context, isbnDetails);
                bookDetailsDto.ISBNDetails = isbnDtoAdd;
            }
            else
            {
                this.iSBNRepository.UpdateISBNDetails(context, isbnDetails, isbnDetailsDto);
            }
        }

        private void UpdatePhysicalBookDetailsForABook(
            BookContext context, 
            DataModels.BookDetails bookDetailsDto, 
            PhysicalBookDetails physicalBookDetails)
        {
            var physicalBookDto = this.physicalBookRepository.GetPhysicalBookDetailsByPhysicalBookId(
                                                    context,
                                                    bookDetailsDto.PhysicalBookDetails?.PhysicalBookId);

            if (physicalBookDetails == null && physicalBookDto != null)
            {
                this.physicalBookRepository.RemovePhysicalBookDetails(context, physicalBookDto);
            }
            else if (physicalBookDetails != null && physicalBookDto == null)
            {
                var physicalDtoAdd = this.physicalBookRepository.AddPhysicalBookDetail(context, physicalBookDetails);
                bookDetailsDto.PhysicalBookDetails = physicalDtoAdd;
            }
            else
            {
                this.physicalBookRepository.UpdatePhysicalBookDetails(context, physicalBookDetails, physicalBookDto);
            }
        }
    }
}