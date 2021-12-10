using RazorEx.DAL.Entities;
using RazorEX.BAL.DTOs.PostDTO;

namespace RazorEX.BAL.Utilities.Mapper
{
    public class DeletePostMapper
    {
        public static Post Map(PostDTO PostDTO)
        {
            return new Post()
            {
                Description = PostDTO.Description,
                Slug = PostDTO.Slug,
                CategoryId = PostDTO.CategoryId,
                Title = PostDTO.Title,
                SubCategoryId = PostDTO.SubCategoryId

            };
        }

    }

}










