using System.Collections.Generic;
using System.Threading.Tasks;
using MokSportsApp.Models;

namespace MokSportsApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> AuthenticateUser(string email, string password);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task<List<League>> GetUserLeaguesAsync(int userId);
    }
}
