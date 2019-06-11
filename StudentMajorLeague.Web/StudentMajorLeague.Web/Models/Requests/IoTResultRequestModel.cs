using System.ComponentModel.DataAnnotations;

namespace StudentMajorLeague.Web.Models.Requests
{
    public class IoTResultRequestModel
    {
        [Required(ErrorMessage = "User id is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Competition id is required")]
        public int CompetitionId { get; set; }

        [Required(ErrorMessage = "Result is required")]
        public string Result { get; set; }
    }
}