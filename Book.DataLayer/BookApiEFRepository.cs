using Book.Abstract.Interfaces;
using Book.DataLayer.Context;
using Book.DataLayer.Mapper;
using Book.Domain.Models;
using System.Collections.Generic;
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

        public IEnumerable<BookDetails> GetBooks(int pageSize, int pageNumber)
        {
            using (var context = new BookContext())
            {
                var bookList = context.Books.Include(b => b.ISBNDetails).Include(c => c.PhysicalBookDetails)
                                            .OrderBy(x => x.Name)
                                            .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return bookList.MapDataModelToBookDetailList();
            }
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

        public BookDetails CreateBookDetails(BookDetails bookDetails)
        {
            using (var context = new BookContext())
            {
                DataModels.BookDetails bookDetailsDto = new DataModels.BookDetails
                {
                    Name = bookDetails.Name,
                    AuthorName = bookDetails.AuthorName,
                    Description = bookDetails.Description,
                    ImageData = bookDetails.ImageData,
                    ImageMimeType = bookDetails.ImageMimeType,
                    Publisher = bookDetails.Publisher,
                    ISBNDetails = bookDetails.ISBNDetails.MapDomainISBNToDataISBN(),
                    PhysicalBookDetails = bookDetails.PhysicalBookDetails.MapDomainPhysicalBookToDataPhysicalBook()
                };

                context.Books.Add(bookDetailsDto);
                context.Entry(bookDetailsDto).State = EntityState.Added;
                context.SaveChanges();

                return bookDetailsDto.MapDataBookDetails();
            }
        }

        public BookDetails RemoveBookDetails(long bookId)
        {
            using (var context = new BookContext())
            {
                var bookDetail = context.Books.Include(b => b.ISBNDetails).Include(c => c.PhysicalBookDetails)
                                               .FirstOrDefault(book => book.BookId == bookId);

                var isbnDetailsDto = bookDetail.ISBNDetails;
                var physicalBookDetailsDto = bookDetail.PhysicalBookDetails;

                if (bookDetail == null)
                {
                    return null;
                }

                context.Books.Remove(bookDetail);
                context.Entry(bookDetail).State = EntityState.Deleted;
                context.SaveChanges();

                this.iSBNRepository.RemoveISBNDetails(context, isbnDetailsDto);
                this.physicalBookRepository.RemovePhysicalBookDetails(context, physicalBookDetailsDto);

                return bookDetail.MapDataBookDetails();
            }
        }
    }
}