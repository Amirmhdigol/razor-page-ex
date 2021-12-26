using System.ComponentModel.DataAnnotations;

namespace razor_page_ex.Areas.UserPanel.Models
{
    public class ChargeWalletViewModel
    {
        [Display(Name = " مبلغ ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }
    }
}
