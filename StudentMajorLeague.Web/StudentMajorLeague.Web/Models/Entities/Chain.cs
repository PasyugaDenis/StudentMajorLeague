using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMajorLeague.Web.Models.Entities
{
    public class Chain
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? LastBlockId { get; set; }

        public DateTime CreatedOn { get; set; }


        public User User { get; set; }

        public ICollection<HistoryBlock> HistoryBlocks { get; set; }
    }
}