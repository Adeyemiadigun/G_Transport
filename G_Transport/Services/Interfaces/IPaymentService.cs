using System.Linq.Expressions;
using G_Transport.Dtos;
using G_Transport.Models.Domain;

namespace G_Transport.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<BaseResponse<PaymentDto>> GetPaymentAsync(Guid id);
        Task<BaseResponse<PaginationDto<PaymentDto>>> GetPaymentsAsync(PaginationRequest request);
        Task<BaseResponse<PaginationDto<PaymentDto>>> GetCustomerPaymentsAsync(PaginationRequest request);

        Task<BaseResponse<string>> InitializePayment(PaymentRequest request);
        Task<BaseResponse<PaymentDto>> VerifyPaymentAsync(string reference);

    }
}
