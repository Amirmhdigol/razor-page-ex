using RazorEX.BAL.DTOs.CategoryDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace razor_page_ex.Areas.Adminstration.Models
{
    public class CreateCategoryViewModel
    {
        [Display(Name = " عنوان")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string Title { get; set; }

        [Display(Name = " Slug")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string Slug { get; set; }
        
        [Display(Name ="parent id")]
        public int? ParentId { get; set; }
        
        [Display(Name = " MetaTag")]
        public string MetaTag { get; set; }
        
        [Display(Name = " MetaDescription")]
        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }

        public CreateCategoryDto Map()
        {
            return new CreateCategoryDto()
            {
                Title = Title,
                Slug = Slug,
                MetaDescription = MetaDescription,
                MetaTag = MetaTag,
                ParentId = ParentId
                
            };
        }

    }


}
