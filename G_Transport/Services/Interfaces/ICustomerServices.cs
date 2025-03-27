using System.Linq.Expressions;
using G_Transport.Dtos;
using G_Transport.Models.Domain;

namespace G_Transport.Services.Interfaces
{
    public interface ICustomerServices
    {
        Task<BaseResponse<CustomerDto>> CreateAsync(RegisterCUstomerRequestModel model);
        Task<bool> DeleteAsync(Guid id);
        Task<BaseResponse<CustomerDto>> GetAsync(Expression<Func<Customer,bool>> exp);
        Task<BaseResponse<CustomerDto>> GetAsync();
        Task<BaseResponse<PaginationDto<CustomerDto>>> GetAllAsync(PaginationRequest request);
        Task<BaseResponse<ICollection<CustomerDto>>> GetSelected(List<Guid> ids);
        int CustomerCount();

    }
}
