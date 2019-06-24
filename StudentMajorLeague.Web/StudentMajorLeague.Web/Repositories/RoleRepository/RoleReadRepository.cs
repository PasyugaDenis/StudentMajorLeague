using StudentMajorLeague.Web.Infrastructure;
using StudentMajorLeague.Web.Models.Entities;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.RoleRepository
{
    public class RoleReadRepository : IRoleReadRepository
    {
        private SMLDbContext dbContext;

        public RoleReadRepository(SMLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Role> GetByIdAsync(int id)
        {
            var result = dbContext.Roles.Where(m => m.Id == id).SingleOrDefaultAsync();

            return result;
        }

        public Task<Role> GetByValueAsync(string value)
        {
            var result = dbContext.Roles.Where(m => m.Value == value).SingleOrDefaultAsync();

            return result;
        }

        public Task<Role> GetAdminRoleAsync()
        {
            var result = dbContext.Roles.Where(m => m.Value.ToLower() == "admin").SingleOrDefaultAsync();

            return result;
        }

        public Task<Role> GetStudentRoleAsync()
        {
            var result = dbContext.Roles.Where(m => m.Value.ToLower() == "student").SingleOrDefaultAsync();

            return result;
        }
    }
}