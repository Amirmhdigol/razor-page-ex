using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorEx.DAL.Entities;
using RazorEX.BAL.Contracts;
using RazorEX.BAL.DTOs;
using RazorEX.BAL.Utilities;

namespace razor_page_ex.Pages.Register
{
    [BindProperties]
    public class SignupModel : PageModel
    {
        public readonly ISignup _signup;
        private readonly IViewRenderService _viewRender;
        public SignupModel(ISignup signup, IViewRenderService viewRender)
        {
            _signup = signup;
            _viewRender = viewRender;
        }

        [Required]
        [MaxLength(25, ErrorMessage = "Name cannot be greater than 25")]
        [MinLength(4, ErrorMessage = "Name cannot be less than 4")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(25, ErrorMessage = "password cannot be greater than 25")]
        [MinLength(6, ErrorMessage = "password cannot be less than 6")]
        public string Password { get; set; }

        /// <summary>
        /// the model state check these data annotations Modelstate.IsValid down here
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string RepeatPassword { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            var Result = new RazorEx.DAL.Entities.User()
            {
                UserName = UserName,
                Password = Password,
                Role = UserRole.User,
                CreationDate = DateTime.Now,
                IsActive = true ,//for Using Email set this to false
                IsDelete = false,
                RePassword = RepeatPassword,
                ActiveCode = NameGenerator.GenerateUniqCode()
            } ;
            var add = _signup.Register(Result);
            if (add == 0)
            {
                ModelState.AddModelError("UserName", "موفقیت امیز نبود");
                return Page();
            }

            //Send Activation by Email ↓↓↓↓
            //string body = _viewRender.RenderToStringAsync("_ActiveEmail", Result);          
            //SendEmail.Send("amirmhdigol82@gmail.com", "فعالسازی", body);

            return RedirectToPage("../Index");

        }
    }
}
