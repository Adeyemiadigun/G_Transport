using G_Transport.Dtos;
using G_Transport.Models.Domain;
using System.Linq.Expressions;

namespace G_Transport.Repositories.Interfaces
{
    public interface IBookingRepository:IBaseRepository<Booking>
    {
        Task<Booking> GetAsync(Guid id);
        Task<Booking?> GetAsync(Expression<Func<Booking, bool>> exp);
        Task<PaginationDto<Booking>> GetAllAsync(PaginationRequest request);
        Task<PaginationDto<Booking>> GetAllAsync(Expression<Func<Booking, bool>> exp,PaginationRequest request);
       
    }
}
