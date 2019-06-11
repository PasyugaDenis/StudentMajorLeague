using StudentMajorLeague.Web.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.CompetitionService
{
    public interface ICompetitionReadService
    {
        Task<Competition> GetByIdAsync(int id);

        Task<List<Competition>> GetAllAsync();
    }
}
