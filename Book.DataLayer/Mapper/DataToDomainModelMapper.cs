using Book.Domain.Models;
using System;
using System.Collections.Generic;

namespace Book.DataLayer.Mapper
{
    public static class DataToDomainModelMapper
    {
        public static IEnumerable<BookDetails> MapDataModelToBookDetailList(this List<DataModels.BookDetails> bookDtoDetailsList)
        {
            if (bookDtoDetailsList.Count == 0)
            {
                return null;
            }

            List<BookDetails> bookDetailsList = new List<BookDetails>();

            foreach (var bookDetail in bookDtoDetailsList)
            {
                bookDetailsList.Add(bookDetail.MapDataBookDetails());
            }

            return bookDetailsList;
        }

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

        internal static ISBNDetails MapISBNDataDetails(DataModels.ISBNDetails iSBNDetails)
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

        internal static PhysicalBookDetails MapISBNDataDetails(DataModels.PhysicalBookDetails physicalBookDetails)
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

        public static DataModels.ISBNDetails MapDomainISBNToDataISBN(this ISBNDetails iSBNDetails)
        {
            if (iSBNDetails == null)
            {
                return null;
            }

            return new DataModels.ISBNDetails
            {
                ISBN10 = iSBNDetails.ISBN10,
                ISBN13 = iSBNDetails.ISBN13
            };
        }

        internal static DataModels.PhysicalBookDetails MapDomainPhysicalBookToDataPhysicalBook(this PhysicalBookDetails physicalBookDetails)
        {
            if (physicalBookDetails == null)
            {
                return null;
            }

            return new DataModels.PhysicalBookDetails
            {
                BookWeight = physicalBookDetails.BookWeight,
                Height = physicalBookDetails.Height,
                Length = physicalBookDetails.Length,
                NumberOfPages = physicalBookDetails.NumberOfPages,
                Width = physicalBookDetails.Width
            };
        }

        internal static DataModels.Review MapDomainReviewToDataReview(this Review review)
        {
            if (review == null)
            {
                return null;
            }

            return new DataModels.Review
            {
                Rating = review.Rating,
                ReviewingOrganization = review.ReviewingOrganization,
                ReviewTitle = review.ReviewTitle,
                ReviewType = new DataModels.ReviewType
                {
                    ReviewTypeId = (int)review.Type,
                    TypeName = review.Type.ToString()
                },
                ReviewDescription = review.ReviewDescription,
                ReviewId = review.ReviewId
            };
        }

        internal static Review MapDataReviewToDomainReview(this DataModels.Review reviewDto)
        {
            if (reviewDto == null)
            {
                return null;
            }

            return new Review
            {
                Rating = reviewDto.Rating,
                ReviewDescription = reviewDto.ReviewDescription,
                ReviewTitle = reviewDto.ReviewTitle,
                ReviewId = reviewDto.ReviewId,
                ReviewingOrganization = reviewDto.ReviewingOrganization,
                Type = reviewDto.ReviewType == null 
                       ? ReviewType.Critical 
                       : (ReviewType)reviewDto.ReviewType.ReviewTypeId,
                BookId = reviewDto.Book == null ? 0 : reviewDto.Book.BookId,
                ReviewerName = reviewDto.ReviewerName
            };
        }
    }
}