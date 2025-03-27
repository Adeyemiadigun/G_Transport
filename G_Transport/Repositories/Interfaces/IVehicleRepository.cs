using G_Transport.Dtos;
using G_Transport.Models.Domain;
using System.Linq.Expressions;

namespace G_Transport.Repositories.Interfaces
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        Task<Vehicle> GetAsync(Guid id);
        Task<Vehicle?> GetAsync(Expression<Func<Vehicle, bool>> exp);
        Task<PaginationDto<Vehicle>> GetAllAsync(PaginationRequest request);
    }
}
