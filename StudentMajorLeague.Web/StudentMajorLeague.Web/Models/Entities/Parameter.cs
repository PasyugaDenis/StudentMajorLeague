using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMajorLeague.Web.Models.Entities
{
    public class Parameter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int UserId { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public string Description { get; set; }
    }
}