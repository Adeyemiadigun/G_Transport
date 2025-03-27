using G_Transport.Context;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace G_Transport.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly G_TransportContext _context;
        public RoleRepository(G_TransportContext context)
        { 
            _context = context;
        }
        public async Task<Role?> GetRoleAsync(string roleName)
        {
            return await _context.Set<Role>()
                .Include(x => x.Users)
                .FirstOrDefaultAsync(r => r.Name == roleName);
        }
    }
}
