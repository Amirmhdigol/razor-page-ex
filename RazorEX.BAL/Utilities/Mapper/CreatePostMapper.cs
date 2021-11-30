using RazorEx.DAL.Entities;
using RazorEX.BAL.DTOs.PostDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Utilities.Mapper
{
    public class CreatePostMapper
    {
        public static Post Map(CreatePostDTO createPostDTO)
        {
            return new Post()
            {
                Description = createPostDTO.Description,
                CategoryId = createPostDTO.CategoryId,
                Slug = createPostDTO.Slug,
                Title = createPostDTO.Title,
                UserId = createPostDTO.UserId,
                Visit = 0,
                IsDelete = false,
                SubCategoryId = createPostDTO.SubCategoryId

            };
        }

    }
}









