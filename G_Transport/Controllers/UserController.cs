using G_Transport.Dtos;
using System.Security.Claims;
using G_Transport.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using G_Transport.Auth;

namespace G_Transport.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UsersController(IUserService userService,IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var response = await _userService.LoginAsync(model);
            if (response.Status)
            {
                var token = _authService.GenerateJwtToken(response.Data);
                return Ok(new { Token = token, Data = response});
            }
            return BadRequest(response);

        }
    }
}
