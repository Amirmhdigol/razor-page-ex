using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Entities
{
    public class Question
    {
        public int QuestionId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ProductsId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Products Products { get; set; }
        public User User { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
