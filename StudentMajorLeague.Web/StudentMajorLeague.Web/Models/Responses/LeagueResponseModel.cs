namespace StudentMajorLeague.Web.Models.Responses
{
    public class LeagueResponseModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? MainLeagueId { get; set; }
    }
}