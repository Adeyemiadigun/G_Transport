using G_Transport.Models.Domain;

namespace G_Transport.Services.Interfaces
{
    public interface ITicketService
    {
        Task<Ticket> CreateTicketAsync(Ticket ticket);
        Task<Ticket> GetTicketAsync(Guid id);
        Task<List<Ticket>> GetTicketsAsync();
    }

}
