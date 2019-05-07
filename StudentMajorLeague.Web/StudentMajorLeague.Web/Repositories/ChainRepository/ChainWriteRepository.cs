using StudentMajorLeague.Web.Infrastructure;
using StudentMajorLeague.Web.Models.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.ChainRepository
{
    public class ChainWriteRepository : IChainWriteRepository
    {
        private SMLDbContext dbContext;

        public ChainWriteRepository(SMLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Chain> AddAsync(Chain model)
        {
            var result = dbContext.Chains.Add(model);

            await dbContext.SaveChangesAsync();

            return result;
        }

        public Task UpdateAsync(Chain model)
        {
            dbContext.Entry(model).State = EntityState.Modified;
            return dbContext.SaveChangesAsync();
        }

        public Task RemoveAsync(Chain model)
        {
            dbContext.Chains.Remove(model);
            return dbContext.SaveChangesAsync();
        }
    }
}