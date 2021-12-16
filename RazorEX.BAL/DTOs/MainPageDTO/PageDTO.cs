using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.DTOs.MainPageDTO
{
    public class PageDTO
    {
        public List<PostDTO.PostDTO> LatestPosts { get; set; }
        public List<PostDTO.PostDTO> SpecialPosts { get; set; }
        public List<MainPageCategoryDTO> Categories { get; set; }
    }

    public class MainPageCategoryDTO
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public bool IsMainCategory { get; set; }
        public int PostChild { get; set; }
    }
}
    