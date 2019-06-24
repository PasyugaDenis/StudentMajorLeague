using StudentMajorLeague.Web.Infrastructure;
using StudentMajorLeague.Web.Models.Entities;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.UserRepository
{
    public class UserWriteRepository : IUserWriteRepository
    {
        private SMLDbContext dbContext;

        public UserWriteRepository(SMLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> AddAsync(User user)
        {
            var result = dbContext.Users.Add(user);

            await dbContext.SaveChangesAsync();

            return result;
        }

        public Task UpdateAsync(User user)
        {
            dbContext.Entry(user).State = EntityState.Modified;
            return dbContext.SaveChangesAsync();
        }

        public Task RemoveAsync(User user)
        {
            dbContext.Users.Remove(user);
            return dbContext.SaveChangesAsync();
        }
    }
}