using Microsoft.EntityFrameworkCore;
using Ticketing.Core.Services.Interfaces;
using Ticketing.DataLayer.Context;
using Ticketing.DataLayer.Entities.User;

namespace Ticketing.Core.Services;

public class UserServices : IUserServices
{
    private readonly TicketingContext _context;

    public UserServices(TicketingContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.User.ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id, bool includeTicket=false)
    {
        if (includeTicket)
        {
            return await _context.User.Include(t => t.Ticket)
                .Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        return await _context.User.FindAsync(id);
    }

    public async Task<int> AddUserAsync(User user)
    {
        await _context.User.AddAsync(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.User.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        _context.User.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistUser(int id)
    {
        return await _context.User.Where(u => u.Id == id).AnyAsync();
    }
}