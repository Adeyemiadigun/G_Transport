using G_Transport.Dtos;
using G_Transport.Models.Domain;
using System.Linq.Expressions;

namespace G_Transport.Repositories.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        Task<Review> GetAsync(Guid id);
        Task<Review?> GetAsync(Expression<Func<Review, bool>> exp);
        Task<PaginationDto<Review>> GetAllAsync(PaginationRequest request);
        Task<PaginationDto<Review>> GetAllAsync(Expression<Func<Review, bool>> exp, PaginationRequest request);
    }
}
