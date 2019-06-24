using StudentMajorLeague.Web.Infrastructure;
using StudentMajorLeague.Web.Models.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.CompetitionRepository
{
    public class CompetitionWriteRepository : ICompetitionWriteRepository
    {
        private SMLDbContext dbContext;

        public CompetitionWriteRepository(SMLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Competition> AddAsync(Competition competition)
        {
            var result = dbContext.Competitions.Add(competition);

            await dbContext.SaveChangesAsync();

            return result;
        }

        public Task UpdateAsync(Competition competiton)
        {
            dbContext.Entry(competiton).State = EntityState.Modified;

            return dbContext.SaveChangesAsync();
        }

        public Task RemoveAsync(Competition competition)
        {
            dbContext.Competitions.Remove(competition);

            return dbContext.SaveChangesAsync();
        }
    }
}