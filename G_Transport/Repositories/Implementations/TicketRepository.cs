using G_Transport.Context;
using G_Transport.Models.Domain;
using G_Transport.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace G_Transport.Repositories.Implementations
{
    public class TicketRepository : BaseRepository<Ticket>,ITicketRepository
    {

        public TicketRepository(G_TransportContext context) :base(context) { }
       

        public async Task<Ticket> GetByIdAsync(Guid id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task<List<Ticket>> GetAllAsync( Guid id)
        {
            return await _context.Tickets.Where(x => x.CustomerId == id).ToListAsync();
        }

    }
}

