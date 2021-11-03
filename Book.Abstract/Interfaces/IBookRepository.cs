using Book.Domain.Models;
using System.Collections.Generic;

namespace Book.Abstract.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<BookDetails> GetBooks(int pageSize, int pageNumber);
        BookDetails GetBookByBookId(long bookId);
        BookDetails UpdateBookDetails(BookDetails bookDetails);
        BookDetails CreateBookDetails(BookDetails bookDetails);
        BookDetails RemoveBookDetails(long bookId);
    }
}