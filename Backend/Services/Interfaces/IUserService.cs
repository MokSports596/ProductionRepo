using System.Collections.Generic;
using System.Threading.Tasks;
using MokSportsApp.Models;

namespace MokSportsApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> AuthenticateUser(string email, string password, string deviceToken);
        Task AddUser(User user, string deviceToken);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task<List<League>> GetUserLeaguesAsync(int userId);
    }
}
