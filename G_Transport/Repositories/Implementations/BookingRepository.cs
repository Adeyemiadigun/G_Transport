using System.Linq.Expressions;
using G_Transport.Context;
using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace G_Transport.Repositories.Implementations
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {

        public BookingRepository(G_TransportContext context):base(context)
        {
        }

        public async Task<PaginationDto<Booking>> GetAllAsync(PaginationRequest request)
        {
            var booking = _context.Set<Booking>()
                            .Include(x => x.Trip)
                            .Include(x => x.Customer)
                            .AsQueryable();
            var totalRecord = booking.Count();
            var  totalPages = totalRecord / request.PageSize;
            var result = await _context.Set<Booking>().Skip((request.CurrentPage-1) * request.PageSize).Take(request.PageSize)
                .ToListAsync();
            return new PaginationDto<Booking>
            {
                TotalPages = totalPages,
                TotalItems = totalRecord,
                Items = result,
                HasNextPage = totalPages / request.CurrentPage == 1 ? false : true,
                HasPreviousPage = request.CurrentPage - 1 == 0 ? false : true,
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage

            };
                
        }

        public async Task<PaginationDto<Booking>> GetAllAsync(Expression<Func<Booking, bool>> exp, PaginationRequest request)
        {

            var booking = _context.Set<Booking>()
                            .Include(x => x.Trip)
                            .Include(x => x.Customer)
                            .Where(exp)
                            .AsQueryable();
            var totalRecord = booking.Count();
            var totalPages = totalRecord / request.PageSize;
            var result = await _context.Set<Booking>().Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize)
                .ToListAsync();
            return new PaginationDto<Booking>
            {
                TotalPages = totalPages,
                TotalItems = totalRecord,
                Items = result,
                HasNextPage = totalPages / request.CurrentPage == 1 ? false : true,
                HasPreviousPage = request.CurrentPage - 1 == 0 ? false : true,
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage

            };
        }

        public async Task<Booking?> GetAsync(Guid id)
        {
            return await _context.Set<Booking>()
                .Include(x => x.Trip)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }

        public async Task<Booking?> GetAsync(Expression<Func<Booking, bool>> exp)
        {
            return await _context.Set<Booking>()
               .Include(x => x.Trip)
               .Include(x => x.Customer)
               .FirstOrDefaultAsync(exp);
        }
    }
}
