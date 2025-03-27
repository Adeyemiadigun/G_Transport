using System.Linq.Expressions;
using G_Transport.Context;
using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace G_Transport.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(G_TransportContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PaginationDto<User>> GetAllAsync(PaginationRequest request)
        {
            var users = _context.Set<User>()
              .Include(a => a.Roles)
              .ThenInclude(a => a.Role)
             .AsQueryable();
            var totalRecord = users.Count();
            var totalPages = totalRecord / request.PageSize;
            var result = await _context.Set<User>()
            .Skip((request.CurrentPage - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PaginationDto<User>
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

        public async Task<User?> GetAsync(Guid id)
        {
            return await _context.Set<User>()
                .Include(a => a.Roles)
                .ThenInclude(a => a.Role)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetAsync(Expression<Func<User, bool>> exp)
        {
            return await _context.Set<User>()
                .Include(a => a.Roles)
                .ThenInclude(a => a.Role)
                .FirstOrDefaultAsync(exp);
        }
    }
}
