using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.DataLayer.Entities.Ticket;

namespace Ticketing.Core.Services.Interfaces
{
    public interface ITicketService
    {
        public Task<IEnumerable<Ticket>> GetTicketsAsync();
        public Task<Ticket?> GetTicketByIdAsync(int id, bool includeAnswerAndQuestions = false);
        public Task<int> AddTicketAsync(Ticket ticket);
        public void UpdateTicket(Ticket ticket);
        public void DeleteTicket(Ticket ticket);
        public Task<bool> IsExistTicket(int id);
    }
}