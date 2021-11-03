using Book.DataLayer.Context;
using Book.DataLayer.DataModels;
using System.Linq;
using System.Data.Entity;

namespace Book.DataLayer
{
    internal class BookRepository
    {
        internal BookDetails GetBookDetailsIncludingISBNAndPhysicalByBookId(BookContext context, long? bookId)
        {
            if (!bookId.HasValue)
            {
                return null;
            }

            return context.Books.Include(b => b.ISBNDetails).Include(c => c.PhysicalBookDetails)
                                        .FirstOrDefault(book => book.BookId == bookId);
        }

        internal void UpdateBookDetails(BookContext context, BookDetails bookDetailsDto, Domain.Models.BookDetails bookDetails)
        {
            bookDetailsDto.AuthorName = bookDetails.AuthorName;
            bookDetailsDto.Name = bookDetails.Name;
            bookDetailsDto.Publisher = bookDetails.Publisher;
            bookDetailsDto.ImageData = bookDetails.ImageData;
            bookDetailsDto.ImageMimeType = bookDetails.ImageMimeType;
            bookDetailsDto.Description = bookDetails.Description;

            context.Entry(bookDetailsDto).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}