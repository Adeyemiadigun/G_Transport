using G_Transport.Dtos;
using G_Transport.Models.Domain;
using System.Linq.Expressions;

namespace G_Transport.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetAsync(Guid id);
        Task<User?> GetAsync(Expression<Func<User, bool>> exp);
        Task<PaginationDto<User>> GetAllAsync(PaginationRequest request);
    }
}
