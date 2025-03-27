using G_Transport.Context;
using G_Transport.Repositories.Interfaces;

namespace G_Transport.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly G_TransportContext _context;

        public UnitOfWork(G_TransportContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
