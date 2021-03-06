using RazorEX.BAL.DTOs.CategoryDTO;
using System;

namespace RazorEX.BAL.DTOs.PostDTO
{
    public class PostDTO
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public int Visit { get; set; }
        public bool IsSpecial { get; set; }
        public DateTime CreationDate { get; set; }
        public CategoryDto Category { get; set; }
        public CategoryDto SubCategory { get; set; }
    }
}





