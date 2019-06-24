using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.LeagueService
{
    public interface ILeagueWriteService
    {
        Task<League> AddAsync(League model);

        Task UpdateAsync(League model);

        Task RemoveAsync(League model);
    }
}
