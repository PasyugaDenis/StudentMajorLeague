using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.ResultRepository
{
    public interface IResultWriteRepository
    {
        Task<Result> AddAsync(Result model);

        Task UpdateAsync(Result model);

        Task RemoveAsync(Result model);
    }
}
