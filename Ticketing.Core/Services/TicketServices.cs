using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ticketing.Core.Services.Interfaces;
using Ticketing.DataLayer.Context;
using Ticketing.DataLayer.Entities.Ticket;

namespace Ticketing.Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly TicketingContext _context;

        public TicketService(TicketingContext context)
        {
            _context = context;
        }
        

        public async Task<IEnumerable<Ticket>> GetTicketsAsync()
        {
            return await _context.Ticket.ToListAsync();
        }

        public async Task<Ticket?> GetTicketByIdAsync(int id, bool includeAnswerAndQuestions = false)
        {
            if (includeAnswerAndQuestions)
            {
                return await _context.Ticket.Include(q => q.TicketQuestions )
                    .Include(a=>a.TicketAnswers)
                    .Where(t => t.Id == id).FirstOrDefaultAsync();
            }

            return await _context.Ticket.Where(t=>t.Id==id).FirstOrDefaultAsync();
        }

        public async Task<int> AddTicketAsync(Ticket ticket)
        {
            await _context.Ticket.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return ticket.Id;
        }

        public void UpdateTicket(Ticket ticket)
        {
            _context.Ticket.Update(ticket);
            _context.SaveChanges();
        }

        public void DeleteTicket(Ticket ticket)
        {
            _context.Ticket.Remove(ticket);
            _context.SaveChanges();
        }
    }

}
