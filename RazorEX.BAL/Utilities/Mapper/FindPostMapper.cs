using RazorEx.DAL.Entities;
using RazorEX.BAL.DTOs.PostDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Utilities.Mapper
{
    public class FindPostMapper
    {
        public static PostDTO Map(Post post)
        {
            return new PostDTO()
            {
                Description = post.Description,
                CategoryId = post.CategoryId,
                UserName = post.User?.UserName,
                Slug = post.Slug,
                Title = post.Title,
                UserId = post.UserId,
                Visit = post.Visit,
                CreationDate = post.CreationDate,
                Category = post.Category == null ? null : ToCategoryDTO.ToCatgoryDTO(post.Category),
                ImageName = post.ImageName,
                PostId = post.Id,
                SubCategoryId = post.SubCategoryId,
                SubCategory = post.SubCategory == null ? null : ToCategoryDTO.ToCatgoryDTO(post.SubCategory)
            };
        }
    }
}
