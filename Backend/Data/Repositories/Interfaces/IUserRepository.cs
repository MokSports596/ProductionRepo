using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int userId);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int userId);
    }
}
