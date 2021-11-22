using RazorEx.DAL.Entities;
using RazorEX.BAL.DTOs.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEX.BAL.Utilities
{
    public class ToCategoryDTO
    {
        public static CategoryDto ToCatgoryDTO(Category category)
        {
            return new CategoryDto()
            {
                Title = category.Title,
                Slug = category.Slug,
                MetaDescription = category.MetaDescription,
                MetaTag = category.MetaTag,
                Id= category.Id,
                ParentId = category.ParentId 
            };
        }
    }
}
