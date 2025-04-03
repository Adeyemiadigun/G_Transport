using System.Linq.Expressions;
using G_Transport.Context;
using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace G_Transport.Repositories.Implementations
{
    public class TripRepository : BaseRepository<Trip>, ITripRepository
    {
        public TripRepository(G_TransportContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PaginationDto<Trip>> GetAllAsync(PaginationRequest request)
        {
            var trips = _context.Set<Trip>()
              .Include(x => x.Vehicle)
              .AsQueryable();
            var totalRecord = trips.Count();
            var totalPages = totalRecord / request.PageSize;
            var result = await trips
            .Skip((request.CurrentPage - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PaginationDto<Trip>
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

        public async Task<PaginationDto<Trip>> GetAllAsync(Expression<Func<Trip, bool>> exp, PaginationRequest request)
        {
            var trips = _context.Set<Trip>()
               .Include(x => x.Vehicle)
               .Where(exp);

            var totalRecord = trips.Count();
            var totalPages = totalRecord / request.PageSize;
            var result = await trips
            .Skip((request.CurrentPage - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PaginationDto<Trip>
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

        public async Task<Trip?> GetAsync(Guid id)
        {
            return await _context.Set<Trip>().Include(x => x.Bookings).Include(x => x.Vehicle).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Trip?> GetAsync(Expression<Func<Trip, bool>> exp)
        {
            return await _context.Set<Trip>().FirstOrDefaultAsync(exp);
        }
        public int GetAll(Expression<Func<Trip, bool>> exp)
        {
            return _context.Set<Trip>().Where(exp).Count();
        }
    }
}
