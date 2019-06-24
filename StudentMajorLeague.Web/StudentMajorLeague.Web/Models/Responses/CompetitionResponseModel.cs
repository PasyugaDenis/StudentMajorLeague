using System;

namespace StudentMajorLeague.Web.Models.Responses
{
    public class CompetitionResponseModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public DateTime? Date { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }
    }
}