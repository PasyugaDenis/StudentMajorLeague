using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMajorLeague.Web.Models.Entities
{
    public class Competition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public DateTime? Date { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }


        public ICollection<Result> Results { get; set; }
    }
}