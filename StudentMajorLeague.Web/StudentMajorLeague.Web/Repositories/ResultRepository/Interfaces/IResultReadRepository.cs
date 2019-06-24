using StudentMajorLeague.Web.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.ResultRepository
{
    public interface IResultReadRepository
    {
        Task<Result> GetByIdAsync(int id);

        Task<List<Result>> GetAllAsync();
    }
}
