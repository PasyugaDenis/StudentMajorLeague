using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.LeagueRepository
{
    public interface ILeagueWriteRepository
    {
        Task<League> AddAsync(League model);

        Task UpdateAsync(League model);

        Task RemoveAsync(League model);
    }
}
