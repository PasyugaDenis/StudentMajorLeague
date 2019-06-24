using StudentMajorLeague.Web.Models.Entities;
using StudentMajorLeague.Web.Repositories.CompetitionRepository;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.CompetitionService
{
    public class CompetitionWriteService : ICompetitionWriteService
    {
        private ICompetitionWriteRepository competitionWriteRepository;

        public CompetitionWriteService(ICompetitionWriteRepository competitionWriteRepository)
        {
            this.competitionWriteRepository = competitionWriteRepository;
        }

        public Task<Competition> AddAsync(Competition model)
        {
            var result = competitionWriteRepository.AddAsync(model);

            return result;
        }

        public Task UpdateAsync(Competition model)
        {
            var result = competitionWriteRepository.UpdateAsync(model);

            return result;
        }

        public Task RemoveAsync(Competition model)
        {
            var result = competitionWriteRepository.RemoveAsync(model);

            return result;
        }
    }
}