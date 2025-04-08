using G_Transport.Models.Domain;
using G_Transport.Repositories.Interfaces;
using G_Transport.Services.Interfaces;

namespace G_Transport.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICurrentUser _currentUser;

        public TicketService(ITicketRepository ticketRepository, ICurrentUser currentUser)
        {
            _ticketRepository = ticketRepository;
            _currentUser = currentUser;
        }

        public async Task<Ticket> CreateTicketAsync(Ticket ticket)
        {
            await _ticketRepository.CreateAsync(ticket);
            return ticket;
        }

        public async Task<Ticket> GetTicketAsync(Guid id)
        {
            return await _ticketRepository.GetByIdAsync(id);
        }

        public async Task<List<Ticket>> GetTicketsAsync()
        {
            var currentUser = _currentUser.GetCurrentuserId();
            return await _ticketRepository.GetAllAsync(currentUser);
        }
    }

}
