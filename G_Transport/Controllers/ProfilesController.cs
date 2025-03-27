using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using G_Transport.Services.Interfaces;
using G_Transport.Models.Domain;
using G_Transport.Dtos;

namespace G_Transport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [Authorize(Roles = "Admin,Customer,Driver")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(Guid id)
        {
            var profile = await _profileService.GetAsync(id);
            if (profile == null)
                return NotFound("Profile not found");
            return Ok(profile);
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(Guid id)
        {
            var result = await _profileService.DeleteAsync(id);
            if (!result)
                return NotFound("Profile not found");
            return Ok("Profile deleted successfully");
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequestModel model)
        {
            var response = await _profileService.UpdateAsync(model);
            if (!response.Status)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}

