using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.ChainRepository
{
    public interface IChainWriteRepository
    {
        Task<Chain> AddAsync(Chain model);

        Task UpdateAsync(Chain model);
    }
}
