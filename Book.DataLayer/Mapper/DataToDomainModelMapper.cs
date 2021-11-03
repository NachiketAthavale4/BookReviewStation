using Book.Domain.Models;

namespace Book.DataLayer.Mapper
{
    public static class DataToDomainModelMapper
    {
        public static BookDetails MapDataBookDetails(this DataModels.BookDetails bookDetailsDto)
        {
            if (bookDetailsDto == null)
            {
                return null;
            }

            return new BookDetails
            {
                AuthorName = bookDetailsDto.AuthorName,
                Name = bookDetailsDto.Name,
                BookId = bookDetailsDto.BookId,
                Description = bookDetailsDto.Description,
                Publisher = bookDetailsDto.Publisher,
                ImageData = bookDetailsDto.ImageData,
                ImageMimeType = bookDetailsDto.ImageMimeType,
                ISBNDetails = MapISBNDataDetails(bookDetailsDto.ISBNDetails),
                PhysicalBookDetails = MapISBNDataDetails(bookDetailsDto.PhysicalBookDetails)
            };
        }

        private static ISBNDetails MapISBNDataDetails(DataModels.ISBNDetails iSBNDetails)
        {
            if (iSBNDetails == null)
            {
                return null;
            }

            return new ISBNDetails
            {
                ISBN10 = iSBNDetails.ISBN10,
                ISBN13 = iSBNDetails.ISBN13
            };
        }

        private static PhysicalBookDetails MapISBNDataDetails(DataModels.PhysicalBookDetails physicalBookDetails)
        {
            if (physicalBookDetails == null)
            {
                return null;
            }

            return new PhysicalBookDetails
            {
                BookWeight = physicalBookDetails.BookWeight,
                Height = physicalBookDetails.Height,
                Length = physicalBookDetails.Length,
                NumberOfPages = physicalBookDetails.NumberOfPages,
                Width = physicalBookDetails.Width
            };
        }
    }
}