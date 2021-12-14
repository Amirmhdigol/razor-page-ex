using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace razor_page_ex.Areas.Adminstration.Models.PostsM
{
    public class EditPostViewModel
    {
        [Display(Name = "انتخاب دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryId { get; set; }
       
        [Display(Name = "انتخاب زیر دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? SubCategoryId { get; set; }
        
        [Display(Name = "انتخاب عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        
        [Display(Name = "انتخابslug ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Slug { get; set; }
        
        [Display(Name = "انتخاب توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [UIHint("Ckeditor4")]
        public string Description { get; set; }

        [Display(Name = "پست ویژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool IsSpecial { get; set; }

        [Display(Name = "انتخاب عکس")]
        public IFormFile ImageFile { get; set; }
    }
}
