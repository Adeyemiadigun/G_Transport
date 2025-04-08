using G_Transport.Models.Domain;

namespace G_Transport.Repositories.Interfaces
{
    public interface ITicketRepository :IBaseRepository<Ticket>
    {
        Task<Ticket> GetByIdAsync(Guid id);
        Task<List<Ticket>> GetAllAsync(Guid id);
    }
}
