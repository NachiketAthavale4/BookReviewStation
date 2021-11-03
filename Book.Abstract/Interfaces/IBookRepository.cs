using Book.Domain.Models;

namespace Book.Abstract.Interfaces
{
    public interface IBookRepository
    {
        BookDetails GetBookByBookId(long bookId);
        BookDetails UpdateBookDetails(BookDetails bookDetails);
    }
}