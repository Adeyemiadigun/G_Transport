using G_Transport.Dtos;
using G_Transport.Models.Domain;
using G_Transport.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicket(Guid id)
    {
        var ticket = await _ticketService.GetTicketAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }

        return Ok(ticket);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTickets()
    {
        var tickets = await _ticketService.GetTicketsAsync();
        return Ok(tickets);
    }
}
