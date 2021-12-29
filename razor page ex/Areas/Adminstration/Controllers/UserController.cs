using Microsoft.AspNetCore.Mvc;
using razor_page_ex.Areas.Adminstration.Models.UserM;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs.UserDTO;
using RazorEX.BAL.Services;
using RazorEX.BAL.Utilities;

namespace razor_page_ex.Areas.Adminstration.Controllers
{
    public class UserController : AdminBase
    {
        private readonly IUser _userService;

        public UserController(IUser userService)
        {
            _userService = userService;
        }

        public IActionResult Index(int PageId = 1, string UserName = "")
        {
            var IndexPageModel = _userService.GetUsersByFilter(PageId, 3, UserName);
            return View(model: IndexPageModel);
        }

        [HttpGet("Aminstration/User/Edit/{UId}")]
        public IActionResult Edit(int UId)
        {
            var FindedUser = _userService.GetUserById(UId);
            if (FindedUser == null)
                return null;

            var EditPageModel = new EditUserViewModel()
            {
                FullName = FindedUser.UserName,
                Role = FindedUser.Role,
                Password = FindedUser.Password
            };

            return View(model: EditPageModel);
        }

        [HttpPost("Aminstration/User/Edit/{UId}")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int UId, EditUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var result = _userService.EditUser(new EditUserDTO()
            {
                FullName = viewModel.FullName,
                Password = viewModel.Password,
                Role = viewModel.Role,
                UserId = UId
            });

            if (result.Status != OperationResultStatus.Success)    
            {
                ModelState.AddModelError(nameof(EditUserViewModel.FullName), result.Message);
                return View(model: viewModel);
            }

            return RedirectToAction("Index");
        }
        [HttpGet("Aminstration/User/delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            _userService.Delete(Id);
            return RedirectToAction("Index");
        }

        public IActionResult DeletedUsers()
        {
            var model = _userService.DeletedUsers();
            return View(model);
        }
    }
}