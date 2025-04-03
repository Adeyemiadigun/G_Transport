using System.Linq.Expressions;
using G_Transport.Context;
using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace G_Transport.Repositories.Implementations
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(G_TransportContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PaginationDto<Vehicle>> GetAllAsync(PaginationRequest request)
        {
            var vehicles =  _context.Set<Vehicle>()
            .Include(x => x.trips)
            .ThenInclude(x => x.Reviews)
            .AsQueryable();
            var totalRecord = vehicles.Count();
            var totalPages = totalRecord / request.PageSize;
            var result = await vehicles
            .Skip((request.CurrentPage - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

            return new PaginationDto<Vehicle>
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

        public async Task<Vehicle?> GetAsync(Guid id)
        {
            return await _context.Set<Vehicle>()
                .Include(x => x.trips)
                .ThenInclude(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Vehicle?> GetAsync(Expression<Func<Vehicle, bool>> exp)
        {
            return await _context.Set<Vehicle>()
               .Include(x => x.trips)
               .ThenInclude(x => x.Reviews)
               .FirstOrDefaultAsync(exp);
        }
    }
}
