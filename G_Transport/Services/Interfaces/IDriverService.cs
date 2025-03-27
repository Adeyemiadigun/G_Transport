using System.Linq.Expressions;
using G_Transport.Dtos;
using G_Transport.Models.Domain;

namespace G_Transport.Services.Interfaces
{
    public interface IDriverService
    {
        Task<BaseResponse<DriverDto>> CreateAsync(RegisterDriverRequestModel model);
        Task<bool> DeleteAsync(Guid id);
        Task<BaseResponse<DriverDto>> GetAsync(Guid id);
        Task<BaseResponse<PaginationDto<DriverDto>>> GetAllAsync(PaginationRequest request);
        Task<BaseResponse<ICollection<DriverDto?>>> GetAllAsync();
        Task<BaseResponse<ICollection<DriverDto>>> GetSelected(List<Guid> ids);
        Task<BaseResponse<DriverDto>> GetAsync(Expression<Func<Driver, bool>> exp);

    }
}
