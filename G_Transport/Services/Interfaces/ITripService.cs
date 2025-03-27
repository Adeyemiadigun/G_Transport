using System.Linq.Expressions;
using G_Transport.Dtos;
using G_Transport.Models.Domain;

namespace G_Transport.Services.Interfaces
{
    public interface ITripService
    {
        Task<BaseResponse<TripDto>> CreateAsync(CreateTripRequestModel model);
        Task<bool> DeleteAsync(Guid id);
        Task<BaseResponse<PaginationDto<TripDto>>> GetAllAsync(PaginationRequest request);
        Task<BaseResponse<PaginationDto<TripDto>>> GetAllAvailableAsync(PaginationRequest request);
        Task<BaseResponse<PaginationDto<TripDto>>> GetAllRecent(PaginationRequest request);
        Task<BaseResponse<PaginationDto<TripDto>>> GetAllWithoutReviewAsync(PaginationRequest request);
        Task<BaseResponse<TripDto>> UpdateAsync(UpdateTripRequestModel model);
        Task<BaseResponse<TripDto>> GetAsync(Expression<Func<Trip, bool>> exp);
        Task<BaseResponse<TripDto>> GetDriverAssigned();
        int TripCount(Expression<Func<Trip, bool>> exp);

    }
}
