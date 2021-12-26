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
            _UserService.ChargeWallet(User.Identity.Name, viewModel.Amount , "شارژ حساب");

            return RedirectToAction("Index");
        }
    }
}