using System.Linq.Expressions;
using G_Transport.Dtos;
using G_Transport.Models.Domain;

namespace G_Transport.Services.Interfaces
{
    public interface IReviewService
    {
        Task<BaseResponse<ReviewDto>> CreateAsync(CreateReviewDto model);
        Task<bool> DeleteAsync(Guid id);
        Task<BaseResponse<PaginationDto<ReviewDto>>> GetAllAsync(PaginationRequest request);
         
        Task<BaseResponse<TripDto>> UpdateAsync(UpdateTripRequestModel model);
        Task<BaseResponse<PaginationDto<ReviewDto>>> GetAllCustomerReviewsAsync(PaginationRequest request);
    }
}
