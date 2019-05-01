using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.RoleRepository
{
    public interface IRoleReadRepository
    {
        Task<Role> GetByIdAsync(int id);

        Task<Role> GetByValueAsync(string value);

        Task<Role> GetAdminRoleAsync();

        Task<Role> GetStudentRoleAsync();
    }
}
