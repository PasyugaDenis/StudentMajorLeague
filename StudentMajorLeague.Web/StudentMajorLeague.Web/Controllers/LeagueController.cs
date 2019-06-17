using StudentMajorLeague.Web.Models.Entities;
using StudentMajorLeague.Web.Models.Requests;
using StudentMajorLeague.Web.Models.Responses;
using StudentMajorLeague.Web.Services.LeagueService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudentMajorLeague.Web.Controllers
{
    [RoutePrefix("api/League")]
    public class LeagueController : ApiController
    {
        private ILeagueReadService leagueReadService;
        private ILeagueWriteService leagueWriteService;

        public LeagueController(
            ILeagueReadService leagueReadService,
            ILeagueWriteService leagueWriteService)
        {
            this.leagueReadService = leagueReadService;
            this.leagueWriteService = leagueWriteService;
        }

        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var result = new List<LeagueResponseModel>();

                var leagues = await leagueReadService.GetAllAsync();

                leagues.ForEach(m =>
                {
                    result.Add(new LeagueResponseModel
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Description = m.Description,
                        MainLeagueId = m.MainLeagueId
                    });
                });

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
                var result = await leagueReadService.GetByIdAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IHttpActionResult> Add(LeagueRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var league = new League
                {
                    Title = model.Title,
                    Description = model.Description,
                    MainLeagueId = model.MainLeagueId
                };

                var result = await leagueWriteService.AddAsync(league);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("Update")]
        [HttpPost]
        public async Task<IHttpActionResult> Update(LeagueRequestModel model)
        {
            try
            {
                var league = await leagueReadService.GetByIdAsync(model.Id);

                league.Title = model.Title;
                league.Description = model.Description;
                league.MainLeagueId = model.MainLeagueId;

                await leagueWriteService.UpdateAsync(league);

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
                var league = await leagueReadService.GetByIdAsync(id);

                await leagueWriteService.RemoveAsync(league);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}