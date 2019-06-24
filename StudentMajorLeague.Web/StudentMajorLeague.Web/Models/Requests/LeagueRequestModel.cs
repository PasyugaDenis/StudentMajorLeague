using System.ComponentModel.DataAnnotations;

namespace StudentMajorLeague.Web.Models.Requests
{
    public class LeagueRequestModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        public string Description { get; set; }

        public int? MainLeagueId { get; set; }
    }
}