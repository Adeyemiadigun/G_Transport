using G_Transport.Services.Interfaces;
using System.Security.Claims;

namespace G_Transport.Services.Implementations
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _http;

        public CurrentUser(IHttpContextAccessor http)
        {
            _http = http;
        }

        public string GetCurrentUser()
        {
            return _http.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
        }

        public Guid GetCurrentuserId()
        {
            var userIdClaim = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("User is not logged in");

            return Guid.Parse(userIdClaim);
        }
    }
}
