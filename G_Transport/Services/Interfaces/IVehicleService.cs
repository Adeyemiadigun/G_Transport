using System.Linq.Expressions;
using G_Transport.Dtos;
using G_Transport.Models.Domain;

namespace G_Transport.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<BaseResponse<VehicleDto>> CreateAsync(RegisterVehicleRequestModel model);
        Task<bool> DeleteAsync(Guid id);
        Task<BaseResponse<PaginationDto<VehicleDto>>> GetAllAsync(PaginationRequest request);
        Task<BaseResponse<ICollection<VehicleDto>>> GetSelected(List<Guid> ids);
        Task<BaseResponse<VehicleDto>> GetAsync(Expression<Func<Vehicle, bool>> exp);

    }
}
