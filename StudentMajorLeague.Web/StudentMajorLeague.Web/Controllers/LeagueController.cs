using StudentMajorLeague.Web.Infrastructure;
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
    }
}