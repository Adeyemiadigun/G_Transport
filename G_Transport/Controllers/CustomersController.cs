using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using G_Transport.Dtos;
using G_Transport.Services.Interfaces;

namespace G_Transport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerService;

        public CustomerController(ICustomerServices customerService)
        {
            _customerService = customerService;
        }
        // [Authorize(Roles = "Admin,Customer")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCUstomerRequestModel model)
        {
            var response = await _customerService.CreateAsync(model);
            if (!response.Status)
                return BadRequest(response);
            return Ok(response);
        }

        // [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCustomers([FromQuery] PaginationRequest request)
        {
            var response = await _customerService.GetAllAsync(request);
            if (!response.Status)
                return NoContent();
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var response = await _customerService.GetAsync(c => c.Id == id);
            if (!response.Status)
                return NotFound(response);
            return Ok(response);
        }

        // [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var result = await _customerService.DeleteAsync(id);
            if (!result)
                return NotFound("Customer not found");
            return Ok("Customer deleted successfully");
        }
        [HttpGet("profile")]
        [Authorize(Roles = "Customer")] // Ensures only authenticated users can access this endpoint
        public async Task<IActionResult> GetCustomerProfile()
        {
            var response = await _customerService.GetAsync();
            if (!response.Status)
            {
                return NotFound(response); // Returns 404 if customer is not found
            }
            return Ok(response);
        }
        [HttpGet("count")]
        public IActionResult GetCustomerCount()
        {
            int count = _customerService.CustomerCount();
            return Ok(new { customerCount = count });
        }
    }
}

