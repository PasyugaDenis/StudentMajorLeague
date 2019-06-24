using StudentMajorLeague.Web.Models.Entities;
using StudentMajorLeague.Web.Repositories.RoleRepository;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.RoleService
{
    public class RoleReadService : IRoleReadService
    {
        private IRoleReadRepository roleReadRepository;

        public RoleReadService(IRoleReadRepository roleReadRepository)
        {
            this.roleReadRepository = roleReadRepository;
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            var result = await roleReadRepository.GetByIdAsync(id);

            return result;
        }

        public async Task<Role> GetByValueAsync(string value)
        {
            var result = await roleReadRepository.GetByValueAsync(value);

            return result;
        }

        public async Task<Role> GetAdminRoleAsync()
        {
            var result = await roleReadRepository.GetAdminRoleAsync();

            return result;
        }

        public async Task<Role> GetStudentRoleAsync()
        {
            var result = await roleReadRepository.GetStudentRoleAsync();

            return result;
        }
    }
}