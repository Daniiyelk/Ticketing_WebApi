using Ticketing.DataLayer.Entities.TicketAnswer;

namespace Ticketing.Core.Services.Interfaces;

public interface ITicketAnswerServices
{
    public Task<IEnumerable<TicketAnswer>> GetAllTicketAnswersAsync();
    public Task<TicketAnswer?> GetTicketAnswerByIdAsync(int id);
    public Task AddTicketAnswerAsync(TicketAnswer ticketAnswer);
    public Task SaveChangesAsync();
}