using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using razor_page_ex.Areas.UserPanel.Models;
using RazorEX.BAL.Contracts;

namespace razor_page_ex.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class Wallet : Controller
    {
        private readonly IUser _UserService;

        public Wallet(IUser userService)
        {
            _UserService = userService;
        }

        public IActionResult Index()
        {
            ViewBag.TableData = _UserService.UserTransactionList(User.Identity.Name);
            return View(_UserService.GetUserByUserName(User.Identity.Name));
        }
        public IActionResult ChargeWallet()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChargeWallet(ChargeWalletViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TableData = _UserService.UserTransactionList(User.Identity.Name);
                return View(viewModel);
            }
            var WalletId = _UserService.ChargeWallet(User.Identity.Name, viewModel.Amount, "شارژ حساب");

            var payment = new ZarinpalSandbox.Payment(viewModel.Amount);
            var res = payment.PaymentRequest("شارژ کیف پول", "https://localhost:44339/OnlinePayment/" + WalletId, "amirgolpa45@gmail.com", "+989305312307");
             if (res.Result.Status == 100)
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            return RedirectToAction("Index");
        }
    }
}