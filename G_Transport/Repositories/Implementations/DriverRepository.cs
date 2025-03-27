using System.Linq.Expressions;
using G_Transport.Context;
using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Expr;
using Org.BouncyCastle.Asn1.Ocsp;

namespace G_Transport.Repositories.Implementations
{
    public class DriverRepository : BaseRepository<Driver>, IDriverRepository
    {
        public DriverRepository(G_TransportContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> Count()
        {
            return await _context.Set<Driver>().CountAsync();
        }

        //public async Task<PaginationDto<Driver>> GetAllAsync(PaginationRequest request)
        //{

        //    var drivers = _context.Set<Driver>()
        //                   .Include(x => x.Profile)
        //                   .Include(x => x.Vehicle)
        //                   .Where(c => c.IsDeleted == false)
        //                   .AsQueryable();
        //    var totalRecord = drivers.Count();
        //    var totalPages = (int)Math.Ceiling((double)totalRecord / request.PageSize);
        //    var result = await _context.Set<Driver>()
        //        .Skip((request.CurrentPage - 1) * request.PageSize)
        //        .Take(request.PageSize)
        //        .ToListAsync();

        //    return new PaginationDto<Driver>
        //    {
        //        TotalPages = totalPages,
        //        TotalItems = totalRecord,
        //        Items = result,
        //        HasNextPage = totalPages / request.CurrentPage == 1 ? false : true,
        //        HasPreviousPage = request.CurrentPage - 1 == 0 ? false : true,
        //        PageSize = request.PageSize,
        //        CurrentPage = request.CurrentPage

        //    };
        //}
        public async Task<PaginationDto<Driver>> GetAllAsync(PaginationRequest request)
        {
            var driversQuery = _context.Set<Driver>()
                .Include(x => x.Profile) // Ensure Profile is included
                .Include(x => x.Vehicle) // Ensure Vehicle is included
                .Where(c => c.IsDeleted == false)
                .AsQueryable();

            var totalRecord =  driversQuery.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecord / request.PageSize);

            var result = await driversQuery // Use the same query
                .Skip((request.CurrentPage - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PaginationDto<Driver>
            {
                TotalPages = totalPages,
                TotalItems = totalRecord,
                Items = result,
                HasNextPage = request.CurrentPage < totalPages,
                HasPreviousPage = request.CurrentPage > 1,
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage
            };
        }


        public async Task<ICollection<Driver>> GetAllAsync(Expression<Func<Driver, bool>> exp)
        {
            var drivers = await _context.Set<Driver>()
                           .Include(x => x.Profile)
                           .Include(x => x.Vehicle)
                           .ThenInclude(x => x!.trips)
                           .Where(exp)
                           .ToListAsync();
            return drivers;
           
        }

        public async Task<Driver?> GetAsync(Guid id)
        {
            return await _context.Set<Driver>()
                .Include(x => x.Profile)
                .Include(x => x.Vehicle)
                .ThenInclude(x => x!.trips)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Driver?> GetAsync(Expression<Func<Driver, bool>> exp)
        {
            return await _context.Set<Driver>()
                .Include(x => x.Profile)
                .Include(x => x.Vehicle)
                .ThenInclude(x => x!.trips)
                .FirstOrDefaultAsync(exp);
        }
    }
}
