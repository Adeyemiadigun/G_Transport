using Microsoft.AspNetCore.Mvc;
using G_Transport.Services.Interfaces;
using G_Transport.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G_Transport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateVehicle([FromBody] RegisterVehicleRequestModel model)
        {
            var response = await _vehicleService.CreateAsync(model);
            if (!response.Status)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetVehicle(Guid id)
        {
            var response = await _vehicleService.GetAsync(v => v.Id == id);
            if (!response.Status)
                return NotFound(response);
            return Ok(response);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllVehicles([FromQuery] PaginationRequest request)
        {
            var response = await _vehicleService.GetAllAsync(request);
            if (!response.Status)
                return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVehicle(Guid id)
        {
            var success = await _vehicleService.DeleteAsync(id);
            if (!success)
                return NotFound(new { Status = false, Message = "Vehicle not found" });
            return Ok(new { Status = true, Message = "Vehicle deleted successfully" });
        }
    }
}
