using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.DTOs.ForumDTOs
{
    public class AddAnswerDTO
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
