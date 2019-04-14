using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMajorLeague.Web.Models.Entities
{
    public class HistoryBlock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string Changes { get; set; }

        public string Hash { get; set; }

        public string PreviousHash { get; set; }

        public int AuthirId { get; set; }

        public int ChainId { get; set; }
    }
}