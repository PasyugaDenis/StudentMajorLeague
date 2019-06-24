using StudentMajorLeague.Web.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.LeagueService
{
    public interface ILeagueReadService
    {
        Task<League> GetByIdAsync(int id);

        Task<List<League>> GetSubleaguesAsync(int mainLeagueId);

        Task<List<League>> GetAllAsync();
    }
}
