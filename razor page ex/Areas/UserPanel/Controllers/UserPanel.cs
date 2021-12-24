using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using razor_page_ex.Areas.UserPanel.Models;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.UserDTO;
using RazorEX.BAL.Utilities;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace razor_page_ex.Areas.UserPannel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class UserPanel : Controller
    {
        private readonly IUser _user;

        public UserPanel(IUser user)
        {
            _user = user;
        }

        public IActionResult Index()
        {
            var model = _user.GetUserByUserName(User.Identity.Name);
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var FindedUser = _user.GetUserById(id);

            if (FindedUser == null)
                return null;

            var model = new EditUserFromUserPanelViewModel()
            {
                UserName = FindedUser.UserName,
                Password = FindedUser.Password,
                UserId = id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id ,EditUserFromUserPanelViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            var Operation = _user.EditUserFromUserPanel(new EditUserDTO()
            {
                FullName = viewModel.UserName,
                Password = viewModel.Password,
                UserId = id
            });

            if (Operation.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(viewModel.UserName,Operation.Message);
                return View(viewModel);
            }
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/Register/SignIn?EditProfile=true");
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/Register/SignIn");
        }
    }
}
