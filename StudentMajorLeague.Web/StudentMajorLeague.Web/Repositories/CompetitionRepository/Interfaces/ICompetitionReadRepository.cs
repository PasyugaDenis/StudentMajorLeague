using StudentMajorLeague.Web.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.CompetitionRepository
{
    public interface ICompetitionReadRepository
    {
        Task<Competition> GetByIdAsync(int id);

        Task<List<Competition>> GetAllAsync();
    }
}
