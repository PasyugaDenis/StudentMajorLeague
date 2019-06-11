using StudentMajorLeague.Web.Models.Entities;
using StudentMajorLeague.Web.Repositories.CompetitionRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.CompetitionService
{
    public class CompetitionReadService : ICompetitionReadService
    {
        private ICompetitionReadRepository competitionReadRepository;

        public CompetitionReadService(ICompetitionReadRepository competitionReadRepository)
        {
            this.competitionReadRepository = competitionReadRepository;
        }

        public Task<List<Competition>> GetAllAsync()
        {
            var result = competitionReadRepository.GetAllAsync();

            return result;
        }

        public Task<Competition> GetByIdAsync(int id)
        {
            var result = competitionReadRepository.GetByIdAsync(id);

            return result;
        }
    }
}