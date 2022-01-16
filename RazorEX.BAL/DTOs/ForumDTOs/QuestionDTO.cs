using RazorEx.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.DTOs.ForumDTOs
{
    public class QuestionDTO
    {
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public int ProductsId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public User User { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
