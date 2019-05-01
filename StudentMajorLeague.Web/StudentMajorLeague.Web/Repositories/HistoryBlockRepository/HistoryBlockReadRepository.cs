using StudentMajorLeague.Web.Infrastructure;
using StudentMajorLeague.Web.Models.Entities;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.HistoryBlockRepository
{
    public class HistoryBlockReadRepository : IHistoryBlockReadRepository
    {
        private SMLDbContext dbContext;

        public HistoryBlockReadRepository(SMLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<HistoryBlock> GetByIdAsync(int id)
        {
            var result = dbContext.HistoryBlocks.Where(m => m.Id == id).SingleOrDefaultAsync();

            return result;
        }
    }
}