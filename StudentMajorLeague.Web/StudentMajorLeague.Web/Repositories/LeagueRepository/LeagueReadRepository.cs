using StudentMajorLeague.Web.Infrastructure;
using StudentMajorLeague.Web.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StudentMajorLeague.Web.Repositories.LeagueRepository
{
    public class LeagueReadRepository:ILeagueReadRepository
    {
        private SMLDbContext dbContext;

        public LeagueReadRepository(SMLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<League> GetByIdAsync(int id)
        {
            var result = dbContext.Leagues.Where(m => m.Id == id).SingleOrDefaultAsync();

            return result;
        }

        public Task<List<League>> GetSubleaguesAsync(int mainLeagueId)
        {
            var result = dbContext.Leagues.Where(m => m.MainLeagueId == mainLeagueId).ToListAsync();

            return result;
        }

        public Task<List<League>> GetAllAsync()
        {
            var result = dbContext.Leagues.ToListAsync();

            return result;
        }
    }
}