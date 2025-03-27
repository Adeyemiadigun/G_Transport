using G_Transport.Dtos;
using G_Transport.Models.Domain;

namespace G_Transport.Services.Interfaces
{
    public interface IProfileService
    {
        Task<bool> DeleteAsync(Guid id);
        Task<Profile> GetAsync(Guid id);
        Task<BaseResponse<ProfileDto>> UpdateAsync(UpdateProfileRequestModel model);
        Task<BaseResponse<PaginationDto<ProfileDto>>> GetAllAsync(PaginationRequest request);


    }
}
