using System.Linq.Expressions;
using G_Transport.Dtos;
using G_Transport.Models.Domain;

namespace G_Transport.Repositories.Interfaces
{
    public interface IProfileRepository : IBaseRepository<Profile>
    {
        Task<Profile> GetAsync(Expression<Func<Profile, bool>> exp);
        Task<PaginationDto<Profile>> GetAllAsync(PaginationRequest request);

    }
}
