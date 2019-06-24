using System;

namespace StudentMajorLeague.Web.Models.Responses
{
    public class UserResponseModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int RoleId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Birthday { get; set; }

        public string Education { get; set; }

        public string Phone { get; set; }

        public string City { get; set; }

        public int LeagueId { get; set; }

        public double? Height { get; set; }

        public double? Weight { get; set; }
    }
}