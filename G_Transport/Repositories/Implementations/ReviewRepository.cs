using System.Linq.Expressions;
using G_Transport.Context;
using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace G_Transport.Repositories.Implementations
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(G_TransportContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PaginationDto<Review>> GetAllAsync(PaginationRequest request)
        {
            var reviews = _context.Set<Review>()
               .Include(x => x.Trip)
                .ThenInclude(x => x.Vehicle)
                .Include(x => x.Customer)
              .AsQueryable();
            var totalRecord = reviews.Count();
            var totalPages = totalRecord / request.PageSize;
            var result = await _context.Set<Review>()
            .Skip((request.CurrentPage - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PaginationDto<Review>
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

        public async Task<PaginationDto<Review>> GetAllAsync(Expression<Func<Review, bool>> exp,PaginationRequest request)
        {

            var reviews = _context.Set<Review>()
               .Include(x => x.Trip)
                .ThenInclude(x => x.Vehicle)
                .Include(x => x.Customer)
                .Where(exp)
              .AsQueryable();
            var totalRecord = reviews.Count();
            var totalPages = totalRecord / request.PageSize;
            var result = await _context.Set<Review>()
            .Skip((request.CurrentPage - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PaginationDto<Review>
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

        public async Task<Review?> GetAsync(Guid id)
        {
            return await _context.Set<Review>()
               .Include(x => x.Trip)
               .ThenInclude(x => x.Vehicle)
               .Include(x => x.Customer)
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Review?> GetAsync(Expression<Func<Review, bool>> exp)
        {
            return await _context.Set<Review>()
              .Include(x => x.Trip)
              .ThenInclude(x => x.Vehicle)
              .Include(x => x.Customer)
              .FirstOrDefaultAsync(exp);
        }
    }
}
