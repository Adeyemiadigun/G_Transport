using G_Transport.Context;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G_Transport.Repositories.Implementations
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {

        public UserRoleRepository(G_TransportContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserRole> GetByUserAndRoleIdAsync(Guid userId, Guid roleId)
        {
            return await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }

        public async Task<ICollection<UserRole>> GetRolesByUserIdAsync(Guid userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Role) // Load the related Role
                .ToListAsync();
        }
    }
}

