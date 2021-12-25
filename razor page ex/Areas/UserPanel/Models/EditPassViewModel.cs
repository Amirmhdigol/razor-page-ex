using System.ComponentModel.DataAnnotations;

namespace razor_page_ex.Areas.UserPanel.Models
{
    public class EditPassViewModel
    {
        [Display(Name = " رمز عبور قبلی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string OldPassword { get; set; }

        [Display(Name = " رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string RePassword { get; set; }
    }
}
