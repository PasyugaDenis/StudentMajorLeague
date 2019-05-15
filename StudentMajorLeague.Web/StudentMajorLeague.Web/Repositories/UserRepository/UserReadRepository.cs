using StudentMajorLeague.Web.Infrastructure;
using StudentMajorLeague.Web.Models.Entities;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.UserRepository
{
    public class UserReadRepository : IUserReadRepository
    {
        private SMLDbContext dbContext;

        public UserReadRepository(SMLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<User> GetByIdAsync(int id)
        {
            var result = dbContext.Users.Where(m => m.Id == id).SingleOrDefaultAsync();

            return result;
        }

        public Task<User> GetByEmailAsync(string email)
        {
            var result = dbContext.Users.Where(m => m.Email == email).SingleOrDefaultAsync();

            return result;
        }

        public Task<User[]> GetAllAsync()
        {
            var result = dbContext.Users.ToArrayAsync();

            return result;
        }
    }
}