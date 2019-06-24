using StudentMajorLeague.Web.Models.Entities;
using StudentMajorLeague.Web.Models.Requests;
using StudentMajorLeague.Web.Models.Responses;
using StudentMajorLeague.Web.Services.ChainService;
using StudentMajorLeague.Web.Services.CompetitionService;
using StudentMajorLeague.Web.Services.ResultService;
using System;
using System.Collections.Generic;
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

        private ICompetitionReadService competitionReadService;
        private ICompetitionWriteService competitionWriteService;

        public CompetitionController(
            IResultReadService resultReadService,
            IResultWriteService resultWriteService,
            IChainReadService chainReadService,
            IChainWriteService chainWriteService,
            ICompetitionReadService competitionReadService,
            ICompetitionWriteService competitionWriteService)
        {
            this.resultReadService = resultReadService;
            this.resultWriteService = resultWriteService;

            this.chainReadService = chainReadService;
            this.chainWriteService = chainWriteService;

            this.competitionReadService = competitionReadService;
            this.competitionWriteService = competitionWriteService;
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

        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var result = new List<CompetitionResponseModel>();

                var competitions = await competitionReadService.GetAllAsync();

                competitions.ForEach(m => result.Add(new CompetitionResponseModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Type = m.Type,
                    Date = m.Date,
                    Location = m.Location,
                    Description = m.Description
                }));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var competition = await competitionReadService.GetByIdAsync(id);

                var result = new CompetitionResponseModel
                {
                    Id = competition.Id,
                    Title = competition.Title,
                    Type = competition.Type,
                    Date = competition.Date,
                    Location = competition.Location,
                    Description = competition.Description
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IHttpActionResult> Add(CompetitionRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var competition = new Competition
                {
                    Title = model.Title,
                    Type = model.Type,
                    Date = model.Date,
                    Location = model.Location,
                    Description = model.Description
                };

                var result = await competitionWriteService.AddAsync(competition);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("Update")]
        [HttpPost]
        public async Task<IHttpActionResult> Update(CompetitionRequestModel model)
        {
            try
            {
                var competition = await competitionReadService.GetByIdAsync(model.Id);

                competition.Title = model.Title;
                competition.Type = model.Type;
                competition.Date = model.Date;
                competition.Location = model.Location;
                competition.Description = model.Description;

                await competitionWriteService.UpdateAsync(competition);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("Remove/{id:int}")]
        [HttpGet]
        public async Task<IHttpActionResult> Remove(int id)
        {
            try
            {
                var competition = await competitionReadService.GetByIdAsync(id);

                await competitionWriteService.RemoveAsync(competition);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}