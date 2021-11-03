using Book.DataLayer.Context;
using Book.DataLayer.DataModels;
using System.Data.Entity;

namespace Book.DataLayer
{
    internal class ISBNRepository
    {
        internal ISBNDetails GetISBNDetailsByISBNId(BookContext context, long? ISBNId)
        {
            if (!ISBNId.HasValue)
            {
                return null;
            }

            return context.ISBNDetails.Find(ISBNId);
        }

        internal void RemoveISBNDetails(BookContext context, ISBNDetails iSBNDetails)
        {
            context.ISBNDetails.Remove(iSBNDetails);
            context.Entry(iSBNDetails).State = EntityState.Deleted;
            context.SaveChanges();
        }

        internal ISBNDetails AddISBNDetail(BookContext context, Domain.Models.ISBNDetails iSBNDetails)
        {
            var isbnDto = new ISBNDetails
            {
                ISBN10 = iSBNDetails.ISBN10,
                ISBN13 = iSBNDetails.ISBN13
            };

            context.ISBNDetails.Add(isbnDto);
            context.Entry(isbnDto).State = EntityState.Added;
            context.SaveChanges();

            return isbnDto;
        }

        internal void UpdateISBNDetails(BookContext context, Domain.Models.ISBNDetails iSBNDetails, ISBNDetails ISBNDto)
        {
            context.ISBNDetails.Attach(ISBNDto);
            ISBNDto.ISBN10 = iSBNDetails.ISBN10;
            ISBNDto.ISBN13 = iSBNDetails.ISBN13;
            context.Entry(ISBNDto).Property(x => x.ISBN10).IsModified = true;
            context.Entry(ISBNDto).Property(x => x.ISBN13).IsModified = true;
            context.SaveChanges();
        }
    }
}
