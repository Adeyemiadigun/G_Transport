using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using G_Transport.Dtos;
using G_Transport.Services.Interfaces;

namespace G_Transport.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequestModel model)
        {
            var response = await _bookingService.CreateAsync(model);
            if (!response.Status)
                return BadRequest(response);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            var result = await _bookingService.DeleteAsync(id);
            if (!result)
                return NotFound("Booking not found");
            return Ok("Booking deleted successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBookings([FromQuery] PaginationRequest request)
        {
            var response = await _bookingService.GetAllAsync(request);
            if (!response.Status)
                return NotFound(response);
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("customer-bookings")]
        public async Task<IActionResult> GetCustomerBookings([FromQuery] PaginationRequest request)
        {
            var response = await _bookingService.GetAllCustomerBookings(request);
            if (!response.Status)
                return NotFound(response);
            return Ok(response);
        }
    }
}
