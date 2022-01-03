using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace razor_page_ex.Areas.Adminstration.Models.ProductM
{
    public class EditProductViewModel
    {
        [Display(Name = "انتخاب  عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public string Title { get; set; }

        [Display(Name = "انتخاب  قیمت")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public int Price { get; set; }

        [Display(Name = "انتخاب  توضیحات")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        [UIHint("Ckeditor4")]
        public string Description { get; set; }

        [Display(Name = "انتخاب  تگ ها ")]
        public string Tags { get; set; }

        [Display(Name = "انتخاب  عکس دوره")]
        public IFormFile ProductImageName { get; set; }

        [Display(Name = "انتخاب  فایل معرفی")]
        public IFormFile DemoFileName { get; set; }

        [Display(Name = "انتخاب  مدرس- فروشنده")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public int TeacherId { get; set; }

        [Display(Name = "انتخاب  دسته بندی")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public int CategoryId { get; set; }

        [Display(Name = "انتخاب زیر دسته بندی")]
        public int? SubCategoryId { get; set; }

        [Display(Name = "انتخاب  سطح دوره")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public int LevelId { get; set; }

        [Display(Name = "انتخاب  وضعیت")]
        [Required(ErrorMessage = "{0} را وارد کنید ")]
        public int StatusId { get; set; }
    }
}
