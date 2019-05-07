using StudentMajorLeague.Web.Infrastructure;
using StudentMajorLeague.Web.Models.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.LeagueRepository
{
    public class LeagueWriteRepository : ILeagueWriteRepository
    {
        private SMLDbContext dbContext;

        public LeagueWriteRepository(SMLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<League> AddAsync(League league)
        {
            var result = dbContext.Leagues.Add(league);

            await dbContext.SaveChangesAsync();

            return result;
        }

        public Task UpdateAsync(League league)
        {
            dbContext.Entry(league).State = EntityState.Modified;
            return dbContext.SaveChangesAsync();
        }

        public Task RemoveAsync(League league)
        {
            dbContext.Leagues.Remove(league);

            return dbContext.SaveChangesAsync();
        }
    }
}