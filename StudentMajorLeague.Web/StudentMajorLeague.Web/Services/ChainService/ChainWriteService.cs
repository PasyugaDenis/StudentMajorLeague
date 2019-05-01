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

        private IHistoryBlockReadRepository blockReadRepository;
        private IHistoryBlockWriteRepository blockWriteRepository;

        public ChainWriteService(
            IChainReadService chainReadService,
            IChainReadRepository chainReadRepository,
            IChainWriteRepository chainWriteRepository,
            IHistoryBlockReadRepository blockReadRepository,
            IHistoryBlockWriteRepository blockWriteRepository)
        {
            this.chainReadService = chainReadService;

            this.chainReadRepository = chainReadRepository;
            this.chainWriteRepository = chainWriteRepository;

            this.blockReadRepository = blockReadRepository;
            this.blockWriteRepository = blockWriteRepository;
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

                block.CreatedOn = DateTime.UtcNow;

                if (chain.HistoryBlocks?.Count > 0)
                {
                    var lastBlock = await blockReadRepository.GetByIdAsync(chain.LastBlockId.Value);
                    block.PreviousHash = lastBlock.Hash;
                }

                var newBlock = await blockWriteRepository.AddAsync(block);

                //reload chain
                chain = await chainReadRepository.GetByIdAsync(chainId);

                chain.LastBlockId = newBlock.Id;

                await chainWriteRepository.UpdateAsync(chain);

                return newBlock;
            }

            return null;
        }
    }
}
