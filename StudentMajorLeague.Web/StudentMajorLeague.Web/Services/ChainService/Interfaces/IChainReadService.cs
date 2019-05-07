using StudentMajorLeague.Web.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.ChainService
{
    public interface IChainReadService
    {
        Task<Chain> GetChainByIdAsync(int chainId);

        Task<HistoryBlock> GetBlockByIdAsync(int blockId);

        Task<HistoryBlock> GetLastBlockInChainAsync(int chainId);

        Task<List<HistoryBlock>> GetBlocksInChainAsync(int chainId);

        Task<bool> IsChainValidAsync(int chainId);

        Task<int> GetMaxBlocksIdAsync();
    }
}
