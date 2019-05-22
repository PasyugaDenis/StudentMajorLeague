using StudentMajorLeague.Web.Models.Entities;
using StudentMajorLeague.Web.Repositories.ResultRepository;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.ResultService
{
    public class ResultWriteService : IResultWriteService
    {
        private IResultWriteRepository resultWriteRepository;

        public ResultWriteService(IResultWriteRepository resultWriteRepository)
        {
            this.resultWriteRepository = resultWriteRepository;
        }

        public async Task<Result> AddAsync(Result model)
        {
            var result = await resultWriteRepository.AddAsync(model);

            return result;
        }
    }
}