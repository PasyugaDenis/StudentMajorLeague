using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using StudentMajorLeague.Web.Infrastructure;
using StudentMajorLeague.Web.Models.Entities;

namespace StudentMajorLeague.Web.Repositories.ResultRepository
{
    public class ResultWriteRepository:IResultWriteRepository
    {
        private SMLDbContext dbContext;

        public ResultWriteRepository(SMLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result> AddAsync(Result model)
        {
            var result = dbContext.Results.Add(model);

            await dbContext.SaveChangesAsync();

            return result;
        }

        public Task UpdateAsync(Result model)
        {
            dbContext.Entry(model).State = EntityState.Modified;

            return dbContext.SaveChangesAsync();
        }

        public Task RemoveAsync(Result model)
        {
            dbContext.Results.Remove(model);

            return dbContext.SaveChangesAsync();
        }
    }
}