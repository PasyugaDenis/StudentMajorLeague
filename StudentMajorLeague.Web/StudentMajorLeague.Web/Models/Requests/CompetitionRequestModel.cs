using System;
using System.ComponentModel.DataAnnotations;

namespace StudentMajorLeague.Web.Models.Requests
{
    public class CompetitionRequestModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        public string Description { get; set; }
    }
}