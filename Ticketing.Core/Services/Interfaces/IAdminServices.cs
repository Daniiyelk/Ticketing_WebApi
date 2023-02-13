using Ticketing.DataLayer.Entities.Admin;

namespace Ticketing.Core.Services.Interfaces;

public interface IAdminServices
{
    public Task<IEnumerable<Admin>> GetAdminsAsync();
    public Task<Admin?> GetAdminByIdAsync(int id, bool includeAnswers = false);
    public Task AddAdminAsync(Admin admin);
    public Task UpdateAdmin(Admin admin);
    public Task SaveChangesAsync();
    public Task DeleteAdmin(Admin admin);
    public Task<bool> IsExistAdmin(int id);
}