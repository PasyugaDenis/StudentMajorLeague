using StudentMajorLeague.Web.Infrastructure;
using StudentMajorLeague.Web.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.HistoryBlockRepository
{
    public class HistoryBlockWriteRepository : IHistoryBlockWriteRepository
    {
        private SMLDbContext dbContext;

        public HistoryBlockWriteRepository(SMLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<HistoryBlock> AddAsync(HistoryBlock model)
        {
            var result = dbContext.HistoryBlocks.Add(model);

            await dbContext.SaveChangesAsync();

            return result;
        }

        public Task RemoveAsync(HistoryBlock model)
        {
            dbContext.HistoryBlocks.Remove(model);
            return dbContext.SaveChangesAsync();
        }

        public Task RemoveRangeAsync(IEnumerable<HistoryBlock> models)
        {
            dbContext.HistoryBlocks.RemoveRange(models);
            return dbContext.SaveChangesAsync();
        }
    }
}