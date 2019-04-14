using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMajorLeague.Web.Models.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int RoleId { get; set; }
    }
}