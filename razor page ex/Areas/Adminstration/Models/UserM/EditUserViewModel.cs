using RazorEx.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace razor_page_ex.Areas.Adminstration.Models.UserM
{
    public class EditUserViewModel
    {
        [Display(Name = "انتخاب نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FullName { get; set; }

        [Display(Name = "انتخاب نقش کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public UserRole Role { get; set; }

        [Display(Name = "انتخاب نقش کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }
    }
}
