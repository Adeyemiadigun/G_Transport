using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using G_Transport.Dtos;
using G_Transport.Services.Interfaces;

namespace G_Transport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateDriver([FromBody] RegisterDriverRequestModel model)
        {
            var response = await _driverService.CreateAsync(model);
            if (!response.Status)
                return BadRequest(response);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteDriver(Guid id)
        {
            var result = await _driverService.DeleteAsync(id);
            if (!result)
                return NotFound("Driver not found");
            return Ok("Driver deleted successfully");
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllDrivers([FromQuery] PaginationRequest request)
        {
            var response = await _driverService.GetAllAsync(request);
            if (!response.Status)
                return NotFound(response);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("available")]
        public async Task<IActionResult> GetAllAvailableDrivers()
        {
            var response = await _driverService.GetAllAsync();
            if (!response.Status)
            {
                return NotFound(new { response.Status, response.Message });
            }
            return Ok(response);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetDriver([FromQuery] Guid id)
        {
            var response = await _driverService.GetAsync(id);
            if (!response.Status)
                return NotFound(response);
            return Ok(response);
        }
    }
}

