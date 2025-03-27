using G_Transport.Dtos;
using G_Transport.Models.Domain;
using System.Linq.Expressions;

namespace G_Transport.Repositories.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<Payment> GetAsync(Guid id);
        Task<Payment?> GetAsync(Expression<Func<Payment, bool>> exp);
        Task<PaginationDto<Payment>> GetAllAsync(PaginationRequest request);
        Task<PaginationDto<Payment>> GetAllAsync(Expression<Func<Payment, bool>> exp,PaginationRequest request);
    }
}
