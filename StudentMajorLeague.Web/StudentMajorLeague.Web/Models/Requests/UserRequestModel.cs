using System;
using System.ComponentModel.DataAnnotations;

namespace StudentMajorLeague.Web.Models.Requests
{
    public class UserRequestModel
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Birthday { get; set; }

        public string Education { get; set; }

        public string Phone { get; set; }

        public string City { get; set; }

        [Required(ErrorMessage = "Id is required")]
        public int LeagueId { get; set; }

        public double? Height { get; set; }

        public double? Weight { get; set; }
    }
}