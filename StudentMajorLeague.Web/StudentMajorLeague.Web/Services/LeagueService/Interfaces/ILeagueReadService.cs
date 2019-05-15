using StudentMajorLeague.Web.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.LeagueService
{
    public interface ILeagueReadService
    {
        Task<List<League>> GetAllAsync();
    }
}
