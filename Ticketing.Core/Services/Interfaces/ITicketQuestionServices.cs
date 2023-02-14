using Ticketing.DataLayer.Entities.TicketQuestion;

namespace Ticketing.Core.Services.Interfaces;

public interface ITicketQuestionServices
{
    public Task<IEnumerable<TicketQuestion>> GetTicketQuestionAsync();
    public Task<TicketQuestion?> GetTicketQuestionByIdAsync(int id);
    public Task AddTicketQuestionAsync(TicketQuestion ticketQuestion);
    public Task SaveChangesAsync();
    public Task<bool> IsExistTicketQuestion(int id);
    public Task UpdateTicketQuestion(TicketQuestion ticketQuestion);
}