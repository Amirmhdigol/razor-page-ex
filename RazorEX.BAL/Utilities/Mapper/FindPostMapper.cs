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
                Slug = post.Slug,
                Title = post.Title,
                UserId = post.UserId,
                Visit = post.Visit,
                CreationDate = post.CreationDate,
                Category = ToCategoryDTO.ToCatgoryDTO(post.Category),
                ImageName = post.ImageName,
                PostId = post.Id
            };
        }
    }
}
