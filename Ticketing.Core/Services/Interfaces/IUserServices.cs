using Ticketing.DataLayer.Entities.User;

namespace Ticketing.Core.Services.Interfaces;

public interface IUserServices
{
    public Task<IEnumerable<User>> GetUsersAsync();
    public Task<User?> GetUserByIdAsync(int id, bool includeTicket=false);
    public Task<int> AddUserAsync(User user);
    public Task UpdateUserAsync(User user);
    public Task DeleteUserAsync(User user);
}