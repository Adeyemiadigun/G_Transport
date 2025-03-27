using System.Linq.Expressions;
using G_Transport.Context;
using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace G_Transport.Repositories.Implementations
{
    public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(G_TransportContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginationDto<Profile>> GetAllAsync(PaginationRequest request)
        {
            try
            {
                var res = _context.Set<Profile>().AsQueryable();
                var totalRecord = res.Count();
                var totalPages = totalRecord / request.PageSize;
                var items = res.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
                return new PaginationDto<Profile>
                {
                    Items = items,
                    TotalItems = totalRecord,
                    TotalPages = totalPages,
                    HasNextPage = totalPages - request.CurrentPage == 0 ? false : true,
                    HasPreviousPage = request.CurrentPage - 1 == 0 ? false : true,
                    PageSize = request.PageSize,
                    CurrentPage = request.CurrentPage,

                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public Task<Profile> GetAsync(Expression<Func<Profile, bool>> exp)
        {
            throw new NotImplementedException();
        }

     
    }
}
