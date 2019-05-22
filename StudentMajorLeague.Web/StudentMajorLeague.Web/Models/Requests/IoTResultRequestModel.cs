namespace StudentMajorLeague.Web.Models.Requests
{
    public class IoTResultRequestModel
    {
        public int UserId { get; set; }

        public int CompetitionId { get; set; }

        public string Result { get; set; }
    }
}