using StudentMajorLeague.Web.Models.Entities;
using StudentMajorLeague.Web.Repositories.ChainRepository;
using StudentMajorLeague.Web.Repositories.HistoryBlockRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.ChainService
{
    public class ChainReadService : IChainReadService
    {
        private IChainReadRepository chainReadRepository;

        private IHistoryBlockReadRepository historyBlockReadRepository;

        public ChainReadService(
            IChainReadRepository chainReadRepository,
            IHistoryBlockReadRepository historyBlockReadRepository)
        {
            this.chainReadRepository = chainReadRepository;

            this.historyBlockReadRepository = historyBlockReadRepository;
        }

        public async Task<Chain> GetChainByIdAsync(int chainId)
        {
            var result = await chainReadRepository.GetByIdAsync(chainId);

            return result;
        }

        public async Task<HistoryBlock> GetBlockByIdAsync(int blockId)
        {
            var result = await historyBlockReadRepository.GetByIdAsync(blockId);

            return result;
        }

        public async Task<HistoryBlock> GetLastBlockInChainAsync(int chainId)
        {
            var chain = await chainReadRepository.GetByIdAsync(chainId);

            if (chain.LastBlockId.HasValue)
            {
                var result = await historyBlockReadRepository.GetByIdAsync(chain.LastBlockId.Value);

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<HistoryBlock>> GetBlocksInChainAsync(int chainId)
        {
            var chain = await chainReadRepository.GetByIdAsync(chainId);

            return chain.HistoryBlocks.ToList();
        }

        public async Task<bool> IsChainValidAsync(int chainId)
        {
            var chain = await chainReadRepository.GetByIdAsync(chainId);

            if (chain.HistoryBlocks?.Count > 2)
            {
                HistoryBlock previousBlock = null;
                HistoryBlock block = null;

                for (var i = 0; i < chain.HistoryBlocks.Count; i++)
                {
                    block = chain.HistoryBlocks.ElementAt(i);

                    if (block.Hash != block.HashValues())
                    {
                        return false;
                    }

                    if (previousBlock != null && previousBlock.Hash != block.PreviousHash)
                    {
                        return false;
                    }

                    previousBlock = block;
                }
            }

            return true;
        }
    }
}
