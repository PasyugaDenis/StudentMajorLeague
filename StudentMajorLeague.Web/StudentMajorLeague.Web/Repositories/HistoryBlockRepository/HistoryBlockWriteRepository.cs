using StudentMajorLeague.Web.Infrastructure;
using StudentMajorLeague.Web.Models.Entities;
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
    }
}