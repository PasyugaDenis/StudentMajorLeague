using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.RoleService
{
    public interface IRoleReadService
    {
        Task<Role> GetByIdAsync(int id);

        Task<Role> GetByValueAsync(string value);

        Task<Role> GetAdminRoleAsync();

        Task<Role> GetStudentRoleAsync();
    }
}
