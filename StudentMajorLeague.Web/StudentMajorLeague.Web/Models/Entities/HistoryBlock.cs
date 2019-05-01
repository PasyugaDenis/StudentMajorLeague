using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace StudentMajorLeague.Web.Models.Entities
{
    public class HistoryBlock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string Changes { get; set; }

        public string Hash { get; set; }

        public string PreviousHash { get; set; }

        public int AuthorId { get; set; }

        public int ChainId { get; set; }


        public Chain Chain { get; set; }


        public string HashValues()
        {
            var value = $"Id={Id};CreatedOn={CreatedOn.ToString()};Changes={Changes};AuthorId={AuthorId}";

            var bytes = Encoding.Unicode.GetBytes(value);

            var CSP = new MD5CryptoServiceProvider();

            var byteHash = CSP.ComputeHash(bytes);

            var hash = new StringBuilder();

            foreach (byte b in byteHash)
            {
                hash.Append(string.Format("{0:x2}", b));
            }

            return hash.ToString();
        }
    }
}