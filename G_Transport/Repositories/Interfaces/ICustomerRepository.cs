using System.Linq.Expressions;
using G_Transport.Dtos;
using G_Transport.Models.Domain;

namespace G_Transport.Repositories.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer> GetAsync(Guid id);
        Task<Customer?> GetAsync(Expression<Func<Customer, bool>> exp);
        Task<PaginationDto<Customer>> GetAllAsync(PaginationRequest request);
        int GetAll(Expression<Func<Customer, bool>> exp);
    }
}
