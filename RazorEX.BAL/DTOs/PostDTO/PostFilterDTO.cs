using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.DTOs.PostDTO
{
    public class PostFilterDTO
    {
        public int PageCount { get; set; }
        public List<PostDTO> Posts { get; set; }
        public PostFilterParams FilterParams { get; set; }
    }
    public class PostFilterParams
    {
        public int Take { get; set; }
        public int PageId { get; set; }
        public string Title { get; set; }
        public string CategorySlug { get; set; }
    }
}
