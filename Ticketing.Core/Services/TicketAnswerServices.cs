using Microsoft.EntityFrameworkCore;
using Ticketing.Core.Services.Interfaces;
using Ticketing.DataLayer.Context;
using Ticketing.DataLayer.Entities.TicketAnswer;

namespace Ticketing.Core.Services;

public class TicketAnswerServices:ITicketAnswerServices
{
    private readonly TicketingContext _context;

    public TicketAnswerServices(TicketingContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TicketAnswer>> GetAllTicketAnswersAsync()
    {
        return await _context.TicketAnswer.ToListAsync();
    }

    public async Task<TicketAnswer?> GetTicketAnswerByIdAsync(int id)
    {
        return await _context.TicketAnswer.FindAsync(id);
    }

    public async Task AddTicketAnswerAsync(TicketAnswer ticketAnswer)
    {
        await _context.TicketAnswer.AddAsync(ticketAnswer);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}