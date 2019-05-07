using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMajorLeague.Web.Models.Entities
{
    public class User
    {
        //user
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int RoleId { get; set; }

        //profile
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime? Birthday { get; set; }

        public string Education { get; set; }

        public string Phone { get; set; }

        public string City { get; set; }

        public int LeagueId { get; set; }

        //parameters
        public double? Weight { get; set; }

        public double? Height { get; set; }


        public virtual Role Role { get; set; }

        public virtual League League { get; set; }

        public virtual Chain Chain { get; set; }

        public virtual ICollection<Result> Results { get; set; }
    }
}