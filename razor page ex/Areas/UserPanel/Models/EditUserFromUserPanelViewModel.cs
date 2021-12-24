using System.ComponentModel.DataAnnotations;

namespace razor_page_ex.Areas.UserPanel.Models
{
    public class EditUserFromUserPanelViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "تغییر نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set;}

        [Display(Name = "تغییر رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set;}
    }
}
