using G_Transport.Dtos;
using G_Transport.Models.Domain;
using System.Linq.Expressions;

namespace G_Transport.Repositories.Interfaces
{
    public interface ITripRepository : IBaseRepository<Trip>
    {
        Task<Trip> GetAsync(Guid id);
        Task<Trip?> GetAsync(Expression<Func<Trip, bool>> exp);
        Task<PaginationDto<Trip>> GetAllAsync(PaginationRequest request);
        Task<PaginationDto<Trip>?> GetAllAsync(Expression<Func<Trip,bool> >exp, PaginationRequest request   );
        int GetAll(Expression<Func<Trip, bool>> exp);
    }
}
