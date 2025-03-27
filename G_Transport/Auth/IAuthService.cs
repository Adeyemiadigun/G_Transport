using G_Transport.Dtos;

namespace G_Transport.Auth
{
    public interface IAuthService
    {
        string GenerateJwtToken(UserDto userDto);
    }
}
