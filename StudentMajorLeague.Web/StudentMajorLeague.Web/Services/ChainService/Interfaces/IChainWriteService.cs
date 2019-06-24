using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.ChainService
{
    public interface IChainWriteService
    {
        Task<Chain> CreateChainAsync(Chain chain);

        Task<HistoryBlock> AddHistoryBlockToChainAsync(int chainId, HistoryBlock block);

        Task RemoveChainAsync(int chainId);
    }
}
