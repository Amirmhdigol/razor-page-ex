using System;
using System.ComponentModel.DataAnnotations;

namespace razor_page_ex.Areas.UserPanel.Models
{
    public class UserWalletViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "مبلغ ")]
        public int Amount { get; set; }
        
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "مبلغ ")]
        public int Type { get; set; }
        
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "مبلغ ")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "مبلغ ")]
        public DateTime TransactionDate { get; set; }

    }
}
