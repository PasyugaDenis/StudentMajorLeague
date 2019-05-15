using StudentMajorLeague.Web.Models.Entities;
using StudentMajorLeague.Web.Repositories.LeagueRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.LeagueService
{
    public class LeagueReadService : ILeagueReadService
    {
        private ILeagueReadRepository leagueReadRepository;

        public LeagueReadService(ILeagueReadRepository leagueReadRepository)
        {
            this.leagueReadRepository = leagueReadRepository;
        }

        public Task<List<League>> GetAllAsync()
        {
            var result = leagueReadRepository.GetAllAsync();

            return result;
        }
    }
}