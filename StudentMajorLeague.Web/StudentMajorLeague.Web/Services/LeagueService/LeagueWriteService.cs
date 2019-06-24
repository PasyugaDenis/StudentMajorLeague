using StudentMajorLeague.Web.Models.Entities;
using StudentMajorLeague.Web.Repositories.LeagueRepository;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.LeagueService
{
    public class LeagueWriteService : ILeagueWriteService
    {
        private ILeagueWriteRepository leagueWriteRepository;

        public LeagueWriteService(ILeagueWriteRepository leagueReadRepository)
        {
            this.leagueWriteRepository = leagueReadRepository;
        }

        public Task<League> AddAsync(League league)
        {
            var result = leagueWriteRepository.AddAsync(league);

            return result;
        }

        public Task UpdateAsync(League league)
        {
            var result = leagueWriteRepository.UpdateAsync(league);

            return result;
        }

        public Task RemoveAsync(League league)
        {
            var result = leagueWriteRepository.RemoveAsync(league);

            return result;
        }
    }
}