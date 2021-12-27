using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorEX.BAL.Contracts;

namespace razor_page_ex.Pages
{
    public class OnlinePaymentModel : PageModel
    {
        private readonly IUser _userService;

        public OnlinePaymentModel(IUser userService)
        {
            _userService = userService;
        }
        public bool IsSuccess { get; set; }
        public long RefId { get; set; }

        
        public IActionResult OnGet(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                   HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                   && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

                var wallet = _userService.GetWalletByWalletId(id);

                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    RefId = res.RefId;
                    IsSuccess = true;
                    wallet.IsPay = true;
                    _userService.UpdateWallet(wallet);
                }
            }
            return Page();
        }
    }
}
