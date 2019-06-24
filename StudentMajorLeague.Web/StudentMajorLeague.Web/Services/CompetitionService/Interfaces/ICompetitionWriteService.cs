using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.CompetitionService
{
    public interface ICompetitionWriteService
    {
        Task<Competition> AddAsync(Competition model);

        Task UpdateAsync(Competition model);

        Task RemoveAsync(Competition model);
    }
}
