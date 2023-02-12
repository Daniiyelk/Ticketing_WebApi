using Microsoft.EntityFrameworkCore;
using Ticketing.Core.Services.Interfaces;
using Ticketing.DataLayer.Context;
using Ticketing.DataLayer.Entities.Admin;

namespace Ticketing.Core.Services;

public class AdminServices : IAdminServices
{
    private readonly TicketingContext _context;

    public AdminServices(TicketingContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Admin>> GetAdminsAsync()
    {
        return await _context.Admin.ToListAsync();
    }

    public async Task<Admin?> GetAdminByIdAsync(int id, bool includeAnswers = false)
    {
        if (includeAnswers)
        {
            return await _context.Admin.Include(a => a.TicketAnswer)
                .Where(ad => ad.id == id).FirstOrDefaultAsync();
        }

        return await _context.Admin.FindAsync(id);
    }

    public async Task AddAdminAsync(Admin admin)
    {
        await _context.Admin.AddAsync(admin);
        await SaveChangesAsync();
    }

    public async Task UpdateAdmin(Admin admin)
    {
        _context.Admin.Update(admin);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAdmin(Admin admin)
    {
        _context.Admin.Remove(admin);
        await SaveChangesAsync();
    }
}