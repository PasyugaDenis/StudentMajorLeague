using StudentMajorLeague.Web.Infrastructure;
using StudentMajorLeague.Web.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.ResultRepository
{
    public class ResultReadRepository : IResultReadRepository
    {
        private SMLDbContext dbContext;

        public ResultReadRepository(SMLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Result> GetByIdAsync(int id)
        {
            var result = dbContext.Results.Where(m => m.Id == id).SingleOrDefaultAsync();

            return result;
        }

        public Task<List<Result>> GetAllAsync()
        {
            var result = dbContext.Results.ToListAsync();

            return result;
        }
    }
}