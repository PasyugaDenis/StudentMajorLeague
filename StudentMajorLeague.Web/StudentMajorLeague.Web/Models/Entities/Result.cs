using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMajorLeague.Web.Models.Entities
{
    public class Result
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CompetitionId { get; set; }

        public string Title { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }


        public User User { get; set; }

        public Competition Competition { get; set; }
    }
}