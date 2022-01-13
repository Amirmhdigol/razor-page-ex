using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class ProductVote
    {
        public int ProductVoteId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ProductsId { get; set; }
        [Required]
        public bool Vote { get; set; }
        public DateTime VoteDate { get; set; }= DateTime.Now;

        public Products Products { get; set; }
        public User User { get; set; }
    }
}
