using System.ComponentModel.DataAnnotations;

namespace StudentMajorLeague.Web.Models.Requests
{
    public class AuthorizationRequestModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}