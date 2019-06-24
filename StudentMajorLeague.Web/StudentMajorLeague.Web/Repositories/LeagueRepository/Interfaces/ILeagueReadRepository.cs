using StudentMajorLeague.Web.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.LeagueRepository
{
    public interface ILeagueReadRepository
    {
        Task<League> GetByIdAsync(int id);

        Task<List<League>> GetSubleaguesAsync(int mainLeagueId);

        Task<List<League>> GetAllAsync();
    }
}
