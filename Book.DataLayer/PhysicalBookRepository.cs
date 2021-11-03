using Book.DataLayer.Context;
using Book.DataLayer.DataModels;
using System.Data.Entity;

namespace Book.DataLayer
{
    internal class PhysicalBookRepository
    {
        internal PhysicalBookDetails GetPhysicalBookDetailsByPhysicalBookId(BookContext context, long? physicalBookId)
        {
            if (!physicalBookId.HasValue)
            {
                return null;
            }

            return context.PhysicalBookDetails.Find(physicalBookId);
        }

        internal void RemovePhysicalBookDetails(BookContext context, PhysicalBookDetails physicalBookDetails)
        {
            context.PhysicalBookDetails.Remove(physicalBookDetails);
            context.Entry(physicalBookDetails).State = EntityState.Deleted;
            context.SaveChanges();
        }

        internal PhysicalBookDetails AddPhysicalBookDetail(BookContext context, Domain.Models.PhysicalBookDetails physicalBookDetails)
        {
            var physicalBookDto = new PhysicalBookDetails
            {
                NumberOfPages = physicalBookDetails.NumberOfPages,
                Width = physicalBookDetails.Width,
                Length = physicalBookDetails.Length,
                Height = physicalBookDetails.Height,
                BookWeight = physicalBookDetails.BookWeight
            };

            context.PhysicalBookDetails.Add(physicalBookDto);
            context.Entry(physicalBookDto).State = EntityState.Added;
            context.SaveChanges();

            return physicalBookDto;
        }

        internal void UpdatePhysicalBookDetails(
            BookContext context, 
            Domain.Models.PhysicalBookDetails physicalBookDetails, 
            PhysicalBookDetails physicalBookDto)
        {
            context.PhysicalBookDetails.Attach(physicalBookDto);
            physicalBookDto.NumberOfPages = physicalBookDetails.NumberOfPages;
            physicalBookDto.BookWeight = physicalBookDetails.BookWeight;
            physicalBookDto.Width = physicalBookDetails.Width;
            physicalBookDto.Height = physicalBookDetails.Height;
            physicalBookDto.Length = physicalBookDetails.Length;
            context.Entry(physicalBookDto).Property(x => x.NumberOfPages).IsModified = true;
            context.Entry(physicalBookDto).Property(x => x.BookWeight).IsModified = true;
            context.Entry(physicalBookDto).Property(x => x.Width).IsModified = true;
            context.Entry(physicalBookDto).Property(x => x.Height).IsModified = true;
            context.Entry(physicalBookDto).Property(x => x.Length).IsModified = true;
            context.SaveChanges();
        }
    }
}