using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.DTOs.ForumDTOs
{
    public class AddQuestionDTO
    {
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public int ProductsId { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [UIHint("Ckeditor4")]
        [Display(Name = "بدنه پیام")]
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
