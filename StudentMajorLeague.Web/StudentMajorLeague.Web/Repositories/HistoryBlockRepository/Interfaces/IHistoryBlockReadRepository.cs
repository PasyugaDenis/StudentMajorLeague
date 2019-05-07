using StudentMajorLeague.Web.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.HistoryBlockRepository
{
    public interface IHistoryBlockReadRepository
    {
        Task<HistoryBlock> GetByIdAsync(int id);

        Task<List<HistoryBlock>> GetBlocksByChainIdAsync(int chainId);

        Task<int> GetMaxIdAsync();
    }
}
