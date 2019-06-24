using StudentMajorLeague.Web.Infrastructure;
using StudentMajorLeague.Web.Models.Entities;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.ChainRepository
{
    public class ChainReadRepository : IChainReadRepository
    {
        private SMLDbContext dbContext;

        public ChainReadRepository(SMLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Chain> GetByIdAsync(int id)
        {
            var result = dbContext.Chains.Where(m => m.Id == id).SingleOrDefaultAsync();

            return result;
        }
    }
}