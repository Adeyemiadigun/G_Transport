using G_Transport.Dtos;

namespace G_Transport.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BaseResponse<BookingDto>> CreateAsync(CreateBookingRequestModel model);
        Task<bool> DeleteAsync(Guid id);
        Task<BaseResponse<PaginationDto<BookingDto>>> GetAllAsync(PaginationRequest request);
        Task<BaseResponse<PaginationDto<BookingDto>>> GetAllCustomerBookings(PaginationRequest request);
    }
}
