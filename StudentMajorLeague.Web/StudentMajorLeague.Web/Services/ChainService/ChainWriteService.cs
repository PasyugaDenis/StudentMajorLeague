using StudentMajorLeague.Web.Models.Entities;
using StudentMajorLeague.Web.Repositories.ChainRepository;
using StudentMajorLeague.Web.Repositories.HistoryBlockRepository;
using System;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.ChainService
{
    public class ChainWriteService : IChainWriteService
    {
        private IChainReadService chainReadService;

        private IChainReadRepository chainReadRepository;
        private IChainWriteRepository chainWriteRepository;

        private IHistoryBlockReadRepository historyBlockReadRepository;
        private IHistoryBlockWriteRepository historyBlockWriteRepository;

        public ChainWriteService(
            IChainReadService chainReadService,
            IChainReadRepository chainReadRepository,
            IChainWriteRepository chainWriteRepository,
            IHistoryBlockReadRepository historyBlockReadRepository,
            IHistoryBlockWriteRepository historyBlockWriteRepository)
        {
            this.chainReadService = chainReadService;

            this.chainReadRepository = chainReadRepository;
            this.chainWriteRepository = chainWriteRepository;

            this.historyBlockReadRepository = historyBlockReadRepository;
            this.historyBlockWriteRepository = historyBlockWriteRepository;
        }

        public async Task<Chain> CreateChainAsync(Chain chain)
        {
            chain.CreatedOn = DateTime.UtcNow;

            var result = await chainWriteRepository.AddAsync(chain);

            return result;
        }

        public async Task<HistoryBlock> AddHistoryBlockToChainAsync(int chainId, HistoryBlock block)
        {
            if (await chainReadService.IsChainValidAsync(chainId))
            {
                var chain = await chainReadRepository.GetByIdAsync(chainId);
                var blocks = await historyBlockReadRepository.GetBlocksByChainIdAsync(chainId);

                block.ChainId = chainId;

                if (blocks?.Count > 0)
                {
                    var lastBlock = await historyBlockReadRepository.GetByIdAsync(chain.LastBlockId.Value);
                    block.PreviousHash = lastBlock.Hash;
                }

                var newBlock = await historyBlockWriteRepository.AddAsync(block);

                //reload chain
                chain = await chainReadRepository.GetByIdAsync(chainId);

                chain.LastBlockId = newBlock.Id;

                await chainWriteRepository.UpdateAsync(chain);

                return newBlock;
            }

            return null;
        }

        public async Task RemoveChainAsync(int chainId)
        {
            //remove lastBlockId
            var chain = await chainReadRepository.GetByIdAsync(chainId);

            chain.LastBlockId = null;

            await chainWriteRepository.UpdateAsync(chain);

            //remove blocks
            var blocks = await historyBlockReadRepository.GetBlocksByChainIdAsync(chainId);

            await historyBlockWriteRepository.RemoveRangeAsync(blocks);

            //remove chain
            chain = await chainReadRepository.GetByIdAsync(chainId);

            await chainWriteRepository.RemoveAsync(chain);
        }
    }
}
