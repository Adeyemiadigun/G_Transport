using G_Transport.Models.Domain;

namespace G_Transport.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleAsync(string roleName);
    }
}
