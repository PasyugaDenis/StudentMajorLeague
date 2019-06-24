using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.CompetitionRepository
{
    public interface ICompetitionWriteRepository
    {
        Task<Competition> AddAsync(Competition model);

        Task UpdateAsync(Competition model);

        Task RemoveAsync(Competition model);
    }
}
