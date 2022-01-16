using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class Answer
    {
        public int AnswerId { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public bool IsTheTrueAnswer { get; set; } = false;

        public Question Question { get; set; }
        public User User { get; set; }

    }
}
