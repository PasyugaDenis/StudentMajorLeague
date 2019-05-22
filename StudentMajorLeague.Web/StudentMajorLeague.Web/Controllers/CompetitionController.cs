using StudentMajorLeague.Web.Models.Entities;
using StudentMajorLeague.Web.Models.Requests;
using StudentMajorLeague.Web.Services.ChainService;
using StudentMajorLeague.Web.Services.ResultService;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudentMajorLeague.Web.Controllers
{
    [RoutePrefix("api/Competition")]
    public class CompetitionController : ApiController
    {
        private IResultReadService resultReadService;
        private IResultWriteService resultWriteService;

        private IChainReadService chainReadService;
        private IChainWriteService chainWriteService;

        public CompetitionController(
            IResultReadService resultReadService,
            IResultWriteService resultWriteService,
            IChainReadService chainReadService,
            IChainWriteService chainWriteService)
        {
            this.resultReadService = resultReadService;
            this.resultWriteService = resultWriteService;

            this.chainReadService = chainReadService;
            this.chainWriteService = chainWriteService;
        }

        [Route("SetResult")]
        [HttpPost]
        public async Task<IHttpActionResult> SetResult(IoTResultRequestModel result)
        {
            try
            {
                var model = new Result
                {
                    UserId = result.UserId,
                    CompetitionId = result.CompetitionId,
                    Value = result.Result
                };

                var newModel = await resultWriteService.AddAsync(model);

                var maxId = await chainReadService.GetMaxBlocksIdAsync();

                var block = new HistoryBlock
                {
                    Id = ++maxId,
                    Changes = $"User result: {result.Result}",
                    AuthorId = result.UserId,
                    CreatedOn = DateTime.UtcNow.Date
                };

                block.Hash = block.HashValues();

                await chainWriteService.AddHistoryBlockToChainAsync(result.UserId, block);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}