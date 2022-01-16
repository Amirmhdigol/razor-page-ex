using RazorEx.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.DTOs.ForumDTOs
{
    public class ShowQuestionDTO
    {
        public QuestionDTO Question { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
