using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.DTOs.PostCommentsDTO
{
    public class PostCommentDTO
    {
        public int PostCommentId { get; set; }
        public string UserFullName { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
