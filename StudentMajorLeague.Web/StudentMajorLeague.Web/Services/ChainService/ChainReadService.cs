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

        public Task<Chain> GetChainByIdAsync(int chainId)
        {
            var result = chainReadRepository.GetByIdAsync(chainId);

            return result;
        }

        public Task<HistoryBlock> GetBlockByIdAsync(int blockId)
        {
            var result = historyBlockReadRepository.GetByIdAsync(blockId);

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

        public Task<List<HistoryBlock>> GetBlocksInChainAsync(int chainId)
        {
            var blocks = historyBlockReadRepository.GetBlocksByChainIdAsync(chainId);

            return blocks;
        }

        public async Task<bool> IsChainValidAsync(int chainId)
        {
            var chain = await chainReadRepository.GetByIdAsync(chainId);
            var blocks = await historyBlockReadRepository.GetBlocksByChainIdAsync(chainId);

            if (blocks?.Count > 0)
            {
                HistoryBlock previousBlock = null;
                HistoryBlock block = null;

                for (var i = 0; i < blocks.Count; i++)
                {
                    block = blocks.ElementAt(i);

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

        public Task<int> GetMaxBlocksIdAsync()
        {
            var result = historyBlockReadRepository.GetMaxIdAsync();

            return result;
        }
    }
}
