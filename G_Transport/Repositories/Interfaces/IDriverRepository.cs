using G_Transport.Dtos;
using G_Transport.Models.Domain;
using System.Linq.Expressions;

namespace G_Transport.Repositories.Interfaces
{
    public interface IDriverRepository : IBaseRepository<Driver>
    {
        Task<Driver> GetAsync(Guid id);
        Task<Driver?> GetAsync(Expression<Func<Driver, bool>> exp);
        Task<PaginationDto<Driver>> GetAllAsync(PaginationRequest request);
        Task<ICollection<Driver>> GetAllAsync(Expression<Func<Driver,bool>> exp);
        Task<int> Count();
    }
}
