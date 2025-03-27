using G_Transport.Dtos;

namespace G_Transport.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<UserDto>> GetAsync(Guid id);
        Task<BaseResponse<PaginationDto<UserDto>>> GetAllAsync(PaginationRequest request);
        Task<BaseResponse<UserDto>> LoginAsync(LoginRequestModel model);

    }
}
