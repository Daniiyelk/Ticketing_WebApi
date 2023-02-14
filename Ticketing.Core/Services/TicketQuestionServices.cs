using Microsoft.EntityFrameworkCore;
using Ticketing.Core.Services.Interfaces;
using Ticketing.DataLayer.Context;
using Ticketing.DataLayer.Entities.TicketQuestion;

namespace Ticketing.Core.Services;

public class TicketQuestionServices:ITicketQuestionServices
{
    private readonly TicketingContext _context;

    public TicketQuestionServices(TicketingContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TicketQuestion>> GetTicketQuestionAsync()
    {
        return await _context.TicketQuestion.ToListAsync();
    }

    public async Task<TicketQuestion?> GetTicketQuestionByIdAsync(int id)
    {
        return await _context.TicketQuestion.FindAsync(id);
    }

    public async Task AddTicketQuestionAsync(TicketQuestion ticketQuestion)
    {
        _context.TicketQuestion.Add(ticketQuestion);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistTicketQuestion(int id)
    {
        return await _context.TicketQuestion.Where(t => t.id == id).AnyAsync();
    }

    public async Task UpdateTicketQuestion(TicketQuestion ticketQuestion)
    {
        _context.TicketQuestion.Update(ticketQuestion);
        await SaveChangesAsync();
    }
}