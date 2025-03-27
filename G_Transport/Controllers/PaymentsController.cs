using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using G_Transport.Dtos;
using G_Transport.Services.Interfaces;
using System.Threading.Tasks;
using System;

namespace G_Transport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("all-payments")]
        public async Task<IActionResult> GetAllPayments([FromQuery] PaginationRequest request)
        {
            var response = await _paymentService.GetPaymentsAsync(request);
            if (!response.Status)
                return NotFound(response);
            return Ok(response);
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("payment/{id}")]
        public async Task<IActionResult> GetPayment(Guid id)
        {
            var response = await _paymentService.GetPaymentAsync(id);
            if (!response.Status)
                return NotFound(response);
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("customer-payments")]
        public async Task<IActionResult> GetCustomerPayments([FromQuery] PaginationRequest request)
        {
            var response = await _paymentService.GetCustomerPaymentsAsync(request);

            if (!response.Status)
            {
                return NotFound(new { message = response.Message });
            }

            return Ok(response);
        }

        [HttpPost("initialize")]
        public async Task<IActionResult> InitializePayment([FromBody] PaymentRequest request)
        {
            var response = await _paymentService.InitializePayment(request);

            if (response.Status)
            {
                return Ok(new { status = true, paymentUrl = response.Data });
            }
            return BadRequest(new { status = false, message = response.Message });
        }

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyPayment([FromQuery] string reference)
        {
            var result = await _paymentService.VerifyPaymentAsync(reference);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
