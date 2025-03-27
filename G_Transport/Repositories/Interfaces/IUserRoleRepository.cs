using G_Transport.Models.Domain;

namespace G_Transport.Repositories.Interfaces
{
    public interface IUserRoleRepository : IBaseRepository<UserRole>
    {
        Task<UserRole> GetByUserAndRoleIdAsync(Guid userId, Guid roleId);
        Task<ICollection<UserRole>> GetRolesByUserIdAsync(Guid userId);
    }
}

