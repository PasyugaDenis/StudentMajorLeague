using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.ChainRepository
{
    public interface IChainReadRepository
    {
        Task<Chain> GetByIdAsync(int id);
    }
}
