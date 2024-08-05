using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface RoleInterface
    {
        Task<IEnumerable<Role>> GetAllRoles();
        Task<Role> GetRoleById(int roleId);
        Task AddRole(Role role);
        Task UpdateRole(Role role);
        Task DeleteRole(int roleId);
    }
}
