using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMajorLeague.Web.Models.Entities
{
    public class Chain
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int? LastBlockId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}