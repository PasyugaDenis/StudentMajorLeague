using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMajorLeague.Web.Models.Entities
{
    public class League
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int MainLeagueId { get; set; }
    }
}