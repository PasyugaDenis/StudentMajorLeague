using System;

namespace StudentMajorLeague.Web.Models.Requests
{
    public class UserRegistrationRequestModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime Birthday { get; set; }

        public int LeagueId { get; set; }

        public int? RoleId { get; set; }
    }
}